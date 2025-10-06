# SOLID Principles

Los principios SOLID son un conjunto de cinco principios de diseño orientado a objetos que ayudan a los desarrolladores a crear software más mantenible, flexible y escalable. Estos principios fueron popularizados por Robert C. Martin (también conocido como "Uncle Bob") y son fundamentales para el desarrollo de software de alta calidad.

1. **S** - Single Responsibility Principle (SRP): Un módulo o clase debe tener una única razón para cambiar, lo que significa que debe tener una única responsabilidad o función.

2. **O** - Open/Closed Principle (OCP): Los módulos o clases deben estar abiertos a la extensión pero cerrados a la modificación. Esto se puede lograr mediante la herencia y la implementación de interfaces.

3. **L** - Liskov Substitution Principle (LSP): Los objetos de una clase derivada deben poder reemplazar a los objetos de la clase base sin alterar el comportamiento del programa.

4. **I** - Interface Segregation Principle (ISP): Los clientes no deben verse obligados a depender de interfaces que no utilizan. Es mejor tener varias interfaces específicas en lugar de una única interfaz general.

5. **D** - Dependency Inversion Principle (DIP): Las dependencias deben ser abstraídas. Los módulos de alto nivel no deben depender de módulos de bajo nivel, ambos deben depender de abstracciones (interfaces).

Estos principios son esenciales para el diseño de software orientado a objetos y ayudan a los desarrolladores a crear sistemas que son más fáciles de entender, mantener y escalar con el tiempo.

## Por qué son importantes los principios SOLID

Los principios SOLID son importantes porque:

- Mejoran la mantenibilidad del código, permitiendo que los cambios sean localizados y reduciendo el “efecto dominó” en el sistema.
- Incrementan la testabilidad, ya que las clases están más aisladas y las dependencias pueden ser fácilmente intercambiadas por mocks o stubs.
- Fomentan la extensibilidad, facilitando la incorporación de nuevas funcionalidades sin romper el código existente (gracias a OCP y DIP).
- Mejoran la legibilidad y cohesión del sistema, asegurando que las responsabilidades estén claramente definidas (SRP e ISP).
- Disminuyen el acoplamiento y aumentan la reutilización, promoviendo un código más modular y flexible.
- Facilitan la comprensión del código, haciendo que sea más sencillo para nuevos desarrolladores entender la estructura y el propósito del sistema.
- Ayudan a crear sistemas más robustos y menos propensos a errores.

## Conceptos Preliminares

Antes de profundizar en cada principio SOLID, es importante entender algunos conceptos fundamentales que nos ayudarán a comprender mejor su aplicación:

### Acoplamiento y Cohesión

**Acoplamiento** se refiere al grado de dependencia entre diferentes módulos o clases. Un bajo acoplamiento es deseable porque:

- Facilita el mantenimiento: los cambios en un módulo tienen menor impacto en otros
- Mejora la testabilidad: cada módulo puede probarse de forma más independiente
- Aumenta la reutilización: los módulos pueden usarse en diferentes contextos

**Cohesión** mide qué tan relacionadas están las responsabilidades dentro de un módulo o clase. Una alta cohesión es deseable porque:

- Hace el código más comprensible: todas las funciones están relacionadas con un propósito común
- Facilita el mantenimiento: los cambios relacionados se concentran en un lugar
- Mejora la reutilización: módulos con propósito claro son más fáciles de reutilizar

### Abstracción vs Implementación

**Abstracción** define *qué* hace algo (el contrato), mientras que la **implementación** define *cómo* lo hace (los detalles). Esta separación es clave porque:

- Permite cambiar implementaciones sin afectar el código cliente
- Facilita el testing mediante el uso de mocks y stubs
- Mejora la flexibilidad del sistema al permitir múltiples implementaciones

### Inversión de Control (IoC)

La **Inversión de Control** es un patrón donde el control del flujo del programa se invierte. En lugar de que un objeto cree sus dependencias directamente, estas son proporcionadas desde el exterior. Esto:

- Reduce el acoplamiento entre clases
- Facilita el testing al permitir inyectar dependencias falsas
- Mejora la flexibilidad al permitir cambiar comportamientos sin modificar código

### Polimorfismo

El **polimorfismo** permite que objetos de diferentes tipos respondan a la misma interfaz de manera específica. Es fundamental para:

- Implementar el principio Abierto/Cerrado (OCP)
- Crear código extensible sin modificar código existente
- Facilitar el intercambio de implementaciones

Estos conceptos son la base sobre la cual se construyen los principios SOLID y entenderlos nos ayudará a aplicar cada principio de manera más efectiva.
