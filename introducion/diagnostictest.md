# Prueba Diagnóstica

## Pregunta 1

En pruebas unitarias de calidad, lo que debe verificarse es principalmente el ______
del sistema, más que los detalles internos de implementación. A) rendimiento B)
estilo C) comportamiento D) cobertura

Respuesta correcta: C) comportamiento

Explicación: En pruebas unitarias de calidad, el enfoque principal es verificar el comportamiento del sistema, asegurándose de que funcione según lo esperado desde la perspectiva del usuario o del cliente. Los detalles internos de implementación, como el rendimiento, el estilo o la cobertura del código, son aspectos secundarios en este contexto.

## Pregunta 2

Identifica el principio SOLID descrito: Los módulos de alto nivel no deberían
depender de módulos de bajo nivel; ambos deberían depender de abstracciones.”
A) SRP B) OCP C) LSP D) DIP

Respuesta correcta: D) DIP

Explicación: El principio de Inversión de Dependencias (DIP) establece que los módulos de alto nivel no deben depender de módulos de bajo nivel, sino que ambos deben depender de abstracciones. Esto promueve un diseño más flexible y desacoplado, facilitando el mantenimiento y la evolución del sistema.

## Pregunta 3

Tienes una clase de servicio que depende de 3 repositorios y 2 adaptadores
externos. Siguiendo buenas prácticas de pruebas y DI, ¿cuántos dobles de prueba
(mocks/fakes/stubs) esperarías inyectar en una prueba unitaria del servicio (no de
integración), asumiendo que no hay dependencias adicionales? A) 2 B) 3 C) 5 D)
0

Respuesta correcta: C) 5

Explicación: En una prueba unitaria del servicio, se espera inyectar dobles de prueba para todas las dependencias externas, que en este caso son 3 repositorios y 2 adaptadores. Por lo tanto, se necesitarían 5 dobles de prueba para aislar completamente la unidad bajo prueba.

## Pregunta 4

Explica cómo reducir el acoplamiento entre un controlador y un servicio con
inyección de dependencias y abstracciones. Menciona interfaces y constructor
injection (o equivalent en tu lenguaje).

Respuesta sugerida: Para reducir el acoplamiento entre un controlador y un servicio, se puede utilizar la inyección de dependencias junto con abstracciones. Primero, se define una interfaz que representa el contrato del servicio, por ejemplo, `IUserService`. Luego, en el controlador, en lugar de depender directamente de una implementación concreta del servicio, se inyecta la interfaz a través del constructor (constructor injection). Esto permite que el controlador dependa de una abstracción en lugar de una implementación específica, facilitando la sustitución del servicio por diferentes implementaciones o dobles de prueba durante las pruebas unitarias. Por ejemplo:

```csharp
public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // Métodos del controlador que utilizan _userService
}
```

Al seguir este enfoque, se mejora la flexibilidad y mantenibilidad del código, ya que el controlador no está acoplado a una implementación específica del servicio.

## Pregunta 5

Un mock y un stub son exactamente lo mismo, solo que con nombres diferentes. V o F (Justifica en 1 línea opcionalmente.)

Respuesta correcta: F

Justificación: Un mock es un objeto simulado que verifica las interacciones, mientras que un stub es un objeto simulado que proporciona respuestas predefinidas sin verificar interacciones.

Ambos, mocks y stubs, se configuran y preparan en la fase de Arrange de una prueba unitaria. En esta fase se crean y ajustan los dobles de prueba necesarios antes de ejecutar el código bajo prueba.

Nota: una prueba unitaria está compuesta por 3 fases: Arrange, Act y Assert. En la fase de Arrange se preparan los datos y se configuran los dobles de prueba necesarios. En la fase de Act se ejecuta el código bajo prueba. Finalmente, en la fase de Assert se verifican los resultados obtenidos contra los esperados. Dentro de esta, un mock se utiliza para verificar que ciertas interacciones ocurrieron, mientras que un stub simplemente proporciona datos predefinidos para las pruebas sin verificar interacciones.

Usa un stub cuando solo necesitas datos falsos.
Usa un mock cuando quieres comprobar cómo se usó la función.

## pregunta 6

La composición fuerte y agregación son: A) lo mismo. B) diferentes. C) dos conceptos que no tienen relación entre sí.

Respuesta correcta: B) diferentes.

Justificación: La composición fuerte implica una relación de "parte-todo" donde la parte no puede existir sin el todo, mientras que la agregación permite que las partes existan de forma independiente del todo.

## pregunta 7

Explica con tus palabras el Principio de Sustitución de Liskov (LSP) y da un ejemplo sencillo (sin código) donde una subclase rompe las expectativas del cliente del supertipo. 

Respuesta sugerida: El Principio de Sustitución de Liskov establece que los objetos de un supertipo deben poder ser reemplazados por objetos de un subtipo sin alterar las propiedades deseables del programa. En otras palabras, si S es un subtipo de T, entonces los objetos de tipo T en un programa deberían poder ser reemplazados por objetos de tipo S sin que el programa falle.

## pregunta 8

Implementa una estructura compuesta con:
-Cell<T> (hoja) que contiene un valor T.

- Box<T> (compuesto) que contiene una colección de elementos que pueden ser Cell<T> o Box<T> Define una interfaz común con un método:
    R reduce(R seed, Func<R,T,R> folder, Predicate<T> predicate)
El método debe acumular solo los valores T que cumplan el predicate. Incluye un pequeño ejemplo que sume enteros pares dentro de una estructura anidada.

```typescript
interface IComponent<T> {
    reduce<R>(seed: R, folder: (acc: R, value: T) => R, predicate: (value: T) => boolean): R;
}
class Cell<T> implements IComponent<T> {
    constructor(private value: T) {}

    reduce<R>(seed: R, folder: (acc: R, value: T) => R, predicate: (value: T) => boolean): R {
        return predicate(this.value) ? folder(seed, this.value) : seed;
    }
}
class Box<T> implements IComponent<T> {
    private children: IComponent<T>[] = [];
  
    add(component: IComponent<T>): void {
        this.children.push(component);
    }

    reduce<R>(seed: R, folder: (acc: R, value: T) => R, predicate: (value: T) => boolean): R {
        return this.children.reduce((acc, child) => child.reduce(acc, folder, predicate), seed);
    }
}
// Ejemplo de uso:
const root = new Box<number>();
const box1 = new Box<number>();
box1.add(new Cell(1));
box1.add(new Cell(2));
const box2 = new Box<number>();
box2.add(new Cell(3));
box2.add(new Cell(4));
root.add(box1);
root.add(box2);
const sumEven = root.reduce(0, (acc, val) => acc + val, (val) => val % 2 === 0);
console.log(sumEven); // Output: 6 (2 + 4)
```
