using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

/// <summary>
/// Ejemplo de Composite + Strategy Pattern en C#
/// Demuestra cómo ambos patrones trabajan juntos en un motor de precios
/// 
/// Crédito: Profesor Bismarck Ponce
/// Uso educativo - Desarrollo de Software
/// </summary>
namespace PriceEngineDemo
{
    // ============================================================
    // COMPOSITE PATTERN
    // ============================================================
    
    /// <summary>
    /// Component: Interfaz común para items y bundles (productos y paquetes)
    /// </summary>
    public interface IPriceComponent
    {
        decimal GetSubtotal();            // Calcula subtotal (recursivo en Composite)
        string Print(int depth = 0);      // Imprime estructura jerárquica
    }

    /// <summary>
    /// Leaf: Producto individual - No tiene hijos
    /// </summary>
    public sealed class Item : IPriceComponent
    {
        public string Name { get; }
        public decimal UnitPrice { get; }
        public int Qty { get; }

        public Item(string name, decimal unitPrice, int qty)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Name is required", nameof(name));
            if (unitPrice < 0) 
                throw new ArgumentOutOfRangeException(nameof(unitPrice));
            if (qty <= 0) 
                throw new ArgumentOutOfRangeException(nameof(qty));
            
            Name = name;
            UnitPrice = unitPrice;
            Qty = qty;
        }

        public decimal GetSubtotal() => UnitPrice * Qty;

