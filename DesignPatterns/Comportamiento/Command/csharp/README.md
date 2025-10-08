# Command - C#

## 🌟 Repositorios
- **[Refactoring Guru](https://refactoring.guru/design-patterns/command/csharp/example)**
- **[DoFactory](https://www.dofactory.com/net/command-design-pattern)**

## 💡 Ejemplo
```csharp
public interface ICommand
{
    void Execute();
    void Undo();
}

public class LightOnCommand : ICommand
{
    private readonly Light _light;
    public void Execute() => _light.TurnOn();
    public void Undo() => _light.TurnOff();
}
```

[← Volver](../README.md)
