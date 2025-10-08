# Patrones de Diseño de Comportamiento

## Introducción

Los **patrones de comportamiento** se encargan de los algoritmos y la asignación de responsabilidades entre objetos. No solo describen patrones de objetos o clases, sino también patrones de comunicación entre ellos, caracterizando flujos de control complejos difíciles de seguir en tiempo de ejecución.

### ¿Por qué usar patrones de comportamiento?

- **Comunicación flexible**: Facilitan la comunicación entre objetos
- **Responsabilidades claras**: Definen cómo se distribuyen las responsabilidades
- **Acoplamiento reducido**: Minimizan las dependencias entre objetos
- **Algoritmos intercambiables**: Permiten variar algoritmos independientemente
- **Flujos de control claros**: Simplifican flujos de control complejos

---

## Los 11 Patrones de Comportamiento del GoF

### Resumen Rápido

| Patrón | Propósito Principal | Cuándo Usarlo |
|--------|---------------------|---------------|
| [**Chain of Responsibility**](./ChainOfResponsibility/) | Pasa solicitudes a través de una cadena de manejadores | Cuando múltiples objetos pueden manejar una solicitud |
| [**Command**](./Command/) | Encapsula una solicitud como un objeto | Cuando necesitas parametrizar acciones, deshacer/rehacer |
| [**Interpreter**](./Interpreter/) | Define gramática para un lenguaje | Cuando necesitas interpretar un lenguaje especializado |
| [**Iterator**](./Iterator/) | Accede secuencialmente a elementos de una colección | Cuando necesitas recorrer colecciones sin exponer estructura |
| [**Mediator**](./Mediator/) | Define un objeto que encapsula cómo interactúan objetos | Cuando tienes muchas interacciones complejas entre objetos |
| [**Memento**](./Memento/) | Captura y restaura el estado de un objeto | Cuando necesitas deshacer/rehacer operaciones |
| [**Observer**](./Observer/) | Notifica cambios a múltiples objetos | Cuando cambios en un objeto deben reflejarse en otros |
| [**State**](./State/) | Cambia comportamiento según el estado interno | Cuando el comportamiento depende del estado |
| [**Strategy**](./Strategy/) | Encapsula algoritmos intercambiables | Cuando tienes múltiples variantes de un algoritmo |
| [**Template Method**](./TemplateMethod/) | Define esqueleto de algoritmo en clase base | Cuando tienes pasos comunes pero implementaciones variables |
| [**Visitor**](./Visitor/) | Define nuevas operaciones sin cambiar clases | Cuando necesitas añadir operaciones a una estructura de objetos |

---

## Comparación de Patrones de Comportamiento

### Strategy vs State

- **Strategy**: Cliente elige el algoritmo explícitamente
- **State**: El contexto cambia de estado automáticamente

### Strategy vs Template Method

- **Strategy**: Usa composición (familia de algoritmos)
- **Template Method**: Usa herencia (variaciones de un algoritmo)

### Observer vs Mediator

- **Observer**: Comunicación uno-a-muchos (broadcast)
- **Mediator**: Comunicación muchos-a-muchos centralizada

### Command vs Strategy

- **Command**: Encapsula una acción (qué hacer)
- **Strategy**: Encapsula un algoritmo (cómo hacerlo)

### Memento vs Command

- **Memento**: Guarda estado para restaurar
- **Command**: Guarda operaciones para deshacer

---

## Patrones de Comportamiento en Detalle

### 1. Chain of Responsibility (Cadena de Responsabilidad)

**Problema que resuelve**: Múltiples objetos pueden manejar una solicitud, pero el que la maneja no se conoce de antemano.

**Solución**: Pasa la solicitud a través de una cadena de manejadores hasta que uno la procese.

**Casos de uso**:
- Middleware en aplicaciones web
- Sistemas de validación con múltiples validadores
- Manejadores de eventos en UI

**Estado**: _Próximamente_

---

### 2. Command (Comando)

**Problema que resuelve**: Necesitas parametrizar objetos con operaciones, encolar solicitudes o soportar deshacer.

**Solución**: Encapsula una solicitud como un objeto con toda la información necesaria.

**Casos de uso**:
- Sistemas de deshacer/rehacer
- Macros y scripts
- Menús y botones en UI
- Cola de tareas

**Estado**: _Próximamente_

---

### 3. Interpreter (Intérprete)

**Problema que resuelve**: Necesitas interpretar sentencias de un lenguaje especializado.

**Solución**: Define una representación de su gramática junto con un intérprete.

**Casos de uso**:
- Expresiones regulares
- Consultas SQL simples
- Lenguajes de configuración
- Calculadoras de expresiones

**Estado**: _Próximamente_

---

### 4. Iterator (Iterador)

**Problema que resuelve**: Necesitas acceder a elementos de una colección sin exponer su representación.

**Solución**: Proporciona una forma de acceder secuencialmente a los elementos.

**Casos de uso**:
- Recorrer listas, árboles, grafos
- Java Collections Framework
- C# IEnumerable
- JavaScript iterators

**Estado**: _Próximamente_

---

### 5. Mediator (Mediador)

