# Open/Closed Principle (OCP)

> “Los módulos o clases deben estar abiertos a la extensión pero cerrados a la modificación.”  
> Esto significa que el comportamiento de una clase debe poder ampliarse sin necesidad de alterar su código fuente. Se logra mediante la herencia, interfaces o composición, permitiendo agregar nuevas funcionalidades sin afectar las existentes.

## En la práctica

Cuando llega un nuevo requisito, agregas código nuevo (estrategias, decoradores, handlers…), sin editar el código estable ya probado.

Con esto logramos:

- **Estabilidad:** evitas romper lo que ya funciona.
- **Evolutivo:** agregas variantes sin if/else gigantes.
- **Testable:** cada extensión se prueba en aislamiento.
- **Colaborativo:** equipos diferentes pueden extender sin pisarse.

> Si para cada nueva regla o flujo debes abrir la misma clase y tocar varios `switch`, entonces algo anda mal.

## Mecanismos típicos para cumplir OCP

- **Polimorfismo vía interfaces:** Implementa patrones como Strategy o Policy para intercambiar comportamientos.
  - *Explicación:* El polimorfismo permite que diferentes clases implementen la misma interfaz, facilitando el reemplazo de comportamientos en tiempo de ejecución sin modificar el código cliente.

- **Composición:** Usa patrones como Decorator o Chain of Responsibility para agregar funcionalidades sin modificar clases existentes.
  - *Explicación:* La composición permite construir objetos complejos combinando otros objetos, agregando funcionalidades de manera flexible y evitando la herencia rígida.

- **Inversión de dependencias (DIP) y contenedores DI:** Permite inyectar nuevas implementaciones sin alterar el código consumidor.
  - *Explicación:* Al invertir las dependencias y usar contenedores de inyección, el código depende de abstracciones, facilitando la extensión y el reemplazo de componentes.

- **Reglas declarativas:** Aplica patrones como Specification o utiliza motores de reglas para definir lógica extensible.
  - *Explicación:* Las reglas declarativas separan la lógica de negocio de la implementación, permitiendo modificar o agregar reglas sin cambiar el código principal.

- **Pipelines/Behaviors:** Implementa flujos dinámicos con MediatR, middlewares o pipelines personalizables.
  - *Explicación:* Los pipelines permiten encadenar comportamientos de manera flexible, facilitando la extensión de procesos sin modificar la lógica central.

- **Configuración/Plugins:** Descubre e integra nuevas implementaciones por convención o mediante mecanismos de plugins.
  - *Explicación:* Los sistemas de plugins o configuración dinámica permiten agregar funcionalidades nuevas sin modificar el código base, solo añadiendo nuevos módulos o configuraciones.

## Cómo aplicarlo

1. **Identifica puntos de extensión:** Busca áreas en tu código donde se puedan agregar nuevas funcionalidades sin afectar el comportamiento existente.
2. **Aplica patrones de diseño:** Utiliza los mecanismos mencionados (polimorfismo, composición, DIP, etc.) para implementar extensiones.
3. **Escribe pruebas:** Asegúrate de que cada nueva funcionalidad esté cubierta por pruebas, permitiendo cambios futuros sin romper lo existente.
4. **Documenta las extensiones:** Mantén una buena documentación sobre cómo extender el sistema, facilitando la colaboración y el mantenimiento.
5. **Refactoriza cuando sea necesario:** Si una clase comienza a acumular demasiadas responsabilidades o variantes, considera dividirla en componentes más pequeños y específicos.

## ¿Cómo refactorizar una clase que no cumple OCP?

Cuando una clase no cumple con el principio abierto/cerrado, suele presentar muchos `if`, `switch` o cambios frecuentes para soportar nuevos comportamientos. Para refactorizarla:

1. **Identifica las variantes:** Localiza el código que cambia según nuevas reglas o funcionalidades.
2. **Extrae una abstracción:** Define una interfaz o clase base que represente el comportamiento variable.
3. **Crea implementaciones concretas:** Por cada variante, crea una clase que implemente la interfaz o herede de la clase base.
4. **Utiliza composición o inyección:** Modifica la clase original para depender de la abstracción, recibiendo la implementación adecuada por composición, inyección de dependencias o configuración.
5. **Elimina condicionales:** Sustituye los `if`/`switch` por el uso polimórfico de las nuevas clases.
6. **Agrega pruebas:** Verifica que cada implementación funcione de forma aislada y que la clase original siga comportándose correctamente.

**Ejemplo breve:**

Antes (no cumple OCP):

```csharp
public class CalculadoraDescuentos {
    public decimal Calcular(decimal monto, string tipo) {
        if (tipo == "Navidad") return monto * 0.9m;
        if (tipo == "BlackFriday") return monto * 0.8m;
        // Nuevos tipos requieren modificar esta clase
        return monto;
    }
}
```

Después (cumple OCP):

```csharp
public interface IDescuento {
    decimal Calcular(decimal monto);
}

public class DescuentoNavidad : IDescuento {
    public decimal Calcular(decimal monto) => monto * 0.9m;
}

public class DescuentoBlackFriday : IDescuento {
    public decimal Calcular(decimal monto) => monto * 0.8m;
}

// La clase original ahora usa la abstracción
public class CalculadoraDescuentos {
    private readonly IDescuento _descuento;
    public CalculadoraDescuentos(IDescuento descuento) {
        _descuento = descuento;
    }
    public decimal Calcular(decimal monto) => _descuento.Calcular(monto);
}
```

Así, para agregar un nuevo tipo de descuento, solo creas una nueva clase que implemente `IDescuento`, sin modificar el código existente.