        public string Print(int depth = 0)
        {
            var indent = new string(' ', depth * 2);
            return $"{indent}- {Name} x{Qty}: {GetSubtotal().ToString("C", CultureInfo.InvariantCulture)}";
        }
    }

    /// <summary>
    /// Composite: Paquete que contiene otros componentes
    /// Implementa "Composite Seguro" - solo Bundle maneja hijos
    /// </summary>
    public sealed class Bundle : IPriceComponent
    {
        private readonly List<IPriceComponent> _children = new();

        public string Name { get; }
        public decimal PercentDiscount { get; }

        public Bundle(string name, decimal percentDiscount = 0m)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Name is required", nameof(name));
            if (percentDiscount < 0m || percentDiscount > 1m) 
                throw new ArgumentOutOfRangeException(nameof(percentDiscount), 
                    "Discount must be between 0 and 1");
            
            Name = name;
            PercentDiscount = percentDiscount;
        }

        public void Add(IPriceComponent child)
        {
            if (child is null) throw new ArgumentNullException(nameof(child));
            if (ReferenceEquals(child, this)) 
                throw new InvalidOperationException("Cannot add a bundle to itself.");
            _children.Add(child);
        }

        /// <summary>
        /// Operación recursiva del Composite:
        /// 1. Suma subtotales de todos los hijos (Items y Bundles)
        /// 2. Aplica descuento del bundle
        /// </summary>
        public decimal GetSubtotal()
        {
            var sum = _children.Sum(c => c.GetSubtotal());
            return PercentDiscount > 0m ? sum * (1m - PercentDiscount) : sum;
        }

        public string Print(int depth = 0)
        {
            var indent = new string(' ', depth * 2);
            var subtotal = GetSubtotal().ToString("C", CultureInfo.InvariantCulture);
            var header = $"{indent}+ {Name} (desc {PercentDiscount:P0}) = {subtotal}";
            var lines = new List<string> { header };
            foreach (var c in _children)
                lines.Add(c.Print(depth + 1));
            return string.Join(Environment.NewLine, lines);
        }
    }

    // ============================================================
    // STRATEGY PATTERN
    // ============================================================
    
    /// <summary>
    /// Contexto para las estrategias de impuestos
    /// </summary>
    public sealed class Order
    {
        public string Country { get; init; } = "US";
        public IPriceComponent Root { get; init; }

        public Order(string country, IPriceComponent root)
        {
            Country = country ?? "US";
            Root = root ?? throw new ArgumentNullException(nameof(root));
        }
    }

    /// <summary>
    /// Strategy: Interfaz común para todas las estrategias de impuestos
    /// </summary>
    public interface ITaxStrategy
    {
        /// <summary>Verifica si esta estrategia aplica a la orden</summary>
        bool AppliesTo(Order order);
        
        /// <summary>Aplica el cálculo de impuesto al monto base</summary>
        decimal Apply(decimal baseAmount, Order order);
    }

    /// <summary>
    /// Concrete Strategy: Impuesto de Estados Unidos (7%)
    /// </summary>
    public sealed class UsTaxStrategy : ITaxStrategy
    {
        public bool AppliesTo(Order order) => 
            string.Equals(order.Country, "US", StringComparison.OrdinalIgnoreCase);
        
        public decimal Apply(decimal baseAmount, Order order) => baseAmount * 1.07m;
    }

    /// <summary>
    /// Concrete Strategy: Impuesto de Francia (20% IVA)
    /// </summary>
    public sealed class FrTaxStrategy : ITaxStrategy
    {
        public bool AppliesTo(Order order) => 
            string.Equals(order.Country, "FR", StringComparison.OrdinalIgnoreCase);
        
        public decimal Apply(decimal baseAmount, Order order) => baseAmount * 1.20m;
    }

    /// <summary>
    /// Concrete Strategy: Sin impuesto para exportación
    /// </summary>
    public sealed class ExportTaxStrategy : ITaxStrategy
    {
        public bool AppliesTo(Order order) => 
            string.Equals(order.Country, "EXPORT", StringComparison.OrdinalIgnoreCase);
        
        public decimal Apply(decimal baseAmount, Order order) => baseAmount;
    }

    /// <summary>
    /// Null Object Pattern: Estrategia por defecto si ninguna aplica
    /// </summary>
    public sealed class NoTaxStrategy : ITaxStrategy
    {
        public bool AppliesTo(Order order) => true;
        public decimal Apply(decimal baseAmount, Order order) => baseAmount;
    }

    // ============================================================
    // SERVICE: ORQUESTACIÓN DE PATRONES
    // ============================================================
    
    /// <summary>
    /// Motor de precios que combina Composite y Strategy:
    /// 1. Usa Composite para calcular subtotal recursivamente
    /// 2. Usa Strategy para aplicar impuestos según el país
    /// </summary>
    public sealed class PriceEngineService
    {
        private readonly IEnumerable<ITaxStrategy> _strategies;
        private readonly MidpointRounding _rounding;

        public PriceEngineService(
            IEnumerable<ITaxStrategy> strategies, 
            MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
            _rounding = rounding;
        }

        public decimal Total(Order order)
        {
            if (order is null) throw new ArgumentNullException(nameof(order));
            if (order.Root is null) 
                throw new ArgumentException("Order.Root is required", nameof(order));

            // Paso 1: Composite calcula subtotal con descuentos
            var subtotal = order.Root.GetSubtotal();

            // Paso 2: Strategy selecciona y aplica impuestos
            var strategy = _strategies.FirstOrDefault(s => s.AppliesTo(order)) 
                          ?? new NoTaxStrategy();
            var total = strategy.Apply(subtotal, order);

            return Math.Round(total, 2, _rounding);
        }
    }

    // ============================================================
    // DEMO
    // ============================================================
    
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("=== COMPOSITE + STRATEGY PATTERN DEMO ===\n");
            Console.WriteLine("Ejemplo por: Profesor Bismarck Ponce\n");
            
            // Crear productos individuales (Leaf del Composite)
            var laptop = new Item("Laptop", 1000m, 1);
            var mouse  = new Item("Mouse",    50m, 2);  
            var kb     = new Item("Keyboard", 80m, 1);  

            // Crear bundle de periféricos con 5% descuento (Composite)
            var peripherals = new Bundle("Peripherals", 0.05m);
            peripherals.Add(mouse);
            peripherals.Add(kb);

            // Crear bundle "Back to School" con 10% descuento (Composite anidado)
            // Contiene laptop Y el bundle de periféricos
            var backToSchool = new Bundle("BackToSchool", 0.10m);
            backToSchool.Add(laptop);
            backToSchool.Add(peripherals);

            // Root bundle sin descuento
            var root = new Bundle("OrderRoot");
            root.Add(backToSchool);

            // Configurar estrategias de impuestos (Strategy)
            var strategies = new ITaxStrategy[]
            {
                new UsTaxStrategy(),
                new FrTaxStrategy(),
                new ExportTaxStrategy()
            };
            
            var engine = new PriceEngineService(strategies);

            // Imprimir estructura jerárquica (Composite en acción)
            Console.WriteLine("ESTRUCTURA DEL PEDIDO:");
            Console.WriteLine(root.Print());
            Console.WriteLine();

            // Cálculo paso a paso:
            // Mouse: $50 x 2 = $100
            // Keyboard: $80 x 1 = $80
            // Peripherals (5% desc): ($100 + $80) * 0.95 = $171
            // Laptop: $1000 x 1 = $1000
            // BackToSchool (10% desc): ($1000 + $171) * 0.90 = $1053.90
            Console.WriteLine($"Subtotal calculado recursivamente: {root.GetSubtotal():C}");
            Console.WriteLine();

            // Aplicar diferentes estrategias de impuestos
            var orderUS = new Order("US", root);
            var orderFR = new Order("FR", root);
            var orderEX = new Order("EXPORT", root);

            Console.WriteLine("TOTALES CON ESTRATEGIAS DE IMPUESTOS:");
            Console.WriteLine($"Total US (7% tax):      {engine.Total(orderUS):C}");     // $1,127.67
            Console.WriteLine($"Total FR (20% IVA):     {engine.Total(orderFR):C}");    // $1,264.68
            Console.WriteLine($"Total Export (0% tax):  {engine.Total(orderEX):C}");    // $1,053.90
        }
    }
}
```

### Salida del Programa

```
=== COMPOSITE + STRATEGY PATTERN DEMO ===

