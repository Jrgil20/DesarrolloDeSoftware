# Memento - Java

## 🌟 Repositorios
- **[iluwatar/java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/memento)**
- **[Refactoring Guru](https://refactoring.guru/design-patterns/memento/java/example)**

## 💡 Ejemplo
```java
class EditorMemento {
    private final String state;
    EditorMemento(String state) { this.state = state; }
    String getState() { return state; }
}

class Editor {
    private String text;
    
    public EditorMemento save() {
        return new EditorMemento(text);
    }
    
    public void restore(EditorMemento memento) {
        text = memento.getState();
    }
}
```

[← Volver](../README.md)
