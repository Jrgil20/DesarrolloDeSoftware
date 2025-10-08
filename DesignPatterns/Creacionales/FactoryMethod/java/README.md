# Factory Method - Implementación en Java

## 📖 Descripción

Esta carpeta contiene referencias y enlaces a implementaciones de alta calidad del patrón Factory Method en Java, seleccionadas de repositorios reconocidos con excelentes prácticas de programación.

---

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)

**El repositorio más popular de patrones de diseño en Java**

- **Enlace**: [Factory Method en java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/factory-method)
- **Autor**: Ilkka Seppälä y colaboradores
- **Características**:
  - ✅ Implementación enterprise-grade
  - ✅ Código limpio y bien documentado
  - ✅ Pruebas unitarias incluidas
  - ✅ Ejemplos del mundo real
  - ✅ Diagramas UML

**Estructura del ejemplo**:
```
factory-method/
├── src/main/java/com/iluwatar/factory/method/
│   ├── App.java                    # Aplicación principal
│   ├── WeaponType.java             # Enum de tipos de armas
│   ├── Weapon.java                 # Interfaz del producto
│   ├── OrcWeapon.java              # Producto concreto
│   ├── ElfWeapon.java              # Producto concreto
│   ├── Blacksmith.java             # Creator abstracto
│   ├── OrcBlacksmith.java          # Creator concreto
│   └── ElfBlacksmith.java          # Creator concreto
└── src/test/java/...               # Tests
```

**Cómo usar este ejemplo**:
1. Clona el repositorio: `git clone https://github.com/iluwatar/java-design-patterns.git`
2. Navega a: `cd java-design-patterns/factory-method`
3. Compila: `mvn clean install`
4. Ejecuta: `mvn exec:java -Dexec.mainClass="com.iluwatar.factory.method.App"`

---

### 2. **Refactoring Guru - Factory Method**

**Recurso educativo de referencia mundial**

- **Enlace**: [Factory Method en Refactoring Guru](https://refactoring.guru/design-patterns/factory-method/java/example)
- **Características**:
  - ✅ Explicaciones visuales detalladas
  - ✅ Ejemplo de aplicación de UI multiplataforma
  - ✅ Código descargable
  - ✅ Comparaciones con otros patrones

**Ejemplo destacado**: Sistema de botones multiplataforma (Windows/HTML)

---

### 3. **DesignPatternsPHP (adaptable a Java)**

- **Enlace**: [Factory Method Pattern](https://designpatternsphp.readthedocs.io/en/latest/Creational/FactoryMethod/README.html)
- **Características**:
  - Explicaciones claras
  - Diagrama UML interactivo
  - Código adaptable

---

## 💡 Ejemplo Simple (Referencia rápida)

```java
// Basado en los repositorios mencionados

// Product Interface
public interface Vehicle {
    void deliver();
}

// Concrete Products
public class Truck implements Vehicle {
    @Override
    public void deliver() {
        System.out.println("Entrega por tierra en un camión");
    }
}

public class Ship implements Vehicle {
    @Override
    public void deliver() {
        System.out.println("Entrega por mar en un barco");
    }
}

// Creator
public abstract class Logistics {
    // Factory Method
    public abstract Vehicle createVehicle();
    
    public void planDelivery() {
        Vehicle vehicle = createVehicle();
        vehicle.deliver();
    }
}

// Concrete Creators
public class RoadLogistics extends Logistics {
    @Override
    public Vehicle createVehicle() {
        return new Truck();
    }
}

public class SeaLogistics extends Logistics {
    @Override
    public Vehicle createVehicle() {
        return new Ship();
    }
}

// Client
public class Main {
    public static void main(String[] args) {
        Logistics logistics = new RoadLogistics();
        logistics.planDelivery(); // Salida: Entrega por tierra en un camión
        
        logistics = new SeaLogistics();
        logistics.planDelivery(); // Salida: Entrega por mar en un barco
    }
}
```

---

## 🔧 Características Específicas de Java

### 1. **Uso de Interfaces vs Clases Abstractas**
```java
// Preferir interfaces para productos
public interface Product {
    void operation();
}

// Clase abstracta para el Creator
public abstract class Creator {
    public abstract Product factoryMethod();
}
```

### 2. **Generics (Opcional)**
```java
public abstract class Creator<T extends Product> {
    public abstract T factoryMethod();
}
```

### 3. **Enums para Tipos**
```java
public enum ProductType {
    TYPE_A, TYPE_B, TYPE_C
}

public class ProductFactory {
    public Product createProduct(ProductType type) {
        return switch (type) {
            case TYPE_A -> new ConcreteProductA();
            case TYPE_B -> new ConcreteProductB();
            case TYPE_C -> new ConcreteProductC();
        };
    }
}
```

---

## 📚 Recursos Adicionales

### Libros
- **"Effective Java" por Joshua Bloch** - Item 1: Consider static factory methods instead of constructors
- **"Head First Design Patterns"** - Capítulo 4: Factory Pattern

### Artículos
- [Baeldung - Factory Method Pattern](https://www.baeldung.com/java-factory-pattern)
- [Oracle Java Tutorials - Abstract Methods and Classes](https://docs.oracle.com/javase/tutorial/java/IandI/abstract.html)

### Videos
- [Programming with Mosh - Factory Method Pattern](https://www.youtube.com/watch?v=EcFVTgRHJLM)

---

## ⚙️ Configuración del Proyecto

### Maven (pom.xml)
```xml
<project>
    <properties>
        <maven.compiler.source>17</maven.compiler.source>
        <maven.compiler.target>17</maven.compiler.target>
    </properties>
    
    <dependencies>
        <!-- JUnit para tests -->
        <dependency>
            <groupId>org.junit.jupiter</groupId>
            <artifactId>junit-jupiter</artifactId>
            <version>5.9.0</version>
            <scope>test</scope>
        </dependency>
    </dependencies>
</project>
```

### Gradle (build.gradle)
```gradle
plugins {
    id 'java'
}

java {
    sourceCompatibility = JavaVersion.VERSION_17
    targetCompatibility = JavaVersion.VERSION_17
}

dependencies {
    testImplementation 'org.junit.jupiter:junit-jupiter:5.9.0'
}
```

---

## 🎯 Mejores Prácticas en Java

1. **Nombrar métodos factory claramente**: `createProduct()`, `newInstance()`, `getInstance()`
2. **Usar interfaces para productos**: Mayor flexibilidad y desacoplamiento
3. **Considerar Factory estáticos**: Para casos simples, usar métodos estáticos
4. **Documentación Javadoc**: Especialmente en métodos factory
5. **Principio de responsabilidad única**: El creator solo debe encargarse de la creación

---

## 📝 Notas

- Los ejemplos están optimizados para **Java 17+** (uso de `switch` expressions)
- Para versiones anteriores, adaptar la sintaxis según corresponda
- Considerar el uso de **Lombok** para reducir boilerplate code

---

## 🙏 Créditos

Los ejemplos y referencias provienen de:

- **iluwatar/java-design-patterns** - Licencia: MIT
  - [Repositorio](https://github.com/iluwatar/java-design-patterns)
  - Mantenido por Ilkka Seppälä y comunidad open source

- **Refactoring Guru** - Alexander Shvets
  - [Sitio web](https://refactoring.guru)
  - Contenido educativo de alta calidad

Se agradece profundamente a los autores y mantenedores de estos recursos educativos.

---

[← Volver a Factory Method](../README.md) | [📂 Ver todos los patrones creacionales](../../Creacionales.md)

