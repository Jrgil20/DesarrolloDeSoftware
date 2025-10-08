using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Ejemplo de Facade + Proxy Pattern en C#
/// Demuestra cómo ambos patrones trabajan juntos en un sistema de checkout
/// 
/// FACADE: CheckoutFacade orquesta 5 servicios en una sola llamada
/// PROXY 1: CachingTaxServiceProxy optimiza llamadas a API de impuestos
/// PROXY 2: PaymentProtectionProxy controla acceso a servicio de pagos
/// 
/// Crédito: Profesor Bismarck Ponce
/// Uso educativo - Desarrollo de Software
/// </summary>
namespace CheckoutDemo
{
    // ============================================================
    // MODELOS (Records de C# 9+)
    // ============================================================
    
    public record CartItem(string ProductId, int Qty);
    public record Product(string Id, string Name, decimal UnitPrice);
    public record CheckoutRequest(
        string Country, 
        string UserId, 
        IEnumerable<CartItem> Items, 
        string CardToken, 
        string Email
    );
    public record CheckoutResult(
        string TransactionId, 
        decimal Subtotal, 
        decimal TaxRate, 
        decimal Total
    );

    // ============================================================
    // INTERFACES DE SERVICIOS
    // ============================================================
    
    public interface IProductCatalog 
    { 
        Task<Product?> GetByIdAsync(string productId, CancellationToken ct); 
    }
    
    public interface IInventory     
    { 
        Task<bool> ReserveAsync(string productId, int qty, CancellationToken ct); 
    }
    
    /// <summary>Subject para el Proxy de caching</summary>
    public interface ITaxService    
    { 
        Task<decimal> GetRateAsync(string country, CancellationToken ct); 
    }
    
    /// <summary>Subject para el Proxy de protección</summary>
    public interface IPaymentService
    { 
        Task<string> ChargeAsync(decimal amount, string cardToken, CancellationToken ct); 
    }
    
    public interface INotifier      
    { 
        Task SendAsync(string email, string subject, string body, CancellationToken ct); 
    }
    
    public interface ICurrentUser   
    { 
        bool HasPermission(string permission); 
    }

    // ============================================================
    // FACADE PATTERN
    // ============================================================
    
    public interface ICheckoutFacade
    {
        Task<CheckoutResult> CheckoutAsync(CheckoutRequest request, CancellationToken ct);
    }

    /// <summary>
    /// FACADE: Proporciona UNA interfaz simple para un proceso complejo
    /// 
    /// Orquesta 5 servicios diferentes:
    /// 1. ProductCatalog - Obtener info de productos
    /// 2. Inventory - Reservar stock
    /// 3. TaxService - Calcular impuestos (usa Proxy con cache)
    /// 4. PaymentService - Cobrar (usa Proxy con protección)
    /// 5. Notifier - Enviar confirmación
    /// </summary>
    public sealed class CheckoutFacade : ICheckoutFacade
    {
        private readonly IProductCatalog _catalog;
        private readonly IInventory _inventory;
        private readonly ITaxService _tax;
        private readonly IPaymentService _payment;
        private readonly INotifier _notifier;

        public CheckoutFacade(
            IProductCatalog c, 
            IInventory i, 
            ITaxService t, 
            IPaymentService p, 
            INotifier n)
            => (_catalog, _inventory, _tax, _payment, _notifier) = (c, i, t, p, n);

