# Iterator - Implementación en C#

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Iterator C#**
- **Enlace**: [Iterator](https://refactoring.guru/design-patterns/iterator/csharp/example)

### 2. **DoFactory**
- **Enlace**: [DoFactory](https://www.dofactory.com/net/iterator-design-pattern)

---

## 💡 Ejemplo (IEnumerable + yield)

```csharp
// C# usa IEnumerable/IEnumerator
public class CustomCollection<T> : IEnumerable<T>
{
    private List<T> _items = new();
    
    public void Add(T item) => _items.Add(item);
    
    // yield return simplifica Iterator
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _items)
        {
            yield return item;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// Uso con foreach
var collection = new CustomCollection<string>();
collection.Add("A");
collection.Add("B");

foreach (var item in collection)
{
    Console.WriteLine(item);
}

// Iterator personalizado
public IEnumerable<int> GetEvenNumbers(int max)
{
    for (int i = 0; i <= max; i += 2)
    {
        yield return i;
    }
}

// Uso
foreach (var num in GetEvenNumbers(10))
{
    Console.WriteLine(num);  // 0, 2, 4, 6, 8, 10
}
```

---

## 🙏 Créditos
- **Refactoring Guru**
- **Microsoft**

[← Volver](../README.md)
