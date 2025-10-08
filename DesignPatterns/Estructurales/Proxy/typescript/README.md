# Proxy - Implementación en TypeScript

## 📖 Descripción

Referencias a implementaciones del patrón Proxy en TypeScript usando ES6 Proxy nativo y wrappers personalizados.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Proxy TypeScript**
- **Enlace**: [Proxy en TypeScript](https://refactoring.guru/design-patterns/proxy/typescript/example)

### 2. **torokmark/design_patterns_in_typescript**
- **Enlace**: [GitHub](https://github.com/torokmark/design_patterns_in_typescript)

### 3. **MDN - JavaScript Proxy**
- **Enlace**: [MDN Proxy](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Proxy)

---

## 💡 Tipos de Proxy Implementados

### 1. Virtual Proxy (Lazy Loading)

```typescript
// Subject Interface
interface Image {
    display(): void;
}

// Real Subject
class RealImage implements Image {
    constructor(private filename: string) {
        this.loadFromDisk();
    }
    
    private loadFromDisk(): void {
        console.log(`Loading image: ${this.filename}`);
        // Simular operación costosa
    }
    
    public display(): void {
        console.log(`Displaying image: ${this.filename}`);
    }
}

// Proxy
class ImageProxy implements Image {
    private realImage: RealImage | null = null;
    
    constructor(private filename: string) {}
    
    public display(): void {
        // Lazy initialization
        if (!this.realImage) {
            this.realImage = new RealImage(this.filename);
        }
        this.realImage.display();
    }
}

// Uso
const image = new ImageProxy("large_photo.jpg");
console.log("Proxy created"); // No carga aún ✅

image.display(); // Primera vez: carga
image.display(); // Segunda vez: usa objeto existente
```

### 2. ES6 Proxy Nativo (Muy Poderoso)

```typescript
// Proxy de validación
interface User {
    name: string;
    age: number;
    email: string;
}

const user: User = {
    name: "John",
    age: 30,
    email: "john@example.com"
};

// Proxy con validación
const userProxy = new Proxy(user, {
    set(target, property, value) {
        console.log(`Setting ${String(property)} to ${value}`);
        
        // Validación
        if (property === 'age') {
            if (typeof value !== 'number' || value < 0 || value > 150) {
                throw new Error('Invalid age');
            }
        }
        
        if (property === 'email') {
            if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
                throw new Error('Invalid email');
            }
        }
        
        target[property as keyof User] = value;
        return true;
    },
    
    get(target, property) {
        console.log(`Getting ${String(property)}`);
        return target[property as keyof User];
    }
});

// Uso
userProxy.age = 25;  // ✅ OK
console.log(userProxy.age); // ✅ OK

try {
    userProxy.age = 200; // ❌ Throws error
} catch (e) {
    console.error(e.message); // "Invalid age"
}
```

### 3. Caching Proxy

```typescript
class CachingProxy<T extends object> {
    private cache = new Map<string, { value: any; timestamp: number }>();
    private cacheDuration = 5000; // 5 segundos
    
    constructor(private target: T) {}
    
    public createProxy(): T {
        return new Proxy(this.target, {
            get: (target, property, receiver) => {
                const value = Reflect.get(target, property, receiver);
                
                // Solo cachear métodos
                if (typeof value === 'function') {
                    return (...args: any[]) => {
                        const cacheKey = `${String(property)}_${JSON.stringify(args)}`;
                        const cached = this.cache.get(cacheKey);
                        
                        if (cached && Date.now() - cached.timestamp < this.cacheDuration) {
                            console.log('💾 Cache hit');
                            return cached.value;
                        }
                        
                        console.log('🔍 Cache miss');
                        const result = value.apply(target, args);
                        
                        this.cache.set(cacheKey, {
                            value: result,
                            timestamp: Date.now()
                        });
                        
                        return result;
                    };
                }
                
                return value;
            }
        });
    }
}

// Uso
class DatabaseService {
    public getUser(id: number): User {
        console.log('Querying database...');
        return { id, name: 'John' };
    }
}

const dbService = new DatabaseService();
const cachingProxy = new CachingProxy(dbService);
const proxiedService = cachingProxy.createProxy();

proxiedService.getUser(1); // Cache miss - query DB
proxiedService.getUser(1); // Cache hit - instant
```

### 4. Protection Proxy

```typescript
interface SecureDocument {
    read(): string;
    write(content: string): void;
}

class RealDocument implements SecureDocument {
    private content: string = "Secret content";
    
    public read(): string {
        return this.content;
    }
    
    public write(content: string): void {
        this.content = content;
    }
}

class ProtectionProxy implements SecureDocument {
    constructor(
        private realDocument: RealDocument,
        private userRole: 'admin' | 'user' | 'guest'
    ) {}
    
    public read(): string {
        if (this.userRole === 'guest') {
            throw new Error('Access denied: guests cannot read');
        }
        console.log(`✓ Access granted for ${this.userRole}`);
        return this.realDocument.read();
    }
    
    public write(content: string): void {
        if (this.userRole !== 'admin') {
            throw new Error('Access denied: only admins can write');
        }
        console.log(`✓ Write access granted for ${this.userRole}`);
        this.realDocument.write(content);
    }
}

// Uso
const doc = new RealDocument();

const adminProxy = new ProtectionProxy(doc, 'admin');
adminProxy.write('New content'); // ✅ OK

const userProxy = new ProtectionProxy(doc, 'user');
const content = userProxy.read(); // ✅ OK
try {
    userProxy.write('Hack'); // ❌ Throws error
} catch (e) {
    console.error(e.message);
}
```

### 5. Logging Proxy con ES6 Proxy

```typescript
function createLoggingProxy<T extends object>(target: T, name: string): T {
    return new Proxy(target, {
        get(target, property, receiver) {
            const value = Reflect.get(target, property, receiver);
            
            if (typeof value === 'function') {
                return function(...args: any[]) {
                    console.log(`📞 [${name}] Calling ${String(property)}(${args.join(', ')})`);
                    const result = value.apply(target, args);
                    console.log(`✓ [${name}] ${String(property)} returned:`, result);
                    return result;
                };
            }
            
            return value;
        },
        
        set(target, property, value, receiver) {
            console.log(`✏️ [${name}] Setting ${String(property)} = ${value}`);
            return Reflect.set(target, property, value, receiver);
        }
    });
}

// Uso
class Calculator {
    public add(a: number, b: number): number {
        return a + b;
    }
}

const calc = new Calculator();
const proxy = createLoggingProxy(calc, 'Calculator');

proxy.add(5, 3);
// Output:
// 📞 [Calculator] Calling add(5, 3)
// ✓ [Calculator] add returned: 8
```

---

## 🔧 Características TypeScript

### Proxy con Type Safety
```typescript
type ProxyHandler<T> = {
    get?: (target: T, property: keyof T) => any;
    set?: (target: T, property: keyof T, value: any) => boolean;
};

function createTypedProxy<T extends object>(
    target: T, 
    handler: ProxyHandler<T>
): T {
    return new Proxy(target, handler as ProxyHandler<T>);
}
```

---

## 📚 Recursos

- [MDN - Proxy](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Proxy)
- [TypeScript Proxy Examples](https://www.typescriptlang.org/docs/handbook/2/classes.html)
- [JavaScript.info - Proxy](https://javascript.info/proxy)

---

## 🙏 Créditos

- **Refactoring Guru** - Alexander Shvets
- **torokmark** - Mark Torok
- **MDN Contributors**

---

[← Volver a Proxy](../README.md)
