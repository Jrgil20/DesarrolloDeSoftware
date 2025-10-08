# Iterator - Implementación en Java

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Iterator](https://github.com/iluwatar/java-design-patterns/tree/master/iterator)

### 2. **Refactoring Guru - Iterator Java**
- **Enlace**: [Iterator](https://refactoring.guru/design-patterns/iterator/java/example)

---

## 💡 Ejemplo (Java Collections)

```java
// Java Collections Framework usa Iterator
List<String> list = Arrays.asList("A", "B", "C");

// Forma 1: Iterator explícito
Iterator<String> it = list.iterator();
while (it.hasNext()) {
    String item = it.next();
    System.out.println(item);
}

// Forma 2: for-each (usa Iterator internamente)
for (String item : list) {
    System.out.println(item);
}

// Custom Iterator
interface Iterator<T> {
    boolean hasNext();
    T next();
}

interface Collection<T> {
    Iterator<T> createIterator();
}

class ArrayCollection<T> implements Collection<T> {
    private T[] items;
    
    public Iterator<T> createIterator() {
        return new ArrayIterator<>(this);
    }
    
    private class ArrayIterator implements Iterator<T> {
        private int position = 0;
        private ArrayCollection<T> collection;
        
        public boolean hasNext() {
            return position < collection.items.length;
        }
        
        public T next() {
            return collection.items[position++];
        }
    }
}
```

---

## 🙏 Créditos
- **iluwatar/java-design-patterns**
- **Refactoring Guru**

[← Volver](../README.md)
