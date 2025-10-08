# Patrones de Diseño Creacionales

## Introducción

Los **patrones creacionales** se encargan de optimizar y gestionar los mecanismos de creación de objetos. Estos patrones abstraen el proceso de instanciación, haciendo que el sistema sea independiente de cómo sus objetos son creados, compuestos y representados.

### ¿Por qué usar patrones creacionales?

- **Flexibilidad**: Permiten que el código sea más flexible al delegar la responsabilidad de creación
- **Reutilización**: Promueven la reutilización de código existente
- **Desacoplamiento**: Separan la lógica de creación de la lógica de negocio
- **Control**: Proporcionan mayor control sobre el proceso de instanciación
- **Encapsulación**: Ocultan la complejidad de la creación de objetos

---

## Los 5 Patrones Creacionales del GoF

### Resumen Rápido

| Patrón | Propósito Principal | Cuándo Usarlo |
|--------|---------------------|---------------|
| [**Factory Method**](./FactoryMethod/) | Define una interfaz para crear objetos, dejando a las subclases decidir qué clase instanciar | Cuando no sabes de antemano los tipos exactos de objetos que necesitas crear |
| [**Abstract Factory**](./AbstractFactory/) | Proporciona una interfaz para crear familias de objetos relacionados sin especificar sus clases concretas | Cuando necesitas crear familias de objetos que deben ser compatibles entre sí |
| [**Singleton**](./Singleton/) | Garantiza que una clase tenga una única instancia y proporciona un punto de acceso global | Cuando necesitas exactamente una instancia de una clase en toda la aplicación |
| [**Builder**](./Builder/) | Separa la construcción de un objeto complejo de su representación | Cuando necesitas crear objetos complejos paso a paso con múltiples configuraciones |
| [**Prototype**](./Prototype/) | Crea nuevos objetos clonando instancias existentes | Cuando crear un objeto es costoso y prefieres clonar uno existente |

---

## Comparación de Patrones Creacionales

### Factory Method vs Abstract Factory

- **Factory Method**: Se enfoca en crear UN tipo de objeto a través de herencia
- **Abstract Factory**: Se enfoca en crear FAMILIAS de objetos relacionados a través de composición

### Singleton vs Prototype

- **Singleton**: Garantiza UNA única instancia
- **Prototype**: Crea MÚLTIPLES instancias clonando un prototipo

### Builder vs Factory Method

- **Builder**: Construcción paso a paso de objetos complejos con muchas opciones
- **Factory Method**: Creación simple de objetos mediante una interfaz estándar

---

## Patrones Creacionales en Detalle

### 1. [Factory Method](./FactoryMethod/)

**Problema que resuelve**: Necesitas crear objetos sin especificar la clase exacta del objeto que se creará.

**Solución**: Define un método en una clase base que las subclases sobrescriben para crear instancias específicas.

**Casos de uso**:
- Frameworks que necesitan delegar la creación de objetos a subclases
- Sistemas con múltiples tipos de documentos (PDF, Word, Excel)
- Gestión de diferentes tipos de conexiones (MySQL, PostgreSQL, MongoDB)

[📖 Ver documentación completa →](./FactoryMethod/)

---

### 2. [Abstract Factory](./AbstractFactory/)

**Problema que resuelve**: Necesitas crear familias de objetos relacionados que deben funcionar juntos sin especificar sus clases concretas.

**Solución**: Proporciona una interfaz que agrupa múltiples factory methods relacionados.

**Casos de uso**:
- Interfaces de usuario multiplataforma (Windows, macOS, Linux)
- Temas visuales (claro, oscuro, alto contraste)
- Sistemas de bases de datos con diferentes proveedores

[📖 Ver documentación completa →](./AbstractFactory/)

---

### 3. [Singleton](./Singleton/)

**Problema que resuelve**: Necesitas asegurar que una clase tenga solo una instancia y proporcionar acceso global a ella.

**Solución**: Controla su propia instanciación mediante un método estático y constructor privado.

**Casos de uso**:
- Gestores de configuración global
- Pool de conexiones a base de datos
- Loggers centralizados
- Caché global

[📖 Ver documentación completa →](./Singleton/)

---

### 4. [Builder](./Builder/)

**Problema que resuelve**: Necesitas construir objetos complejos paso a paso con múltiples configuraciones opcionales.

**Solución**: Separa la construcción del objeto de su representación mediante una clase builder.

**Casos de uso**:
- Construcción de objetos con muchos parámetros opcionales
- Creación de consultas SQL complejas
- Generación de documentos (HTML, XML, JSON)
- Configuración de objetos complejos (HttpClient, DatabaseConnection)

[📖 Ver documentación completa →](./Builder/)

---

### 5. [Prototype](./Prototype/)

**Problema que resuelve**: Necesitas crear nuevos objetos copiando instancias existentes cuando la creación directa es costosa.

