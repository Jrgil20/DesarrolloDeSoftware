# Proxy - Implementación en C#

## 📖 Descripción

Referencias a implementaciones del patrón Proxy en C#/.NET con DispatchProxy y Castle DynamicProxy.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Proxy C#**
- **Enlace**: [Proxy en C#](https://refactoring.guru/design-patterns/proxy/csharp/example)

### 2. **DoFactory - Proxy Pattern**
- **Enlace**: [DoFactory Proxy](https://www.dofactory.com/net/proxy-design-pattern)

---

## 💡 Tipos de Proxy Implementados

### 1. Virtual Proxy (Lazy Loading)

```csharp
// Subject Interface
public interface IImage
{
    void Display();
}

// Real Subject (Costoso)
public class RealImage : IImage
{
    private readonly string _filename;
    
    public RealImage(string filename)
    {
        _filename = filename;
        LoadFromDisk(); // Operación costosa
    }
    
    private void LoadFromDisk()
    {
        Console.WriteLine($"Loading image: {_filename}");
        Thread.Sleep(2000); // Simular carga
    }
    
    public void Display()
    {
        Console.WriteLine($"Displaying image: {_filename}");
    }
}

// Proxy (Lazy)
public class ImageProxy : IImage
{
    private RealImage? _realImage;
    private readonly string _filename;
    
    public ImageProxy(string filename)
    {
        _filename = filename;
    }
    
    public void Display()
    {
        // Lazy initialization
        _realImage ??= new RealImage(_filename);
        _realImage.Display();
    }
}

// Uso
IImage image = new ImageProxy("large_photo.jpg");
Console.WriteLine("Proxy created");
// No ha cargado aún ✅

image.Display(); // Primera vez: carga (2 seg)
image.Display(); // Segunda vez: instantáneo
```

### 2. Protection Proxy

```csharp
public interface IBankAccount
{
    void Withdraw(decimal amount);
    decimal GetBalance();
}

public class RealBankAccount : IBankAccount
{
    private decimal _balance = 1000m;
    
    public void Withdraw(decimal amount)
    {
        _balance -= amount;
        Console.WriteLine($"Withdrawn: ${amount}");
    }
    
    public decimal GetBalance() => _balance;
}

public class ProtectionProxy : IBankAccount
{
    private readonly RealBankAccount _realAccount;
    private readonly string _userRole;
    
    public ProtectionProxy(string userRole)
    {
        _userRole = userRole;
        _realAccount = new RealBankAccount();
    }
    
    public void Withdraw(decimal amount)
    {
        if (_userRole == "OWNER")
        {
            _realAccount.Withdraw(amount);
        }
        else
        {
            Console.WriteLine("❌ Access denied: insufficient permissions");
        }
    }
    
    public decimal GetBalance()
    {
        if (_userRole is "OWNER" or "VIEWER")
        {
            return _realAccount.GetBalance();
        }
        Console.WriteLine("❌ Access denied");
        return 0m;
    }
}
```

### 3. DispatchProxy (Dynamic Proxy .NET)

```csharp
using System.Reflection;

public class LoggingProxy<T> : DispatchProxy where T : class
{
    private T? _target;
    
    public static T Create(T target)
    {
        object proxy = Create<T, LoggingProxy<T>>();
        ((LoggingProxy<T>)proxy).SetTarget(target);
        return (T)proxy;
    }
    
    private void SetTarget(T target)
    {
        _target = target;
    }
    
    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        Console.WriteLine($"⏱ Before: {targetMethod?.Name}");
        var stopwatch = Stopwatch.StartNew();
        
        var result = targetMethod?.Invoke(_target, args);
        
        stopwatch.Stop();
        Console.WriteLine($"✓ After: {targetMethod?.Name} ({stopwatch.ElapsedMilliseconds}ms)");
        
        return result;
    }
}

// Uso
public interface IUserService
{
    User GetUser(int id);
    void SaveUser(User user);
}

public class UserService : IUserService
{
    public User GetUser(int id) => new User { Id = id, Name = "John" };
    public void SaveUser(User user) => Console.WriteLine($"Saving {user.Name}");
}

// Crear proxy dinámico
var realService = new UserService();
var proxy = LoggingProxy<IUserService>.Create(realService);

var user = proxy.GetUser(1);
// Output:
// ⏱ Before: GetUser
// ✓ After: GetUser (5ms)
```

### 4. Caching Proxy

```csharp
public class CachingProxy<T> : IBankAccount where T : IBankAccount
{
    private readonly T _realObject;
    private readonly Dictionary<string, object> _cache = new();
    private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(5);
    private readonly Dictionary<string, DateTime> _cacheTimestamps = new();
    
    public CachingProxy(T realObject)
    {
        _realObject = realObject;
    }
    
    public decimal GetBalance()
    {
        string cacheKey = nameof(GetBalance);
        
        if (_cache.TryGetValue(cacheKey, out var cachedValue) &&
            _cacheTimestamps.TryGetValue(cacheKey, out var timestamp) &&
            DateTime.Now - timestamp < _cacheDuration)
        {
            Console.WriteLine("💾 Cache hit");
            return (decimal)cachedValue;
        }
        
        Console.WriteLine("🔍 Cache miss - fetching from real object");
        var balance = _realObject.GetBalance();
        
        _cache[cacheKey] = balance;
        _cacheTimestamps[cacheKey] = DateTime.Now;
        
        return balance;
    }
    
    public void Withdraw(decimal amount)
    {
        _realObject.Withdraw(amount);
        _cache.Clear(); // Invalidar cache
    }
}
```

---

## 🔧 Castle DynamicProxy (Biblioteca Popular)

```csharp
// Install: dotnet add package Castle.Core

using Castle.DynamicProxy;

public class LoggingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Calling: {invocation.Method.Name}");
        
        invocation.Proceed(); // Llamar al método real
        
        Console.WriteLine($"Completed: {invocation.Method.Name}");
    }
}

// Uso
var generator = new ProxyGenerator();
var interceptor = new LoggingInterceptor();

var proxy = generator.CreateInterfaceProxyWithTarget<IUserService>(
    new UserService(), 
    interceptor
);

proxy.GetUser(1); 
// Output:
// Calling: GetUser
// Completed: GetUser
```

---

## 🎓 Ejemplo Real del Mundo: Sistema de Checkout (Proxy + Facade)

**Crédito**: Profesor **Bismarck Ponce** - Ejemplo utilizado en clase

Este ejemplo muestra cómo **Proxy** y **Facade** trabajan juntos, con 2 tipos de proxy implementados.

### 📦 Archivo Ejecutable Completo

El código completo está disponible en: **[CheckoutDemo.cs](./CheckoutDemo.cs)** (enlace a Facade)

### Proxies Implementados

#### 1. **Caching Proxy** (Optimización)

```csharp
public sealed class CachingTaxServiceProxy : ITaxService
{
    private readonly Dictionary<string, (decimal rate, DateTimeOffset exp)> _cache = new();
    private readonly TimeSpan _ttl = TimeSpan.FromMinutes(5);
    
    public async Task<decimal> GetRateAsync(string country, CancellationToken ct)
    {
        var key = country.ToUpperInvariant();
        
        // Cache check
        if (_cache.TryGetValue(key, out var hit) && hit.exp > DateTimeOffset.UtcNow)
            return hit.rate;  // 💾 HIT
        
        // Cache miss: call real service
        var rate = await _inner.GetRateAsync(country, ct);  // 🔍 MISS
        _cache[key] = (rate, DateTimeOffset.UtcNow.Add(_ttl));
        
        return rate;
    }
}
```

**Beneficio**:
- Primera llamada: Consulta API externa (lenta)
- Siguientes 5 min: Retorna desde memoria (rápido)
- Reduce latencia ~90% y costos de API

#### 2. **Protection Proxy** (Seguridad)

```csharp
public sealed class PaymentProtectionProxy : IPaymentService
{
    private readonly ICurrentUser _current;
    
    public Task<string> ChargeAsync(decimal amount, string cardToken, CancellationToken ct)
    {
        // Verificar permisos ANTES de cobrar
        if (!_current.HasPermission("PAYMENT_EXECUTE"))
            throw new UnauthorizedAccessException("Missing permission");
        
        // Autorizado: delegar al servicio real
        return _inner.ChargeAsync(amount, cardToken, ct);
    }
}
```

**Beneficio**:
- Centraliza control de acceso
- Previene cobros no autorizados
- Falla rápido (antes de llamar gateway de pagos)

### Resultados Demostrados

**Escenario 1**: Usuario autorizado
- ✅ Pago procesado
- ✅ TaxService llamado 1 vez
- ✅ Email enviado

**Escenario 1B**: Segunda compra
- ✅ TaxService NO llamado (cache)
- ✅ Rendimiento mejorado

**Escenario 2**: Usuario no autorizado
- ❌ Bloqueado por Protection Proxy
- ❌ No se procesó pago
- ✅ Seguridad funcionó

### 🎯 Análisis Completo

Para análisis detallado con diagramas de arquitectura y secuencia:
- [Ver en Patrones Combinados](../../../PatronesCombinadosEjemplos.md#facade--proxy-sistema-de-checkout)

---

## 📚 Recursos

- [Microsoft - DispatchProxy](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.dispatchproxy)
- [Castle DynamicProxy](http://www.castleproject.org/projects/dynamicproxy/)
- [C# Proxy Pattern](https://www.dofactory.com/net/proxy-design-pattern)

---

## 🙏 Créditos

- **Profesor Bismarck Ponce** - Ejemplo de Proxy + Facade en sistema de checkout
- **Refactoring Guru** - Alexander Shvets
- **DoFactory**
- **Castle Project**

---

[← Volver a Proxy](../README.md)
