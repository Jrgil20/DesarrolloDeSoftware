# Memento - TypeScript

## 🌟 Repositorios
- **[Refactoring Guru](https://refactoring.guru/design-patterns/memento/typescript/example)**

## 💡 Ejemplo
```typescript
class EditorMemento {
    constructor(private readonly state: string) {}
    getState(): string { return this.state; }
}

class Editor {
    private text: string = "";
    
    save(): EditorMemento {
        return new EditorMemento(this.text);
    }
    
    restore(memento: EditorMemento): void {
        this.text = memento.getState();
    }
}
```

[← Volver](../README.md)
