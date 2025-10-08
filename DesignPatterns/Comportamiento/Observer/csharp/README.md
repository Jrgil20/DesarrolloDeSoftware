# Observer - Implementación en C#

## 🌟 Repositorios

### 1. **Refactoring Guru - Observer C#**
- **Enlace**: [Observer](https://refactoring.guru/design-patterns/observer/csharp/example)

---

## 💡 Ejemplo (C# Events)

```csharp
// C# tiene soporte nativo para Observer con events
public class Stock
{
    private decimal _price;
    
    // Event (Observer pattern nativo)
    public event EventHandler<PriceChangedEventArgs> PriceChanged;
    
    public decimal Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                _price = value;
                OnPriceChanged(new PriceChangedEventArgs(value));
            }
        }
    }
    
    protected virtual void OnPriceChanged(PriceChangedEventArgs e)
    {
        PriceChanged?.Invoke(this, e);
    }
}

public class PriceChangedEventArgs : EventArgs
{
    public decimal NewPrice { get; }
    public PriceChangedEventArgs(decimal price) => NewPrice = price;
}

// Uso
var stock = new Stock();

// Suscribirse
stock.PriceChanged += (sender, e) => 
    Console.WriteLine($"Price changed to: ${e.NewPrice}");

stock.Price = 100m;  // Dispara evento
```

---

## 🙏 Créditos
- **Microsoft - C# Events**
- **Refactoring Guru**

[← Volver](../README.md)
