# Decorator - Implementación en C#

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Decorator C#**
- **Enlace**: [Decorator](https://refactoring.guru/design-patterns/decorator/csharp/example)

### 2. **DoFactory**
- **Enlace**: [DoFactory](https://www.dofactory.com/net/decorator-design-pattern)

---

## 💡 Ejemplo (.NET Streams)

```csharp
// .NET usa Decorator en Streams
using var stream = new GZipStream(
    new FileStream("file.gz", FileMode.Open),
    CompressionMode.Decompress
);

// Custom Decorator
public interface INotifier
{
    void Send(string message);
}

public class EmailNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"📧 Email: {message}");
    }
}

// Decorator base
public abstract class NotifierDecorator : INotifier
{
    protected INotifier _wrappee;
    
    protected NotifierDecorator(INotifier wrappee)
    {
        _wrappee = wrappee;
    }
    
    public virtual void Send(string message)
    {
        _wrappee.Send(message);
    }
}

// Concrete Decorators
public class SMSDecorator : NotifierDecorator
{
    public SMSDecorator(INotifier wrappee) : base(wrappee) { }
    
    public override void Send(string message)
    {
        base.Send(message);  // Delegar
        Console.WriteLine($"📱 SMS: {message}");  // Añadir
    }
}

// Uso apilado
INotifier notifier = new EmailNotifier();
notifier = new SMSDecorator(notifier);
notifier = new SlackDecorator(notifier);

notifier.Send("Hello");  // Email + SMS + Slack
```

---

## 🙏 Créditos
- **Refactoring Guru**
- **DoFactory**

[← Volver](../README.md)
