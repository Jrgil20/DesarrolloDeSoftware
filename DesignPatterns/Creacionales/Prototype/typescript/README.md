# Prototype - Implementación en TypeScript

## 📖 Descripción

Referencias a implementaciones del patrón Prototype en TypeScript con clonación type-safe.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Prototype TypeScript**
- **Enlace**: [Prototype en TypeScript](https://refactoring.guru/design-patterns/prototype/typescript/example)

### 2. **torokmark/design_patterns_in_typescript**
- **Enlace**: [GitHub](https://github.com/torokmark/design_patterns_in_typescript)

---

## 💡 Ejemplo de Referencia

```typescript
interface Prototype {
    clone(): this;
}

class Shape implements Prototype {
    public x: number = 0;
    public y: number = 0;
    public color: string = "";
    
    constructor(source?: Shape) {
        if (source) {
            this.x = source.x;
            this.y = source.y;
            this.color = source.color;
        }
    }
    
    public clone(): this {
        return Object.create(this);
    }
}

class Circle extends Shape {
    public radius: number = 0;
    
    constructor(source?: Circle) {
        super(source);
        if (source) {
            this.radius = source.radius;
        }
    }
    
    public clone(): this {
        return new Circle(this) as this;
    }
}

// Prototype Registry
class ShapeRegistry {
    private cache = new Map<string, Shape>();
    
    public register(key: string, shape: Shape): void {
        this.cache.set(key, shape);
    }
    
    public getClone(key: string): Shape | undefined {
        const prototype = this.cache.get(key);
        return prototype ? prototype.clone() : undefined;
    }
}

// Uso
const registry = new ShapeRegistry();
const redCircle = new Circle();
redCircle.color = "red";
redCircle.radius = 10;

registry.register("red-circle", redCircle);

const clone1 = registry.getClone("red-circle");
const clone2 = registry.getClone("red-circle");

console.log(clone1 === clone2); // false (diferentes instancias)
```

---

## 🔧 Deep Clone con Structured Clone (Modern)

```typescript
// Navegadores modernos y Node.js 17+
function deepClone<T>(obj: T): T {
    return structuredClone(obj);
}

// Alternativa con JSON (limitaciones: no clona funciones, Date, etc.)
function jsonClone<T>(obj: T): T {
    return JSON.parse(JSON.stringify(obj));
}

// Uso
const original = { name: "John", age: 30, address: { city: "NYC" } };
const cloned = deepClone(original);

cloned.address.city = "LA";
console.log(original.address.city); // "NYC" (no afectado)
```

---

## 📚 Recursos

- [MDN - structuredClone](https://developer.mozilla.org/en-US/docs/Web/API/structuredClone)
- [TypeScript Deep Dive - Cloning](https://basarat.gitbook.io/typescript/main-1/typed-closures)

---

## 🙏 Créditos

- **Refactoring Guru** - Alexander Shvets
- **torokmark** - Mark Torok

---

[← Volver a Prototype](../README.md)

