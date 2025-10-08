# Chain of Responsibility - Implementación en Java

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Chain of Responsibility](https://github.com/iluwatar/java-design-patterns/tree/master/chain-of-responsibility)
- **Ejemplo**: Request handlers (OrcCommander, OrcOfficer, OrcSoldier)

### 2. **Refactoring Guru - Chain of Responsibility Java**
- **Enlace**: [Chain](https://refactoring.guru/design-patterns/chain-of-responsibility/java/example)

---

## 💡 Ejemplo

```java
interface Handler {
    Handler setNext(Handler handler);
    String handle(String request);
}

abstract class BaseHandler implements Handler {
    private Handler next;
    
    public Handler setNext(Handler handler) {
        this.next = handler;
        return handler;
    }
    
    public String handle(String request) {
        if (next != null) {
            return next.handle(request);
        }
        return null;
    }
}

class BasicSupportHandler extends BaseHandler {
    public String handle(String request) {
        if (request.equals("basic")) {
            return "BasicSupport handled";
        }
        return super.handle(request);
    }
}

// Uso
Handler chain = new BasicSupportHandler();
chain.setNext(new TechnicalHandler())
     .setNext(new ManagerHandler());
     
chain.handle("basic");
```

---

## 🙏 Créditos
- **iluwatar/java-design-patterns**
- **Refactoring Guru**

[← Volver](../README.md)
