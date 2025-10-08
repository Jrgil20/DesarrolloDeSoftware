# Template Method - Implementación en Java

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Template Method](https://github.com/iluwatar/java-design-patterns/tree/master/template-method)
- **Ejemplo**: Story templates (diferentes tipos de historias)

### 2. **Refactoring Guru - Template Method Java**
- **Enlace**: [Template Method](https://refactoring.guru/design-patterns/template-method/java/example)

---

## 💡 Ejemplo

```java
abstract class DataMiner {
    // Template Method (final = no se puede override)
    public final void mine(String path) {
        File file = openFile(path);
        byte[] rawData = extractData(file);
        Data data = parseData(rawData);
        analyzeData(data);
        sendReport(data);
        closeFile(file);
    }
    
    protected File openFile(String path) { /* común */ }
    protected void closeFile(File file) { /* común */ }
    
    protected abstract byte[] extractData(File file);  // Específico
    protected abstract Data parseData(byte[] raw);     // Específico
}

class PDFDataMiner extends DataMiner {
    @Override
    protected byte[] extractData(File file) {
        return extractPDFData(file);
    }
    
    @Override
    protected Data parseData(byte[] raw) {
        return parsePDFFormat(raw);
    }
}
```

---

## 🙏 Créditos
- **iluwatar/java-design-patterns** - Ilkka Seppälä
- **Refactoring Guru** - Alexander Shvets

[← Volver](../README.md)