Ejemplo por: Profesor Bismarck Ponce

ESTRUCTURA DEL PEDIDO:
+ OrderRoot (desc 0%) = $1,053.90
  + BackToSchool (desc 10%) = $1,053.90
    - Laptop x1: $1,000.00
    + Peripherals (desc 5%) = $171.00
      - Mouse x2: $100.00
      - Keyboard x1: $80.00

Subtotal calculado recursivamente: $1,053.90

TOTALES CON ESTRATEGIAS DE IMPUESTOS:
Total US (7% tax):      $1,127.67
Total FR (20% IVA):     $1,264.68
Total Export (0% tax):  $1,053.90
```

### 🔑 Puntos Clave del Diseño

#### Composite Pattern
- ✅ `IPriceComponent` es la interfaz común
- ✅ `Item` es el Leaf (producto simple)
- ✅ `Bundle` es el Composite (contenedor recursivo)
- ✅ Operación recursiva `GetSubtotal()` suma toda la jerarquía
- ✅ Composite Seguro: solo `Bundle` tiene `Add()`

#### Strategy Pattern
- ✅ `ITaxStrategy` define el contrato de estrategias
- ✅ Múltiples estrategias concretas (US, FR, Export)
- ✅ `PriceEngineService` selecciona estrategia en runtime
- ✅ Null Object (`NoTaxStrategy`) para casos sin estrategia
- ✅ LINQ para selección de estrategia aplicable

#### Principios SOLID
- ✅ **SRP**: Cada estrategia tiene una responsabilidad
- ✅ **OCP**: Puedes añadir países sin modificar código existente
- ✅ **LSP**: Estrategias son intercambiables
- ✅ **DIP**: `PriceEngineService` depende de `ITaxStrategy`, no de implementaciones

---

## 📚 Recursos

- [C# Design Patterns](https://www.dofactory.com/net/strategy-design-pattern)
- [Fluent Builder in C#](https://www.codeproject.com/Articles/31490/Fluent-Builder)
- [LINQ FirstOrDefault](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault)

---

## 🙏 Créditos

- **Profesor Bismarck Ponce** - Ejemplo de Strategy + Composite en motor de precios
- **Refactoring Guru** - Alexander Shvets
- **DoFactory**

---

[← Volver a Strategy](../README.md)
