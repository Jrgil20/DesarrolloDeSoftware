# Chain of Responsibility (Cadena de Responsabilidad)

## Categoría
**Patrón de Comportamiento**

---

## Propósito

Evita acoplar el emisor de una petición a su receptor dando a más de un objeto la posibilidad de responder a la petición. Encadena los objetos receptores y pasa la petición a través de la cadena hasta que un objeto la maneje.

---

## Definición Formal

**Chain of Responsibility** es un patrón de diseño de comportamiento que permite pasar solicitudes a lo largo de una cadena de manejadores. Al recibir una solicitud, cada manejador decide procesarla o pasarla al siguiente manejador de la cadena.

### Intención del GoF

> "Evita acoplar el emisor de una solicitud a su receptor dando a más de un objeto la oportunidad de manejar la solicitud. Encadena los objetos receptores y pasa la solicitud a lo largo de la cadena hasta que un objeto la maneje."

---

## Explicación Detallada

El patrón Chain of Responsibility desacopla al que envía una petición del que la procesa, permitiendo que múltiples objetos tengan la oportunidad de manejarla. La petición se pasa por una cadena de objetos hasta que uno la procesa o hasta llegar al final de la cadena.

### Conceptos Clave

1. **Cadena de Manejadores**: Lista enlazada de objetos procesadores
2. **Desacoplamiento**: El emisor no conoce quién manejará la solicitud
3. **Flexibilidad**: Puedes añadir/quitar manejadores dinámicamente
4. **Responsabilidad Única**: Cada manejador decide si puede procesar
5. **Propagación**: La solicitud se propaga hasta ser manejada

### Metáfora: Call Center

```
Cliente llama al call center
  ↓
Operador Nivel 1 (trata problemas básicos)
  ↓ (si no puede)
Técnico Nivel 2 (problemas técnicos)
  ↓ (si no puede)
Manager (problemas complejos/quejas)

Cada nivel intenta resolver, si no puede, escala.
```

---

## Problema Detallado

### Escenario: Sistema de Soporte Técnico

Una empresa tiene 3 niveles de soporte: básico, técnico y gerencial. Cada uno puede manejar ciertos tipos de problemas.

**Sin Chain of Responsibility**:
```java
// ❌ Sistema centralizado que conoce todos los manejadores
class SupportSystem {
    private BasicSupport basic = new BasicSupport();
    private TechnicalSupport technical = new TechnicalSupport();
    private ManagerSupport manager = new ManagerSupport();
    
    public void handleRequest(Request request) {
        // ❌ Lógica de decisión centralizada y rígida
        if (request.getPriority() == Priority.LOW) {
            basic.handle(request);
        } else if (request.getPriority() == Priority.MEDIUM) {
            if (basic.canHandle(request)) {
                basic.handle(request);
            } else {
                technical.handle(request);
            }
        } else if (request.getPriority() == Priority.HIGH) {
            if (technical.canHandle(request)) {
                technical.handle(request);
            } else {
                manager.handle(request);
            }
        }
        // ❌ Lógica compleja, difícil de extender
    }
}
```

**Problemas**:
1. **Acoplamiento fuerte**: Sistema conoce todos los manejadores
2. **Difícil extensión**: Añadir nivel requiere modificar SupportSystem
3. **Lógica compleja**: Condicionales anidados
4. **No flexible**: Orden hardcodeado
5. **Violación OCP**: Cerrado a extensión

---

## Solución con Chain of Responsibility

