# Tabla de Patrones de Diseño GoF

## Patrones Creacionales

| Patrón | Propósito | Problema Clave | Uso Común |
|--------|-----------|----------------|-----------|
| **Factory Method** | Define una interfaz para crear objetos, dejando a las subclases la implementación. | Necesitas crear objetos sin especificar su clase concreta. | Crear objetos polimórficos según el contexto. Ej.: Gestión de tipos de pedidos (Auto, Health). |
| **Abstract Factory** | Proporciona una interfaz para crear familias de objetos relacionados. | Necesitas garantizar que un grupo de objetos sean compatibles entre sí. | Sistemas de interfaz de usuario para distintas plataformas (Windows, macOS). |
| **Singleton** | Garantiza una única instancia de una clase con acceso global controlado. | Necesitas una única instancia global para un recurso compartido. | Configuraciones globales, gestores de conexión a bases de datos. |
| **Builder** | Construye un objeto complejo paso a paso. | Necesitas crear objetos complejos con múltiples configuraciones posibles. | Construcción de documentos, objetos JSON o estructuras jerárquicas complejas. |
| **Prototype** | Crea objetos clonando instancias existentes. | Necesitas crear copias de objetos configurados previamente. | Clonar objetos con configuraciones personalizadas, como formularios prellenados. |

## Patrones Estructurales

| Patrón | Propósito | Problema Clave | Uso Común |
|--------|-----------|----------------|-----------|
| **Adapter** | Permite la interacción entre clases con interfaces incompatibles. | Tienes clases que no pueden comunicarse debido a diferencias en sus interfaces. | Integrar una API antigua con un sistema nuevo. |
| **Bridge** | Separa una abstracción de su implementación para que ambas puedan variar independientemente. | Deseas mantener abstracciones e implementaciones desacopladas. | Sistemas de renderizado gráfico, donde las abstracciones (formas) y las implementaciones (librerías de dibujo) cambian de manera independiente. |
| **Composite** | Compone objetos en estructuras jerárquicas para tratarlos como un único objeto. | Necesitas tratar colecciones de objetos y objetos individuales de manera uniforme. | Árboles jerárquicos como menús, sistemas de archivos o estructuras XML. |
| **Decorator** | Añade responsabilidades a objetos dinámicamente sin modificar su estructura. | Deseas extender funcionalidades sin alterar clases base. | Extender el comportamiento de UI (añadir bordes, colores, scroll) o funciones de log en sistemas existentes. |
| **Facade** | Proporciona una interfaz simplificada para un subsistema complejo. | Deseas simplificar el uso de un sistema con muchas interacciones complejas. | Simplificar interacciones con sistemas como bases de datos o APIs. |
| **Flyweight** | Optimiza el uso de memoria compartiendo objetos que son intrínsecamente iguales. | Deseas reducir el uso de memoria para objetos similares. | Representar caracteres o nodos gráficos reutilizables. |
| **Proxy** | Proporciona un sustituto para controlar el acceso a otro objeto. | Necesitas controlar el acceso a un objeto (remoto, pesado o seguro). | Control de acceso, almacenamiento en caché, o proxies remotos. |

## Patrones De Comportamiento

