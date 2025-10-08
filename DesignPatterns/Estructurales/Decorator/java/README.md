# Decorator - Implementación en Java

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Decorator](https://github.com/iluwatar/java-design-patterns/tree/master/decorator)
- **Ejemplo**: Troll decorators (SimpleTroll, ClubbedTroll)

### 2. **Refactoring Guru - Decorator Java**
- **Enlace**: [Decorator](https://refactoring.guru/design-patterns/decorator/java/example)
- **Ejemplo**: Notifiers (Email, SMS, Slack decorators)

---

## 💡 Ejemplo (Java I/O Streams)

```java
// Java I/O usa Decorator extensivamente
BufferedInputStream bis = new BufferedInputStream(
    new FileInputStream("file.txt")
);

// Custom Decorator
interface DataSource {
    void writeData(String data);
    String readData();
}

class FileDataSource implements DataSource {
    private String filename;
    
    public void writeData(String data) {
        // Escribir a archivo
    }
    
    public String readData() {
        // Leer de archivo
        return "data";
    }
}

// Decorator base
abstract class DataSourceDecorator implements DataSource {
    protected DataSource wrappee;
    
    DataSourceDecorator(DataSource source) {
        this.wrappee = source;
    }
    
    public void writeData(String data) {
        wrappee.writeData(data);
    }
    
    public String readData() {
        return wrappee.readData();
    }
}

// Concrete Decorators
class EncryptionDecorator extends DataSourceDecorator {
    EncryptionDecorator(DataSource source) {
        super(source);
    }
    
    public void writeData(String data) {
        super.writeData(encrypt(data));
    }
    
    public String readData() {
        return decrypt(super.readData());
    }
    
    private String encrypt(String data) { return "encrypted_" + data; }
    private String decrypt(String data) { return data.replace("encrypted_", ""); }
}

// Uso apilado
DataSource source = new FileDataSource("file.txt");
source = new EncryptionDecorator(source);
source = new CompressionDecorator(source);

source.writeData("Hello");  // Comprime → Encripta → Escribe
```

---

## 🙏 Créditos
- **iluwatar/java-design-patterns**
- **Refactoring Guru**

[← Volver](../README.md)
