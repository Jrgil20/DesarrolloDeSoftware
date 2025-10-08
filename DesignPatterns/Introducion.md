# Introducción: Patrones de Diseño

> “Un software capaz de evolucionar tiene que ser reutilizable (al menos para las versiones futuras).”

## El software cambia

Los requisitos, el entorno y las prioridades de un sistema evolucionan con el tiempo. Para anticiparse a esos cambios es necesario diseñar pensando en qué aspectos pueden variar y separar responsabilidades de forma que las modificaciones afecten lo mínimo posible.

Los patrones de diseño son soluciones probadas orientadas al cambio: proporcionan estructuras y guías para lidiar con variaciones previsibles en comportamiento, creación y organización del software.

## Clasificación de patrones

- Patrones arquitecturales  
    Expresan un paradigma fundamental para estructurar un sistema software. Proporcionan un conjunto de subsistemas predefinidos, con reglas y guías para organizar las relaciones entre ellos. Ej.: MVC, Microservicios, Event-Driven Architecture.

- Patrones de diseño  
    Compuestos por varias unidades arquitecturales más pequeñas. Describen esquemas para estructurar subsistemas y componentes, resolviendo problemas recurrentes de interacción, creación y comportamiento. Ej.: Factory, Observer, Strategy.

- Patrones elementales (idioms)  
    Específicos de un lenguaje o plataforma. Describen cómo implementar componentes particulares de un patrón utilizando construcciones, convenciones y técnicas propias del lenguaje. Ej.: gestión de recursos en C++, RAII; uso de traits o mixins en lenguajes específicos.

## Resumen

Diseñar pensando en el cambio implica identificar puntos de variación y aplicar patrones adecuados para encapsularlos. La correcta elección entre patrón arquitectural, de diseño o idiomático depende del alcance del problema: sistema, subsistema o detalles de implementación.

## EJEMPLOS DE PATRONES

### Patrones arquitecturales

- Jerarquía de capas  
- Tuberías y filtros  
- Cliente/Servidor  
- Maestro‑Esclavo  
- Control centralizado y distribuido

### Patrones de diseño

- Proxies  
- Factorías (Factory)  
- Adaptadores (Adapter)  
- Composición  
- Broker

### Patrones elementales (idioms)

- Modularidad  
- Interfaces mínimas  
- Encapsulación  
- Objetos  
- Acciones y eventos  
- Concurrencia

---

## CATEGORÍAS DE LOS PATRONES DE DISEÑO

### Patrones Creacionales

Tratan de la inicialización y configuración de clases y objetos. Ej.: Factory Method, Abstract Factory, Builder, Singleton.

### Patrones Estructurales

Tratan de desacoplar interfaz e implementación; cómo se componen clases y objetos. Ej.: Adapter, Decorator, Facade, Proxy.

### Patrones de Comportamiento

Tratan de las interacciones dinámicas entre colectivos de clases y objetos; distribución de responsabilidades. Ej.: Observer, Strategy, Command, Mediator.

---

## CÓMO SELECCIONAR UN PATRÓN DE DISEÑO

1. Entender el problema  
    - Define claramente qué necesitas resolver: ¿creación, estructura o comportamiento?  
    - Identifica restricciones (tiempo, rendimiento, complejidad).  
    - Analiza requisitos: ¿necesitas flexibilidad, escalabilidad o extensibilidad?  
    - Ejemplos: limitar instancias → Singleton; notificar varios objetos → Observer.

2. Clasifica el problema  
    - Creacional: relacionado con la creación de objetos (usar Factory, Builder, Abstract Factory).  
    - Estructural: relacionado con la organización y la reutilización (usar Adapter, Facade, Decorator).  
    - Comportamiento: relacionado con comunicación y responsabilidades (usar Observer, Strategy, Command).

3. Preguntas clave para selección  
    - Creacionales: ¿controlar creación? → Factory/Abstract Factory. ¿Una sola instancia? → Singleton. ¿Construcción paso a paso? → Builder.  
    - Estructurales: ¿conectar interfaces incompatibles? → Adapter. ¿Añadir comportamiento dinámico? → Decorator. ¿Simplificar interfaz? → Facade.  
    - Comportamiento: ¿notificar múltiples observadores? → Observer. ¿Permitir algoritmos intercambiables? → Strategy. ¿Encapsular acciones? → Command.

4. Considera el contexto  
    - Tamaño del proyecto: en proyectos pequeños evita patrones innecesarios.  
    - Complejidad del sistema: en sistemas distribuidos o grandes, algunos patrones facilitan la gestión.  
    - Frecuencia del problema: si aparece repetidamente, aplicar un patrón suele compensar.

5. Evaluación de ventajas y desventajas  
    - Evalúa el coste y la complejidad añadida por el patrón.  
    - Prefiere la solución más simple que resuelva el problema: evita el overengineering.