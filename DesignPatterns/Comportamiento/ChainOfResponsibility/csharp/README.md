# Chain of Responsibility - Implementación en C#

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Chain C#**
- **Enlace**: [Chain](https://refactoring.guru/design-patterns/chain-of-responsibility/csharp/example)

### 2. **DoFactory**
- **Enlace**: [DoFactory](https://www.dofactory.com/net/chain-of-responsibility-design-pattern)

---

## 💡 Ejemplo (ASP.NET Core Middleware)

```csharp
// ASP.NET Core usa Chain of Responsibility
public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseAuthentication();  // Handler 1
        app.UseAuthorization();   // Handler 2
        app.UseRouting();         // Handler 3
        app.UseEndpoints(...);    // Handler final
    }
}

// Custom middleware
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    
    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        // Antes
        Console.WriteLine($"Request: {context.Request.Path}");
        
        // Pasar al siguiente
        await _next(context);
        
        // Después
        Console.WriteLine($"Response: {context.Response.StatusCode}");
    }
}
```

---

## 🙏 Créditos
- **Refactoring Guru**
- **Microsoft - ASP.NET Core**

[← Volver](../README.md)
