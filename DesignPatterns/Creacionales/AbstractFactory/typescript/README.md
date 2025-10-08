# Abstract Factory - Implementación en TypeScript

## 📖 Descripción

Esta carpeta contiene referencias a implementaciones de alta calidad del patrón Abstract Factory en TypeScript, demostrando cómo crear familias de objetos type-safe que garantizan compatibilidad.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Abstract Factory TypeScript**

- **Enlace**: [Abstract Factory en TypeScript](https://refactoring.guru/design-patterns/abstract-factory/typescript/example)
- **Características**:
  - ✅ Ejemplo de muebles (moderno vs victoriano)
  - ✅ Type safety completo
  - ✅ Código ejecutable en playground

### 2. **torokmark/design_patterns_in_typescript**

- **Enlace**: [Abstract Factory Implementation](https://github.com/torokmark/design_patterns_in_typescript)
- **Características**:
  - ✅ Implementación moderna
  - ✅ Tests incluidos
  - ✅ TypeScript best practices

### 3. **Patterns.dev**

- **Enlace**: [Abstract Factory Pattern](https://www.patterns.dev/posts/abstract-factory-pattern)
- **Características**:
  - ✅ Ejemplos interactivos
  - ✅ Aplicaciones modernas
  - ✅ React integration examples

---

## 💡 Ejemplo de Referencia Rápida

```typescript
// Basado en repositorios reconocidos

// Abstract Products
interface Button {
    render(): void;
    onClick(callback: () => void): void;
}

interface Checkbox {
    check(): void;
    isChecked(): boolean;
}

// Concrete Products - Windows Family
class WindowsButton implements Button {
    public render(): void {
        console.log("🪟 Renderizando botón Windows");
    }
    
    public onClick(callback: () => void): void {
        console.log("Click en botón Windows");
        callback();
    }
}

class WindowsCheckbox implements Checkbox {
    private checked: boolean = false;
    
    public check(): void {
        this.checked = !this.checked;
        console.log(`🪟 Checkbox Windows: ${this.checked ? "✓" : "○"}`);
    }
    
    public isChecked(): boolean {
        return this.checked;
    }
}

// Concrete Products - Mac Family
class MacButton implements Button {
    public render(): void {
        console.log("🍎 Renderizando botón Mac");
    }
    
    public onClick(callback: () => void): void {
        console.log("Click en botón Mac");
        callback();
    }
}

class MacCheckbox implements Checkbox {
    private checked: boolean = false;
    
    public check(): void {
        this.checked = !this.checked;
        console.log(`🍎 Checkbox Mac: ${this.checked ? "✓" : "○"}`);
    }
    
    public isChecked(): boolean {
        return this.checked;
    }
}

// Abstract Factory
interface GUIFactory {
    createButton(): Button;
    createCheckbox(): Checkbox;
}

// Concrete Factories
class WindowsFactory implements GUIFactory {
    public createButton(): Button {
        return new WindowsButton();
    }
    
    public createCheckbox(): Checkbox {
        return new WindowsCheckbox();
    }
}

class MacFactory implements GUIFactory {
    public createButton(): Button {
        return new MacButton();
    }
    
    public createCheckbox(): Checkbox {
        return new MacCheckbox();
    }
}

// Client
class Application {
    private button: Button;
    private checkbox: Checkbox;
    
    constructor(factory: GUIFactory) {
        this.button = factory.createButton();
        this.checkbox = factory.createCheckbox();
    }
    
    public render(): void {
        this.button.render();
        this.checkbox.check();
    }
}

// Configuration & Usage
function getFactory(platform: string): GUIFactory {
    switch (platform.toLowerCase()) {
        case "windows":
            return new WindowsFactory();
        case "mac":
            return new MacFactory();
        default:
            throw new Error(`Unknown platform: ${platform}`);
    }
}

// Main
const platform = process.platform === "darwin" ? "mac" : "windows";
const factory = getFactory(platform);
const app = new Application(factory);
app.render();
```

---

## 🔧 Características Específicas de TypeScript

### 1. **Generic Factories con Type Constraints**
```typescript
interface Product {
    operation(): void;
}

interface AbstractFactory<T extends Product, U extends Product> {
    createProductA(): T;
    createProductB(): U;
}

class ConcreteFactory implements AbstractFactory<ConcreteProductA, ConcreteProductB> {
    public createProductA(): ConcreteProductA {
        return new ConcreteProductA();
    }
    
    public createProductB(): ConcreteProductB {
        return new ConcreteProductB();
    }
}
```

### 2. **Union Types para Product Families**
```typescript
type WindowsProduct = WindowsButton | WindowsCheckbox | WindowsInput;
type MacProduct = MacButton | MacCheckbox | MacInput;

type UIProduct = WindowsProduct | MacProduct;

function renderProduct(product: UIProduct): void {
    if (product instanceof WindowsButton) {
        // TypeScript sabe que es WindowsButton
        product.render();
    }
}
```

### 3. **Type Guards para Factory Selection**
```typescript
type Platform = "windows" | "mac" | "linux";

function isPlatform(value: string): value is Platform {
    return ["windows", "mac", "linux"].includes(value);
}

function getFactory(platform: string): GUIFactory {
    if (!isPlatform(platform)) {
        throw new Error(`Invalid platform: ${platform}`);
    }
    
    const factories: Record<Platform, GUIFactory> = {
        windows: new WindowsFactory(),
        mac: new MacFactory(),
        linux: new LinuxFactory()
    };
    
    return factories[platform];
}
```

### 4. **Discriminated Unions para Product Types**
```typescript
interface BaseProduct {
    kind: string;
}

interface WindowsButton extends BaseProduct {
    kind: "windows-button";
    renderWindows(): void;
}

interface MacButton extends BaseProduct {
    kind: "mac-button";
    renderMac(): void;
}

type Button = WindowsButton | MacButton;

function render(button: Button): void {
    switch (button.kind) {
        case "windows-button":
            button.renderWindows();
            break;
        case "mac-button":
            button.renderMac();
            break;
        default:
            const _exhaustive: never = button;
            throw new Error(`Unknown button: ${_exhaustive}`);
    }
}
```

### 5. **Factory Registry con Map**
```typescript
type FactoryConstructor = new () => GUIFactory;

class FactoryRegistry {
    private static factories = new Map<string, FactoryConstructor>([
        ["windows", WindowsFactory],
        ["mac", MacFactory],
        ["linux", LinuxFactory]
    ]);
    
    public static register(name: string, factory: FactoryConstructor): void {
        this.factories.set(name, factory);
    }
    
    public static getFactory(name: string): GUIFactory {
        const FactoryClass = this.factories.get(name);
        if (!FactoryClass) {
            throw new Error(`Factory '${name}' not found`);
        }
        return new FactoryClass();
    }
    
    public static listFactories(): string[] {
        return Array.from(this.factories.keys());
    }
}
```

### 6. **Async Factory Pattern**
```typescript
interface AsyncGUIFactory {
    createButtonAsync(): Promise<Button>;
    createCheckboxAsync(): Promise<Checkbox>;
}

class RemoteFactory implements AsyncGUIFactory {
    constructor(private apiUrl: string) {}
    
    public async createButtonAsync(): Promise<Button> {
        const response = await fetch(`${this.apiUrl}/button-config`);
        const config = await response.json();
        return new ConfigurableButton(config);
    }
    
    public async createCheckboxAsync(): Promise<Checkbox> {
        const response = await fetch(`${this.apiUrl}/checkbox-config`);
        const config = await response.json();
        return new ConfigurableCheckbox(config);
    }
}

// Usage
async function createUI(factory: AsyncGUIFactory): Promise<void> {
    const [button, checkbox] = await Promise.all([
        factory.createButtonAsync(),
        factory.createCheckboxAsync()
    ]);
    
    button.render();
    checkbox.check();
}
```

### 7. **Branded Types para Product Families**
```typescript
type Brand<K, T> = K & { __brand: T };

type WindowsProduct = Brand<Product, "windows">;
type MacProduct = Brand<Product, "mac">;

interface Product {
    render(): void;
}

function createWindowsProduct(): WindowsProduct {
    return {
        render: () => console.log("Windows product"),
        __brand: "windows" as any
    };
}
```

---

## 📚 Recursos Adicionales

### Documentación TypeScript
- [TypeScript Handbook - Classes](https://www.typescriptlang.org/docs/handbook/2/classes.html)
- [TypeScript Handbook - Generics](https://www.typescriptlang.org/docs/handbook/2/generics.html)
- [Advanced Types](https://www.typescriptlang.org/docs/handbook/2/types-from-types.html)

### Artículos
- [Patterns.dev - Modern Patterns](https://www.patterns.dev/)
- [TypeScript Design Patterns](https://blog.bitsrc.io/design-patterns-in-typescript-e9f84de40449)
- [Enterprise TypeScript Patterns](https://khalilstemmler.com/articles/categories/enterprise-typescript/)

### Libros
- **"Programming TypeScript" por Boris Cherny**
- **"Effective TypeScript" por Dan Vanderkam**

---

## ⚙️ Configuración del Proyecto

### Estructura Recomendada
```
abstract-factory-ts/
├── src/
│   ├── index.ts
│   ├── interfaces/
│   │   ├── button.ts
│   │   ├── checkbox.ts
│   │   └── gui-factory.ts
│   ├── products/
│   │   ├── windows/
│   │   │   ├── windows-button.ts
│   │   │   └── windows-checkbox.ts
│   │   └── mac/
│   │       ├── mac-button.ts
│   │       └── mac-checkbox.ts
│   ├── factories/
│   │   ├── windows-factory.ts
│   │   └── mac-factory.ts
│   └── client/
│       └── application.ts
├── tests/
│   └── factories.test.ts
├── tsconfig.json
├── package.json
└── README.md
```

### tsconfig.json
```json
{
  "compilerOptions": {
    "target": "ES2022",
    "module": "NodeNext",
    "moduleResolution": "NodeNext",
    "lib": ["ES2022"],
    "outDir": "./dist",
    "rootDir": "./src",
    "strict": true,
    "esModuleInterop": true,
    "skipLibCheck": true,
    "forceConsistentCasingInFileNames": true,
    "resolveJsonModule": true,
    "declaration": true,
    "declarationMap": true,
    "sourceMap": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noImplicitReturns": true,
    "noFallthroughCasesInSwitch": true,
    "exactOptionalPropertyTypes": true,
    "noUncheckedIndexedAccess": true
  },
  "include": ["src/**/*"],
  "exclude": ["node_modules", "dist", "tests"]
}
```

### package.json
```json
{
  "name": "abstract-factory-pattern",
  "version": "1.0.0",
  "type": "module",
  "scripts": {
    "build": "tsc",
    "start": "node dist/index.js",
    "dev": "tsx watch src/index.ts",
    "test": "vitest",
    "test:ui": "vitest --ui",
    "lint": "eslint src/**/*.ts",
    "format": "prettier --write \"src/**/*.ts\""
  },
  "devDependencies": {
    "@types/node": "^20.10.0",
    "@typescript-eslint/eslint-plugin": "^6.13.0",
    "@typescript-eslint/parser": "^6.13.0",
    "eslint": "^8.55.0",
    "prettier": "^3.1.0",
    "tsx": "^4.7.0",
    "typescript": "^5.3.3",
    "vitest": "^1.0.4",
    "@vitest/ui": "^1.0.4"
  }
}
```

---

## 🎯 Mejores Prácticas

### 1. **Evitar `any`, usar tipos específicos**
```typescript
// ❌ Evitar
function createFactory(type: any): any {
    // ...
}

// ✅ Correcto
function createFactory(type: Platform): GUIFactory {
    // TypeScript garantiza type safety
}
```

### 2. **Usar `const assertions` para configuración**
```typescript
const FACTORIES = {
    windows: WindowsFactory,
    mac: MacFactory,
    linux: LinuxFactory
} as const;

type FactoryName = keyof typeof FACTORIES;

function getFactory(name: FactoryName): GUIFactory {
    return new FACTORIES[name]();
}
```

### 3. **Interfaces segregadas (ISP)**
```typescript
// En lugar de una interfaz grande
interface GUIFactory {
    createButton(): Button;
    createCheckbox(): Checkbox;
    createInput(): Input;
    createMenu(): Menu;
    // ...
}

// Dividir en interfaces más pequeñas
interface ButtonFactory {
    createButton(): Button;
}

interface CheckboxFactory {
    createCheckbox(): Checkbox;
}

interface GUIFactory extends ButtonFactory, CheckboxFactory {}
```

---

## 🧪 Tests (Vitest)

```typescript
import { describe, it, expect, beforeEach } from 'vitest';
import { WindowsFactory, MacFactory } from './factories';
import { Application } from './application';

describe('Abstract Factory Pattern', () => {
    describe('WindowsFactory', () => {
        it('should create Windows products', () => {
            const factory = new WindowsFactory();
            
            const button = factory.createButton();
            const checkbox = factory.createCheckbox();
            
            expect(button.constructor.name).toBe('WindowsButton');
            expect(checkbox.constructor.name).toBe('WindowsCheckbox');
        });
    });
    
    describe('MacFactory', () => {
        it('should create Mac products', () => {
            const factory = new MacFactory();
            
            const button = factory.createButton();
            const checkbox = factory.createCheckbox();
            
            expect(button.constructor.name).toBe('MacButton');
            expect(checkbox.constructor.name).toBe('MacCheckbox');
        });
    });
    
    describe('Application', () => {
        it('should work with any factory', () => {
            const factories = [new WindowsFactory(), new MacFactory()];
            
            factories.forEach(factory => {
                const app = new Application(factory);
                expect(() => app.render()).not.toThrow();
            });
        });
    });
    
    describe('Type Safety', () => {
        it('should enforce correct return types', () => {
            const factory = new WindowsFactory();
            const button = factory.createButton();
            
            // TypeScript ensures these methods exist
            expect(typeof button.render).toBe('function');
            expect(typeof button.onClick).toBe('function');
        });
    });
});
```

---

## 📝 Notas

- Ejemplos para **TypeScript 5+**
- Compatible con **Node.js 18+**
- Usar **ESLint** + **Prettier** para consistencia
- Considerar **Zod** para validación runtime

---

## 🙏 Créditos

Los ejemplos provienen de:

- **Refactoring Guru** - Alexander Shvets
  - [Sitio web](https://refactoring.guru)

- **torokmark/design_patterns_in_typescript** - Mark Torok
  - [GitHub](https://github.com/torokmark/design_patterns_in_typescript)

- **Patterns.dev**
  - [Sitio web](https://www.patterns.dev)

---

[← Volver a Abstract Factory](../README.md) | [📂 Ver todos los patrones creacionales](../../Creacionales.md)