        public async Task<CheckoutResult> CheckoutAsync(
            CheckoutRequest req, 
            CancellationToken ct)
        {
            Console.WriteLine("\n🛒 === INICIANDO CHECKOUT ===");
            
            // PASO 1 & 2: Validar productos, reservar inventario, calcular subtotal
            decimal subtotal = 0m;
            foreach (var item in req.Items)
            {
                var p = await _catalog.GetByIdAsync(item.ProductId, ct)
                        ?? throw new InvalidOperationException(
                            $"Producto {item.ProductId} no existe.");
                
                if (item.Qty <= 0) 
                    throw new InvalidOperationException(
                        $"Cantidad inválida para {item.ProductId}.");

                var ok = await _inventory.ReserveAsync(item.ProductId, item.Qty, ct);
                if (!ok) 
                    throw new InvalidOperationException(
                        $"Sin stock para {item.ProductId}.");

                subtotal += p.UnitPrice * item.Qty;
            }
            Console.WriteLine($"💰 Subtotal calculado: ${subtotal:0.00}");

            // PASO 3: Aplicar impuestos (a través del Caching Proxy)
            var rate = await _tax.GetRateAsync(req.Country, ct);
            var total = Math.Round(subtotal * (1 + rate), 2, MidpointRounding.AwayFromZero);
            Console.WriteLine($"💵 Total con impuestos ({rate:P0}): ${total:0.00}");

            // PASO 4: Cobrar (a través del Protection Proxy)
            var txId = await _payment.ChargeAsync(total, req.CardToken, ct);
            Console.WriteLine($"✓ Pago completado. TX: {txId}");

            // PASO 5: Notificar
            try
            {
                await _notifier.SendAsync(
                    req.Email, 
                    "Compra confirmada", 
                    $"TX: {txId} | Total: {total:0.00}", 
                    ct);
            }
            catch
            {
                // En producción: loggear error. No revertimos compra.
            }

            Console.WriteLine("✓ === CHECKOUT COMPLETADO ===\n");
            return new CheckoutResult(txId, subtotal, rate, total);
        }
    }

    // ============================================================
    // PROXY PATTERN #1: CACHING (Optimización de rendimiento)
    // ============================================================
    
    /// <summary>
    /// PROXY: Implementa caching para evitar llamadas repetidas a API de impuestos
    /// 
    /// Tipo: Virtual Proxy + Caching Proxy
    /// Beneficio: Reduce latencia y costos de API externa
    /// TTL: 5 minutos
    /// </summary>
    public sealed class CachingTaxServiceProxy : ITaxService
    {
        private readonly ITaxService _inner;
        private readonly Dictionary<string, (decimal rate, DateTimeOffset exp)> _cache = new();
        private readonly TimeSpan _ttl = TimeSpan.FromMinutes(5);

        /// <summary>Contador de llamadas reales (para diagnóstico)</summary>
        public int RealCalls { get; private set; }

        public CachingTaxServiceProxy(ITaxService inner) => _inner = inner;

        public async Task<decimal> GetRateAsync(string country, CancellationToken ct)
        {
            var key = (country ?? "").ToUpperInvariant();
            
            // Verificar si existe en cache y no expiró
            if (_cache.TryGetValue(key, out var hit) && hit.exp > DateTimeOffset.UtcNow)
            {
                Console.WriteLine($"   💾 TaxProxy: Cache HIT para {key}");
                return hit.rate;
            }

            // Cache miss: llamar al servicio real (costoso)
            Console.WriteLine($"   🔍 TaxProxy: Cache MISS para {key} - consultando API real");
            var rate = await _inner.GetRateAsync(country, ct);
            RealCalls++;
            
            // Guardar en cache con expiración
            _cache[key] = (rate, DateTimeOffset.UtcNow.Add(_ttl));
            Console.WriteLine($"   ✓ TaxProxy: Resultado cacheado (expira en {_ttl.TotalMinutes} min)");
            
            return rate;
        }
    }

    // ============================================================
    // PROXY PATTERN #2: PROTECTION (Control de acceso y seguridad)
    // ============================================================
    
    /// <summary>
    /// PROXY: Verifica permisos antes de permitir operaciones de pago
    /// 
    /// Tipo: Protection Proxy
    /// Beneficio: Centraliza control de acceso, evita cobros no autorizados
    /// </summary>
    public sealed class PaymentProtectionProxy : IPaymentService
    {
        private readonly IPaymentService _inner;
        private readonly ICurrentUser _current;

        public PaymentProtectionProxy(IPaymentService inner, ICurrentUser current)
            => (_inner, _current) = (inner, current);

        public Task<string> ChargeAsync(decimal amount, string cardToken, CancellationToken ct)
        {
            // Verificar permisos ANTES de delegar al servicio real
            if (!_current.HasPermission("PAYMENT_EXECUTE"))
            {
                Console.WriteLine("   ❌ PaymentProxy: Acceso DENEGADO - usuario sin permisos");
                throw new UnauthorizedAccessException(
                    "Missing permission PAYMENT_EXECUTE");
            }
            
            Console.WriteLine("   ✓ PaymentProxy: Acceso AUTORIZADO - procesando pago");
            return _inner.ChargeAsync(amount, cardToken, ct);
        }
    }