```java
// ==========================================
// HANDLER INTERFACE
// ==========================================
interface SupportHandler {
    SupportHandler setNext(SupportHandler handler);
    String handle(Request request);
}

// ==========================================
// BASE HANDLER (implementación común)
// ==========================================
abstract class BaseSupportHandler implements SupportHandler {
    private SupportHandler next;
    
    @Override
    public SupportHandler setNext(SupportHandler handler) {
        this.next = handler;
        return handler;  // Para encadenamiento fluido
    }
    
    @Override
    public String handle(Request request) {
        if (next != null) {
            return next.handle(request);  // Pasar al siguiente
        }
        return "Request not handled";  // Fin de cadena
    }
}

// ==========================================
// CONCRETE HANDLERS
// ==========================================
class BasicSupportHandler extends BaseSupportHandler {
    @Override
    public String handle(Request request) {
        if (request.getType().equals("password_reset") || 
            request.getType().equals("account_question")) {
            return "BasicSupport: Handled request - " + request.getType();
        }
        
        System.out.println("BasicSupport: Cannot handle, passing to next...");
        return super.handle(request);  // Pasar al siguiente
    }
}

class TechnicalSupportHandler extends BaseSupportHandler {
    @Override
    public String handle(Request request) {
        if (request.getType().equals("bug_report") || 
            request.getType().equals("technical_issue")) {
            return "TechnicalSupport: Handled request - " + request.getType();
        }
        
        System.out.println("TechnicalSupport: Cannot handle, escalating...");
        return super.handle(request);
    }
}

class ManagerSupportHandler extends BaseSupportHandler {
    @Override
    public String handle(Request request) {
        // Manager maneja todo lo que llegue
        return "Manager: Handled request - " + request.getType();
    }
}

// ==========================================
// REQUEST
// ==========================================
class Request {
    private String type;
    private Priority priority;
    private String description;
    
    public Request(String type, Priority priority, String description) {
        this.type = type;
        this.priority = priority;
        this.description = description;
    }
    
    public String getType() { return type; }
    public Priority getPriority() { return priority; }
}

enum Priority { LOW, MEDIUM, HIGH }

// ==========================================
// CLIENTE
// ==========================================
public class Demo {
    public static void main(String[] args) {
        // Construir la cadena
        SupportHandler chain = new BasicSupportHandler();
        chain.setNext(new TechnicalSupportHandler())
             .setNext(new ManagerSupportHandler());
        
        // Enviar diferentes requests
        Request r1 = new Request("password_reset", Priority.LOW, "...");
        System.out.println(chain.handle(r1));
        // BasicSupport: Handled request - password_reset
        
        Request r2 = new Request("bug_report", Priority.MEDIUM, "...");
        System.out.println(chain.handle(r2));
        // BasicSupport: Cannot handle, passing to next...
        // TechnicalSupport: Handled request - bug_report
        
        Request r3 = new Request("complaint", Priority.HIGH, "...");
        System.out.println(chain.handle(r3));
        // BasicSupport: Cannot handle, passing to next...
        // TechnicalSupport: Cannot handle, escalating...
        // Manager: Handled request - complaint
    }
}
```

---

## Implementaciones por Lenguaje

### 📁 Ejemplos Disponibles

- **[Java](./java/)** - Implementación con encadenamiento fluido
- **[C#](./csharp/)** - Implementación .NET con middleware (ASP.NET Core)
- **[TypeScript](./typescript/)** - Implementación con Express.js middleware

Cada carpeta contiene:
- ✅ Cadena de manejadores completa
- ✅ Construcción fluida de cadenas
- ✅ Ejemplos de middleware web
- ✅ Referencias a repositorios reconocidos

---

## Casos de Uso Reales

### 1. **ASP.NET Core Middleware Pipeline**
```csharp
app.UseAuthentication();  // Handler 1
app.UseAuthorization();   // Handler 2
app.UseRouting();         // Handler 3
app.UseEndpoints(...);    // Handler final
```

### 2. **Express.js Middleware**
```javascript
app.use(logger);          // Handler 1
app.use(authenticator);   // Handler 2
app.use(errorHandler);    // Handler 3
```

### 3. **Event Bubbling (UI)**
```
Button → Panel → Window → Application
Cada nivel puede manejar o propagar el evento
```

### 4. **Logging con Niveles**
```
ConsoleLogger (INFO+) → FileLogger (WARN+) → EmailLogger (ERROR+)
```

---

## Errores Comunes

### ❌ Error 1: Cadena rota

```java
// ❌ INCORRECTO: No pasar al siguiente
class BadHandler extends BaseHandler {
    public String handle(Request req) {
        if (!canHandle(req)) {
            return null;  // ❌ No llama a super.handle()
        }
        return "handled";
    }
}

// ✅ CORRECTO
class GoodHandler extends BaseHandler {
    public String handle(Request req) {
        if (!canHandle(req)) {
            return super.handle(req);  // ✅ Pasa al siguiente
        }
        return "handled";
    }
}
```

### ❌ Error 2: Referencias circulares

```java
// ❌ INCORRECTO
Handler h1 = new Handler1();
Handler h2 = new Handler2();
h1.setNext(h2);
h2.setNext(h1);  // ❌ Círculo infinito

// ✅ CORRECTO: Validar en setNext()
public Handler setNext(Handler handler) {
    if (this.isInChain(handler)) {
        throw new IllegalArgumentException("Circular reference");
    }
    this.next = handler;
    return handler;
}
```

---

## Ejercicios Prácticos

### Ejercicio 1: Sistema de Aprobación de Gastos
Manager ($1000) → Director ($5000) → CEO (ilimitado)

### Ejercicio 2: Filtro de Spam
BlacklistFilter → KeywordFilter → BayesianFilter

### Ejercicio 3: Validación de Datos
NotNullValidator → FormatValidator → RangeValidator → DatabaseValidator

---

## Referencias

- [Refactoring Guru - Chain of Responsibility](https://refactoring.guru/design-patterns/chain-of-responsibility)
- [SourceMaking - Chain](https://sourcemaking.com/design_patterns/chain_of_responsibility)

---

[📂 Ver patrones de comportamiento](../Comportamiento.md) | [🏠 Volver a inicio](../../README.md)

---

*Última actualización: Octubre 2025*