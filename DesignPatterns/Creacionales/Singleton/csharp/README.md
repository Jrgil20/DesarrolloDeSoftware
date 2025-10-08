# Singleton - Implementación en C#

## 📖 Descripción

Referencias a implementaciones del patrón Singleton en C#/.NET, incluyendo variantes thread-safe y uso de Lazy<T>.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Singleton C#**
- **Enlace**: [Singleton en C#](https://refactoring.guru/design-patterns/singleton/csharp/example)

### 2. **DoFactory - Singleton Pattern**
- **Enlace**: [DoFactory Singleton](https://www.dofactory.com/net/singleton-design-pattern)

### 3. **C# in Depth - Jon Skeet**
- **Enlace**: [Implementing Singleton in C#](https://csharpindepth.com/articles/singleton)
- **Características**: ✅ 6 versiones analizadas por Jon Skeet (autoridad en C#)

---

## 💡 Variantes del Patrón

### 1. Lazy with Lazy<T> (Recomendado)
```csharp
public sealed class Singleton
{
    private static readonly Lazy<Singleton> lazy =
        new Lazy<Singleton>(() => new Singleton());
    
    public static Singleton Instance => lazy.Value;
    
    private Singleton() { }
}
```

### 2. Thread-Safe con Double-Check Locking
```csharp
public sealed class Singleton
{
    private static volatile Singleton instance;
    private static readonly object lockObject = new object();
    
    private Singleton() { }
    
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
}
```

### 3. Static Constructor (Jon Skeet's Version 4)
```csharp
public sealed class Singleton
{
    private static readonly Singleton instance = new Singleton();
    
    // Explicit static constructor para evitar beforefieldinit
    static Singleton() { }
    
    private Singleton() { }
    
    public static Singleton Instance => instance;
}
```

---

## 🔧 Con Dependency Injection (.NET Core)

```csharp
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<ISingletonService, SingletonService>();
}

// Uso
public class MyController : ControllerBase
{
    private readonly ISingletonService _service;
    
    public MyController(ISingletonService service)
    {
        _service = service;
    }
}
```

---

## 📚 Recursos

- [C# in Depth - Jon Skeet](https://csharpindepth.com/articles/singleton)
- [Microsoft Docs - Singleton Pattern](https://learn.microsoft.com/en-us/previous-versions/msp-n-p/ff650316(v=pandp.10))

---

## 🙏 Créditos

- **Jon Skeet** - "C# in Depth"
- **Refactoring Guru** - Alexander Shvets
- **DoFactory**

---

[← Volver a Singleton](../README.md)

