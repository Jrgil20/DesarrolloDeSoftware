# Singleton - Implementación en Java

## 📖 Descripción

Referencias a implementaciones del patrón Singleton en Java, incluyendo variantes thread-safe y soluciones a problemas comunes (reflexión, serialización, clonación).

---

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)

- **Enlace**: [Singleton en java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/singleton)
- **Características**:
  - ✅ 5 variantes diferentes (Eager, Lazy, Thread-safe, Bill Pugh, Enum)
  - ✅ Tests exhaustivos
  - ✅ Protección contra reflexión y serialización
  - ✅ Documentación completa

### 2. **Refactoring Guru - Singleton Java**

- **Enlace**: [Singleton en Refactoring Guru](https://refactoring.guru/design-patterns/singleton/java/example)
- **Características**:
  - ✅ Múltiples variantes explicadas
  - ✅ Problemas comunes y soluciones

### 3. **Baeldung - Singleton Pattern**

- **Enlace**: [Baeldung Singleton](https://www.baeldung.com/java-singleton)
- **Características**:
  - ✅ Análisis profundo de cada variante
  - ✅ Comparación de rendimiento
  - ✅ Mejores prácticas

---

## 💡 Variantes del Patrón

### 1. Eager Initialization
```java
public class EagerSingleton {
    private static final EagerSingleton INSTANCE = new EagerSingleton();
    
    private EagerSingleton() {}
    
    public static EagerSingleton getInstance() {
        return INSTANCE;
    }
}
```

### 2. Lazy Initialization (No thread-safe)
```java
public class LazySingleton {
    private static LazySingleton instance;
    
    private LazySingleton() {}
    
    public static LazySingleton getInstance() {
        if (instance == null) {
            instance = new LazySingleton();
        }
        return instance;
    }
}
```

### 3. Thread-Safe con Double-Check Locking
```java
public class ThreadSafeSingleton {
    private static volatile ThreadSafeSingleton instance;
    
    private ThreadSafeSingleton() {
        if (instance != null) {
            throw new IllegalStateException("Instance already exists!");
        }
    }
    
    public static ThreadSafeSingleton getInstance() {
        if (instance == null) {
            synchronized (ThreadSafeSingleton.class) {
                if (instance == null) {
                    instance = new ThreadSafeSingleton();
                }
            }
        }
        return instance;
    }
}
```

### 4. Bill Pugh (Recomendado)
```java
public class BillPughSingleton {
    private BillPughSingleton() {}
    
    private static class SingletonHelper {
        private static final BillPughSingleton INSTANCE = new BillPughSingleton();
    }
    
    public static BillPughSingleton getInstance() {
        return SingletonHelper.INSTANCE;
    }
}
```

### 5. Enum Singleton (Mejor práctica - Joshua Bloch)
```java
public enum SingletonEnum {
    INSTANCE;
    
    public void doSomething() {
        System.out.println("Doing something...");
    }
}

// Uso
SingletonEnum.INSTANCE.doSomething();
```

---

## 🔐 Protección contra Ataques

### Reflexión
```java
private Singleton() {
    if (instance != null) {
        throw new IllegalStateException("Instance already created!");
    }
}
```

### Serialización
```java
public class SerializableSingleton implements Serializable {
    private static final long serialVersionUID = 1L;
    private static volatile SerializableSingleton instance;
    
    private SerializableSingleton() {}
    
    public static SerializableSingleton getInstance() {
        if (instance == null) {
            synchronized (SerializableSingleton.class) {
                if (instance == null) {
                    instance = new SerializableSingleton();
                }
            }
        }
        return instance;
    }
    
    // Prevenir nueva instancia durante deserialización
    protected Object readResolve() {
        return getInstance();
    }
}
```

---

## 📚 Recursos

- [Effective Java - Item 3: Enforce singleton property with private constructor or enum](https://www.amazon.com/Effective-Java-Joshua-Bloch/dp/0134685997)
- [Baeldung - Singleton](https://www.baeldung.com/java-singleton)
- [JournalDev - Singleton Pattern](https://www.journaldev.com/1377/java-singleton-design-pattern-best-practices-examples)

---

## 🙏 Créditos

- **iluwatar/java-design-patterns** - Ilkka Seppälä (MIT License)
- **Refactoring Guru** - Alexander Shvets
- **Joshua Bloch** - "Effective Java"

---

[← Volver a Singleton](../README.md)

