# Builder - Implementación en TypeScript

## 📖 Descripción

Referencias a implementaciones del patrón Builder en TypeScript con type safety y method chaining.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Builder TypeScript**
- **Enlace**: [Builder en TypeScript](https://refactoring.guru/design-patterns/builder/typescript/example)

### 2. **torokmark/design_patterns_in_typescript**
- **Enlace**: [GitHub](https://github.com/torokmark/design_patterns_in_typescript)

---

## 💡 Ejemplo de Referencia

```typescript
interface User {
    name: string;
    email: string;
    age?: number;
    phone?: string;
}

class UserBuilder {
    private user: Partial<User> = {};
    
    public withName(name: string): this {
        this.user.name = name;
        return this;
    }
    
    public withEmail(email: string): this {
        this.user.email = email;
        return this;
    }
    
    public withAge(age: number): this {
        this.user.age = age;
        return this;
    }
    
    public withPhone(phone: string): this {
        this.user.phone = phone;
        return this;
    }
    
    public build(): User {
        if (!this.user.name || !this.user.email) {
            throw new Error("Name and email are required");
        }
        return this.user as User;
    }
}

// Uso
const user = new UserBuilder()
    .withName("John Doe")
    .withEmail("john@example.com")
    .withAge(30)
    .build();
```

---

## 🔧 Con Type Safety Avanzado

```typescript
// Builder que fuerza campos obligatorios
type RequiredFields = "name" | "email";
type OptionalFields = "age" | "phone";

class TypeSafeUserBuilder {
    private user: Pick<User, RequiredFields> & Partial<Pick<User, OptionalFields>>;
    
    constructor(name: string, email: string) {
        this.user = { name, email };
    }
    
    public withAge(age: number): this {
        this.user.age = age;
        return this;
    }
    
    public withPhone(phone: string): this {
        this.user.phone = phone;
        return this;
    }
    
    public build(): User {
        return this.user as User;
    }
}

// Uso - name y email son obligatorios
const user = new TypeSafeUserBuilder("John", "john@example.com")
    .withAge(30)
    .build();
```

---

## 📚 Recursos

- [TypeScript Handbook - Classes](https://www.typescriptlang.org/docs/handbook/2/classes.html)
- [Fluent API in TypeScript](https://blog.logrocket.com/fluent-interfaces-typescript/)

---

## 🙏 Créditos

- **Refactoring Guru** - Alexander Shvets
- **torokmark** - Mark Torok

---

[← Volver a Builder](../README.md)

