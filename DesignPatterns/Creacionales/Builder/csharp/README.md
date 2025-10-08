# Builder - Implementación en C#

## 📖 Descripción

Referencias a implementaciones del patrón Builder en C#/.NET con method chaining y fluent interface.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Builder C#**
- **Enlace**: [Builder en C#](https://refactoring.guru/design-patterns/builder/csharp/example)

### 2. **DoFactory - Builder Pattern**
- **Enlace**: [DoFactory Builder](https://www.dofactory.com/net/builder-design-pattern)

---

## 💡 Ejemplo de Referencia

```csharp
public class HttpRequest
{
    public string Url { get; private set; }
    public string Method { get; private set; }
    public Dictionary<string, string> Headers { get; private set; }
    public string Body { get; private set; }
    
    private HttpRequest() 
    {
        Headers = new Dictionary<string, string>();
    }
    
    public class Builder
    {
        private readonly HttpRequest _request = new HttpRequest();
        
        public Builder WithUrl(string url)
        {
            _request.Url = url;
            return this;
        }
        
        public Builder WithMethod(string method)
        {
            _request.Method = method;
            return this;
        }
        
        public Builder AddHeader(string key, string value)
        {
            _request.Headers[key] = value;
            return this;
        }
        
        public Builder WithBody(string body)
        {
            _request.Body = body;
            return this;
        }
        
        public HttpRequest Build()
        {
            if (string.IsNullOrEmpty(_request.Url))
                throw new InvalidOperationException("URL is required");
                
            return _request;
        }
    }
}

// Uso
var request = new HttpRequest.Builder()
    .WithUrl("https://api.example.com")
    .WithMethod("POST")
    .AddHeader("Content-Type", "application/json")
    .WithBody("{\"name\":\"John\"}")
    .Build();
```

---

## 🔧 Con Fluent Interface

```csharp
public interface IBuilderStep1
{
    IBuilderStep2 WithName(string name);
}

public interface IBuilderStep2
{
    IBuilderStep3 WithAge(int age);
}

public interface IBuilderStep3
{
    User Build();
}

public class UserBuilder : IBuilderStep1, IBuilderStep2, IBuilderStep3
{
    private string _name;
    private int _age;
    
    public IBuilderStep2 WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public IBuilderStep3 WithAge(int age)
    {
        _age = age;
        return this;
    }
    
    public User Build()
    {
        return new User(_name, _age);
    }
}

// Uso (fuerza el orden)
var user = new UserBuilder()
    .WithName("John")  // Debe ser primero
    .WithAge(30)       // Debe ser segundo
    .Build();
```

---

## 📚 Recursos

- [C# Design Patterns](https://www.dofactory.com/net/builder-design-pattern)
- [Fluent Builder in C#](https://www.codeproject.com/Articles/31490/Fluent-Builder)

---

## 🙏 Créditos

- **Refactoring Guru** - Alexander Shvets
- **DoFactory**

---

[← Volver a Builder](../README.md)

