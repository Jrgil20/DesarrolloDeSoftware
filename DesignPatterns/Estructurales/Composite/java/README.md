# Composite - Implementación en Java

## 📖 Descripción

Referencias a implementaciones del patrón Composite en Java, demostrando estructuras de árbol con objetos uniformes.

---

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Composite en java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/composite)
- **Ejemplo**: Messenger sentences (palabras y oraciones compuestas)
- **Características**:
  - ✅ Implementación clara con Java 8+ streams
  - ✅ Tests exhaustivos
  - ✅ Ejemplo del mundo real

### 2. **Refactoring Guru - Composite Java**
- **Enlace**: [Composite en Refactoring Guru](https://refactoring.guru/design-patterns/composite/java/example)
- **Ejemplo**: Graphic editor con formas compuestas

### 3. **Baeldung - Composite Pattern**
- **Enlace**: [Baeldung Composite](https://www.baeldung.com/java-composite-pattern)

---

## 💡 Ejemplo de Referencia

```java
import java.util.*;

// Component
interface FileSystemItem {
    String getName();
    long getSize();
    void print(String indent);
}

// Leaf
class File implements FileSystemItem {
    private final String name;
    private final long size;
    
    public File(String name, long size) {
        this.name = name;
        this.size = size;
    }
    
    @Override
    public String getName() {
        return name;
    }
    
    @Override
    public long getSize() {
        return size;
    }
    
    @Override
    public void print(String indent) {
        System.out.println(indent + "📄 " + name + " (" + size + " bytes)");
    }
}

// Composite
class Folder implements FileSystemItem {
    private final String name;
    private final List<FileSystemItem> items = new ArrayList<>();
    
    public Folder(String name) {
        this.name = name;
    }
    
    public void add(FileSystemItem item) {
        items.add(item);
    }
    
    public void remove(FileSystemItem item) {
        items.remove(item);
    }
    
    @Override
    public String getName() {
        return name;
    }
    
    @Override
    public long getSize() {
        return items.stream()
            .mapToLong(FileSystemItem::getSize)
            .sum();
    }
    
    @Override
    public void print(String indent) {
        System.out.println(indent + "📁 " + name + "/");
        items.forEach(item -> item.print(indent + "  "));
    }
}

// Client
public class CompositeDemo {
    public static void main(String[] args) {
        // Crear estructura de archivos
        Folder root = new Folder("root");
        
        Folder documents = new Folder("Documents");
        documents.add(new File("resume.pdf", 150_000));
        documents.add(new File("cover-letter.doc", 80_000));
        
        Folder photos = new Folder("Photos");
        photos.add(new File("vacation.jpg", 2_000_000));
        photos.add(new File("family.jpg", 1_500_000));
        
        root.add(documents);
        root.add(photos);
        root.add(new File("readme.txt", 5_000));
        
        // Imprimir estructura
        root.print("");
        
        // Calcular tamaño total
        System.out.println("\nTotal size: " + root.getSize() + " bytes");
        System.out.println("Documents size: " + documents.getSize() + " bytes");
    }
}
```

**Salida**:
```
📁 root/
  📁 Documents/
    📄 resume.pdf (150000 bytes)
    📄 cover-letter.doc (80000 bytes)
  📁 Photos/
    📄 vacation.jpg (2000000 bytes)
    📄 family.jpg (1500000 bytes)
  📄 readme.txt (5000 bytes)

Total size: 3735000 bytes
Documents size: 230000 bytes
```

---

## 🔧 Características Java

### 1. Streams para Operaciones Recursivas
```java
public class Folder implements FileSystemItem {
    private List<FileSystemItem> items = new ArrayList<>();
    
    public long getSize() {
        return items.stream()
            .mapToLong(FileSystemItem::getSize)
            .sum();
    }
    
    public List<File> findAllFiles() {
        return items.stream()
            .flatMap(item -> {
                if (item instanceof File) {
                    return Stream.of((File) item);
                } else if (item instanceof Folder) {
                    return ((Folder) item).findAllFiles().stream();
                }
                return Stream.empty();
            })
            .collect(Collectors.toList());
    }
}
```

### 2. Visitor Pattern con Composite
```java
interface FileSystemVisitor {
    void visitFile(File file);
    void visitFolder(Folder folder);
}

interface FileSystemItem {
    void accept(FileSystemVisitor visitor);
}

class File implements FileSystemItem {
    public void accept(FileSystemVisitor visitor) {
        visitor.visitFile(this);
    }
}

class Folder implements FileSystemItem {
    public void accept(FileSystemVisitor visitor) {
        visitor.visitFolder(this);
        items.forEach(item -> item.accept(visitor));
    }
}

// Visitor para buscar archivos
class SearchVisitor implements FileSystemVisitor {
    private String searchTerm;
    private List<File> results = new ArrayList<>();
    
    public void visitFile(File file) {
        if (file.getName().contains(searchTerm)) {
            results.add(file);
        }
    }
    
    public void visitFolder(Folder folder) {
        // Solo visita, no hace nada especial
    }
}
```

### 3. Type-Safe Composite con Generics
```java
public abstract class Component<T extends Component<T>> {
    protected String name;
    protected List<T> children = new ArrayList<>();
    
    public void add(T component) {
        children.add(component);
    }
    
    public void remove(T component) {
        children.remove(component);
    }
    
    public abstract void operation();
}
```

---

## 📚 Recursos

- [Baeldung - Composite Pattern](https://www.baeldung.com/java-composite-pattern)
- [Java Design Patterns - Composite](https://java-design-patterns.com/patterns/composite/)
- [Effective Java - Favor composition over inheritance](https://www.amazon.com/Effective-Java-Joshua-Bloch/dp/0134685997)

---

## 🙏 Créditos

- **iluwatar/java-design-patterns** - Ilkka Seppälä (MIT License)
- **Refactoring Guru** - Alexander Shvets
- **Baeldung**

---

[← Volver a Composite](../README.md)
