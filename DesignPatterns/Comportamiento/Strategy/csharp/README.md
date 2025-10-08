# Strategy - Implementación en C#

## 📖 Descripción

Referencias a implementaciones del patrón Strategy en C#/.NET con delegates y expresiones lambda.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Strategy C#**
- **Enlace**: [Strategy en C#](https://refactoring.guru/design-patterns/strategy/csharp/example)

### 2. **DoFactory - Strategy Pattern**
- **Enlace**: [DoFactory Strategy](https://www.dofactory.com/net/strategy-design-pattern)

---

## 💡 Ejemplo de Referencia

```csharp
// Strategy Interface
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

// Concrete Strategies
public class CreditCardStrategy : IPaymentStrategy
{
    private readonly string _cardNumber;
    
    public CreditCardStrategy(string cardNumber)
    {
        _cardNumber = cardNumber;
    }
    
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paying ${amount} with credit card ending in {_cardNumber[^4..]}");
    }
}

public class PayPalStrategy : IPaymentStrategy
{
    private readonly string _email;
    
    public PayPalStrategy(string email)
    {
        _email = email;
    }
    
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paying ${amount} via PayPal: {_email}");
    }
}

// Context
public class ShoppingCart
{
    private IPaymentStrategy _paymentStrategy;
    private decimal _total = 0m;
    
    public void SetPaymentStrategy(IPaymentStrategy strategy)
    {
        _paymentStrategy = strategy;
    }
    
    public void Checkout()
    {
        if (_paymentStrategy == null)
            throw new InvalidOperationException("Payment strategy not set");
            
        _paymentStrategy.Pay(_total);
    }
    
    public void AddItem(decimal price) => _total += price;
}

// Client
class Program
{
    static void Main()
    {
        var cart = new ShoppingCart();
        cart.AddItem(49.99m);
        cart.AddItem(29.99m);
        
        // Tarjeta de crédito
        cart.SetPaymentStrategy(new CreditCardStrategy("1234-5678-9012-3456"));
        cart.Checkout();
        
        // PayPal
        cart.SetPaymentStrategy(new PayPalStrategy("user@example.com"));
        cart.Checkout();
    }
}
```

---

## 🔧 Características C#

### 1. Delegates (Alternativa Ligera)
```csharp
public class Calculator
{
    public delegate int OperationStrategy(int a, int b);
    
    private OperationStrategy _strategy;
    
    public void SetStrategy(OperationStrategy strategy)
    {
        _strategy = strategy;
    }
    
    public int Execute(int a, int b)
    {
        return _strategy(a, b);
    }
}

// Uso
var calc = new Calculator();

calc.SetStrategy((a, b) => a + b);  // Suma
Console.WriteLine(calc.Execute(5, 3)); // 8

calc.SetStrategy((a, b) => a * b);  // Multiplicación
Console.WriteLine(calc.Execute(5, 3)); // 15
```

### 2. Func<T> y Action<T>
```csharp
public class PriceCalculator
{
    private Func<decimal, decimal> _discountStrategy;
    
    public void SetDiscountStrategy(Func<decimal, decimal> strategy)
    {
        _discountStrategy = strategy;
    }
    
    public decimal CalculateFinalPrice(decimal price)
    {
        return _discountStrategy?.Invoke(price) ?? price;
    }
}

// Uso
var calculator = new PriceCalculator();

calculator.SetDiscountStrategy(price => price * 0.9m);  // 10% off
var finalPrice = calculator.CalculateFinalPrice(100m);  // 90
```

### 3. Strategy con Dependency Injection
```csharp
// Startup.cs o Program.cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IPaymentStrategy, CreditCardStrategy>();
    services.AddScoped<ShoppingCart>();
}

// Uso en controller
public class CheckoutController : ControllerBase
{
    private readonly ShoppingCart _cart;
    
    public CheckoutController(ShoppingCart cart)
    {
        _cart = cart;
    }
    
    [HttpPost]
    public IActionResult Checkout([FromServices] IPaymentStrategy paymentStrategy)
    {
        _cart.SetPaymentStrategy(paymentStrategy);
        _cart.Checkout();
        return Ok();
    }
}
```

---

## 🎓 Ejemplo Real del Mundo: Motor de Precios (Composite + Strategy)

**Crédito**: Profesor **Bismarck Ponce** - Ejemplo utilizado en clase

Este ejemplo muestra cómo **Strategy** y **Composite** trabajan juntos en un sistema de e-commerce real.

### 📦 Archivo Ejecutable Completo

El código completo está disponible en: **[PriceEngineDemo.cs](./PriceEngineDemo.cs)**

### Concepto

Un motor de precios que:
- Usa **Composite** para manejar jerarquías de productos con descuentos anidados
- Usa **Strategy** para aplicar diferentes reglas de impuestos según el país

### Flujo del Sistema

```
1. Composite construye jerarquía:
   Item → Bundle → Bundle (anidado)
   
2. Composite calcula subtotal:
   GetSubtotal() recursivo suma toda la estructura
   
3. Strategy selecciona regla fiscal:
   FirstOrDefault(s => s.AppliesTo(order))
   
4. Strategy aplica impuesto:
   UsTaxStrategy → 7%
   FrTaxStrategy → 20%
   ExportTaxStrategy → 0%
```

### Extracto de Código Clave

```csharp
public sealed class PriceEngineService
{
    private readonly IEnumerable<ITaxStrategy> _strategies;

    public decimal Total(Order order)
    {
        // 1. COMPOSITE: Calcula subtotal recursivamente
        var subtotal = order.Root.GetSubtotal();

        // 2. STRATEGY: Selecciona estrategia aplicable
        var strategy = _strategies.FirstOrDefault(s => s.AppliesTo(order)) 
                      ?? new NoTaxStrategy();
        
        // 3. STRATEGY: Aplica cálculo de impuesto
        var total = strategy.Apply(subtotal, order);

        return Math.Round(total, 2);
    }
}
```

### Resultado

```
+ OrderRoot (desc 0%) = $1,053.90
  + BackToSchool (desc 10%) = $1,053.90
    - Laptop x1: $1,000.00
    + Peripherals (desc 5%) = $171.00
      - Mouse x2: $100.00
      - Keyboard x1: $80.00

Total US (7%):      $1,127.67
Total FR (20%):     $1,264.68
Total Export (0%):  $1,053.90
```

### 💡 Lecciones de Diseño

1. **Separación de responsabilidades**: Composite maneja estructura, Strategy maneja impuestos
2. **Chain of Responsibility implícito**: `FirstOrDefault` con predicado
3. **Null Object Pattern**: `NoTaxStrategy` como fallback
4. **LINQ para selección**: Código declarativo y legible
5. **Validación defensiva**: Validaciones en constructores

### 🎯 Aplicabilidad

Este diseño es ideal para:
- Sistemas de facturación con múltiples reglas fiscales
- E-commerce con promociones jerárquicas
- Sistemas de cotización con descuentos por volumen
- Pricing engines con reglas complejas

---

## 📚 Recursos

- [C# Design Patterns](https://www.dofactory.com/net/strategy-design-pattern)
- [Microsoft Docs - Delegates](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/)
- [Func and Action in C#](https://www.c-sharpcorner.com/article/func-and-action-in-c-sharp/)
- [LINQ FirstOrDefault](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault)

---

## 🙏 Créditos

- **Profesor Bismarck Ponce** - Ejemplo de Strategy + Composite en motor de precios
- **Refactoring Guru** - Alexander Shvets
- **DoFactory**
- **Microsoft Learn**

---

[← Volver a Strategy](../README.md)
