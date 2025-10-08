# Observer - Implementación en Java

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Observer](https://github.com/iluwatar/java-design-patterns/tree/master/observer)

### 2. **Refactoring Guru - Observer Java**
- **Enlace**: [Observer](https://refactoring.guru/design-patterns/observer/java/example)

---

## 💡 Ejemplo

```java
import java.util.*;

interface Observer {
    void update(String event);
}

interface Subject {
    void attach(Observer observer);
    void detach(Observer observer);
    void notifyObservers(String event);
}

class ConcreteSubject implements Subject {
    private List<Observer> observers = new ArrayList<>();
    
    public void attach(Observer obs) {
        observers.add(obs);
    }
    
    public void detach(Observer obs) {
        observers.remove(obs);
    }
    
    public void notifyObservers(String event) {
        for (Observer obs : observers) {
            obs.update(event);
        }
    }
}

// Uso con Java 8+ (Functional interfaces)
Subject subject = new ConcreteSubject();
subject.attach(event -> System.out.println("Observer 1: " + event));
subject.attach(event -> System.out.println("Observer 2: " + event));

subject.notifyObservers("Data changed");
```

---

## 🙏 Créditos
- **iluwatar/java-design-patterns**
- **Refactoring Guru**

[← Volver](../README.md)
