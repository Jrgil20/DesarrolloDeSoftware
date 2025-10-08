# Patrones de Diseño Estructurales

## Introducción

Los **patrones estructurales** se encargan de cómo componer clases y objetos para formar estructuras más grandes y flexibles. Estos patrones explican cómo ensamblar objetos y clases en estructuras más complejas manteniendo estas estructuras flexibles y eficientes.

### ¿Por qué usar patrones estructurales?

- **Composición flexible**: Facilitan la composición de objetos y clases
- **Simplificación**: Simplifican la estructura haciendo que las relaciones sean más fáciles de identificar
- **Desacoplamiento**: Reducen el acoplamiento entre componentes
- **Reutilización**: Promueven la reutilización de código existente
- **Extensibilidad**: Permiten extender funcionalidades sin modificar código existente

---

## Los 7 Patrones Estructurales del GoF

### Resumen Rápido

| Patrón | Propósito Principal | Cuándo Usarlo |
|--------|---------------------|---------------|
| **Adapter** | Permite que interfaces incompatibles trabajen juntas | Cuando necesitas usar una clase con interfaz incompatible |
| [**Composite**](./Composite/) | Compone objetos en estructuras de árbol | Cuando necesitas tratar objetos individuales y composiciones uniformemente |
| **Bridge** | Separa abstracción de implementación | Cuando quieres evitar una explosión de subclases |
| **Decorator** | Añade responsabilidades a objetos dinámicamente | Cuando necesitas extender funcionalidades sin usar herencia |
| [**Facade**](./Facade/) | Proporciona interfaz simplificada a un subsistema | Cuando quieres simplificar el acceso a un sistema complejo |
| **Flyweight** | Optimiza memoria compartiendo objetos | Cuando tienes muchos objetos similares y quieres reducir memoria |
| [**Proxy**](./Proxy/) | Proporciona sustituto o marcador de posición | Cuando necesitas controlar el acceso a un objeto |

---

## Comparación de Patrones Estructurales

### Adapter vs Bridge

- **Adapter**: Hace que interfaces incompatibles trabajen juntas (reactivo)
- **Bridge**: Separa abstracción e implementación desde el diseño (proactivo)

### Decorator vs Proxy

- **Decorator**: Añade nuevas responsabilidades a un objeto
- **Proxy**: Controla el acceso a un objeto

### Composite vs Decorator

- **Composite**: Representa jerarquías parte-todo
- **Decorator**: Añade responsabilidades sin jerarquía fija

### Facade vs Mediator

- **Facade**: Simplifica interfaz de un subsistema (unidireccional)
- **Mediator**: Coordina comunicación entre objetos (bidireccional)

---

## Patrones Estructurales en Detalle

### 1. Adapter (Adaptador)

**Problema que resuelve**: Necesitas usar una clase cuya interfaz no coincide con la que necesitas.

**Solución**: Crea una clase adaptadora que traduce la interfaz de una clase a otra.

**Casos de uso**:

- Integrar bibliotecas de terceros
- Trabajar con APIs legadas
- Sistemas de logging con diferentes interfaces

**Estado**: _Próximamente_

---

### 2. [Composite (Compuesto)](./Composite/)

**Problema que resuelve**: Necesitas tratar objetos individuales y composiciones de manera uniforme.

**Solución**: Compone objetos en estructuras de árbol donde tanto hojas como composites implementan la misma interfaz.

**Casos de uso**:
- Sistemas de archivos (archivos y carpetas)
- Interfaces gráficas (componentes y contenedores)
- Estructuras organizacionales (empleados y gerentes)

[📖 Ver documentación completa →](./Composite/)

---

### 3. Bridge (Puente)

**Problema que resuelve**: Evitar una explosión de subclases cuando tienes múltiples dimensiones de variación.

**Solución**: Separa una abstracción de su implementación para que puedan variar independientemente.

**Casos de uso**:
- Interfaces gráficas multiplataforma
- Drivers de bases de datos
- Sistemas de renderizado (formas + colores)

**Estado**: _Próximamente_

---

### 4. Decorator (Decorador)

**Problema que resuelve**: Necesitas añadir responsabilidades a objetos sin afectar a otros objetos.

**Solución**: Envuelve un objeto con otro que añade funcionalidad, manteniendo la misma interfaz.

**Casos de uso**:
- Streams de I/O (BufferedReader, FileReader)
- Componentes de UI con efectos (scroll, border)
- Middleware en aplicaciones web

**Estado**: _Próximamente_

---

### 5. [Facade (Fachada)](./Facade/)

**Problema que resuelve**: Un subsistema complejo es difícil de usar.

**Solución**: Proporciona una interfaz unificada y simplificada a un conjunto de interfaces de un subsistema.

**Casos de uso**:
- APIs de alto nivel sobre sistemas complejos
- Simplificar interacciones con bibliotecas
- Puntos de entrada únicos a subsistemas

[📖 Ver documentación completa →](./Facade/)

---

### 6. Flyweight (Peso Ligero)

**Problema que resuelve**: Muchos objetos similares consumen demasiada memoria.

**Solución**: Comparte objetos comunes entre múltiples contextos para reducir uso de memoria.

**Casos de uso**:
- Renderizado de caracteres en editores
- Partículas en videojuegos
- Objetos de cache compartidos

**Estado**: _Próximamente_

---

### 7. [Proxy (Apoderado)](./Proxy/)

**Problema que resuelve**: Necesitas controlar el acceso a un objeto.

**Solución**: Proporciona un sustituto que controla el acceso al objeto real.

