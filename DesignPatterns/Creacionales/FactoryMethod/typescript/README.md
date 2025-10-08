# Factory Method - Implementación en TypeScript

## 📖 Descripción

Esta carpeta contiene referencias y enlaces a implementaciones de alta calidad del patrón Factory Method en TypeScript, seleccionadas de repositorios reconocidos con excelentes prácticas de programación type-safe.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Factory Method TypeScript**

**Recurso educativo líder mundial**

- **Enlace**: [Factory Method en TypeScript](https://refactoring.guru/design-patterns/factory-method/typescript/example)
- **Características**:
  - ✅ Código TypeScript puro y type-safe
  - ✅ Ejemplos descargables
  - ✅ Playground interactivo
  - ✅ Explicaciones detalladas con diagramas

---

### 2. **torokmark/design_patterns_in_typescript** (⭐ 5,000+)

**Colección completa de patrones en TypeScript**

- **Enlace**: [Factory Method Implementation](https://github.com/torokmark/design_patterns_in_typescript)
- **Autor**: Mark Torok
- **Características**:
  - ✅ Implementaciones limpias y modernas
  - ✅ Tests incluidos
  - ✅ Documentación clara
  - ✅ TypeScript features aprovechadas

---

### 3. **Thxmxs/Patrones-de-diseno-TypeScript**

**Patrones de diseño con ejemplos prácticos en español**

- **Enlace**: [Repositorio GitHub](https://github.com/Thxmxs/Patrones-de-diseno-TypeScript)
- **Características**:
  - ✅ Documentación en español
  - ✅ Diagramas de clases
  - ✅ Ejemplos didácticos

---

### 4. **typescript-design-patterns.com**

- **Enlace**: [Factory Method Pattern](https://sbcode.net/typescript/factory/)
- **Características**:
  - ✅ Tutoriales paso a paso
  - ✅ Videos explicativos
  - ✅ Código ejecutable

---

## 💡 Ejemplo Simple (Referencia rápida)

```typescript
// Basado en los repositorios mencionados con TypeScript best practices

// Product Interface
interface Vehicle {
    deliver(): string;
}

// Concrete Products
class Truck implements Vehicle {
    public deliver(): string {
        return "Entrega por tierra en un camión 🚚";
    }
}

class Ship implements Vehicle {
    public deliver(): string {
        return "Entrega por mar en un barco 🚢";
    }
}

class Plane implements Vehicle {
    public deliver(): string {
        return "Entrega por aire en un avión ✈️";
    }
}

// Creator Abstract Class
abstract class Logistics {
    // Factory Method
    public abstract createVehicle(): Vehicle;
    
    // Template method que usa el factory method
    public planDelivery(): string {
        const vehicle = this.createVehicle();
        return `Logística planificada: ${vehicle.deliver()}`;
    }
}

// Concrete Creators
class RoadLogistics extends Logistics {
    public createVehicle(): Vehicle {
        return new Truck();
    }
}

class SeaLogistics extends Logistics {
    public createVehicle(): Vehicle {
        return new Ship();
    }
}

class AirLogistics extends Logistics {
    public createVehicle(): Vehicle {
        return new Plane();
    }
}

// Client Code
function clientCode(logistics: Logistics): void {
    console.log(logistics.planDelivery());
}

// Usage
console.log("🚀 Aplicación iniciada");
clientCode(new RoadLogistics());
clientCode(new SeaLogistics());
clientCode(new AirLogistics());
```

---

## 🔧 Características Específicas de TypeScript

### 1. **Interfaces vs Type Aliases**
```typescript
// Interface (preferida para contratos extensibles)
interface Product {
    operation(): void;
}

// Type Alias (para tipos más complejos)
type ProductFactory = () => Product;

// Extending interfaces
interface AdvancedProduct extends Product {
    advancedOperation(): void;
}
```

### 2. **Abstract Classes con Métodos Concretos**
```typescript
abstract class Creator {
    // Factory method abstracto
    public abstract factoryMethod(): Product;
    
    // Método concreto que usa el factory
    public someOperation(): string {
        const product = this.factoryMethod();
        return `Creator: Trabajando con ${product.operation()}`;
    }
}
```

### 3. **Generics para Type Safety**
```typescript
abstract class GenericCreator<T extends Product> {
    public abstract create(): T;
    
    public use(): void {
        const product = this.create();
        product.operation();
    }
}

class ConcreteCreator extends GenericCreator<ConcreteProduct> {
    public create(): ConcreteProduct {
        return new ConcreteProduct();
    }
}
```

### 4. **Union Types y Type Guards**
```typescript
type VehicleType = "truck" | "ship" | "plane";

class VehicleFactory {
    public createVehicle(type: VehicleType): Vehicle {
        switch (type) {
            case "truck":
                return new Truck();
            case "ship":
                return new Ship();
            case "plane":
                return new Plane();
            default:
                // TypeScript exhaustiveness checking
                const _exhaustive: never = type;
                throw new Error(`Unknown vehicle type: ${_exhaustive}`);
        }
    }
}
```

### 5. **Readonly Properties**
```typescript
class Product {
    public readonly name: string;
    public readonly price: number;
    
    constructor(name: string, price: number) {
        this.name = name;
        this.price = price;
    }
}

// O con parameter properties
class Product {
    constructor(
        public readonly name: string,
        public readonly price: number
    ) {}
}
```

### 6. **Utility Types**
```typescript
// Partial para factories que crean objetos configurables
type ProductConfig = {
    name: string;
    price: number;
    category: string;
};

class ConfigurableProductFactory {
    public create(config: Partial<ProductConfig>): Product {
        return new Product({
            name: config.name ?? "Default",
            price: config.price ?? 0,
            category: config.category ?? "General"
        });
    }
}
```

### 7. **Decorators (Experimental)**
```typescript
// tsconfig.json: "experimentalDecorators": true

function LogCreation(target: any, propertyKey: string, descriptor: PropertyDescriptor) {
    const originalMethod = descriptor.value;
    descriptor.value = function(...args: any[]) {
        console.log(`Creating product via ${propertyKey}`);
        return originalMethod.apply(this, args);
    };
}

class ConcreteCreator extends Creator {
    @LogCreation
    public factoryMethod(): Product {
        return new ConcreteProduct();
    }
}
```

### 8. **Async/Await Factory Methods**
```typescript
abstract class AsyncCreator {
    public abstract createAsync(): Promise<Product>;
    
    public async processAsync(): Promise<void> {
        const product = await this.createAsync();
        product.operation();
    }
}

class DatabaseProductCreator extends AsyncCreator {
    public async createAsync(): Promise<Product> {
        const data = await fetch('/api/product');
        const json = await data.json();
        return new Product(json);
    }
}
```

---

## 📚 Recursos Adicionales

### Documentación Oficial
- [TypeScript Handbook](https://www.typescriptlang.org/docs/handbook/intro.html)
- [TypeScript Classes](https://www.typescriptlang.org/docs/handbook/2/classes.html)
- [TypeScript Generics](https://www.typescriptlang.org/docs/handbook/2/generics.html)

### Libros
- **"Programming TypeScript" por Boris Cherny**
- **"Effective TypeScript" por Dan Vanderkam**
- **"TypeScript Quickly" por Yakov Fain y Anton Moiseev**

### Cursos en Línea
- [TypeScript Deep Dive (Free Book)](https://basarat.gitbook.io/typescript/)
- [Total TypeScript by Matt Pocock](https://www.totaltypescript.com/)
- [Execute Program - TypeScript](https://www.executeprogram.com/courses/typescript)

### Artículos y Tutoriales
- [TypeScript Design Patterns](https://dev.to/alexsergey/typescript-design-patterns-5cn9)
- [Patterns.dev - TypeScript Patterns](https://www.patterns.dev/)
- [refactoringguru.cn](https://refactoringguru.cn/design-patterns/typescript)

---

## ⚙️ Configuración del Proyecto

### Estructura Recomendada
```
factory-method-ts/
├── src/
│   ├── index.ts           # Punto de entrada
│   ├── interfaces/
│   │   └── product.ts     # Interfaces de productos
│   ├── products/
│   │   ├── truck.ts
│   │   ├── ship.ts
│   │   └── plane.ts
│   ├── creators/
│   │   ├── logistics.ts   # Creator abstracto
│   │   ├── road-logistics.ts
│   │   ├── sea-logistics.ts
│   │   └── air-logistics.ts
│   └── client.ts
├── tests/
│   └── logistics.test.ts
├── tsconfig.json
├── package.json
└── README.md
```

### tsconfig.json
```json
{
  "compilerOptions": {
    "target": "ES2020",
    "module": "commonjs",
    "lib": ["ES2020"],
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
    "noFallthroughCasesInSwitch": true
  },
  "include": ["src/**/*"],
  "exclude": ["node_modules", "dist", "tests"]
}
```

### package.json
```json
{
  "name": "factory-method-pattern",
  "version": "1.0.0",
  "description": "Factory Method Pattern implementation in TypeScript",
  "main": "dist/index.js",
  "types": "dist/index.d.ts",
  "scripts": {
    "build": "tsc",
    "start": "node dist/index.js",
    "dev": "ts-node src/index.ts",
    "watch": "tsc --watch",
    "test": "jest",
    "lint": "eslint src/**/*.ts",
    "format": "prettier --write \"src/**/*.ts\""
  },
  "devDependencies": {
    "@types/node": "^20.0.0",
    "@types/jest": "^29.0.0",
    "@typescript-eslint/eslint-plugin": "^6.0.0",
    "@typescript-eslint/parser": "^6.0.0",
    "eslint": "^8.0.0",
    "jest": "^29.0.0",
    "prettier": "^3.0.0",
    "ts-jest": "^29.0.0",
    "ts-node": "^10.0.0",
    "typescript": "^5.0.0"
  }
}
```

### Instalación y Ejecución

```bash
# Instalar dependencias
npm install

# Compilar TypeScript
npm run build

# Ejecutar
npm start

# Modo desarrollo (con ts-node)
npm run dev

# Tests
npm test

# Lint
npm run lint
```

---

## 🎯 Mejores Prácticas en TypeScript

### 1. **Usar `strict` Mode**
```json
// tsconfig.json
{
  "compilerOptions": {
    "strict": true
  }
}
```

### 2. **Evitar `any`, usar `unknown`**
```typescript
// ❌ Evitar
function process(data: any) { }

// ✅ Mejor
function process(data: unknown) {
    if (typeof data === 'string') {
        // TypeScript sabe que data es string aquí
        console.log(data.toUpperCase());
    }
}
```

### 3. **Usar Type Guards**
```typescript
function isProduct(obj: any): obj is Product {
    return obj && typeof obj.operation === 'function';
}

function useProduct(obj: unknown): void {
    if (isProduct(obj)) {
        obj.operation(); // Type-safe
    }
}
```

### 4. **Naming Conventions**
```typescript
// Interfaces sin prefijo 'I' (recomendación moderna)
interface Product { }  // ✅
interface IProduct { } // ❌ Anticuado

// PascalCase para clases e interfaces
class ConcreteProduct { }

// camelCase para métodos y variables
public createProduct(): Product { }
```

### 5. **Usar `const assertions`**
```typescript
const vehicleTypes = ["truck", "ship", "plane"] as const;
type VehicleType = typeof vehicleTypes[number]; // "truck" | "ship" | "plane"
```

---

## 🧪 Ejemplo de Tests (Jest + TypeScript)

```typescript
// logistics.test.ts
import { RoadLogistics, SeaLogistics, AirLogistics } from './logistics';
import { Truck, Ship, Plane } from './vehicles';

describe('Factory Method Pattern', () => {
    describe('RoadLogistics', () => {
        it('should create a Truck', () => {
            const logistics = new RoadLogistics();
            const vehicle = logistics.createVehicle();
            expect(vehicle).toBeInstanceOf(Truck);
        });
        
        it('should plan delivery correctly', () => {
            const logistics = new RoadLogistics();
            const result = logistics.planDelivery();
            expect(result).toContain('camión');
        });
    });
    
    describe('SeaLogistics', () => {
        it('should create a Ship', () => {
            const logistics = new SeaLogistics();
            const vehicle = logistics.createVehicle();
            expect(vehicle).toBeInstanceOf(Ship);
        });
    });
    
    describe('Type Safety', () => {
        it('should enforce correct types at compile time', () => {
            const logistics = new RoadLogistics();
            const vehicle = logistics.createVehicle();
            
            // TypeScript ensures 'deliver' exists
            const message: string = vehicle.deliver();
            expect(typeof message).toBe('string');
        });
    });
});
```

**jest.config.js**:
```javascript
module.exports = {
    preset: 'ts-jest',
    testEnvironment: 'node',
    roots: ['<rootDir>/src', '<rootDir>/tests'],
    testMatch: ['**/__tests__/**/*.ts', '**/?(*.)+(spec|test).ts'],
    transform: {
        '^.+\\.ts$': 'ts-jest',
    },
};
```

---

## 📝 Notas

- Ejemplos optimizados para **TypeScript 5+**
- Compatible con **Node.js 18+** y navegadores modernos
- Se recomienda usar **ESLint** y **Prettier** para consistencia de código
- Considerar **Zod** o **io-ts** para validación de tipos en runtime

---

## 🙏 Créditos

Los ejemplos y referencias provienen de:

- **Refactoring Guru** - Alexander Shvets
  - [Sitio web](https://refactoring.guru)
  - Ejemplos de alta calidad en TypeScript

- **torokmark/design_patterns_in_typescript** - Mark Torok
  - [Repositorio GitHub](https://github.com/torokmark/design_patterns_in_typescript)
  - Licencia: MIT

- **TypeScript Documentation** - Microsoft
  - [Documentación oficial](https://www.typescriptlang.org/)

Se agradece a los autores y la comunidad TypeScript por sus contribuciones.

---

[← Volver a Factory Method](../README.md) | [📂 Ver todos los patrones creacionales](../../Creacionales.md)

