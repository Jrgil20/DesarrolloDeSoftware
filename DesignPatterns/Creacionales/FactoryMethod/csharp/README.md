# Factory Method - Implementación en C#

## 📖 Descripción

Esta carpeta contiene referencias y enlaces a implementaciones de alta calidad del patrón Factory Method en C#/.NET, seleccionadas de repositorios reconocidos con excelentes prácticas de programación.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Factory Method C#**

**Recurso educativo líder mundial para patrones de diseño**

- **Enlace**: [Factory Method en C#](https://refactoring.guru/design-patterns/factory-method/csharp/example)
- **Características**:
  - ✅ Código completo descargable
  - ✅ Ejemplos del mundo real
  - ✅ Explicaciones visuales detalladas
  - ✅ Diagramas UML interactivos

**Ejemplo destacado**: Sistema de logística multiplataforma (carretera/mar)

---

### 2. **DoFactory - C# Design Patterns**

**Biblioteca de patrones profesional para .NET**

- **Enlace**: [Factory Method Pattern](https://www.dofactory.com/net/factory-method-design-pattern)
- **Características**:
  - ✅ Ejemplos estructurales y del mundo real
  - ✅ Código optimizado para .NET
  - ✅ Explicaciones paso a paso
  - ✅ Diagrama UML profesional

---

### 3. **exceptionnotfound/DesignPatterns** (GitHub)

**Colección completa de patrones en C#**

- **Enlace**: [Factory Method](https://github.com/exceptionnotfound/DesignPatterns)
- **Características**:
  - ✅ Proyectos .NET completos
  - ✅ Ejemplos prácticos
  - ✅ Código bien estructurado

---

## 💡 Ejemplo Simple (Referencia rápida)

```csharp
// Basado en los repositorios mencionados

// Product Interface
public interface IVehicle
{
    void Deliver();
}

// Concrete Products
public class Truck : IVehicle
{
    public void Deliver()
    {
        Console.WriteLine("Entrega por tierra en un camión");
    }
}

public class Ship : IVehicle
{
    public void Deliver()
    {
        Console.WriteLine("Entrega por mar en un barco");
    }
}

// Creator Abstract Class
public abstract class Logistics
{
    // Factory Method
    public abstract IVehicle CreateVehicle();
    
    // Template method que usa el factory method
    public void PlanDelivery()
    {
        var vehicle = CreateVehicle();
        vehicle.Deliver();
    }
}

// Concrete Creators
public class RoadLogistics : Logistics
{
    public override IVehicle CreateVehicle()
    {
        return new Truck();
    }
}

public class SeaLogistics : Logistics
{
    public override IVehicle CreateVehicle()
    {
        return new Ship();
    }
}

// Client Code
class Program
{
    static void Main(string[] args)
    {
        Logistics logistics;
        
        logistics = new RoadLogistics();
        logistics.PlanDelivery(); // Salida: Entrega por tierra en un camión
        
        logistics = new SeaLogistics();
        logistics.PlanDelivery(); // Salida: Entrega por mar en un barco
    }
}
```

---

## 🔧 Características Específicas de C# / .NET

### 1. **Uso de Interfaces vs Clases Abstractas**
```csharp
// Interfaces (preferidas para contratos)
public interface IProduct
{
    void Operation();
}

// Clases abstractas (cuando hay implementación común)
public abstract class Creator
{
    public abstract IProduct FactoryMethod();
    
    // Método template compartido
    public void SomeOperation()
    {
        var product = FactoryMethod();
        product.Operation();
    }
}
```

### 2. **Uso de Generics**
```csharp
public abstract class Creator<T> where T : IProduct
{
    public abstract T FactoryMethod();
}

public class ConcreteCreator : Creator<ConcreteProduct>
{
    public override ConcreteProduct FactoryMethod()
    {
        return new ConcreteProduct();
    }
}
```

### 3. **Expression-bodied Members (C# 6+)**
```csharp
public class ConcreteCreator : Creator
{
    public override IProduct FactoryMethod() => new ConcreteProduct();
}
```

### 4. **Pattern Matching (C# 7+)**
```csharp
public class ProductFactory
{
    public IProduct CreateProduct(ProductType type) => type switch
    {
        ProductType.TypeA => new ConcreteProductA(),
        ProductType.TypeB => new ConcreteProductB(),
        ProductType.TypeC => new ConcreteProductC(),
        _ => throw new ArgumentException($"Unknown type: {type}")
    };
}
```

### 5. **Dependency Injection con .NET Core**
```csharp
// Startup.cs o Program.cs
public void ConfigureServices(IServiceCollection services)
{
    // Registrar factory como servicio
    services.AddScoped<IProductFactory, ConcreteProductFactory>();
    
    // O usar factory delegate
    services.AddScoped<IProduct>(provider => {
        var config = provider.GetRequiredService<IConfiguration>();
        return CreateProductBasedOnConfig(config);
    });
}
```

### 6. **Async Factory Methods**
```csharp
public abstract class AsyncCreator
{
    public abstract Task<IProduct> CreateProductAsync();
    
    public async Task ProcessAsync()
    {
        var product = await CreateProductAsync();
        await product.OperationAsync();
    }
}
```

---

## 📚 Recursos Adicionales

### Documentación Oficial de Microsoft
- [Factory Method Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core)
- [Design Patterns in C#](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles#dependency-inversion)

### Libros
- **"C# Design Patterns" por Vaskaran Sarcar**
- **"Pro .NET Design Pattern Framework" por DoFactory**
- **"Head First Design Patterns" (ejemplos adaptables a C#)**

### Cursos en Línea
- [Pluralsight - C# Design Patterns](https://www.pluralsight.com/courses/c-sharp-design-patterns-factory-abstract)
- [LinkedIn Learning - Design Patterns in C#](https://www.linkedin.com/learning/c-sharp-design-patterns-part-1)

### Artículos
- [C# Corner - Factory Method Pattern](https://www.c-sharpcorner.com/UploadFile/8a67c0/factory-method-design-pattern-in-C-Sharp/)
- [Code Maze - Factory Method Pattern](https://code-maze.com/factory-method/)

---

## ⚙️ Configuración del Proyecto

### .NET Core / .NET 6+ (Recomendado)

**Crear proyecto**:
```bash
dotnet new console -n FactoryMethodPattern
cd FactoryMethodPattern
```

**Archivo .csproj**:
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  
  <!-- Para pruebas unitarias -->
  <ItemGroup>
    <PackageReference Include="xunit" Version="2.5.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.0" />
  </ItemGroup>
</Project>
```

**Ejecutar**:
```bash
dotnet run
```

**Ejecutar tests**:
```bash
dotnet test
```

---

## 🎯 Mejores Prácticas en C#

### 1. **Convenciones de Nomenclatura**
```csharp
// Interfaces con prefijo 'I'
public interface IProduct { }

// Clases con PascalCase
public class ConcreteProduct : IProduct { }

// Métodos factory claros
public abstract IProduct CreateProduct();
public static IProduct NewInstance();
```

### 2. **Uso de Null Safety (C# 8+)**
```csharp
#nullable enable

public abstract class Creator
{
    public abstract IProduct? CreateProduct(string type);
    
    public void Process(string type)
    {
        var product = CreateProduct(type);
        if (product is not null)
        {
            product.Operation();
        }
    }
}
```

### 3. **Records para Productos Inmutables (C# 9+)**
```csharp
public record Product(string Name, decimal Price) : IProduct
{
    public void Display() => Console.WriteLine($"{Name}: ${Price}");
}
```

### 4. **Init-only Properties**
```csharp
public class Product : IProduct
{
    public string Name { get; init; }
    public decimal Price { get; init; }
}

// Uso
var product = new Product { Name = "Item", Price = 99.99m };
```

### 5. **Logging e Instrumentación**
```csharp
public class ConcreteCreator : Creator
{
    private readonly ILogger<ConcreteCreator> _logger;
    
    public ConcreteCreator(ILogger<ConcreteCreator> logger)
    {
        _logger = logger;
    }
    
    public override IProduct CreateProduct()
    {
        _logger.LogInformation("Creating product");
        return new ConcreteProduct();
    }
}
```

---

## 🧪 Ejemplo de Tests Unitarios (xUnit)

```csharp
using Xunit;

public class LogisticsTests
{
    [Fact]
    public void RoadLogistics_ShouldCreateTruck()
    {
        // Arrange
        var logistics = new RoadLogistics();
        
        // Act
        var vehicle = logistics.CreateVehicle();
        
        // Assert
        Assert.IsType<Truck>(vehicle);
    }
    
    [Fact]
    public void SeaLogistics_ShouldCreateShip()
    {
        // Arrange
        var logistics = new SeaLogistics();
        
        // Act
        var vehicle = logistics.CreateVehicle();
        
        // Assert
        Assert.IsType<Ship>(vehicle);
    }
    
    [Theory]
    [InlineData(typeof(RoadLogistics), typeof(Truck))]
    [InlineData(typeof(SeaLogistics), typeof(Ship))]
    public void Logistics_ShouldCreateCorrectVehicleType(
        Type logisticsType, 
        Type expectedVehicleType)
    {
        // Arrange
        var logistics = (Logistics)Activator.CreateInstance(logisticsType);
        
        // Act
        var vehicle = logistics.CreateVehicle();
        
        // Assert
        Assert.IsType(expectedVehicleType, vehicle);
    }
}
```

---

## 📝 Notas

- Ejemplos optimizados para **.NET 8** (última LTS)
- Compatible con **.NET Core 3.1+** y **.NET Framework 4.7.2+** con ajustes menores
- Se recomienda usar **nullable reference types** para mayor seguridad
- Considerar **Source Generators** (C# 9+) para reducir boilerplate en casos avanzados

---

## 🙏 Créditos

Los ejemplos y referencias provienen de:

- **Refactoring Guru** - Alexander Shvets
  - [Sitio web](https://refactoring.guru)
  - Contenido educativo de referencia mundial

- **DoFactory** - Data & Object Factory
  - [Sitio web](https://www.dofactory.com)
  - Patrones profesionales para .NET

- **Microsoft Learn** - Documentación oficial
  - [Microsoft Docs](https://learn.microsoft.com)

Se agradece a los autores y la comunidad .NET por sus contribuciones educativas.

---

[← Volver a Factory Method](../README.md) | [📂 Ver todos los patrones creacionales](../../Creacionales.md)

