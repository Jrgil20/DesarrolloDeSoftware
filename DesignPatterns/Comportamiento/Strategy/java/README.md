# Strategy - Implementación en Java

## 📖 Descripción

Referencias a implementaciones del patrón Strategy en Java, demostrando encapsulación de algoritmos intercambiables.

---

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Strategy en java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/strategy)
- **Ejemplo**: Dragon slaying strategies (MeleeStrategy, ProjectileStrategy, SpellStrategy)
- **Características**:
  - ✅ Implementación clara con lambdas Java 8+
  - ✅ Tests completos
  - ✅ Ejemplo del mundo real

### 2. **Refactoring Guru - Strategy Java**
- **Enlace**: [Strategy en Refactoring Guru](https://refactoring.guru/design-patterns/strategy/java/example)
- **Ejemplo**: Payment strategies

### 3. **Baeldung - Strategy Pattern**
- **Enlace**: [Baeldung Strategy](https://www.baeldung.com/java-strategy-pattern)

---

## 💡 Ejemplo de Referencia

```java
// Strategy Interface
@FunctionalInterface
public interface PaymentStrategy {
    void pay(double amount);
}

// Concrete Strategies
public class CreditCardStrategy implements PaymentStrategy {
    private String cardNumber;
    private String cvv;
    
    public CreditCardStrategy(String cardNumber, String cvv) {
        this.cardNumber = cardNumber;
        this.cvv = cvv;
    }
    
    @Override
    public void pay(double amount) {
        System.out.println("Paying $" + amount + " with credit card: " + 
                          cardNumber.substring(cardNumber.length() - 4));
    }
}

public class PayPalStrategy implements PaymentStrategy {
    private String email;
    
    public PayPalStrategy(String email) {
        this.email = email;
    }
    
    @Override
    public void pay(double amount) {
        System.out.println("Paying $" + amount + " via PayPal: " + email);
    }
}

// Context
public class ShoppingCart {
    private PaymentStrategy paymentStrategy;
    private double total = 0.0;
    
    public void setPaymentStrategy(PaymentStrategy strategy) {
        this.paymentStrategy = strategy;
    }
    
    public void checkout() {
        if (paymentStrategy == null) {
            throw new IllegalStateException("Payment strategy not set");
        }
        paymentStrategy.pay(total);
    }
    
    public void addItem(double price) {
        total += price;
    }
}

// Client con lambdas (Java 8+)
public class StrategyDemo {
    public static void main(String[] args) {
        ShoppingCart cart = new ShoppingCart();
        cart.addItem(49.99);
        cart.addItem(29.99);
        
        // Usando implementación tradicional
        cart.setPaymentStrategy(new CreditCardStrategy("1234-5678-9012-3456", "123"));
        cart.checkout();
        
        // Usando lambda (Java 8+)
        cart.setPaymentStrategy(amount -> 
            System.out.println("Paying $" + amount + " with cryptocurrency"));
        cart.checkout();
    }
}
```

---

## 🔧 Características Java

### 1. Lambdas y Functional Interfaces (Java 8+)
```java
@FunctionalInterface
interface SortStrategy {
    void sort(List<Integer> list);
}

// Uso con lambda
Sorter sorter = new Sorter();
sorter.setStrategy(Collections::sort);  // Method reference
sorter.setStrategy(list -> list.sort(Comparator.reverseOrder()));
```

### 2. Strategy con Enums
```java
public enum DiscountStrategy {
    NONE(price -> price),
    SEASONAL(price -> price * 0.9),
    LOYALTY(price -> price * 0.85),
    CLEARANCE(price -> price * 0.5);
    
    private final Function<Double, Double> calculation;
    
    DiscountStrategy(Function<Double, Double> calculation) {
        this.calculation = calculation;
    }
    
    public double apply(double price) {
        return calculation.apply(price);
    }
}

// Uso
double finalPrice = DiscountStrategy.SEASONAL.apply(100.0); // 90.0
```

### 3. Strategy Factory
```java
public class StrategyFactory {
    private static final Map<String, PaymentStrategy> strategies = Map.of(
        "credit", new CreditCardStrategy(),
        "paypal", new PayPalStrategy(),
        "crypto", new CryptoStrategy()
    );
    
    public static PaymentStrategy getStrategy(String type) {
        return strategies.getOrDefault(type, 
            () -> { throw new IllegalArgumentException("Unknown strategy: " + type); });
    }
}
```

---

## 📚 Recursos

- [Effective Java - Item 34: Use enums instead of int constants](https://www.amazon.com/Effective-Java-Joshua-Bloch/dp/0134685997)
- [Baeldung - Strategy Pattern](https://www.baeldung.com/java-strategy-pattern)
- [Java 8 Functional Interfaces](https://www.baeldung.com/java-8-functional-interfaces)

---

## 🙏 Créditos

- **iluwatar/java-design-patterns** - Ilkka Seppälä (MIT License)
- **Refactoring Guru** - Alexander Shvets
- **Baeldung**

---

[← Volver a Strategy](../README.md)
