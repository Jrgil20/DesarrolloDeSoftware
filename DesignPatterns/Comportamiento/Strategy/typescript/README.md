# Strategy - Implementación en TypeScript

## 📖 Descripción

Referencias a implementaciones del patrón Strategy en TypeScript con type safety y funciones de primera clase.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Strategy TypeScript**
- **Enlace**: [Strategy en TypeScript](https://refactoring.guru/design-patterns/strategy/typescript/example)

### 2. **torokmark/design_patterns_in_typescript**
- **Enlace**: [GitHub](https://github.com/torokmark/design_patterns_in_typescript)

---

## 💡 Ejemplo de Referencia

```typescript
// Strategy Interface
interface PaymentStrategy {
    pay(amount: number): void;
}

// Concrete Strategies
class CreditCardStrategy implements PaymentStrategy {
    constructor(private cardNumber: string, private cvv: string) {}
    
    public pay(amount: number): void {
        console.log(`Paying $${amount} with credit card ending in ${this.cardNumber.slice(-4)}`);
    }
}

class PayPalStrategy implements PaymentStrategy {
    constructor(private email: string) {}
    
    public pay(amount: number): void {
        console.log(`Paying $${amount} via PayPal: ${this.email}`);
    }
}

class CryptoStrategy implements PaymentStrategy {
    constructor(private walletAddress: string) {}
    
    public pay(amount: number): void {
        console.log(`Paying $${amount} with crypto wallet: ${this.walletAddress}`);
    }
}

// Context
class ShoppingCart {
    private strategy: PaymentStrategy | null = null;
    private total: number = 0;
    
    public setPaymentStrategy(strategy: PaymentStrategy): void {
        this.strategy = strategy;
    }
    
    public checkout(): void {
        if (!this.strategy) {
            throw new Error('Payment strategy not set');
        }
        this.strategy.pay(this.total);
    }
    
    public addItem(price: number): void {
        this.total += price;
    }
}

// Client
const cart = new ShoppingCart();
cart.addItem(49.99);
cart.addItem(29.99);

// Opción 1: Tarjeta de crédito
cart.setPaymentStrategy(new CreditCardStrategy('1234-5678-9012-3456', '123'));
cart.checkout();

// Opción 2: PayPal
cart.setPaymentStrategy(new PayPalStrategy('user@example.com'));
cart.checkout();

// Opción 3: Crypto
cart.setPaymentStrategy(new CryptoStrategy('0x742d35Cc6634C0532925a3b844Bc9e7595f0bEb'));
cart.checkout();
```

---

## 🔧 Características TypeScript

### 1. Function Types (Estrategias como funciones)
```typescript
type SortStrategy<T> = (arr: T[]) => T[];

class Sorter<T> {
    private strategy: SortStrategy<T>;
    
    constructor(strategy: SortStrategy<T>) {
        this.strategy = strategy;
    }
    
    public setStrategy(strategy: SortStrategy<T>): void {
        this.strategy = strategy;
    }
    
    public sort(arr: T[]): T[] {
        return this.strategy(arr);
    }
}

// Uso con arrow functions
const numSorter = new Sorter<number>((arr) => [...arr].sort((a, b) => a - b));
const result = numSorter.sort([3, 1, 4, 1, 5]);

// Cambiar estrategia
numSorter.setStrategy((arr) => [...arr].sort((a, b) => b - a)); // Descendente
```

### 2. Generics con Constraints
```typescript
interface Validator<T> {
    validate(value: T): boolean;
}

class EmailValidator implements Validator<string> {
    public validate(email: string): boolean {
        return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
    }
}

class FormField<T> {
    private validator: Validator<T>;
    
    constructor(private value: T, validator: Validator<T>) {
        this.validator = validator;
    }
    
    public setValidator(validator: Validator<T>): void {
        this.validator = validator;
    }
    
    public isValid(): boolean {
        return this.validator.validate(this.value);
    }
}

// Uso
const emailField = new FormField('user@example.com', new EmailValidator());
console.log(emailField.isValid()); // true
```

### 3. Strategy con Union Types
```typescript
type CompressionStrategy = 'zip' | 'gzip' | 'brotli';

class FileCompressor {
    private strategies: Record<CompressionStrategy, (data: Buffer) => Buffer> = {
        zip: (data) => this.compressZip(data),
        gzip: (data) => this.compressGzip(data),
        brotli: (data) => this.compressBrotli(data)
    };
    
    public compress(data: Buffer, strategy: CompressionStrategy): Buffer {
        return this.strategies[strategy](data);
    }
    
    private compressZip(data: Buffer): Buffer {
        // Implementación ZIP
        return data;
    }
    
    private compressGzip(data: Buffer): Buffer {
        // Implementación GZIP
        return data;
    }
    
    private compressBrotli(data: Buffer): Buffer {
        // Implementación Brotli
        return data;
    }
}
```

### 4. Strategy con Decoradores
```typescript
type DiscountStrategy = (price: number) => number;

function LogDiscount(strategy: DiscountStrategy): DiscountStrategy {
    return function(price: number): number {
        const finalPrice = strategy(price);
        console.log(`Original: $${price}, Final: $${finalPrice}`);
        return finalPrice;
    };
}

const seasonalDiscount: DiscountStrategy = (price) => price * 0.9;
const loyaltyDiscount: DiscountStrategy = (price) => price * 0.85;

const loggedSeasonalDiscount = LogDiscount(seasonalDiscount);
loggedSeasonalDiscount(100); // Logs: Original: $100, Final: $90
```

---

## 📚 Recursos

- [TypeScript Handbook - Advanced Types](https://www.typescriptlang.org/docs/handbook/2/types-from-types.html)
- [Patterns.dev - Strategy Pattern](https://www.patterns.dev/posts/strategy-pattern/)
- [Functional Programming in TypeScript](https://github.com/gcanti/fp-ts)

---

## 🙏 Créditos

- **Refactoring Guru** - Alexander Shvets
- **torokmark** - Mark Torok
- **Patterns.dev** - Addy Osmani

---

[← Volver a Strategy](../README.md)
