# Prototype - Implementación en Java

## 📖 Descripción

Referencias a implementaciones del patrón Prototype en Java, demostrando clonación de objetos con manejo de shallow y deep copy.

---

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Prototype en java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/prototype)
- **Ejemplo**: Clonación de bestias (Beast, Mage, Warlord)

### 2. **Refactoring Guru - Prototype Java**
- **Enlace**: [Prototype en Refactoring Guru](https://refactoring.guru/design-patterns/prototype/java/example)
- **Ejemplo**: Clonación de formas geométricas

---

## 💡 Ejemplo de Referencia

```java
// Implementación con Cloneable
public abstract class Shape implements Cloneable {
    public int x;
    public int y;
    public String color;
    
    public Shape() {}
    
    public Shape(Shape source) {
        if (source != null) {
            this.x = source.x;
            this.y = source.y;
            this.color = source.color;
        }
    }
    
    @Override
    public abstract Shape clone();
}

public class Circle extends Shape {
    public int radius;
    
    public Circle() {}
    
    public Circle(Circle source) {
        super(source);
        if (source != null) {
            this.radius = source.radius;
        }
    }
    
    @Override
    public Circle clone() {
        return new Circle(this);
    }
}

// Prototype Registry
public class ShapeCache {
    private static Map<String, Shape> cache = new HashMap<>();
    
    public static Shape getShape(String shapeId) {
        Shape cachedShape = cache.get(shapeId);
        return cachedShape != null ? cachedShape.clone() : null;
    }
    
    public static void loadCache() {
        Circle circle = new Circle();
        circle.x = 5;
        circle.y = 7;
        circle.radius = 10;
        circle.color = "red";
        cache.put("1", circle);
    }
}

// Uso
ShapeCache.loadCache();
Shape clonedCircle = ShapeCache.getShape("1");
```

---

## 🔧 Deep Copy vs Shallow Copy

### Shallow Copy
```java
public class Address {
    public String city;
    public String country;
}

public class Person implements Cloneable {
    public String name;
    public Address address;  // Referencia compartida!
    
    @Override
    protected Object clone() throws CloneNotSupportedException {
        return super.clone(); // Shallow copy
    }
}
```

### Deep Copy
```java
public class Person implements Cloneable {
    public String name;
    public Address address;
    
    @Override
    public Person clone() {
        Person cloned = new Person();
        cloned.name = this.name;
        cloned.address = new Address();  // Nueva instancia
        cloned.address.city = this.address.city;
        cloned.address.country = this.address.country;
        return cloned;
    }
}
```

---

## 📚 Recursos

- [Baeldung - Prototype Pattern](https://www.baeldung.com/java-pattern-prototype)
- [Java Cloning - Joshua Bloch "Effective Java"](https://www.amazon.com/Effective-Java-Joshua-Bloch/dp/0134685997)

---

## 🙏 Créditos

- **iluwatar/java-design-patterns** - Ilkka Seppälä
- **Refactoring Guru** - Alexander Shvets

---

[← Volver a Prototype](../README.md)

