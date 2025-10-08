# Command - Java

## 🌟 Repositorios
- **[iluwatar/java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/command)**
- **[Refactoring Guru](https://refactoring.guru/design-patterns/command/java/example)**

## 💡 Ejemplo
```java
interface Command {
    void execute();
    void undo();
}

class LightOnCommand implements Command {
    private Light light;
    public void execute() { light.on(); }
    public void undo() { light.off(); }
}
```

[← Volver](../README.md)