**Tipos**:
- **Virtual Proxy**: Creación perezosa de objetos costosos
- **Remote Proxy**: Representante local de objeto remoto
- **Protection Proxy**: Control de acceso basado en permisos
- **Caching Proxy**: Almacena resultados de operaciones costosas
- **Smart Reference**: Acciones adicionales al acceder al objeto

**Casos de uso**:
- Lazy loading de imágenes
- Proxies de bases de datos remotas
- Control de acceso y seguridad

[📖 Ver documentación completa →](./Proxy/)

---

## Principios SOLID y Patrones Estructurales

Los patrones estructurales están fuertemente relacionados con los principios SOLID:

### Single Responsibility Principle (SRP)
- **Adapter** separa la adaptación de la lógica de negocio
- **Facade** encapsula complejidad en una interfaz simple

### Open/Closed Principle (OCP)
- **Decorator** permite añadir funcionalidad sin modificar código existente
- **Composite** permite añadir nuevos componentes sin cambiar la estructura

### Liskov Substitution Principle (LSP)
- **Proxy** puede sustituir al objeto real completamente
- **Decorator** mantiene la interfaz del objeto decorado

### Dependency Inversion Principle (DIP)
- **Bridge** separa abstracción de implementación
- **Adapter** permite depender de interfaces estables

---

## Guía de Selección de Patrón

### Diagrama de Decisión

```
¿Qué problema necesitas resolver?
    │
    ├─ ¿Interfaces incompatibles?
    │   └─ Sí → ADAPTER
    │
    ├─ ¿Jerarquía parte-todo?
    │   └─ Sí → COMPOSITE
    │
    ├─ ¿Múltiples dimensiones de variación?
    │   └─ Sí → BRIDGE
    │
    ├─ ¿Añadir funcionalidad dinámicamente?
    │   └─ Sí → DECORATOR
    │
    ├─ ¿Simplificar subsistema complejo?
    │   └─ Sí → FACADE
    │
    ├─ ¿Optimizar memoria con objetos similares?
    │   └─ Sí → FLYWEIGHT
    │
    └─ ¿Controlar acceso a objeto?
        └─ Sí → PROXY
```

### Preguntas Clave

1. **"¿Necesito adaptar interfaces?"**
   - Sí → **Adapter**

2. **"¿Tengo una estructura de árbol?"**
   - Sí, y quiero tratarla uniformemente → **Composite**

3. **"¿Necesito desacoplar abstracción e implementación?"**
   - Sí → **Bridge**

4. **"¿Quiero añadir responsabilidades sin herencia?"**
   - Sí → **Decorator**

5. **"¿El sistema es demasiado complejo?"**
   - Sí, y necesito simplificar → **Facade**

6. **"¿Tengo problemas de memoria?"**
   - Sí, con muchos objetos similares → **Flyweight**

7. **"¿Necesito controlar acceso?"**
   - Sí → **Proxy**

---

## Ejemplos de Uso Combinado

Los patrones estructurales suelen trabajar juntos:

### Composite + Iterator
```
Un Composite puede usar Iterator para recorrer sus elementos
de manera uniforme sin exponer su estructura interna.
```

### Decorator + Strategy
```
Decorator puede usar Strategy para determinar qué decoración
aplicar en tiempo de ejecución.
```

### Facade + Singleton
```
Una Facade suele implementarse como Singleton para proporcionar
un punto de acceso único al subsistema.
```

### Adapter + Facade
```
Adapter puede usar Facade internamente para simplificar
la adaptación de un subsistema complejo.
```

### Proxy + Decorator
```
Ambos usan composición y la misma interfaz, pero con diferentes
intenciones (control vs. extensión).
```

---

## Matriz de Características

| Patrón | Composición | Herencia | Múltiples Objetos | Cambio Dinámico |
|--------|-------------|----------|-------------------|-----------------|
| **Adapter** | ✓ | ✓ | - | - |
| **Composite** | ✓ | - | ✓ | - |
| **Bridge** | ✓ | ✓ | - | ✓ |
| **Decorator** | ✓ | - | ✓ | ✓ |
| **Facade** | ✓ | - | ✓ | - |
| **Flyweight** | ✓ | - | ✓ | - |
| **Proxy** | ✓ | - | - | - |

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
- 📐 **Patrones Estructurales** (estás aquí)
  - [Composite](./Composite/)
  - [Facade](./Facade/)
  - [Proxy](./Proxy/)
  - Adapter _(próximamente)_
  - Bridge _(próximamente)_
  - Decorator _(próximamente)_
  - Flyweight _(próximamente)_
- [🔄 Patrones de Comportamiento](../Comportamiento/)

### Otros Recursos

- [📋 Tabla Completa de Patrones GoF](../DesignPatternsTableGOF.md)
- [📐 Repaso de UML](../UMLreview.md)

---

## Recursos Adicionales

### Libros Recomendados
- **"Design Patterns: Elements of Reusable Object-Oriented Software"** - Gang of Four
- **"Head First Design Patterns"** - Freeman & Freeman
- **"Refactoring to Patterns"** - Joshua Kerievsky

### Recursos Online
- [Refactoring Guru - Structural Patterns](https://refactoring.guru/design-patterns/structural-patterns)
- [SourceMaking - Structural Patterns](https://sourcemaking.com/design_patterns/structural_patterns)

---

## Conclusión

Los patrones estructurales son fundamentales para crear sistemas flexibles y mantenibles. Dominar estos 7 patrones te permitirá:

- ✅ Diseñar sistemas más flexibles y extensibles
- ✅ Reducir el acoplamiento entre componentes
- ✅ Simplificar estructuras complejas
- ✅ Optimizar el uso de recursos
- ✅ Facilitar la integración de componentes

**¡Explora cada patrón en detalle usando los enlaces de arriba!**

---

*Última actualización: Octubre 2025*