**Solución**: Clona objetos existentes en lugar de crearlos desde cero.

**Casos de uso**:
- Objetos con configuración compleja que se reutilizan
- Sistemas de edición gráfica (copiar formas, elementos)
- Carga de objetos desde base de datos que se reutilizan
- Configuraciones predefinidas de aplicaciones

[📖 Ver documentación completa →](./Prototype/)

---

## Principios SOLID y Patrones Creacionales

Los patrones creacionales están fuertemente relacionados con los principios SOLID:

### Single Responsibility Principle (SRP)
- **Factory Method**, **Abstract Factory** y **Builder** separan la lógica de creación de la lógica de negocio

### Open/Closed Principle (OCP)
- Todos los patrones creacionales permiten añadir nuevos tipos sin modificar código existente

### Dependency Inversion Principle (DIP)
- **Factory Method** y **Abstract Factory** permiten depender de abstracciones en lugar de implementaciones concretas

---

## Guía de Selección de Patrón

### Diagrama de Decisión

```
¿Necesitas crear objetos?
    │
    ├─ ¿Solo UNA instancia en toda la app?
    │   └─ Sí → SINGLETON
    │
    ├─ ¿Crear objetos CLONANDO uno existente?
    │   └─ Sí → PROTOTYPE
    │
    ├─ ¿Construcción PASO A PASO de objeto complejo?
    │   └─ Sí → BUILDER
    │
    ├─ ¿Crear FAMILIAS de objetos relacionados?
    │   └─ Sí → ABSTRACT FACTORY
    │
    └─ ¿Delegar creación a SUBCLASES?
        └─ Sí → FACTORY METHOD
```

### Preguntas Clave

1. **"¿Cuántas instancias necesito?"**
   - Una única → **Singleton**
   - Múltiples → Otros patrones

2. **"¿Es compleja la construcción?"**
   - Muy compleja, muchas opciones → **Builder**
   - Simple → **Factory Method**

3. **"¿Necesito familias de objetos?"**
   - Sí, objetos que funcionan juntos → **Abstract Factory**
   - No → **Factory Method**

4. **"¿Puedo clonar en lugar de crear?"**
   - Sí, y es más eficiente → **Prototype**
   - No → Otros patrones

---

## Ejemplos de Uso Combinado

Los patrones creacionales suelen trabajar juntos:

### Builder + Singleton
```
Un Builder puede usar un Singleton para acceder a configuración global
durante la construcción de objetos complejos.
```

### Abstract Factory + Singleton
```
Cada familia de factories puede implementarse como Singleton para
asegurar consistencia en toda la aplicación.
```

### Factory Method + Prototype
```
Un Factory Method puede crear objetos clonando prototipos en lugar
de instanciarlos directamente.
```

---

## Estructura del Contenido

Cada patrón incluye:

- ✅ **Descripción detallada** del problema y solución
- ✅ **Diagramas UML** de clase y secuencia
- ✅ **Implementaciones en múltiples lenguajes** (Java, Python, TypeScript, etc.)
- ✅ **Ejemplos prácticos** del mundo real
- ✅ **Ventajas y desventajas**
- ✅ **Casos de uso reales**
- ✅ **Relación con principios SOLID**
- ✅ **Ejercicios prácticos**

---

## Navegación

### Patrones por Categoría

📁 **Patrones Creacionales** (estás aquí)
- [Factory Method](./FactoryMethod/)
- [Abstract Factory](./AbstractFactory/)
- [Singleton](./Singleton/)
- [Builder](./Builder/)
- [Prototype](./Prototype/)

### Otras Categorías

- [📋 Tabla Completa de Patrones GoF](../DesignPatternsTableGOF.md)
- [📐 Repaso de UML](../UMLreview.md)
- [🏗️ Patrones Estructurales](../Estructurales/) _(próximamente)_
- [🔄 Patrones de Comportamiento](../Comportamiento/) _(próximamente)_

---

## Recursos Adicionales

### Libros Recomendados
- **"Design Patterns: Elements of Reusable Object-Oriented Software"** - Gang of Four
- **"Head First Design Patterns"** - Freeman & Freeman
- **"Refactoring to Patterns"** - Joshua Kerievsky

### Recursos Online
- [Refactoring Guru - Design Patterns](https://refactoring.guru/design-patterns)
- [SourceMaking - Design Patterns](https://sourcemaking.com/design_patterns)

---

## Conclusión

Los patrones creacionales son fundamentales para escribir código flexible, mantenible y escalable. Dominar estos 5 patrones te permitirá:

- ✅ Escribir código más desacoplado y testeable
- ✅ Facilitar el mantenimiento y extensión del sistema
- ✅ Aplicar mejores prácticas de diseño orientado a objetos
- ✅ Comunicarte efectivamente con otros desarrolladores

**¡Comienza explorando cada patrón en detalle usando los enlaces de arriba!**

---

*Última actualización: Octubre 2025*
