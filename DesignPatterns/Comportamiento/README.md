# Patrones de Comportamiento

## Navegación

- [← Patrones Estructurales](../Estructurales/README.md)
- [→ Patrones Creacionales](../Creacionales/README.md)
- [→ Patrones de Diseño](../README.md)

## Contenido

Los patrones de comportamiento se centran en la comunicación entre objetos y la distribución de responsabilidades.

### Patrones Disponibles

- **[Chain of Responsibility](ChainOfResponsibility/README.md)** - Pasar solicitudes por una cadena
- **[Command](Command/README.md)** - Encapsular solicitudes como objetos
- **[Interpreter](Interpreter/README.md)** - Definir gramática e interpretar sentencias
- **[Iterator](Iterator/README.md)** - Acceso secuencial a elementos de colecciones
- **[Mediator](Mediator/README.md)** - Definir cómo interactúan objetos
- **[Memento](Memento/README.md)** - Capturar y restaurar estado interno
- **[Observer](Observer/README.md)** - Notificar cambios a múltiples objetos
- **[State](State/README.md)** - Alterar comportamiento según estado interno
- **[Strategy](Strategy/README.md)** - Definir familia de algoritmos intercambiables
- **[Template Method](TemplateMethod/README.md)** - Definir esqueleto de algoritmo
- **[Visitor](Visitor/README.md)** - Operaciones sobre estructura de objetos

## Características Comunes

### Problemas que Resuelven
- **Acoplamiento fuerte** - Dependencias rígidas entre objetos
- **Complejidad de interacción** - Comunicación compleja entre objetos
- **Rigidez algorítmica** - Algoritmos difíciles de modificar
- **Gestión de estado** - Control de estados complejos

### Beneficios
- **Desacoplamiento** - Objetos independientes
- **Flexibilidad** - Comportamiento intercambiable
- **Reutilización** - Algoritmos reutilizables
- **Mantenibilidad** - Código organizado y claro

## Implementaciones por Lenguaje

Cada patrón incluye implementaciones en:
- **C#** - Ejemplos con .NET
- **Java** - Ejemplos con Java SE
- **TypeScript** - Ejemplos para desarrollo web

## Cuándo Usar Patrones de Comportamiento

- Cuando necesites desacoplar objetos
- Cuando quieras hacer el comportamiento intercambiable
- Cuando necesites manejar estados complejos
- Cuando requieras comunicación flexible entre objetos

## Ejemplos Prácticos

### Observer
- Sistemas de notificaciones
- Modelo MVC
- Eventos en interfaces de usuario

### Strategy
- Algoritmos de ordenamiento
- Métodos de pago
- Estrategias de validación

### Command
- Sistemas de undo/redo
- Colas de comandos
- Operaciones asíncronas

### State
- Máquinas de estado
- Flujos de trabajo
- Juegos con estados

### Template Method
- Frameworks de procesamiento
- Algoritmos con pasos comunes
- Sistemas de validación

## Próximos Pasos

Continúa explorando:
- [Patrones Creacionales](../Creacionales/README.md) - Creación de objetos
- [Patrones Estructurales](../Estructurales/README.md) - Composición de clases y objetos
