# Chain of Responsibility - Implementación en TypeScript

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Chain TypeScript**
- **Enlace**: [Chain](https://refactoring.guru/design-patterns/chain-of-responsibility/typescript/example)

---

## 💡 Ejemplo (Express.js Middleware)

```typescript
// Express.js usa Chain of Responsibility
import express from 'express';

const app = express();

// Handler 1: Logging
app.use((req, res, next) => {
    console.log(`${req.method} ${req.path}`);
    next();  // Pasar al siguiente
});

// Handler 2: Authentication
app.use((req, res, next) => {
    if (!req.headers.authorization) {
        return res.status(401).send('Unauthorized');
    }
    next();
});

// Handler 3: Business Logic
app.get('/api/data', (req, res) => {
    res.json({ data: 'value' });
});

// Pattern implementation
interface Handler<T> {
    setNext(handler: Handler<T>): Handler<T>;
    handle(request: T): T | null;
}

abstract class BaseHandler<T> implements Handler<T> {
    private next: Handler<T> | null = null;
    
    setNext(handler: Handler<T>): Handler<T> {
        this.next = handler;
        return handler;
    }
    
    handle(request: T): T | null {
        if (this.next) {
            return this.next.handle(request);
        }
        return null;
    }
}
```

---

## 🙏 Créditos
- **Refactoring Guru**

[← Volver](../README.md)