**Problema que resuelve**: Múltiples objetos interactúan de manera compleja y directa.

**Solución**: Define un objeto que encapsula cómo interactúan un conjunto de objetos.

**Casos de uso**:
- Ventanas de diálogo con múltiples controles
- Sistemas de chat (chat room como mediator)
- Air traffic control
- Controladores MVC

**Estado**: _Próximamente_

---

### 6. Memento (Recuerdo)

**Problema que resuelve**: Necesitas guardar y restaurar el estado de un objeto sin violar encapsulación.

**Solución**: Captura y externaliza el estado interno de un objeto.

**Casos de uso**:
- Deshacer/Rehacer en editores
- Snapshots de bases de datos
- Puntos de guardado en videojuegos
- Transacciones

**Estado**: _Próximamente_

---

### 7. Observer (Observador)

**Problema que resuelve**: Múltiples objetos necesitan ser notificados cuando otro objeto cambia.

**Solución**: Define dependencia uno-a-muchos para que cuando un objeto cambie, todos sus dependientes sean notificados.

**Casos de uso**:
- Event listeners en UI
- Modelo-Vista en MVC
- Sistemas de notificaciones
- Pub/Sub messaging

**Estado**: _Próximamente_

---

### 8. State (Estado)

**Problema que resuelve**: El comportamiento de un objeto cambia según su estado.

**Solución**: Permite que un objeto altere su comportamiento cuando su estado interno cambia.

**Casos de uso**:
- Máquinas de estado (TCP connection)
- Estados de pedidos (pending, shipped, delivered)
- Estados de documentos (draft, review, published)
- Semáforos

**Estado**: _Próximamente_

---

### 9. [Strategy (Estrategia)](./Strategy/)

**Problema que resuelve**: Necesitas múltiples variantes de un algoritmo que son intercambiables.

**Solución**: Define una familia de algoritmos, encapsula cada uno y los hace intercambiables.

**Casos de uso**:
- Algoritmos de ordenamiento
- Sistemas de pago (tarjeta, PayPal, crypto)
- Compresión de archivos (zip, rar, gzip)
- Rutas de navegación (coche, a pie, bicicleta)

[📖 Ver documentación completa →](./Strategy/)

---

### 10. Template Method (Método Plantilla)

**Problema que resuelve**: Múltiples clases tienen algoritmos similares con pasos comunes.

**Solución**: Define el esqueleto de un algoritmo en una operación, delegando algunos pasos a subclases.

**Casos de uso**:
- Frameworks con hooks
- Procesamiento de datos con pasos fijos
- Algoritmos de ordenamiento con comparación customizable
- Ciclo de vida de componentes

**Estado**: _Próximamente_

---

### 11. Visitor (Visitante)

**Problema que resuelve**: Necesitas añadir operaciones a clases sin modificarlas.

**Solución**: Representa una operación a realizar sobre elementos de una estructura de objetos.

**Casos de uso**:
- Compiladores (AST traversal)
- Exportar datos a diferentes formatos
- Operaciones sobre estructuras Composite
- Reporting sobre estructuras complejas

**Estado**: _Próximamente_

---

## Principios SOLID y Patrones de Comportamiento

Los patrones de comportamiento están fuertemente relacionados con los principios SOLID:

### Single Responsibility Principle (SRP)
- **Strategy** separa algoritmos en clases independientes
- **Command** separa la invocación de la ejecución
- **Visitor** separa operaciones de la estructura

### Open/Closed Principle (OCP)
- **Strategy** permite añadir nuevos algoritmos sin modificar contexto
- **Chain of Responsibility** permite añadir nuevos manejadores
- **Visitor** permite añadir nuevas operaciones

### Liskov Substitution Principle (LSP)
- **Strategy** las estrategias son intercambiables
- **State** los estados son intercambiables

### Interface Segregation Principle (ISP)
- **Command** interfaces específicas por comando
- **Strategy** interfaces específicas por estrategia

### Dependency Inversion Principle (DIP)
- **Observer** sujetos y observadores dependen de abstracciones
- **Strategy** contexto depende de interfaz Strategy

---

## Guía de Selección de Patrón

### Diagrama de Decisión

```
¿Qué comportamiento necesitas?
    │
    ├─ ¿Múltiples manejadores para una solicitud?
    │   └─ Sí → CHAIN OF RESPONSIBILITY
    │
    ├─ ¿Encapsular operaciones como objetos?
    │   └─ Sí → COMMAND
    │
    ├─ ¿Interpretar un lenguaje?
    │   └─ Sí → INTERPRETER
    │
    ├─ ¿Recorrer una colección?
    │   └─ Sí → ITERATOR
    │
    ├─ ¿Coordinar interacciones complejas?
    │   └─ Sí → MEDIATOR
    │
    ├─ ¿Guardar/restaurar estado?
    │   └─ Sí → MEMENTO
    │
    ├─ ¿Notificar cambios a múltiples objetos?
    │   └─ Sí → OBSERVER
    │
    ├─ ¿Comportamiento depende del estado?
    │   └─ Sí → STATE
    │
    ├─ ¿Algoritmos intercambiables?
    │   └─ Sí → STRATEGY
    │
    ├─ ¿Algoritmo con pasos comunes?
    │   └─ Sí → TEMPLATE METHOD
    │
    └─ ¿Operaciones sobre estructura sin modificarla?
        └─ Sí → VISITOR
```

