# Template Method - Implementación en C#

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Template Method C#**
- **Enlace**: [Template Method](https://refactoring.guru/design-patterns/template-method/csharp/example)

### 2. **DoFactory - Template Method**
- **Enlace**: [DoFactory](https://www.dofactory.com/net/template-method-design-pattern)

---

## 💡 Ejemplo

```csharp
public abstract class DataMiner
{
    // Template Method (sealed = no override)
    public sealed void Mine(string path)
    {
        var file = OpenFile(path);
        var rawData = ExtractData(file);
        var data = ParseData(rawData);
        AnalyzeData(data);
        SendReport(data);
        CloseFile(file);
    }
    
    protected virtual FileInfo OpenFile(string path) => new(path);
    protected virtual void CloseFile(FileInfo file) { }
    
    protected abstract byte[] ExtractData(FileInfo file);
    protected abstract Data ParseData(byte[] raw);
    
    protected void AnalyzeData(Data data) { /* común */ }
    protected void SendReport(Data data) { /* común */ }
}

public class PDFDataMiner : DataMiner
{
    protected override byte[] ExtractData(FileInfo file)
    {
        // Lógica específica PDF
        return Array.Empty<byte>();
    }
    
    protected override Data ParseData(byte[] raw)
    {
        // Parsing específico PDF
        return new Data();
    }
}
```

---

## 🙏 Créditos
- **Refactoring Guru** - Alexander Shvets
- **DoFactory**

[← Volver](../README.md)
