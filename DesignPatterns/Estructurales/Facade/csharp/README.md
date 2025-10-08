# Facade - Implementación en C#

## 📖 Descripción

Referencias a implementaciones del patrón Facade en C#/.NET, demostrando simplificación de subsistemas complejos con APIs modernas.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Facade C#**
- **Enlace**: [Facade en C#](https://refactoring.guru/design-patterns/facade/csharp/example)
- **Ejemplo**: Video conversion library

### 2. **DoFactory - Facade Pattern**
- **Enlace**: [DoFactory Facade](https://www.dofactory.com/net/facade-design-pattern)
- **Ejemplo**: Mortgage application system

---

## 💡 Ejemplo de Referencia

```csharp
// Subsistema complejo
public class VideoFile
{
    public string Name { get; }
    public VideoFile(string name) => Name = name;
}

public class CodecFactory
{
    public static ICodec Extract(VideoFile file)
    {
        var extension = Path.GetExtension(file.Name).ToLower();
        return extension switch
        {
            ".mp4" => new MPEG4Codec(),
            ".ogg" => new OggCodec(),
            _ => throw new NotSupportedException($"Codec not supported: {extension}")
        };
    }
}

public interface ICodec
{
    string Type { get; }
}

public class MPEG4Codec : ICodec
{
    public string Type => "mp4";
}

public class OggCodec : ICodec
{
    public string Type => "ogg";
}

public class AudioMixer
{
    public void Fix(VideoFile video)
    {
        Console.WriteLine("AudioMixer: fixing audio...");
    }
}

public class BitrateReader
{
    public VideoFile Read(VideoFile file, ICodec codec)
    {
        Console.WriteLine($"BitrateReader: reading file with {codec.Type}");
        return file;
    }
    
    public VideoFile Convert(VideoFile buffer, ICodec codec)
    {
        Console.WriteLine($"BitrateReader: converting to {codec.Type}");
        return buffer;
    }
}

// FACADE: Simplifica todo el proceso
public class VideoConverterFacade
{
    public FileInfo ConvertVideo(string filename, string format)
    {
        Console.WriteLine("VideoConverter: Starting conversion...");
        
        var file = new VideoFile(filename);
        var sourceCodec = CodecFactory.Extract(file);
        
        var destCodec = format.ToLower() switch
        {
            "mp4" => new MPEG4Codec(),
            "ogg" => new OggCodec(),
            _ => throw new NotSupportedException($"Format not supported: {format}")
        };
        
        var reader = new BitrateReader();
        var buffer = reader.Read(file, sourceCodec);
        var result = reader.Convert(buffer, destCodec);
        
        var audioMixer = new AudioMixer();
        audioMixer.Fix(result);
        
        Console.WriteLine("VideoConverter: Conversion completed!");
        return new FileInfo($"output.{format}");
    }
}

// Cliente: Código extremadamente simple
class Program
{
    static void Main()
    {
        var converter = new VideoConverterFacade();
        var outputFile = converter.ConvertVideo("video.ogg", "mp4");
        Console.WriteLine($"File saved: {outputFile.Name}");
        
        // ✅ Una línea vs. 10+ líneas de configuración manual
    }
}
```

---

## 🔧 Características C#

### 1. Facade con Dependency Injection y Async

```csharp
public interface IOrderFacade
{
    Task<OrderResult> PlaceOrderAsync(OrderRequest request);
}

public class OrderFacade : IOrderFacade
{
    private readonly IInventoryService _inventory;
    private readonly IPaymentService _payment;
    private readonly IShippingService _shipping;
    private readonly IEmailService _email;
    private readonly ILogger<OrderFacade> _logger;
    
    public OrderFacade(
        IInventoryService inventory,
        IPaymentService payment,
        IShippingService shipping,
        IEmailService email,
        ILogger<OrderFacade> logger)
    {
        _inventory = inventory;
        _payment = payment;
        _shipping = shipping;
        _email = email;
        _logger = logger;
    }
    
    public async Task<OrderResult> PlaceOrderAsync(OrderRequest request)
    {
        _logger.LogInformation("Processing order for {Email}", request.CustomerEmail);
        
        try
        {
            // Orquestar múltiples servicios
            var inventoryResult = await _inventory.ReserveItemsAsync(request.Items);
            if (!inventoryResult.Success)
                return OrderResult.Failure("Items not available");
            
            var paymentResult = await _payment.ProcessPaymentAsync(
                request.PaymentInfo, 
                request.TotalAmount
            );
            if (!paymentResult.Success)
            {
                await _inventory.ReleaseItemsAsync(request.Items);
                return OrderResult.Failure("Payment failed");
            }
            
            var shippingResult = await _shipping.ScheduleDeliveryAsync(
                request.ShippingAddress, 
                request.Items
            );
            
            await _email.SendOrderConfirmationAsync(
                request.CustomerEmail, 
                paymentResult.TransactionId
            );
            
            _logger.LogInformation("Order completed successfully");
            return OrderResult.Success(paymentResult.TransactionId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing order");
            return OrderResult.Failure(ex.Message);
        }
    }
}

// Registro en Program.cs o Startup.cs
builder.Services.AddScoped<IOrderFacade, OrderFacade>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IShippingService, ShippingService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Uso en Controller
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderFacade _orderFacade;
    
    public OrdersController(IOrderFacade orderFacade)
    {
        _orderFacade = orderFacade;
    }
    
    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
    {
        var result = await _orderFacade.PlaceOrderAsync(request);
        
        return result.Success 
            ? Ok(new { orderId = result.OrderId }) 
            : BadRequest(new { error = result.ErrorMessage });
    }
}
```

### 2. Extension Methods como Facade

```csharp
// Facade para operaciones complejas de strings
public static class StringFacadeExtensions
{
    public static string ToTitleCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;
        
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(input.ToLower());
    }
    
    public static string Truncate(this string input, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
            return input;
        
        return input[..maxLength] + suffix;
    }
    
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}

// Uso simple
string title = "hello world".ToTitleCase(); // "Hello World"
string short = "Very long text...".Truncate(10); // "Very long ..."
bool valid = "test@example.com".IsValidEmail(); // true
```

### 3. Builder Pattern + Facade

```csharp
// Facade que usa Builder internamente
public class EmailFacade
{
    private readonly IEmailService _emailService;
    
    public EmailFacade(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    public async Task SendWelcomeEmailAsync(string to, string userName)
    {
        // Facade oculta complejidad del email builder
        var email = new EmailBuilder()
            .To(to)
            .Subject($"Welcome {userName}!")
            .Body(GetWelcomeTemplate(userName))
            .WithAttachment("welcome-guide.pdf")
            .WithPriority(EmailPriority.Normal)
            .Build();
        
        await _emailService.SendAsync(email);
    }
    
    public async Task SendPasswordResetAsync(string to, string resetToken)
    {
        var email = new EmailBuilder()
            .To(to)
            .Subject("Password Reset Request")
            .Body(GetResetTemplate(resetToken))
            .WithPriority(EmailPriority.High)
            .Build();
        
        await _emailService.SendAsync(email);
    }
    
    private string GetWelcomeTemplate(string userName) => 
        $"<html><body>Welcome {userName}!</body></html>";
    
    private string GetResetTemplate(string token) => 
        $"<html><body>Reset token: {token}</body></html>";
}
```

---

## 🎓 Ejemplo Real del Mundo: Sistema de Checkout (Facade + Proxy)

**Crédito**: Profesor **Bismarck Ponce** - Ejemplo utilizado en clase

Este ejemplo muestra cómo **Facade** y **Proxy** trabajan juntos en un sistema de e-commerce real.

### 📦 Archivo Ejecutable Completo

El código completo está disponible en: **[CheckoutDemo.cs](./CheckoutDemo.cs)**

### Concepto

Un sistema de checkout que:
- Usa **Facade** para orquestar 5 servicios en una sola llamada
- Usa **Caching Proxy** para optimizar consultas de impuestos
- Usa **Protection Proxy** para controlar acceso a pagos

### Flujo Simplificado

```
Cliente → CheckoutFacade.CheckoutAsync()
           ↓
    ┌──────┴──────┐
    │             │
Servicios    Proxies
Directos     Inteligentes
    │             │
    ├─ Catalog    ├─ CachingTaxProxy → TaxService
    ├─ Inventory  └─ ProtectionPayProxy → PaymentService
    └─ Notifier
```

### Resultados

**Escenario 1** (Usuario con permiso):
- Subtotal: $1100 (1 Laptop + 2 Mouse)
- Impuesto FR (20%): $220
- Total: $1320 ✓
- Llamadas TaxService: 1

**Escenario 1B** (Segunda compra):
- Total: $1320 ✓
- Llamadas TaxService: 0 (CACHE HIT) 💾

**Escenario 2** (Usuario sin permiso):
- Procesamiento bloqueado en paso de pago ❌
- UnauthorizedAccessException lanzada
- No se cobró (seguridad funcionó) 🔒

### 💡 Lecciones Clave

1. **Facade orquesta**, Proxy optimiza y protege
2. **Transparencia**: Facade usa proxies sin conocer detalles
3. **DI-ready**: Todos usan interfaces, fácil testing
4. **Async/await**: Manejo apropiado de operaciones asíncronas
5. **Records**: C# moderno para DTOs

### 🎯 Análisis Completo

Para análisis detallado con diagramas Mermaid:
- [Ver en Patrones Combinados](../../../PatronesCombinadosEjemplos.md#facade--proxy-sistema-de-checkout)

---

## 📚 Recursos

- [C# Design Patterns - Facade](https://www.dofactory.com/net/facade-design-pattern)
- [Microsoft Learn - Gateway Aggregation](https://learn.microsoft.com/en-us/azure/architecture/patterns/gateway-aggregation)
- [ASP.NET Core - Service Layer](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)

---

## 🙏 Créditos

- **Profesor Bismarck Ponce** - Ejemplo de Facade + Proxy en sistema de checkout
- **Refactoring Guru** - Alexander Shvets
- **DoFactory**
- **Microsoft Learn**

---

[← Volver a Facade](../README.md)