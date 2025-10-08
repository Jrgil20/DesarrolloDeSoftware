# Abstract Factory - Implementación en Java

## 📖 Descripción

Esta carpeta contiene referencias a implementaciones de alta calidad del patrón Abstract Factory en Java, seleccionadas de repositorios reconocidos que demuestran cómo crear familias de objetos relacionados sin especificar sus clases concretas.

---

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)

**Implementación enterprise-grade más reconocida**

- **Enlace**: [Abstract Factory en java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/abstract-factory)
- **Autor**: Ilkka Seppälä y colaboradores
- **Características**:
  - ✅ Ejemplo del mundo real: Kingdom Factory
  - ✅ Familias de objetos (Castle, King, Army) por reino (Elf, Orc)
  - ✅ Tests unitarios completos
  - ✅ Documentación exhaustiva
  - ✅ Diagramas UML

**Estructura del ejemplo**:
```
abstract-factory/
├── src/main/java/com/iluwatar/abstractfactory/
│   ├── App.java                    # Demo principal
│   ├── Kingdom.java                # Cliente que usa factory
│   ├── KingdomFactory.java         # Abstract Factory interface
│   ├── ElfKingdomFactory.java      # Concrete Factory 1
│   ├── OrcKingdomFactory.java      # Concrete Factory 2
│   ├── Castle.java                 # Abstract Product
│   ├── King.java                   # Abstract Product
│   ├── Army.java                   # Abstract Product
│   ├── ElfCastle.java             # Concrete Product
│   ├── ElfKing.java               # Concrete Product
│   └── ElfArmy.java               # Concrete Product
└── src/test/java/...
```

**Ejemplo de uso**:
```bash
git clone https://github.com/iluwatar/java-design-patterns.git
cd java-design-patterns/abstract-factory
mvn clean install
mvn exec:java
```

---

### 2. **Refactoring Guru - Abstract Factory Java**

**Recurso educativo de referencia mundial**

