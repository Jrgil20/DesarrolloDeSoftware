# Factory Method (Método de Fábrica)

## Categoría
**Patrón Creacional**

---

## Propósito

Define una interfaz para crear un objeto, pero permite que las subclases decidan qué clase instanciar. Factory Method permite que una clase delegue la instanciación a las subclases.

---

## Problema

Imagina que estás creando una aplicación de gestión logística. La primera versión solo maneja transporte por **camión**, por lo que la mayor parte del código está dentro de la clase `Camion`.

Después de un tiempo, tu aplicación se vuelve popular y recibes solicitudes para incorporar transporte marítimo. Pero hay un problema: la mayor parte del código está acoplado a la clase `Camion`. Añadir `Barco` requeriría modificar toda la base de código, resultando en código complejo y difícil de mantener.

---

## Solución

El patrón Factory Method sugiere reemplazar las llamadas directas de construcción de objetos (usando `new`) con llamadas a un método factory especial. Los objetos aún se crean mediante `new`, pero se hace desde el método factory.

Las subclases pueden sobrescribir el factory method para cambiar la clase de los objetos que se crearán.

---

## Estructura UML

### Diagrama de Clases

```class diagram
┌──────────────────────┐
│   «interface»        │
│     Creator          │
├──────────────────────┤
│ + factoryMethod()    │◄─────────────┐
│ + someOperation()    │              │
└──────────────────────┘              │
         △                            │
         │                            │
    ┌────┴────┐                      │ usa
    │         │                       │
┌─────────┐ ┌─────────┐              │
│Creator1 │ │Creator2 │              │
├─────────┤ ├─────────┤              │
│+factory │ │+factory │              │
│Method() │ │Method() │              │
└────┬────┘ └────┬────┘              │
     │           │                   │
     │ crea      │ crea              │
     ▼           ▼                   │
┌─────────┐ ┌─────────┐              │
│Product1 │ │Product2 │              │
└─────────┘ └─────────┘              │
     △           △                   │
     └───────┬───┘                   │
             │                       │
    ┌────────────────┐               │
    │  «interface»   │               │
    │    Product     │◄──────────────┘
    └────────────────┘
```

---

## Componentes

1. **Product (Producto)**: Interfaz común para todos los objetos que el factory method puede crear
2. **ConcreteProduct**: Implementaciones específicas del producto
3. **Creator (Creador)**: Declara el factory method que retorna objetos Product
4. **ConcreteCreator**: Sobrescribe el factory method para crear instancias específicas

---

## Implementaciones por Lenguaje

Este patrón ha sido implementado en múltiples lenguajes. A continuación encontrarás ejemplos de código en carpetas organizadas por lenguaje:

### 📁 Ejemplos Disponibles

- **[Java](./java/)** - Implementación enterprise con buenas prácticas
- **[C#](./csharp/)** - Implementación .NET con características modernas
- **[TypeScript](./typescript/)** - Implementación type-safe con interfaces

Cada carpeta contiene:
- ✅ Código fuente completo y funcional
- ✅ Diagramas UML específicos del lenguaje
- ✅ Instrucciones de compilación/ejecución
- ✅ Referencias a repositorios de alta calidad

---

## Diagrama de Secuencia

```sequence diagram
:Cliente        :RoadLogistics    :Truck
   │                 │              │
   │──planDelivery()->▌              │
   │                 ▌─┐            │
   │                 ▌ │someOperation()
   │                 ▌<┘            │
   │                 ▌              │
   │                 ▌─createTransport()
   │                 ▌              │
   │                 ▌── «create» ──> ┌───────┐
   │                 ▌               │:Truck │
   │                 ▌               └───────┘
   │                 ▌                   │
   │                 ▌──deliver()──────> ▌
   │                 ▌ <┄resultado┄┄┄┄ ▌
   │<┄┄resultado┄┄┄ ▌                   │
   │                 │                   │
```

---

## Ventajas ✅

1. **Evita acoplamiento fuerte** entre creador y productos concretos
2. **Single Responsibility Principle**: Mueve la lógica de creación a un solo lugar
3. **Open/Closed Principle**: Puedes añadir nuevos productos sin romper código existente
4. **Código más limpio y mantenible**

---

## Desventajas ❌

1. **Complejidad aumentada**: Necesitas crear muchas subclases para implementar el patrón
2. **Puede ser excesivo**: Para casos simples, puede ser innecesario
3. **Jerarquía de clases**: Introduce una jerarquía que debe mantenerse

---

## Cuándo Usar

✅ **Usa Factory Method cuando:**

- No conoces de antemano los tipos exactos de objetos con los que trabajará tu código
- Quieres proporcionar a los usuarios una forma de extender componentes internos
- Quieres ahorrar recursos reutilizando objetos existentes en lugar de crear nuevos

❌ **Evita Factory Method cuando:**

- Los tipos de objetos no cambiarán frecuentemente
- La creación de objetos es simple y directa
- No necesitas desacoplar la creación de objetos

---

## Casos de Uso Reales

### 1. **Frameworks de UI**
```python
# Diferentes botones según la plataforma
class UIFactory:
    def create_button(self): pass

class WindowsFactory(UIFactory):
    def create_button(self):
        return WindowsButton()

class MacFactory(UIFactory):
    def create_button(self):
        return MacButton()
```

### 2. **Sistemas de Pago**
```python
class PaymentFactory:
    def create_payment(self): pass

class CreditCardFactory(PaymentFactory):
    def create_payment(self):
        return CreditCardPayment()

class PayPalFactory(PaymentFactory):
    def create_payment(self):
        return PayPalPayment()
```

### 3. **Procesadores de Documentos**
```python
class DocumentFactory:
    def create_document(self): pass

class PDFFactory(DocumentFactory):
    def create_document(self):
        return PDFDocument()

class WordFactory(DocumentFactory):
    def create_document(self):
        return WordDocument()
```

---

## Relación con Otros Patrones

- **Abstract Factory**: A menudo se implementa usando Factory Methods
- **Prototype**: Factory Method puede usar prototipos para clonar objetos
- **Template Method**: Factory Method es una especialización de Template Method

---

## Relación con Principios SOLID

| Principio | Cómo lo cumple |
|-----------|----------------|
| **SRP** | Separa la lógica de creación de objetos del código que los usa |
| **OCP** | Permite añadir nuevos productos sin modificar el código del creador |
| **LSP** | Las subclases pueden sustituir a la clase base sin romper funcionalidad |
| **DIP** | El código cliente depende de abstracciones, no de clases concretas |

---

## Ejercicios Prácticos

### Ejercicio 1: Sistema de Notificaciones
Crea un sistema que envíe notificaciones por diferentes canales (Email, SMS, Push).

### Ejercicio 2: Generador de Reportes
Implementa un generador de reportes que pueda crear diferentes formatos (Excel, PDF, CSV).

### Ejercicio 3: Conexiones a Base de Datos
Diseña un sistema que cree conexiones a diferentes bases de datos (MySQL, PostgreSQL, MongoDB).

---

## Referencias

- [Refactoring Guru - Factory Method](https://refactoring.guru/design-patterns/factory-method)
- Gang of Four - Design Patterns (Libro original)
- [SourceMaking - Factory Method](https://sourcemaking.com/design_patterns/factory_method)

---

[← Volver a Patrones Creacionales](../Creacionales.md)
