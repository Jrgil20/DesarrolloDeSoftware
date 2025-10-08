# Proxy - Implementación en Java

## 📖 Descripción

Referencias a implementaciones del patrón Proxy en Java, incluyendo proxies dinámicos y los 5 tipos principales.

---

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Proxy en java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/proxy)
- **Ejemplo**: Wizard tower proxy (lazy loading)

### 2. **Refactoring Guru - Proxy Java**
- **Enlace**: [Proxy en Refactoring Guru](https://refactoring.guru/design-patterns/proxy/java/example)
- **Ejemplo**: YouTube video downloader con caching

### 3. **Baeldung - Proxy Pattern**
- **Enlace**: [Baeldung Proxy](https://www.baeldung.com/java-proxy-pattern)

---

## 💡 Tipos de Proxy Implementados

### 1. Virtual Proxy (Lazy Loading)

```java
// Subject Interface
interface Image {
    void display();
}

// Real Subject (Costoso)
class RealImage implements Image {
    private String filename;
    
    public RealImage(String filename) {
        this.filename = filename;
        loadFromDisk(); // Operación costosa
    }
    
    private void loadFromDisk() {
        System.out.println("Loading image: " + filename);
        // Simular carga pesada
        try { Thread.sleep(2000); } catch (InterruptedException e) { }
    }
    
    @Override
    public void display() {
        System.out.println("Displaying image: " + filename);
    }
}

// Proxy (Lazy Loading)
class ImageProxy implements Image {
    private RealImage realImage;
    private String filename;
    
    public ImageProxy(String filename) {
        this.filename = filename;
    }
    
    @Override
    public void display() {
        // Crear objeto real solo cuando se necesita
        if (realImage == null) {
            realImage = new RealImage(filename);
        }
        realImage.display();
    }
}

// Uso
Image image = new ImageProxy("large_photo.jpg"); 
// No carga aún ✅
System.out.println("Proxy created, image not loaded yet");

image.display(); 
// Ahora carga (2 segundos)
image.display(); 
// Segunda vez: usa objeto ya creado (instantáneo)
```

### 2. Protection Proxy (Control de Acceso)

```java
interface BankAccount {
    void withdraw(double amount);
    double getBalance();
}

class RealBankAccount implements BankAccount {
    private double balance = 1000.0;
    
    @Override
    public void withdraw(double amount) {
        balance -= amount;
        System.out.println("Withdrawn: $" + amount);
    }
    
    @Override
    public double getBalance() {
        return balance;
    }
}

class ProtectionProxy implements BankAccount {
    private RealBankAccount realAccount;
    private String userRole;
    
    public ProtectionProxy(String userRole) {
        this.userRole = userRole;
        this.realAccount = new RealBankAccount();
    }
    
    @Override
    public void withdraw(double amount) {
        if ("OWNER".equals(userRole)) {
            realAccount.withdraw(amount);
        } else {
            System.out.println("Access denied: insufficient permissions");
        }
    }
    
    @Override
    public double getBalance() {
        if ("OWNER".equals(userRole) || "VIEWER".equals(userRole)) {
            return realAccount.getBalance();
        }
        System.out.println("Access denied");
        return 0.0;
    }
}

// Uso
BankAccount ownerAccount = new ProtectionProxy("OWNER");
ownerAccount.withdraw(100); // ✅ Permitido

BankAccount guestAccount = new ProtectionProxy("GUEST");
guestAccount.withdraw(100); // ❌ Denegado
```

### 3. Caching Proxy

```java
interface DatabaseQuery {
    List<User> getUsers();
}

class RealDatabase implements DatabaseQuery {
    @Override
    public List<User> getUsers() {
        System.out.println("Querying database... (slow)");
        // Simular query costoso
        try { Thread.sleep(1000); } catch (InterruptedException e) { }
        return Arrays.asList(new User("John"), new User("Jane"));
    }
}

class CachingProxy implements DatabaseQuery {
    private RealDatabase realDb;
    private List<User> cachedUsers;
    private long cacheTime;
    private static final long CACHE_DURATION = 5000; // 5 segundos
    
    public CachingProxy() {
        this.realDb = new RealDatabase();
    }
    
    @Override
    public List<User> getUsers() {
        long now = System.currentTimeMillis();
        
        if (cachedUsers == null || (now - cacheTime) > CACHE_DURATION) {
            System.out.println("Cache miss - fetching from database");
            cachedUsers = realDb.getUsers();
            cacheTime = now;
        } else {
            System.out.println("Cache hit - returning cached data");
        }
        
        return cachedUsers;
    }
}
```

### 4. Dynamic Proxy (java.lang.reflect.Proxy)

```java
import java.lang.reflect.*;

interface Service {
    String getData(String id);
    void saveData(String id, String data);
}

class RealService implements Service {
    public String getData(String id) {
        return "Data for " + id;
    }
    
    public void saveData(String id, String data) {
        System.out.println("Saving: " + data);
    }
}

class LoggingHandler implements InvocationHandler {
    private Object target;
    
    public LoggingHandler(Object target) {
        this.target = target;
    }
    
    @Override
    public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
        System.out.println("Before: " + method.getName());
        long start = System.currentTimeMillis();
        
        Object result = method.invoke(target, args);
        
        long elapsed = System.currentTimeMillis() - start;
        System.out.println("After: " + method.getName() + " (" + elapsed + "ms)");
        
        return result;
    }
}

// Uso de Dynamic Proxy
Service realService = new RealService();
Service proxy = (Service) Proxy.newProxyInstance(
    Service.class.getClassLoader(),
    new Class<?>[] { Service.class },
    new LoggingHandler(realService)
);

String data = proxy.getData("123"); 
// Logs: Before: getData
//       After: getData (0ms)
```

---

## 🔧 Características Java

### Spring AOP (Aspect-Oriented Programming)
```java
@Aspect
@Component
public class LoggingAspect {
    @Around("execution(* com.example.service.*.*(..))")
    public Object logMethodExecution(ProceedingJoinPoint joinPoint) throws Throwable {
        System.out.println("Before: " + joinPoint.getSignature());
        Object result = joinPoint.proceed();
        System.out.println("After: " + joinPoint.getSignature());
        return result;
    }
}

// Spring crea proxies automáticamente
```

---

## 📚 Recursos

- [Baeldung - Java Dynamic Proxies](https://www.baeldung.com/java-dynamic-proxies)
- [Java Proxy Class](https://docs.oracle.com/javase/8/docs/api/java/lang/reflect/Proxy.html)
- [Spring AOP](https://docs.spring.io/spring-framework/docs/current/reference/html/core.html#aop)

---

## 🙏 Créditos

- **iluwatar/java-design-patterns** - Ilkka Seppälä
- **Refactoring Guru** - Alexander Shvets
- **Baeldung**

---

[← Volver a Proxy](../README.md)