- **Enlace**: [Abstract Factory en Refactoring Guru](https://refactoring.guru/design-patterns/abstract-factory/java/example)
- **Características**:
  - ✅ Ejemplo de aplicación GUI multiplataforma
  - ✅ Familias: Windows UI vs macOS UI
  - ✅ Código descargable
  - ✅ Diagramas interactivos

**Ejemplo destacado**: Creación de botones y checkboxes compatibles por plataforma

---

### 3. **SourceMaking - Abstract Factory**

- **Enlace**: [Abstract Factory Pattern](https://sourcemaking.com/design_patterns/abstract_factory)
- **Características**:
  - ✅ Múltiples ejemplos progresivos
  - ✅ Comparación con Factory Method
  - ✅ Anti-patrones comunes

---

## 💡 Ejemplo de Referencia Rápida

```java
// Basado en los repositorios mencionados

// Abstract Products
interface Button {
    void paint();
}

interface Checkbox {
    void check();
}

// Concrete Products - Windows Family
class WindowsButton implements Button {
    @Override
    public void paint() {
        System.out.println("🪟 Renderizando botón Windows");
    }
}

class WindowsCheckbox implements Checkbox {
    @Override
    public void check() {
        System.out.println("🪟 Checkbox Windows activado");
    }
}

// Concrete Products - Mac Family
class MacButton implements Button {
    @Override
    public void paint() {
        System.out.println("🍎 Renderizando botón Mac");
    }
}

class MacCheckbox implements Checkbox {
    @Override
    public void check() {
        System.out.println("🍎 Checkbox Mac activado");
    }
}

// Abstract Factory
interface GUIFactory {
    Button createButton();
    Checkbox createCheckbox();
}

// Concrete Factories
class WindowsFactory implements GUIFactory {
    @Override
    public Button createButton() {
        return new WindowsButton();
    }
    
    @Override
    public Checkbox createCheckbox() {
        return new WindowsCheckbox();
    }
}

class MacFactory implements GUIFactory {
    @Override
    public Button createButton() {
        return new MacButton();
    }
    
    @Override
    public Checkbox createCheckbox() {
        return new MacCheckbox();
    }
}

// Client
class Application {
    private Button button;
    private Checkbox checkbox;
    
    public Application(GUIFactory factory) {
        this.button = factory.createButton();
        this.checkbox = factory.createCheckbox();
    }
    
    public void render() {
        button.paint();
        checkbox.check();
    }
}

// Main
public class AbstractFactoryDemo {
    private static Application configureApplication() {
        Application app;
        GUIFactory factory;
        
        String osName = System.getProperty("os.name").toLowerCase();
        
        if (osName.contains("mac")) {
            factory = new MacFactory();
        } else {
            factory = new WindowsFactory();
        }
        
        app = new Application(factory);
        return app;
    }
    
    public static void main(String[] args) {
        Application app = configureApplication();
        app.render();
    }
}
```

---

## 🔧 Características Específicas de Java

### 1. **Uso de Interfaces para Factories**
```java
// Preferir interfaces para máxima flexibilidad
public interface AbstractFactory {
    ProductA createProductA();
    ProductB createProductB();
}
```

### 2. **Factory Registry Pattern**
```java
public class FactoryRegistry {
    private static final Map<String, AbstractFactory> factories = new HashMap<>();
    
    static {
        factories.put("windows", new WindowsFactory());
        factories.put("mac", new MacFactory());
        factories.put("linux", new LinuxFactory());
    }
    
    public static AbstractFactory getFactory(String key) {
        return factories.get(key);
    }
}
```

### 3. **Factory con Generics**
```java
public interface GenericFactory<T extends Product> {
    T createProduct();
}

public class ConcreteFactory<T extends ConcreteProduct> 
    implements GenericFactory<T> {
    
    private final Class<T> productClass;
    
    public ConcreteFactory(Class<T> productClass) {
        this.productClass = productClass;
    }
    
    @Override
    public T createProduct() {
        try {
            return productClass.getDeclaredConstructor().newInstance();
        } catch (Exception e) {
            throw new RuntimeException("Cannot create product", e);
        }
    }
}
```

### 4. **Builder Pattern Integration**
```java
public class FactoryBuilder {
    private List<Supplier<? extends Product>> productSuppliers = new ArrayList<>();
    
    public FactoryBuilder withProduct(Supplier<? extends Product> supplier) {
        productSuppliers.add(supplier);
        return this;
    }
    
    public AbstractFactory build() {
        return new CustomFactory(productSuppliers);
    }
}
```

### 5. **Enum-based Factories**
```java
public enum UITheme {
    LIGHT {
        @Override
        public GUIFactory getFactory() {
            return new LightThemeFactory();
        }
    },
    DARK {
        @Override
        public GUIFactory getFactory() {
            return new DarkThemeFactory();
        }
    };
    
    public abstract GUIFactory getFactory();
}

// Uso
GUIFactory factory = UITheme.DARK.getFactory();
```

---

## 📚 Recursos Adicionales

### Artículos Técnicos
- [Baeldung - Abstract Factory Pattern](https://www.baeldung.com/java-abstract-factory-pattern)
- [DZone - Abstract Factory in Java](https://dzone.com/articles/design-patterns-abstract-factory)
- [JournalDev - Abstract Factory Design Pattern](https://www.journaldev.com/1418/abstract-factory-design-pattern-in-java)

### Videos
- [Derek Banas - Abstract Factory Pattern](https://www.youtube.com/watch?v=v-GiuMmsXj4)
- [Programming with Mosh - Factory Patterns](https://www.youtube.com/watch?v=EcFVTgRHJLM)

### Libros
- **"Design Patterns: Elements of Reusable Object-Oriented Software"** - GoF (Capítulo sobre Abstract Factory)
- **"Head First Design Patterns"** - Freeman & Freeman (Capítulo 4)
- **"Effective Java"** - Joshua Bloch (Item 1: Static factory methods)

---

## ⚙️ Configuración del Proyecto

### Maven (pom.xml)
```xml
<project>
    <modelVersion>4.0.0</modelVersion>
    <groupId>com.patterns</groupId>
    <artifactId>abstract-factory</artifactId>
    <version>1.0.0</version>
    
    <properties>
        <maven.compiler.source>17</maven.compiler.source>
        <maven.compiler.target>17</maven.compiler.target>
        <junit.version>5.9.3</junit.version>
    </properties>
    
    <dependencies>
        <dependency>
            <groupId>org.junit.jupiter</groupId>
            <artifactId>junit-jupiter</artifactId>
            <version>${junit.version}</version>
            <scope>test</scope>
        </dependency>
        <dependency>
            <groupId>org.assertj</groupId>
            <artifactId>assertj-core</artifactId>
            <version>3.24.2</version>
            <scope>test</scope>
        </dependency>
    </dependencies>
</project>
```

### Gradle (build.gradle)
```gradle
plugins {
    id 'java'
    id 'application'
}

java {
    toolchain {
        languageVersion = JavaLanguageVersion.of(17)
    }
}

dependencies {
    testImplementation 'org.junit.jupiter:junit-jupiter:5.9.3'
    testImplementation 'org.assertj:assertj-core:3.24.2'
}

test {
    useJUnitPlatform()
}

application {
    mainClass = 'com.patterns.AbstractFactoryDemo'
}
```

---

## 🎯 Mejores Prácticas

### 1. **Naming Conventions**
```java
// Factories terminan en "Factory"
public interface GUIFactory { }
public class WindowsFactory implements GUIFactory { }

// Productos agrupados por familia
public interface Button { }
public class WindowsButton implements Button { }
public class MacButton implements Button { }
```

### 2. **Product Family Validation**
```java
public abstract class AbstractFactory {
    public abstract ProductA createProductA();
    public abstract ProductB createProductB();
    
    // Validar que productos sean compatibles
    public void validateProductFamily() {
        ProductA productA = createProductA();
        ProductB productB = createProductB();
        
        if (!productA.isCompatibleWith(productB)) {
            throw new IllegalStateException("Productos incompatibles");
        }
    }
}
```

### 3. **Lazy Initialization con Singleton**
```java
public class WindowsFactory implements GUIFactory {
    private static volatile WindowsFactory instance;
    
    private WindowsFactory() {}
    
    public static WindowsFactory getInstance() {
        if (instance == null) {
            synchronized (WindowsFactory.class) {
                if (instance == null) {
                    instance = new WindowsFactory();
                }
            }
        }
        return instance;
    }
    
    @Override
    public Button createButton() {
        return new WindowsButton();
    }
}
```

### 4. **Documentación con Javadoc**
```java
/**
 * Factory abstracta para crear familias de componentes de UI relacionados.
 * Cada implementación concreta garantiza que todos los productos creados
 * sean compatibles entre sí.
 *
 * @see WindowsFactory
 * @see MacFactory
 */
public interface GUIFactory {
    /**
     * Crea un botón compatible con la familia de productos.
     *
     * @return instancia de Button específica de la plataforma
     */
    Button createButton();
}
```

---

## 🧪 Ejemplo de Tests (JUnit 5)

```java
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ValueSource;
import static org.assertj.core.api.Assertions.*;

class AbstractFactoryTest {
    
    @Test
    void windowsFactory_shouldCreateWindowsProducts() {
        // Given
        GUIFactory factory = new WindowsFactory();
        
        // When
        Button button = factory.createButton();
        Checkbox checkbox = factory.createCheckbox();
        
        // Then
        assertThat(button).isInstanceOf(WindowsButton.class);
        assertThat(checkbox).isInstanceOf(WindowsCheckbox.class);
    }
    
    @Test
    void macFactory_shouldCreateMacProducts() {
        // Given
        GUIFactory factory = new MacFactory();
        
        // When
        Button button = factory.createButton();
        Checkbox checkbox = factory.createCheckbox();
        
        // Then
        assertThat(button).isInstanceOf(MacButton.class);
        assertThat(checkbox).isInstanceOf(MacCheckbox.class);
    }
    
    @Test
    void application_shouldWorkWithAnyFactory() {
        // Given
        GUIFactory windowsFactory = new WindowsFactory();
        GUIFactory macFactory = new MacFactory();
        
        // When & Then - No debe lanzar excepciones
        assertThatCode(() -> {
            new Application(windowsFactory).render();
            new Application(macFactory).render();
        }).doesNotThrowAnyException();
    }
    
    @ParameterizedTest
    @ValueSource(classes = {WindowsFactory.class, MacFactory.class})
    void factories_shouldProduceCompatibleProducts(Class<? extends GUIFactory> factoryClass) 
            throws Exception {
        // Given
        GUIFactory factory = factoryClass.getDeclaredConstructor().newInstance();
        
        // When
        Button button = factory.createButton();
        Checkbox checkbox = factory.createCheckbox();
        
        // Then - Productos de la misma familia deben ser compatibles
        assertThat(button.getClass().getPackage())
            .isEqualTo(checkbox.getClass().getPackage());
    }
}
```

---

## 🔄 Comparación con Factory Method

| Aspecto | Factory Method | Abstract Factory |
|---------|----------------|------------------|
| **Propósito** | Crear UN objeto | Crear FAMILIA de objetos |
| **Métodos** | Un factory method | Múltiples factory methods |
| **Herencia** | Usa subclases | Usa composición |
| **Productos** | Un tipo de producto | Múltiples productos relacionados |
| **Ejemplo** | `createDocument()` | `createButton()`, `createCheckbox()` |

---

## 📝 Notas

- Ejemplos optimizados para **Java 17+**
- Compatible con Java 8+ con ajustes menores
- Considerar **Spring Framework** para inyección de factories en aplicaciones enterprise
- Para aplicaciones modulares, considerar **Java Platform Module System (JPMS)**

---

## 🙏 Créditos

Los ejemplos y referencias provienen de:

- **iluwatar/java-design-patterns** - Licencia: MIT
  - [Repositorio](https://github.com/iluwatar/java-design-patterns)
  - Mantenido por Ilkka Seppälä

- **Refactoring Guru** - Alexander Shvets
  - [Sitio web](https://refactoring.guru)

- **Gang of Four** - Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides
  - Libro: "Design Patterns: Elements of Reusable Object-Oriented Software"

---

[← Volver a Abstract Factory](../README.md) | [📂 Ver todos los patrones creacionales](../../Creacionales.md)