    // ============================================================
    // IMPLEMENTACIONES FAKE PARA DEMO
    // ============================================================
    
    public sealed class InMemoryCatalog : IProductCatalog
    {
        private readonly Dictionary<string, Product> _db = new()
        {
            ["P1"] = new("P1", "Laptop", 1000m),
            ["P2"] = new("P2", "Mouse",    50m),
        };

        public Task<Product?> GetByIdAsync(string productId, CancellationToken ct)
            => Task.FromResult(_db.TryGetValue(productId, out var p) ? p : null);
    }

    public sealed class InMemoryInventory : IInventory
    {
        private readonly Dictionary<string, int> _stock = new()
        {
            ["P1"] = 10,
            ["P2"] = 50,
        };

        public Task<bool> ReserveAsync(string productId, int qty, CancellationToken ct)
        {
            if (!_stock.TryGetValue(productId, out var s) || qty <= 0 || s < qty) 
                return Task.FromResult(false);
            
            _stock[productId] = s - qty;
            Console.WriteLine($"   📦 Inventario: Reservados {qty}x {productId}. Stock restante: {_stock[productId]}");
            return Task.FromResult(true);
        }
    }

    public sealed class FakeTaxService : ITaxService
    {
        public Task<decimal> GetRateAsync(string country, CancellationToken ct)
        {
            var code = (country ?? "").ToUpperInvariant();
            var rate = code switch
            {
                "FR" => 0.20m,  // Francia: 20% IVA
                "US" => 0.07m,  // USA: 7% sales tax
                _    => 0.00m   // Otros: sin impuesto
            };
            
            Console.WriteLine($"   🌍 TaxService REAL: Tasa para {code} = {rate:P0}");
            return Task.FromResult(rate);
        }
    }

    public sealed class FakePaymentService : IPaymentService
    {
        public Task<string> ChargeAsync(decimal amount, string cardToken, CancellationToken ct)
        {
            var txId = $"TX-{Guid.NewGuid().ToString()[..8].ToUpper()}";
            Console.WriteLine($"   💳 PaymentService REAL: Cobrando ${amount:0.00} | TxId: {txId}");
            return Task.FromResult(txId);
        }
    }

    public sealed class ConsoleNotifier : INotifier
    {
        public Task SendAsync(string email, string subject, string body, CancellationToken ct)
        {
            Console.WriteLine($"   📧 Notifier: Email enviado a {email}");
            Console.WriteLine($"      Subject: {subject}");
            Console.WriteLine($"      Body: {body}");
            return Task.CompletedTask;
        }
    }

    public sealed class SimpleCurrentUser : ICurrentUser
    {
        private readonly bool _canPay;
        public SimpleCurrentUser(bool canPay) => _canPay = canPay;
        public bool HasPermission(string permission) => 
            permission == "PAYMENT_EXECUTE" && _canPay;
    }

    // ============================================================
    // DEMO: Tres escenarios que demuestran los patrones
    // ============================================================
    
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║   FACADE + PROXY PATTERN DEMO                      ║");
            Console.WriteLine("║   Ejemplo por: Profesor Bismarck Ponce            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝\n");
            
            var ct = CancellationToken.None;

            // Servicios compartidos entre escenarios
            var catalog = new InMemoryCatalog();
            var notifier = new ConsoleNotifier();

            // Tax service REAL envuelto en Caching Proxy
            var taxReal  = new FakeTaxService();
            var taxProxy = new CachingTaxServiceProxy(taxReal);

            // ========================================================
            // ESCENARIO 1: Usuario con permiso, Francia (20% IVA)
            // Demuestra: Facade + Ambos Proxies funcionando correctamente
            // ========================================================
            Console.WriteLine("┌────────────────────────────────────────────────────┐");
            Console.WriteLine("│ ESCENARIO 1: Usuario autorizado - Francia         │");
            Console.WriteLine("└────────────────────────────────────────────────────┘");
            
            var inv1 = new InMemoryInventory();
            var userAllowed = new SimpleCurrentUser(canPay: true);
            var payment1 = new PaymentProtectionProxy(new FakePaymentService(), userAllowed);
            var facade1 = new CheckoutFacade(catalog, inv1, taxProxy, payment1, notifier);

