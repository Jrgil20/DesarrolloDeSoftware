# Command - TypeScript

## 🌟 Repositorios
- **[Refactoring Guru](https://refactoring.guru/design-patterns/command/typescript/example)**

## 💡 Ejemplo
```typescript
interface Command {
    execute(): void;
    undo(): void;
}

class LightOnCommand implements Command {
    constructor(private light: Light) {}
    
    execute(): void {
        this.light.turnOn();
    }
    
    undo(): void {
        this.light.turnOff();
    }
}
```

[← Volver](../README.md)
