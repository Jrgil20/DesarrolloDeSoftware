# Prototype - Implementación en C#

## 📖 Descripción

Referencias a implementaciones del patrón Prototype en C#/.NET con ICloneable y MemberwiseClone.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Prototype C#**
- **Enlace**: [Prototype en C#](https://refactoring.guru/design-patterns/prototype/csharp/example)

### 2. **DoFactory - Prototype Pattern**
- **Enlace**: [DoFactory Prototype](https://www.dofactory.com/net/prototype-design-pattern)

---

## 💡 Ejemplo de Referencia

```csharp
public abstract class Shape
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Color { get; set; }
    
    protected Shape() { }
    
    protected Shape(Shape source)
    {
        if (source != null)
        {
            X = source.X;
            Y = source.Y;
            Color = source.Color;
        }
    }
    
    public abstract Shape Clone();
}

public class Circle : Shape
{
    public int Radius { get; set; }
    
    public Circle() { }
    
    public Circle(Circle source) : base(source)
    {
        if (source != null)
        {
            Radius = source.Radius;
        }
    }
    
    public override Shape Clone()
    {
        return new Circle(this);
    }
}

// Uso
var circle1 = new Circle { X = 10, Y = 20, Radius = 15, Color = "Red" };
var circle2 = (Circle)circle1.Clone();

Console.WriteLine(circle1 == circle2);  // false (diferentes instancias)
Console.WriteLine(circle1.Radius == circle2.Radius);  // true (mismo valor)
```

---

## 🔧 Con ICloneable

```csharp
public class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Address Address { get; set; }
    
    // Shallow copy
    public object Clone()
    {
        return this.MemberwiseClone();
    }
    
    // Deep copy
    public Person DeepClone()
    {
        var clone = (Person)this.MemberwiseClone();
        clone.Address = new Address
        {
            City = this.Address.City,
            Country = this.Address.Country
        };
        return clone;
    }
}
```

---

## 📚 Recursos

- [C# ICloneable Interface](https://learn.microsoft.com/en-us/dotnet/api/system.icloneable)
- [MemberwiseClone Method](https://learn.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone)

---

## 🙏 Créditos

- **Refactoring Guru** - Alexander Shvets
- **DoFactory**

---

[← Volver a Prototype](../README.md)

