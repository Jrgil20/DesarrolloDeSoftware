# Composite - Implementación en C#

## 📖 Descripción

Referencias a implementaciones del patrón Composite en C#/.NET con LINQ y colecciones.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Composite C#**
- **Enlace**: [Composite en C#](https://refactoring.guru/design-patterns/composite/csharp/example)

### 2. **DoFactory - Composite Pattern**
- **Enlace**: [DoFactory Composite](https://www.dofactory.com/net/composite-design-pattern)

---

## 💡 Ejemplo de Referencia

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

// Component
public interface IFileSystemItem
{
    string Name { get; }
    long GetSize();
    void Print(string indent = "");
}

// Leaf
public class File : IFileSystemItem
{
    public string Name { get; }
    private readonly long _size;
    
    public File(string name, long size)
    {
        Name = name;
        _size = size;
    }
    
    public long GetSize() => _size;
    
    public void Print(string indent = "")
    {
        Console.WriteLine($"{indent}📄 {Name} ({_size} bytes)");
    }
}

// Composite
public class Folder : IFileSystemItem
{
    private readonly List<IFileSystemItem> _items = new();
    
    public string Name { get; }
    
    public Folder(string name)
    {
        Name = name;
    }
    
    public void Add(IFileSystemItem item)
    {
        _items.Add(item);
    }
    
    public void Remove(IFileSystemItem item)
    {
        _items.Remove(item);
    }
    
    public long GetSize()
    {
        return _items.Sum(item => item.GetSize());
    }
    
    public void Print(string indent = "")
    {
        Console.WriteLine($"{indent}📁 {Name}/");
        foreach (var item in _items)
        {
            item.Print(indent + "  ");
        }
    }
}

// Client
class Program
{
    static void Main()
    {
        var root = new Folder("root");
        
        var documents = new Folder("Documents");
        documents.Add(new File("resume.pdf", 150_000));
        documents.Add(new File("cover-letter.doc", 80_000));
        
        var photos = new Folder("Photos");
        photos.Add(new File("vacation.jpg", 2_000_000));
        photos.Add(new File("family.jpg", 1_500_000));
        
        root.Add(documents);
        root.Add(photos);
        root.Add(new File("readme.txt", 5_000));
        
        root.Print();
        
        Console.WriteLine($"\nTotal size: {root.GetSize()} bytes");
        Console.WriteLine($"Documents size: {documents.GetSize()} bytes");
    }
}
```

---

## 🔧 Características C#

### 1. LINQ para Operaciones Recursivas
```csharp
public class Folder : IFileSystemItem
{
    private List<IFileSystemItem> _items = new();
    
    public long GetSize()
    {
        return _items.Sum(item => item.GetSize());
    }
    
    public IEnumerable<File> GetAllFiles()
    {
        return _items.SelectMany(item =>
            item is File file 
                ? new[] { file }
                : item is Folder folder
                    ? folder.GetAllFiles()
                    : Enumerable.Empty<File>()
        );
    }
    
    public int CountFiles()
    {
        return _items.Count(item => item is File) +
               _items.OfType<Folder>()
                     .Sum(folder => folder.CountFiles());
    }
}
```

### 2. Expression Trees para Búsqueda
```csharp
using System.Linq.Expressions;

public class Folder : IFileSystemItem
{
    public IEnumerable<File> FindFiles(Expression<Func<File, bool>> predicate)
    {
        var compiledPredicate = predicate.Compile();
        
        return _items.SelectMany(item => item switch
        {
            File file when compiledPredicate(file) => new[] { file },
            Folder folder => folder.FindFiles(predicate),
            _ => Enumerable.Empty<File>()
        });
    }
}

// Uso
var largeFiles = root.FindFiles(f => f.GetSize() > 1_000_000);
```

### 3. Pattern Matching (C# 9+)
```csharp
public long CalculateTotalSize() => this switch
{
    File file => file.GetSize(),
    Folder folder => folder._items.Sum(item => item.CalculateTotalSize()),
    _ => 0
};
```

### 4. Init-only Properties y Records
```csharp
public record FileItem(string Name, long Size) : IFileSystemItem
{
    public long GetSize() => Size;
    
    public void Print(string indent = "")
    {
        Console.WriteLine($"{indent}📄 {Name} ({Size} bytes)");
    }
}
```

---

## 🎓 Ejemplo Real: Sistema de Precios con Composite + Strategy

**Crédito**: Profesor **Bismarck Ponce** - Ejemplo utilizado en clase

Este ejemplo muestra cómo **Composite** y **Strategy** trabajan juntos en un sistema de precios del mundo real.

### Escenario

Un sistema de e-commerce que:
- Permite productos individuales (Item) y paquetes (Bundle) con descuentos
- Los paquetes pueden contener otros paquetes (estructura jerárquica)
- Calcula impuestos según el país usando diferentes estrategias

### Código Completo

```csharp
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PriceEngineDemo
{
    // ============ COMPOSITE PATTERN ============
    
    /// <summary>
    /// Component: Interfaz común para items y bundles
    /// </summary>
    public interface IPriceComponent
    {
        decimal GetSubtotal();            
        string Print(int depth = 0);      
    }

    /// <summary>
    /// Leaf: Producto individual (hoja del árbol)
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
        /// Operación recursiva: suma subtotales de todos los hijos y aplica descuento
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

    // ============ STRATEGY PATTERN ============
    
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
    /// Strategy: Interfaz para estrategias de cálculo de impuestos
    /// </summary>
    public interface ITaxStrategy
    {
        bool AppliesTo(Order order);
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
    /// Concrete Strategy: Impuesto de Francia (20%)
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
    /// Null Object Pattern: Estrategia por defecto si no aplica ninguna
    /// </summary>
    public sealed class NoTaxStrategy : ITaxStrategy
    {
        public bool AppliesTo(Order order) => true;
        public decimal Apply(decimal baseAmount, Order order) => baseAmount;
    }

    /// <summary>
    /// Motor de precios que usa las estrategias
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

            // Usar Composite para calcular subtotal recursivamente
            var subtotal = order.Root.GetSubtotal();

            // Usar Strategy para aplicar impuestos según el país
            var strategy = _strategies.FirstOrDefault(s => s.AppliesTo(order)) 
                          ?? new NoTaxStrategy();
            var total = strategy.Apply(subtotal, order);

            return Math.Round(total, 2, _rounding);
        }
    }

    // ============ DEMO ============
    
    internal static class Program
    {
        private static void Main()
        {
            // Crear productos individuales (Leaf)
            var laptop = new Item("Laptop", 1000m, 1);
            var mouse  = new Item("Mouse",    50m, 2);  
            var kb     = new Item("Keyboard", 80m, 1);  

            // Crear bundle de periféricos con 5% descuento (Composite)
            var peripherals = new Bundle("Peripherals", 0.05m);
            peripherals.Add(mouse);
            peripherals.Add(kb);

            // Crear bundle "Back to School" con 10% descuento (Composite)
            // Contiene laptop Y el bundle de periféricos (anidación)
            var backToSchool = new Bundle("BackToSchool", 0.10m);
            backToSchool.Add(laptop);
            backToSchool.Add(peripherals);

            // Root bundle sin descuento
            var root = new Bundle("OrderRoot");
            root.Add(backToSchool);

            // Configurar estrategias de impuestos
            var strategies = new ITaxStrategy[]
            {
                new UsTaxStrategy(),
                new FrTaxStrategy(),
                new ExportTaxStrategy()
            };
            
            var engine = new PriceEngineService(strategies);

            // Imprimir estructura jerárquica
            Console.WriteLine(root.Print());
            Console.WriteLine();

            // Cálculo de subtotal recursivo (Composite en acción)
            // Laptop: $1000
            // Mouse: $50 x 2 = $100
            // Keyboard: $80
            // Peripherals (5% desc): ($100 + $80) * 0.95 = $171
            // BackToSchool (10% desc): ($1000 + $171) * 0.90 = $1053.90
            Console.WriteLine($"Subtotal: {root.GetSubtotal():0.00}");

            // Aplicar diferentes estrategias de impuestos
            var orderUS = new Order("US", root);
            var orderFR = new Order("FR", root);
            var orderEX = new Order("EXPORT", root);

            Console.WriteLine($"Total (US 7%): {engine.Total(orderUS):0.00}");     // 1127.67
            Console.WriteLine($"Total (FR 20%): {engine.Total(orderFR):0.00}");    // 1264.68
            Console.WriteLine($"Total (Export 0%): {engine.Total(orderEX):0.00}"); // 1053.90
        }
    }
}
```

### Salida del Programa

```
+ OrderRoot (desc 0%) = $1,053.90
  + BackToSchool (desc 10%) = $1,053.90
    - Laptop x1: $1,000.00
    + Peripherals (desc 5%) = $171.00
      - Mouse x2: $100.00
      - Keyboard x1: $80.00

Subtotal: 1053.90
Total (US 7%): 1127.67
Total (FR 20%): 1264.68
Total (Export 0%): 1053.90
```

### Análisis del Diseño

#### ✅ Composite Pattern en Acción

1. **Component único**: `IPriceComponent` con `GetSubtotal()` y `Print()`
2. **Leaf**: `Item` - productos individuales
3. **Composite**: `Bundle` - contenedor de componentes con descuento
4. **Recursión transparente**: Cliente trata Items y Bundles igual
5. **Composite Seguro**: Solo `Bundle` tiene métodos `Add()`, no `Item`

#### ✅ Strategy Pattern en Acción

1. **Strategy Interface**: `ITaxStrategy` define el contrato
2. **Concrete Strategies**: `UsTaxStrategy`, `FrTaxStrategy`, `ExportTaxStrategy`
3. **Context**: `Order` contiene el país y la estructura de precios
4. **Selection**: `PriceEngineService` selecciona estrategia apropiada en runtime
5. **Null Object**: `NoTaxStrategy` como fallback seguro

#### 🔄 Combinación de Patrones

```
Composite (Estructura)  →  Strategy (Comportamiento)
      ↓                           ↓
  GetSubtotal()    →    PriceEngineService.Total()
   (recursivo)              (estrategias)
```

**Flujo**:
1. **Composite** calcula subtotal recursivamente sumando toda la jerarquía
2. **Strategy** selecciona y aplica la estrategia de impuestos apropiada
3. El resultado es un total flexible que puede manejar estructuras complejas y múltiples reglas fiscales

#### 💡 Ventajas de esta Combinación

1. **Separación de responsabilidades**:
   - Composite: Estructura de productos y cálculo de subtotales
   - Strategy: Lógica de impuestos por país

2. **Extensibilidad**:
   - Añadir nuevos tipos de productos → Nueva clase que implemente `IPriceComponent`
   - Añadir nuevos países → Nueva clase que implemente `ITaxStrategy`

3. **Testabilidad**:
   - Cada estrategia de impuestos se puede testear independientemente
   - La estructura Composite se puede testear sin preocuparse por impuestos

4. **Open/Closed Principle**:
   - Abierto a extensión (nuevas estrategias/productos)
   - Cerrado a modificación (código existente no cambia)

### 🎯 Casos de Uso Adicionales

Este patrón combinado es ideal para:

- **E-commerce**: Precios con descuentos jerárquicos + impuestos por región
- **Sistemas de facturación**: Líneas de pedido + estrategias de cálculo (B2B, B2C, etc.)
- **Sistemas de menú**: Menú compuesto + estrategias de pricing (happy hour, eventos)
- **Cotizaciones**: Estructura de servicios + estrategias de descuento por volumen

---

## 📚 Recursos

- [C# Design Patterns - Composite](https://www.dofactory.com/net/composite-design-pattern)
- [LINQ Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/linq/)
- [Pattern Matching in C#](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching)

---

## 🙏 Créditos

- **Profesor Bismarck Ponce** - Ejemplo de Composite + Strategy en sistemas de precios
- **Refactoring Guru** - Alexander Shvets
- **DoFactory**
- **Microsoft Learn**

---

[← Volver a Composite](../README.md)
