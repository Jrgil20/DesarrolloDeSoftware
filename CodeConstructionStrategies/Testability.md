# Testabilidad/comprobabilidad del código

La **testabilidad** o **comprobabilidad** del código se refiere a la facilidad con la que se puede verificar que el código funciona correctamente mediante pruebas automatizadas o manuales. Un código altamente testeable permite a los desarrolladores identificar y corregir errores de manera eficiente, lo que mejora la calidad del software y reduce el tiempo de desarrollo.

Es el grado en que un sistema o componente facilita ser probado de forma efectiva, rápida y confiable.

En otras palabras: qué tan fácil es configurar sus dependencias, controlar sus entradas, observar su salida/estado y detectar fallas.

## Señales de buena comprobabilidad

- **Dependencias inyectadas** (HTTP, reloj, archivos) en vez de estar acopladas.  
    Permite reemplazar fácilmente servicios externos por implementaciones simuladas (mocks o fakes) durante las pruebas, facilitando el control de los escenarios de test.

- **Lógica pura** separada de efectos externos (IO, red, tiempo).  
    La lógica de negocio se mantiene independiente de operaciones externas, lo que permite probarla sin necesidad de acceder a recursos como bases de datos o servicios web.

- **Seams/puntos de prueba** claros para usar fakes/mocks.  
    El código está estructurado de modo que se pueden insertar objetos simulados en puntos clave, facilitando la simulación de comportamientos y la verificación de interacciones.

- **Observabilidad**: retornos explícitos, errores tipados, logs útiles.  
    El sistema expone información relevante sobre su funcionamiento, lo que ayuda a detectar y diagnosticar fallos durante las pruebas.

- **Tests rápidos y aislados** (no dependen de red/DB reales).  
    Las pruebas pueden ejecutarse en poco tiempo y no requieren infraestructura externa, lo que permite obtener retroalimentación inmediata y confiable sobre el estado del código.

## Estrategias para garantizar la comprobabilidad

- **Inversión de dependencias**: Inyecta colaboraciones como repositorios o servicios externos en lugar de crearlos directamente dentro de los componentes. Esto facilita el reemplazo por dobles de prueba (mocks/fakes) durante los tests.

- **Seams**: Identifica y utiliza puntos en el código donde puedas “inyectar” dobles de prueba. Los seams permiten interceptar dependencias y comportamientos para facilitar la prueba de distintas rutas y escenarios.

- **Pureza y determinismo**: Separa la lógica pura (cálculos, decisiones) de los efectos secundarios (acceso a datos, persistencia, envío de información). La lógica pura es más fácil y rápida de probar porque no depende de factores externos.

- **Reloj e IO abstractos**: Evita el uso directo de funciones como `DateTime.UtcNow` o métodos de archivos (`File.*`). Introduce interfaces o servicios abstractos para el tiempo y el acceso a archivos, permitiendo simular estos comportamientos en pruebas.

- **Estructura de puertos y adaptadores (hexagonal)**: Utiliza una arquitectura que mantenga un núcleo de negocio independiente y testeable, con bordes (adaptadores) fácilmente reemplazables para interactuar con el exterior.

- **Diseño orientado a contratos**: Programa cada operación como si existiera un contrato explícito entre el cliente y el proveedor. Define precondiciones, postcondiciones e invariantes para asegurar que los componentes se comporten de manera predecible y comprobable.

- **Errores tipados y manejo explícito**: Utiliza tipos específicos para representar errores y resultados, en lugar de excepciones genéricas. Esto facilita la verificación de condiciones de error en las pruebas.

- **Observabilidad**: Implementa mecanismos para registrar eventos importantes, errores y estados del sistema. La observabilidad facilita la identificación de problemas durante las pruebas y el análisis posterior.
## Ejemplos prácticos

### 1. Inyección de dependencias (C#)

```csharp
// Interfaz para abstraer el acceso al reloj
public interface IClock {
    DateTime Now { get; }
}

// Implementación real
public class SystemClock : IClock {
    public DateTime Now => DateTime.UtcNow;
}

// Clase que depende del reloj inyectado
public class Servicio {
    private readonly IClock _clock;
    public Servicio(IClock clock) {
        _clock = clock;
    }
    public bool EsHoraDeProcesar() {
        // Fácil de testear porque el reloj es inyectado
        return _clock.Now.Hour == 8;
    }
}
```

*Comentario: Permite simular la hora en pruebas unitarias usando un mock de `IClock`.*

**Contraejemplo:**

```csharp
// Dependencia acoplada directamente
public class Servicio {
    public bool EsHoraDeProcesar() {
        // Difícil de testear porque depende de DateTime.UtcNow directamente
        return DateTime.UtcNow.Hour == 8;
    }
}
```

*Problema: No se puede controlar la hora en pruebas, lo que dificulta simular distintos escenarios y hace los tests poco confiables.*

---

### 2. Separación de lógica pura (JavaScript)

```javascript
// Lógica pura: función determinista
function calcularTotal(precio, cantidad) {
    return precio * cantidad;
}

// Efecto externo separado
function guardarEnBaseDeDatos(total) {
    // Simula guardar en DB
    console.log("Guardado:", total);
}
```

*Comentario: `calcularTotal` se puede probar sin depender de la base de datos.*

**Contraejemplo:**

```javascript
// Lógica y efecto externo mezclados
function calcularYGuardar(precio, cantidad) {
    const total = precio * cantidad;
    // Efecto externo dentro de la función
    guardarEnBaseDeDatos(total);
    return total;
}
```

*Problema: Para probar la función hay que lidiar con el acceso a la base de datos o simularlo, lo que complica y enlentece los tests.*

---

### 3. Uso de fakes/mocks (Python con unittest.mock)

```python
from unittest.mock import Mock

# Dependencia externa
class EmailService:
    def send(self, to, subject, body):
        pass  # En producción enviaría un email

# Código bajo prueba
def notificar_usuario(email_service, usuario):
    email_service.send(usuario.email, "Bienvenido", "Hola!")

# Test usando un mock
mock_service = Mock()
notificar_usuario(mock_service, usuario=type('U', (), {'email': 'test@ejemplo.com'})())
mock_service.send.assert_called_once()
```

*Comentario: Se verifica que la función intenta enviar un email, sin enviar realmente nada.*

**Contraejemplo:**

```python
# Dependencia creada internamente, difícil de reemplazar
def notificar_usuario(usuario):
    service = EmailService()
    service.send(usuario.email, "Bienvenido", "Hola!")
```

*Problema: No se puede interceptar la llamada ni evitar el envío real de emails en pruebas, lo que puede causar efectos secundarios no deseados y dificulta la verificación.*

---

### 4. Observabilidad y errores tipados (TypeScript)

```typescript
type Resultado<T> = { ok: true, valor: T } | { ok: false, error: string };

function dividir(a: number, b: number): Resultado<number> {
    if (b === 0) return { ok: false, error: "División por cero" };
    return { ok: true, valor: a / b };
}
```

*Comentario: El resultado explícito facilita probar ambos casos (éxito y error) sin excepciones ocultas.*

**Contraejemplo:**

```typescript
function dividir(a: number, b: number): number {
    return a / b; // Lanza excepción si b es 0
}
```

*Problema: Si ocurre una división por cero, se lanza una excepción que puede pasar desapercibida en los tests, dificultando la comprobación de errores y el manejo explícito de fallos.*

