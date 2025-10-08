# Abstract Factory - Implementación en C#

## 📖 Descripción

Esta carpeta contiene referencias a implementaciones de alta calidad del patrón Abstract Factory en C#/.NET, demostrando cómo crear familias de objetos relacionados garantizando compatibilidad entre productos.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Abstract Factory C#**

- **Enlace**: [Abstract Factory en C#](https://refactoring.guru/design-patterns/abstract-factory/csharp/example)
- **Características**:
  - ✅ Ejemplo de muebles (moderno vs victoriano)
  - ✅ Familias de productos completamente compatibles
  - ✅ Código descargable
  - ✅ Diagramas UML interactivos

### 2. **DoFactory - Abstract Factory Pattern**

- **Enlace**: [Abstract Factory Pattern](https://www.dofactory.com/net/abstract-factory-design-pattern)
- **Características**:
  - ✅ Ejemplos estructurales y del mundo real
  - ✅ Optimizado para .NET
  - ✅ Código de producción

### 3. **Microsoft Docs - Factory Patterns**

- **Enlace**: [Factory Patterns in .NET](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core)
- **Características**:
  - ✅ Documentación oficial
  - ✅ Patrones enterprise
  - ✅ Integración con DI

---

## 💡 Ejemplo de Referencia Rápida

```csharp
// Basado en repositorios reconocidos

// Abstract Products
public interface IButton
{
    void Render();
}

public interface ICheckbox
{
    void Check();
}

// Concrete Products - Windows Family
public class WindowsButton : IButton
{
    public void Render()
    {
        Console.WriteLine("🪟 Renderizando botón Windows");
    }
}

public class WindowsCheckbox : ICheckbox
{
    public void Check()
    {
        Console.WriteLine("🪟 Checkbox Windows activado");
    }
}

// Concrete Products - Mac Family
public class MacButton : IButton
{
    public void Render()
    {
        Console.WriteLine("🍎 Renderizando botón Mac");
    }
}

public class MacCheckbox : ICheckbox
{
    public void Check()
    {
        Console.WriteLine("🍎 Checkbox Mac activado");
    }
}

// Abstract Factory
public interface IGUIFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}

// Concrete Factories
public class WindowsFactory : IGUIFactory
{
    public IButton CreateButton() => new WindowsButton();
    public ICheckbox CreateCheckbox() => new WindowsCheckbox();
}

public class MacFactory : IGUIFactory
{
    public IButton CreateButton() => new MacButton();
    public ICheckbox CreateCheckbox() => new MacCheckbox();
}

// Client
public class Application
{
    private readonly IButton _button;
    private readonly ICheckbox _checkbox;
    
    public Application(IGUIFactory factory)
    {
        _button = factory.CreateButton();
        _checkbox = factory.CreateCheckbox();
    }
    
    public void Render()
    {
        _button.Render();
        _checkbox.Check();
    }
}

// Program
class Program
{
    static void Main(string[] args)
    {
        IGUIFactory factory;
        
        if (OperatingSystem.IsWindows())
        {
            factory = new WindowsFactory();
        }
        else if (OperatingSystem.IsMacOS())
        {
            factory = new MacFactory();
        }
        else
        {
            factory = new WindowsFactory(); // default
        }
        
        var app = new Application(factory);
        app.Render();
    }
}
```

---

## 🔧 Características Específicas de C# / .NET

### 1. **Dependency Injection con .NET Core**
```csharp
// Startup.cs o Program.cs
public void ConfigureServices(IServiceCollection services)
{
    // Registrar factory según configuración
    var theme = Configuration["UI:Theme"];
    
    if (theme == "Dark")
    {
        services.AddSingleton<IGUIFactory, DarkThemeFactory>();
    }
    else
    {
        services.AddSingleton<IGUIFactory, LightThemeFactory>();
    }
    
    // O usar factory method
    services.AddSingleton<IGUIFactory>(provider => {
        var config = provider.GetRequiredService<IConfiguration>();
        return GUIFactoryProvider.GetFactory(config["Platform"]);
    });
}

// Uso en controller
public class UIController : ControllerBase
{
    private readonly IGUIFactory _factory;
    
    public UIController(IGUIFactory factory)
    {
        _factory = factory;
    }
    
    public IActionResult CreateUI()
    {
        var button = _factory.CreateButton();
        var checkbox = _factory.CreateCheckbox();
        return Ok(new { button, checkbox });
    }
}
```

### 2. **Factory con Generics y Constraints**
```csharp
public interface IAbstractFactory<TProductA, TProductB>
    where TProductA : IProductA
    where TProductB : IProductB
{
    TProductA CreateProductA();
    TProductB CreateProductB();
}

public class ConcreteFactory<TProductA, TProductB> 
    : IAbstractFactory<TProductA, TProductB>
    where TProductA : IProductA, new()
    where TProductB : IProductB, new()
{
    public TProductA CreateProductA() => new TProductA();
    public TProductB CreateProductB() => new TProductB();
}
```

### 3. **Async Factory Methods**
```csharp
public interface IAsyncFactory
{
    Task<IButton> CreateButtonAsync();
    Task<ICheckbox> CreateCheckboxAsync();
}

public class DatabaseFactory : IAsyncFactory
{
    private readonly IDbContext _context;
    
    public DatabaseFactory(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<IButton> CreateButtonAsync()
    {
        var config = await _context.ButtonConfigs
            .FirstOrDefaultAsync();
        return new CustomButton(config);
    }
    
    public async Task<ICheckbox> CreateCheckboxAsync()
    {
        var config = await _context.CheckboxConfigs
            .FirstOrDefaultAsync();
        return new CustomCheckbox(config);
    }
}
```

### 4. **Pattern Matching con Switch Expressions**
```csharp
public class FactoryProvider
{
    public static IGUIFactory GetFactory(Platform platform) => platform switch
    {
        Platform.Windows => new WindowsFactory(),
        Platform.MacOS => new MacFactory(),
        Platform.Linux => new LinuxFactory(),
        Platform.Web => new WebFactory(),
        _ => throw new ArgumentException($"Unknown platform: {platform}")
    };
}
```

### 5. **Records para Immutable Products (C# 9+)**
```csharp
public record Button(string Style, string Color) : IButton
{
    public void Render() => 
        Console.WriteLine($"Button: {Style}, {Color}");
}

public record Checkbox(string Style, bool IsChecked) : ICheckbox
{
    public void Check() => 
        Console.WriteLine($"Checkbox: {Style}, Checked: {IsChecked}");
}
```

### 6. **Factory Registry Pattern**
```csharp
public class FactoryRegistry
{
    private static readonly Dictionary<string, Func<IGUIFactory>> _factories = new()
    {
        ["windows"] = () => new WindowsFactory(),
        ["mac"] = () => new MacFactory(),
        ["linux"] = () => new LinuxFactory()
    };
    
    public static IGUIFactory GetFactory(string key)
    {
        if (_factories.TryGetValue(key.ToLower(), out var factoryCreator))
        {
            return factoryCreator();
        }
        throw new KeyNotFoundException($"Factory '{key}' not found");
    }
    
    public static void RegisterFactory(string key, Func<IGUIFactory> factoryCreator)
    {
        _factories[key.ToLower()] = factoryCreator;
    }
}
```

---

## 📚 Recursos Adicionales

### Documentación Microsoft
- [Dependency Injection in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Design Patterns in .NET](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles)
- [Factory Pattern Best Practices](https://learn.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core)

### Artículos
- [C# Corner - Abstract Factory Pattern](https://www.c-sharpcorner.com/UploadFile/97aa0f/abstract-factory-design-pattern-in-C-Sharp/)
- [Code Maze - Abstract Factory](https://code-maze.com/abstract-factory/)
- [DotNetTutorials - Abstract Factory](https://dotnettutorials.net/lesson/abstract-factory-design-pattern-csharp/)

### Libros
- **"C# Design Patterns" por Vaskaran Sarcar**
- **"Pro .NET Design Pattern Framework" por DoFactory**
- **"Adaptive Code" por Gary McLean Hall**

---

## ⚙️ Configuración del Proyecto

### .NET 8 Project (.csproj)
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  
  <ItemGroup>
    <!-- Para DI -->
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="8.0.0" />
    
    <!-- Para tests -->
    <PackageReference Include="xunit" Version="2.6.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
    <PackageReference Include="FluentAssertions" Version="6.12.0" />
    <PackageReference Include="Moq" Version="4.20.69" />
  </ItemGroup>
</Project>
```

### Commands
```bash
# Crear proyecto
dotnet new console -n AbstractFactoryPattern
cd AbstractFactoryPattern

# Agregar paquetes
dotnet add package Microsoft.Extensions.DependencyInjection

# Ejecutar
dotnet run

# Tests
dotnet test
```

---

## 🎯 Mejores Prácticas

### 1. **Usar Dependency Injection**
```csharp
// ✅ Correcto
public class MyService
{
    private readonly IGUIFactory _factory;
    
    public MyService(IGUIFactory factory)
    {
        _factory = factory;
    }
}

// ❌ Evitar
public class MyService
{
    public void DoWork()
    {
        var factory = new WindowsFactory(); // Acoplamiento fuerte
    }
}
```

### 2. **Validar Compatibilidad de Productos**
```csharp
public abstract class AbstractFactory : IGUIFactory
{
    public abstract IButton CreateButton();
    public abstract ICheckbox CreateCheckbox();
    
    public void ValidateProductFamily()
    {
        var button = CreateButton();
        var checkbox = CreateCheckbox();
        
        if (button.GetTheme() != checkbox.GetTheme())
        {
            throw new InvalidOperationException(
                "Products from different families are incompatible");
        }
    }
}
```

### 3. **Logging e Instrumentación**
```csharp
public class LoggingFactoryDecorator : IGUIFactory
{
    private readonly IGUIFactory _factory;
    private readonly ILogger<LoggingFactoryDecorator> _logger;
    
    public LoggingFactoryDecorator(
        IGUIFactory factory, 
        ILogger<LoggingFactoryDecorator> logger)
    {
        _factory = factory;
        _logger = logger;
    }
    
    public IButton CreateButton()
    {
        _logger.LogInformation("Creating button");
        return _factory.CreateButton();
    }
    
    public ICheckbox CreateCheckbox()
    {
        _logger.LogInformation("Creating checkbox");
        return _factory.CreateCheckbox();
    }
}
```

---

## 🧪 Ejemplo de Tests (xUnit + FluentAssertions)

```csharp
using Xunit;
using FluentAssertions;
using Moq;

public class AbstractFactoryTests
{
    [Fact]
    public void WindowsFactory_ShouldCreateWindowsProducts()
    {
        // Arrange
        IGUIFactory factory = new WindowsFactory();
        
        // Act
        var button = factory.CreateButton();
        var checkbox = factory.CreateCheckbox();
        
        // Assert
        button.Should().BeOfType<WindowsButton>();
        checkbox.Should().BeOfType<WindowsCheckbox>();
    }
    
    [Fact]
    public void MacFactory_ShouldCreateMacProducts()
    {
        // Arrange
        IGUIFactory factory = new MacFactory();
        
        // Act
        var button = factory.CreateButton();
        var checkbox = factory.CreateCheckbox();
        
        // Assert
        button.Should().BeOfType<MacButton>();
        checkbox.Should().BeOfType<MacCheckbox>();
    }
    
    [Theory]
    [InlineData(typeof(WindowsFactory), typeof(WindowsButton), typeof(WindowsCheckbox))]
    [InlineData(typeof(MacFactory), typeof(MacButton), typeof(MacCheckbox))]
    public void Factory_ShouldCreateMatchingProductFamily(
        Type factoryType, 
        Type expectedButtonType, 
        Type expectedCheckboxType)
    {
        // Arrange
        var factory = (IGUIFactory)Activator.CreateInstance(factoryType)!;
        
        // Act
        var button = factory.CreateButton();
        var checkbox = factory.CreateCheckbox();
        
        // Assert
        button.Should().BeOfType(expectedButtonType);
        checkbox.Should().BeOfType(expectedCheckboxType);
    }
    
    [Fact]
    public void Application_ShouldAcceptAnyFactory()
    {
        // Arrange
        var factories = new IGUIFactory[]
        {
            new WindowsFactory(),
            new MacFactory()
        };
        
        // Act & Assert
        foreach (var factory in factories)
        {
            var app = new Application(factory);
            app.Invoking(a => a.Render())
               .Should().NotThrow();
        }
    }
    
    [Fact]
    public void FactoryWithDI_ShouldResolveCorrectly()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddSingleton<IGUIFactory, WindowsFactory>();
        services.AddTransient<Application>();
        
        var provider = services.BuildServiceProvider();
        
        // Act
        var app = provider.GetRequiredService<Application>();
        
        // Assert
        app.Should().NotBeNull();
        app.Invoking(a => a.Render())
           .Should().NotThrow();
    }
}
```

---

## 📝 Notas

- Ejemplos optimizados para **.NET 8** (LTS)
- Compatible con **.NET 6+** con ajustes menores
- Usar **nullable reference types** para mayor seguridad
- Considerar **Source Generators** para reducir boilerplate

---

## 🙏 Créditos

Los ejemplos provienen de:

- **Refactoring Guru** - Alexander Shvets
  - [Sitio web](https://refactoring.guru)

- **DoFactory**
  - [Sitio web](https://www.dofactory.com)

- **Microsoft Learn** - Documentación oficial
  - [Microsoft Docs](https://learn.microsoft.com)

---

[← Volver a Abstract Factory](../README.md) | [📂 Ver todos los patrones creacionales](../../Creacionales.md)

