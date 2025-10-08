# Builder - Implementación en Java

## 📖 Descripción

Referencias a implementaciones del patrón Builder en Java, demostrando construcción paso a paso de objetos complejos con múltiples configuraciones.

---

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Builder en java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/builder)
- **Ejemplo**: Hero builder con múltiples atributos opcionales

### 2. **Refactoring Guru - Builder Java**
- **Enlace**: [Builder en Refactoring Guru](https://refactoring.guru/design-patterns/builder/java/example)
- **Ejemplo**: Construction de casas y coches

### 3. **Baeldung - Builder Pattern**
- **Enlace**: [Baeldung Builder](https://www.baeldung.com/creational-design-patterns#builder)

---

## 💡 Ejemplo de Referencia

```java
// Product
public class Pizza {
    private final String dough;
    private final String sauce;
    private final String topping;
    
    private Pizza(Builder builder) {
        this.dough = builder.dough;
        this.sauce = builder.sauce;
        this.topping = builder.topping;
    }
    
    // Builder estático interno
    public static class Builder {
        // Campos obligatorios
        private final String dough;
        
        // Campos opcionales
        private String sauce = "tomato";
        private String topping = "cheese";
        
        public Builder(String dough) {
            this.dough = dough;
        }
        
        public Builder sauce(String sauce) {
            this.sauce = sauce;
            return this;
        }
        
        public Builder topping(String topping) {
            this.topping = topping;
            return this;
        }
        
        public Pizza build() {
            return new Pizza(this);
        }
    }
}

// Uso
Pizza pizza = new Pizza.Builder("thin")
                .sauce("pesto")
                .topping("mushrooms")
                .build();
```

---

## 🔧 Características Java

### 1. Lombok @Builder
```java
import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class User {
    private String name;
    private int age;
    private String email;
}

// Uso
User user = User.builder()
    .name("John")
    .age(30)
    .email("john@example.com")
    .build();
```

### 2. Con Validación
```java
public static class Builder {
    public Pizza build() {
        validate();
        return new Pizza(this);
    }
    
    private void validate() {
        if (dough == null || dough.isEmpty()) {
            throw new IllegalStateException("Dough is required");
        }
    }
}
```

---

## 📚 Recursos

- [Effective Java - Item 2: Consider a builder when faced with many constructor parameters](https://www.amazon.com/Effective-Java-Joshua-Bloch/dp/0134685997)
- [Baeldung - Builder Pattern](https://www.baeldung.com/creational-design-patterns#builder)
- [Project Lombok](https://projectlombok.org/)

---

## 🙏 Créditos

- **iluwatar/java-design-patterns** - Ilkka Seppälä
- **Joshua Bloch** - "Effective Java"
- **Refactoring Guru** - Alexander Shvets

---

[← Volver a Builder](../README.md)