| Patrón | Propósito | Problema Clave | Uso Común |
|--------|-----------|----------------|-----------|
| **Chain of Responsibility** | Pasa una solicitud a través de una cadena de manejadores potenciales. | Necesitas múltiples manejadores que puedan procesar una solicitud en cadena. | Procesar eventos en sistemas como validaciones o manejadores de errores. |
| **Command** | Encapsula una solicitud como un objeto, permitiendo colas o deshacer acciones. | Deseas desacoplar el emisor y el receptor de solicitudes. | Sistemas de menús o deshacer/rehacer en aplicaciones. |
| **Interpreter** | Define una gramática y un intérprete para evaluar sentencias en un lenguaje. | Necesitas procesar y evaluar sentencias de un lenguaje especializado. | Lenguajes de consulta personalizados o procesamiento de expresiones matemáticas. |
| **Iterator** | Proporciona una forma de acceder secuencialmente a elementos en una colección. | Deseas recorrer colecciones sin exponer su implementación interna. | Recorrer listas, árboles o estructuras de datos complejas. |
| **Mediator** | Coordina interacciones entre objetos para reducir dependencias directas. | Tienes múltiples objetos que interactúan de forma compleja y directa. | Sistemas de UI o chats con múltiples usuarios. |
| **Memento** | Guarda y restaura el estado interno de un objeto sin violar su encapsulación. | Deseas implementar "deshacer" en tu sistema. | Sistemas de edición con deshacer/rehacer (editores de texto, gráficas, etc.). |
| **Observer** | Notifica automáticamente a múltiples objetos cuando otro cambia de estado. | Deseas mantener sincronizados a múltiples objetos relacionados. | Sistemas de notificaciones, eventos de UI o modelos de publicación/suscripción. |
| **State** | Cambia el comportamiento de un objeto según su estado interno. | Necesitas un comportamiento diferente en función del estado. | Máquinas de estado como semáforos o autenticación. |
| **Strategy** | Permite cambiar algoritmos en tiempo de ejecución. | Deseas usar diferentes algoritmos para resolver un problema. | Algoritmos de ordenación, compresión o encriptación. |
| **Template Method** | Define un esqueleto de algoritmo en una clase base, delegando pasos específicos a subclases. | Deseas reutilizar la estructura general de un algoritmo. | Procesamiento de datos, donde partes del flujo cambian según el contexto. |
| **Visitor** | Permite definir nuevas operaciones sobre una estructura de objetos sin cambiar su clase. | Deseas añadir operaciones a estructuras de datos existentes. | Analizadores sintácticos o sistemas de reportes. |

---

## Cómo usar esta tabla

1. **Identifica el problema**: ¿Qué aspecto deseas resolver (creación, estructura o comportamiento)?
2. **Busca patrones con problemas clave similares**.
3. **Revisa el propósito y los casos de uso** para elegir el más adecuado.

---

## Ejemplos Prácticos

### Problema Ejemplo 1: Necesitas procesar pedidos en una jerarquía

**Descripción del problema:**

Estás construyendo un sistema de procesamiento de pedidos en el que los pedidos pueden contener otros sub-pedidos (por ejemplo, un pedido principal puede contener productos individuales o incluso otros pedidos como subcomponentes). Necesitas tratar tanto los pedidos individuales como los compuestos de la misma manera.

**Solución: Patrón Composite**

De la tabla:
- **Propósito**: Componer objetos en estructuras jerárquicas para tratarlos como un único objeto.
- **Problema Clave**: Necesitas tratar colecciones de objetos y objetos individuales de manera uniforme.
- **Uso Común**: Árboles jerárquicos como sistemas de archivos o estructuras de menús.

---

### Problema Ejemplo 2: Necesitas implementar un sistema de notificaciones

**Descripción del problema:**

Estás desarrollando un sistema donde varios usuarios se suscriben a eventos como "promociones" y "actualizaciones". Los usuarios deben ser notificados automáticamente cuando ocurra un evento en el canal al que están suscritos.

**Solución: Patrón Observer**

De la tabla:
- **Propósito**: Notifica automáticamente a múltiples objetos cuando otro cambia de estado.
- **Problema Clave**: Deseas mantener sincronizados a múltiples objetos relacionados.
- **Uso Común**: Sistemas de notificaciones, eventos de UI o modelos de publicación/suscripción.

---

## Resumen de Categorías

- **Patrones Creacionales (5)**: Se enfocan en cómo se crean los objetos.
- **Patrones Estructurales (7)**: Se enfocan en cómo se componen y relacionan las clases y objetos.
- **Patrones De Comportamiento (11)**: Se enfocan en la comunicación y responsabilidades entre objetos.

**Total: 23 Patrones de Diseño GoF**

