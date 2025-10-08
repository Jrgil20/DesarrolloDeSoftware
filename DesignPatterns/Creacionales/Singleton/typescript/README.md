# Singleton - Implementación en TypeScript

## 📖 Descripción

Referencias a implementaciones del patrón Singleton en TypeScript con type safety y mejores prácticas modernas.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Singleton TypeScript**
- **Enlace**: [Singleton en TypeScript](https://refactoring.guru/design-patterns/singleton/typescript/example)

### 2. **torokmark/design_patterns_in_typescript**
- **Enlace**: [GitHub](https://github.com/torokmark/design_patterns_in_typescript)

---

## 💡 Implementación

### Variante Básica
```typescript
class Singleton {
    private static instance: Singleton;
    
    private constructor() {
        // Constructor privado
    }
    
    public static getInstance(): Singleton {
        if (!Singleton.instance) {
            Singleton.instance = new Singleton();
        }
        return Singleton.instance;
    }
    
    public doSomething(): void {
        console.log("Doing something...");
    }
}

// Uso
const instance1 = Singleton.getInstance();
const instance2 = Singleton.getInstance();
console.log(instance1 === instance2); // true
```

### Con Generics
```typescript
abstract class Singleton<T> {
    private static instances: Map<any, any> = new Map();
    
    protected constructor() {
        const constructor = this.constructor;
        if (Singleton.instances.has(constructor)) {
            throw new Error(`Instance of ${constructor.name} already exists`);
        }
        Singleton.instances.set(constructor, this);
    }
    
    public static getInstance<T extends Singleton<any>>(
        this: new () => T
    ): T {
        if (!Singleton.instances.has(this)) {
            Singleton.instances.set(this, new this());
        }
        return Singleton.instances.get(this);
    }
}
```

### Module Pattern (Alternativa)
```typescript
// config.ts
class ConfigManager {
    private settings = new Map<string, any>();
    
    constructor() {
        this.loadSettings();
    }
    
    private loadSettings(): void {
        // Cargar configuración
    }
    
    public get(key: string): any {
        return this.settings.get(key);
    }
    
    public set(key: string, value: any): void {
        this.settings.set(key, value);
    }
}

// Exportar instancia única
export const config = new ConfigManager();

// Uso en otros archivos
import { config } from './config';
config.set('theme', 'dark');
```

---

## ⚠️ Alternativas Modernas

### Dependency Injection con InversifyJS
```typescript
import { injectable, inject, Container } from "inversify";

@injectable()
class DatabaseService {
    constructor() {
        console.log("DatabaseService created");
    }
}

const container = new Container();
container.bind<DatabaseService>(DatabaseService).toSelf().inSingletonScope();

// Siempre retorna la misma instancia
const db1 = container.get(DatabaseService);
const db2 = container.get(DatabaseService);
console.log(db1 === db2); // true
```

---

## 📚 Recursos

- [TypeScript Handbook](https://www.typescriptlang.org/docs/handbook/intro.html)
- [Patterns.dev - Singleton](https://www.patterns.dev/posts/singleton-pattern/)
- [InversifyJS Documentation](https://inversify.io/)

---

## 🙏 Créditos

- **Refactoring Guru** - Alexander Shvets
- **torokmark** - Mark Torok
- **Patterns.dev** - Addy Osmani & Lydia Hallie

---

[← Volver a Singleton](../README.md)

