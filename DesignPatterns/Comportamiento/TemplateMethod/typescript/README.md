# Template Method - Implementación en TypeScript

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Template Method TypeScript**
- **Enlace**: [Template Method](https://refactoring.guru/design-patterns/template-method/typescript/example)

---

## 💡 Ejemplo

```typescript
abstract class DataMiner {
    // Template Method
    public mine(path: string): void {
        const file = this.openFile(path);
        const rawData = this.extractData(file);
        const data = this.parseData(rawData);
        this.analyzeData(data);
        this.sendReport(data);
        this.closeFile(file);
    }
    
    protected openFile(path: string): File { return new File(path); }
    protected closeFile(file: File): void { }
    
    protected abstract extractData(file: File): Uint8Array;
    protected abstract parseData(raw: Uint8Array): Data;
    
    protected analyzeData(data: Data): void { }
    protected sendReport(data: Data): void { }
}

class PDFDataMiner extends DataMiner {
    protected extractData(file: File): Uint8Array {
        // PDF extraction
        return new Uint8Array();
    }
    
    protected parseData(raw: Uint8Array): Data {
        // PDF parsing
        return new Data();
    }
}
```

---

## 🙏 Créditos
- **Refactoring Guru** - Alexander Shvets

[← Volver](../README.md)
