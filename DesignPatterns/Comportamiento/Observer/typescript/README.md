# Observer - Implementación en TypeScript

## 🌟 Repositorios

### 1. **Refactoring Guru - Observer TypeScript**
- **Enlace**: [Observer](https://refactoring.guru/design-patterns/observer/typescript/example)

### 2. **RxJS**
- **Enlace**: [RxJS](https://rxjs.dev/)

---

## 💡 Ejemplo (EventEmitter Pattern)

```typescript
type EventCallback = (data: any) => void;

class EventEmitter {
    private events: Map<string, EventCallback[]> = new Map();
    
    on(event: string, callback: EventCallback): void {
        if (!this.events.has(event)) {
            this.events.set(event, []);
        }
        this.events.get(event)!.push(callback);
    }
    
    off(event: string, callback: EventCallback): void {
        const callbacks = this.events.get(event);
        if (callbacks) {
            const index = callbacks.indexOf(callback);
            if (index > -1) {
                callbacks.splice(index, 1);
            }
        }
    }
    
    emit(event: string, data?: any): void {
        const callbacks = this.events.get(event);
        if (callbacks) {
            callbacks.forEach(callback => callback(data));
        }
    }
}

// Uso
const emitter = new EventEmitter();

emitter.on('data', (data) => console.log('Observer 1:', data));
emitter.on('data', (data) => console.log('Observer 2:', data));

emitter.emit('data', { value: 42 });
```

---

## 🙏 Créditos
- **Refactoring Guru**
- **RxJS Team**

[← Volver](../README.md)