            var req = new CheckoutRequest(
                Country: "FR",
                UserId: "u-001",
                Items: new[]
                {
                    new CartItem("P1", 1),   // Laptop $1000
                    new CartItem("P2", 2)    // Mouse  $50 x 2 = $100
                },
                CardToken: "tok_visa_fr",
                Email: "customer@example.com"
            );

            var result1 = await facade1.CheckoutAsync(req, ct);
            
            Console.WriteLine("\n📊 RESULTADO ESCENARIO 1:");
            Console.WriteLine($"   Subtotal: ${result1.Subtotal:0.00} (esperado: $1100.00)");
            Console.WriteLine($"   TaxRate:  {result1.TaxRate:P0} (esperado: 20%)");
            Console.WriteLine($"   Total:    ${result1.Total:0.00} (esperado: $1320.00)");
            Console.WriteLine($"   TxId:     {result1.TransactionId}");
            Console.WriteLine($"   Llamadas reales a TaxService: {taxProxy.RealCalls}");
            Console.WriteLine();

            // ========================================================
            // ESCENARIO 1B: Segunda compra - Demuestra CACHING PROXY
            // ========================================================
            Console.WriteLine("┌────────────────────────────────────────────────────┐");
            Console.WriteLine("│ ESCENARIO 1B: Segunda compra - Demuestra CACHE     │");
            Console.WriteLine("└────────────────────────────────────────────────────┘");
            
            var inv1b = new InMemoryInventory();
            var payment1b = new PaymentProtectionProxy(new FakePaymentService(), userAllowed);
            var facade1b = new CheckoutFacade(catalog, inv1b, taxProxy, payment1b, notifier);

            var result1b = await facade1b.CheckoutAsync(req, ct);
            
            Console.WriteLine("\n📊 RESULTADO ESCENARIO 1B:");
            Console.WriteLine($"   Subtotal: ${result1b.Subtotal:0.00}");
            Console.WriteLine($"   Total:    ${result1b.Total:0.00}");
            Console.WriteLine($"   Llamadas reales a TaxService: {taxProxy.RealCalls} (NO aumentó - usó cache)");
            Console.WriteLine();

            // ========================================================
            // ESCENARIO 2: Usuario SIN permiso - Demuestra PROTECTION PROXY
            // ========================================================
            Console.WriteLine("┌────────────────────────────────────────────────────┐");
            Console.WriteLine("│ ESCENARIO 2: Usuario SIN permiso - Protection      │");
            Console.WriteLine("└────────────────────────────────────────────────────┘");
            
            var inv2 = new InMemoryInventory();
            var userDenied = new SimpleCurrentUser(canPay: false);
            var payment2 = new PaymentProtectionProxy(new FakePaymentService(), userDenied);
            var facade2 = new CheckoutFacade(catalog, inv2, taxProxy, payment2, notifier);

            try
            {
                _ = await facade2.CheckoutAsync(req, ct);
                Console.WriteLine("\n❌ ERROR: Se esperaba UnauthorizedAccessException y no ocurrió.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"\n✓ RESULTADO ESPERADO: Bloqueado por Protection Proxy");
                Console.WriteLine($"   Excepción: {ex.GetType().Name}");
                Console.WriteLine($"   Mensaje: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║   ANÁLISIS DE PATRONES APLICADOS                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("✓ FACADE (CheckoutFacade):");
            Console.WriteLine("  - Simplifica: 1 llamada vs 5+ operaciones");
            Console.WriteLine("  - Orquesta: Productos → Inventario → Impuestos → Pago → Notificación");
            Console.WriteLine("  - Oculta: Complejidad de integración de servicios");
            Console.WriteLine();
            Console.WriteLine("✓ PROXY #1 (CachingTaxServiceProxy):");
            Console.WriteLine("  - Tipo: Caching/Virtual Proxy");
            Console.WriteLine("  - Optimiza: Reduce llamadas a API externa");
            Console.WriteLine($"  - Resultado: {taxProxy.RealCalls} llamada(s) real(es) para 2 checkouts");
            Console.WriteLine();
            Console.WriteLine("✓ PROXY #2 (PaymentProtectionProxy):");
            Console.WriteLine("  - Tipo: Protection Proxy");
            Console.WriteLine("  - Seguridad: Verifica permisos antes de cobrar");
            Console.WriteLine("  - Resultado: Bloqueó usuario no autorizado ✓");
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║   FIN DE LA DEMO                                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
        }
    }
}

