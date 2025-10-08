# Decorator - Implementación en TypeScript

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Decorator TypeScript**
- **Enlace**: [Decorator](https://refactoring.guru/design-patterns/decorator/typescript/example)

---

## 💡 Ejemplo (React HOC)

```typescript
// Decorator pattern en React (Higher-Order Components)
interface Component {
    render(): string;
}

class TextComponent implements Component {
    constructor(private text: string) {}
    
    render(): string {
        return this.text;
    }
}

// Decorator base
abstract class ComponentDecorator implements Component {
    constructor(protected wrappee: Component) {}
    
    render(): string {
        return this.wrappee.render();
    }
}

// Concrete Decorators
class BoldDecorator extends ComponentDecorator {
    render(): string {
        return `<b>${super.render()}</b>`;
    }
}

class ItalicDecorator extends ComponentDecorator {
    render(): string {
        return `<i>${super.render()}</i>`;
    }
}

// Uso apilado
let component: Component = new TextComponent("Hello");
component = new BoldDecorator(component);
component = new ItalicDecorator(component);

console.log(component.render());  // <i><b>Hello</b></i>
```

---

## 🙏 Créditos
- **Refactoring Guru**

[← Volver](../README.md)
