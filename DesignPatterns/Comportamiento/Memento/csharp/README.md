# Memento - C#

## 🌟 Repositorios
- **[Refactoring Guru](https://refactoring.guru/design-patterns/memento/csharp/example)**

## 💡 Ejemplo (C# Records)
```csharp
public record EditorMemento(string Text, int Cursor);

public class Editor
{
    public string Text { get; set; }
    public int Cursor { get; set; }
    
    public EditorMemento Save() => new(Text, Cursor);
    
    public void Restore(EditorMemento memento)
    {
        Text = memento.Text;
        Cursor = memento.Cursor;
    }
}
```

[← Volver](../README.md)