### Preguntas Clave por Escenario

#### Comunicación entre objetos
- **Uno notifica a muchos** → Observer
- **Muchos coordinados por uno** → Mediator
- **Uno pasa a siguiente en cadena** → Chain of Responsibility

#### Algoritmos y operaciones
- **Algoritmos intercambiables** → Strategy
- **Algoritmo con pasos fijos** → Template Method
- **Operaciones sobre estructura** → Visitor

#### Estado y comandos
- **Comportamiento según estado** → State
- **Operaciones como objetos** → Command
- **Guardar estado** → Memento

---

## Ejemplos de Uso Combinado

Los patrones de comportamiento suelen trabajar juntos:

### Command + Memento
```
Command puede usar Memento para implementar deshacer/rehacer,
guardando el estado antes de ejecutar el comando.
```

### Strategy + Factory Method
```
Factory Method puede crear las estrategias apropiadas
según el contexto o configuración.
```

### Observer + Mediator
```
Mediator puede usar Observer internamente para notificar
cambios entre los objetos que coordina.
```

### Chain of Responsibility + Command
```
Cada manejador en la cadena puede ser un Command,
permitiendo más flexibilidad.
```

### Visitor + Iterator
```
Visitor puede usar Iterator para recorrer estructuras
complejas aplicando operaciones.
```

### State + Strategy
```
State puede usar Strategy para implementar los algoritmos
específicos de cada estado.
```

---

## Matriz de Características

| Patrón | Desacoplamiento | Runtime Flex | Múltiples Objetos | Herencia |
|--------|-----------------|--------------|-------------------|----------|
| **Chain of Resp.** | ✓ | ✓ | ✓ | - |
| **Command** | ✓ | ✓ | ✓ | - |
| **Interpreter** | - | - | ✓ | ✓ |
| **Iterator** | ✓ | - | - | - |
| **Mediator** | ✓ | ✓ | ✓ | - |
| **Memento** | ✓ | - | - | - |
| **Observer** | ✓ | ✓ | ✓ | - |
| **State** | ✓ | ✓ | ✓ | - |
| **Strategy** | ✓ | ✓ | ✓ | - |
| **Template Method** | - | - | - | ✓ |
| **Visitor** | ✓ | - | ✓ | - |

---

## Estructura del Contenido

Cada patrón incluye:

- ✅ **Descripción detallada** del problema y solución
- ✅ **Diagramas UML** con Mermaid (clases y secuencia)
- ✅ **Implementaciones en múltiples lenguajes** (Java, C#, TypeScript)
- ✅ **Ejemplos prácticos** del mundo real
- ✅ **Ventajas y desventajas**
- ✅ **Casos de uso reales**
- ✅ **Relación con principios SOLID**
- ✅ **Ejercicios prácticos**

---

## Navegación

### Patrones por Categoría

- [📦 Patrones Creacionales](../Creacionales/)
- [📐 Patrones Estructurales](../Estructurales/)
- 🔄 **Patrones de Comportamiento** (estás aquí)
  - [Chain of Responsibility](./ChainOfResponsibility/)
  - [Command](./Command/)
  - [Interpreter](./Interpreter/)
  - [Iterator](./Iterator/)
  - [Mediator](./Mediator/)
  - [Memento](./Memento/)
  - [Observer](./Observer/)
  - [State](./State/)
  - [Strategy](./Strategy/)
  - [Template Method](./TemplateMethod/)
  - [Visitor](./Visitor/)

### Otros Recursos

- [📋 Tabla Completa de Patrones GoF](../DesignPatternsTableGOF.md)
- [📐 Repaso de UML](../UMLreview.md)

---

## Recursos Adicionales

### Libros Recomendados
- **"Design Patterns: Elements of Reusable Object-Oriented Software"** - Gang of Four
- **"Head First Design Patterns"** - Freeman & Freeman (excelente cobertura de Observer, Strategy, etc.)
- **"Refactoring to Patterns"** - Joshua Kerievsky

### Recursos Online
- [Refactoring Guru - Behavioral Patterns](https://refactoring.guru/design-patterns/behavioral-patterns)
- [SourceMaking - Behavioral Patterns](https://sourcemaking.com/design_patterns/behavioral_patterns)

---

## Conclusión

Los patrones de comportamiento son fundamentales para crear sistemas con comunicación clara y responsabilidades bien definidas. Dominar estos 11 patrones te permitirá:

- ✅ Diseñar sistemas con bajo acoplamiento
- ✅ Crear algoritmos flexibles e intercambiables
- ✅ Implementar comunicación efectiva entre objetos
- ✅ Gestionar estados y transiciones complejas
- ✅ Facilitar mantenimiento y extensión del código

**¡Explora cada patrón en detalle usando los enlaces de arriba!**

---

*Última actualización: Octubre 2025*
