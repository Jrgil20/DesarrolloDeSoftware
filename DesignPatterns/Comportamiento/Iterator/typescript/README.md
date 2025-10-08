# Iterator - Implementación en TypeScript

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Iterator TypeScript**
- **Enlace**: [Iterator](https://refactoring.guru/design-patterns/iterator/typescript/example)

---

## 💡 Ejemplo (Symbol.iterator + Generator)

```typescript
// TypeScript usa Symbol.iterator
class Range implements Iterable<number> {
    constructor(
        private start: number,
        private end: number
    ) {}
    
    // Iterator con Symbol.iterator
    [Symbol.iterator](): Iterator<number> {
        let current = this.start;
        const end = this.end;
        
        return {
            next(): IteratorResult<number> {
                if (current <= end) {
                    return {
                        value: current++,
                        done: false
                    };
                }
                return {
                    value: null,
                    done: true
                };
            }
        };
    }
}

// Uso con for...of
const range = new Range(1, 5);
for (const num of range) {
    console.log(num);  // 1, 2, 3, 4, 5
}

// Con Generator (más simple)
class RangeGenerator implements Iterable<number> {
    constructor(private start: number, private end: number) {}
    
    *[Symbol.iterator]() {
        for (let i = this.start; i <= this.end; i++) {
            yield i;
        }
    }
}

// Uso
for (const num of new RangeGenerator(1, 5)) {
    console.log(num);
}
```

---

## 🙏 Créditos
- **Refactoring Guru**
- **MDN Web Docs**

[← Volver](../README.md)
