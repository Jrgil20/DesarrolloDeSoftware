# Principio de Segregación de Interfaces (ISP)

> "Los clientes no deberían verse obligados a depender de interfaces que no utilizan." — Robert C. Martin
> "Es mejor tener muchas interfaces específicas en lugar de una única interfaz general." — Robert C. Martin
> En la practica esto se traduce que hay que evitar interfaces "grandes" o "gordas" ( es decir que incluyan metodos que no son relevantes para la tarea de dicha interfaz) que obliguen a las clases que las implementan a definir métodos que no necesitan.

## Explicación del Principio

El Principio de Segregación de Interfaces (ISP) establece que los clientes no deben verse obligados a depender de interfaces que no utilizan. En lugar de tener una única interfaz grande y general, es preferible dividirla en varias interfaces más pequeñas y específicas. Esto permite que las clases implementen solo las interfaces que realmente necesitan, evitando la implementación de métodos innecesarios.

### ¿Qué es una interfaz "gorda"?

Una interfaz "gorda" es aquella cuya definición incluye métodos que no están bien relacionados entre sí, es decir, agrupa funcionalidades que no comparten un propósito claro o cohesivo. Esto provoca que las clases que implementan la interfaz se vean obligadas a definir métodos que no necesitan o que no tienen sentido en su contexto.

> **Nota:** Una interfaz "gorda" no significa que tenga mucho código en su implementación, sino que su definición agrupa métodos poco relacionados, lo que afecta la cohesión y claridad de su propósito.

#### Ejemplo de una interfaz "gorda"

Supongamos que tenemos una interfaz para dispositivos multifunción:

```csharp
public interface IMultifuncion {
    void Imprimir();
    void Escanear();
    void Faxear();
    void Fotocopiar();
}
```

Si una clase `ImpresoraBasica` implementa esta interfaz, se verá obligada a definir métodos como `Escanear`, `Faxear` y `Fotocopiar`, aunque solo pueda imprimir. Esto es un ejemplo de una interfaz "gorda".

```csharp
public class ImpresoraBasica : IMultifuncion {
    public void Imprimir() {
        // Implementación de impresión
    }
    public void Escanear() {
        throw new NotImplementedException();
    }
    public void Faxear() {
        throw new NotImplementedException();
    }
    public void Fotocopiar() {
        throw new NotImplementedException();
    }
}
```

## Ejemplo de una interfaz con mucho código pero no "gorda"

Una interfaz puede tener muchos métodos, pero si todos están relacionados y tienen un propósito claro, no se considera "gorda". Por ejemplo, una interfaz para operaciones matemáticas avanzadas:

```csharp
public interface ICalculadoraCientifica {
    double Sumar(double a, double b);
    double Restar(double a, double b);
    double Multiplicar(double a, double b);
    double Dividir(double a, double b);
    double Seno(double angulo);
    double Coseno(double angulo);
    double Logaritmo(double valor);
    // ...otros métodos matemáticos
}
```

Aunque tiene muchos métodos, todos están relacionados con cálculos matemáticos, por lo que la interfaz mantiene cohesión y no obliga a implementar métodos irrelevantes para el dominio de la calculadora científica.

> Es importante considerar el **contexto** y el **dominio** al evaluar si una interfaz es "gorda". En este ejemplo, aunque la interfaz tiene muchos métodos, todos son relevantes para el dominio de una calculadora científica. No se trata de la cantidad de métodos, sino de que todos tengan sentido dentro del contexto de uso. Una interfaz es problemática cuando obliga a implementar métodos que no tienen relación con el propósito principal de la clase que la implementa.

## ¿Por qué aplicar el ISP?

Aplicar el Principio de Segregación de Interfaces aporta varios beneficios clave:

- **Cohesión alta:** Cada interfaz representa una capacidad o responsabilidad clara, facilitando el diseño orientado a objetos.
- **Menos acoplamiento:** Los clientes dependen solo de los métodos que realmente necesitan, reduciendo dependencias innecesarias.
- **Testabilidad:** Es más sencillo crear mocks o fakes para pruebas, ya que las interfaces pequeñas requieren menos métodos a simular.
- **Evolución segura:** Agregar nuevas capacidades no afecta a quienes no las utilizan, minimizando el riesgo de romper implementaciones existentes.
- **Facilita otros principios SOLID:** Contratos pequeños y específicos ayudan a cumplir el SRP (Responsabilidad Única), DIP (Inversión de Dependencias) y LSP (Sustitución de Liskov).

En resumen, el ISP promueve interfaces enfocadas, flexibles y fáciles de mantener, mejorando la calidad y escalabilidad del software.

## Heurísticas (metodos intuitivos) para aplicar el ISP

A continuación se presentan algunas heurísticas prácticas para aplicar el Principio de Segregación de Interfaces:

- **Una capacidad por interfaz:** Cada interfaz debe representar una única responsabilidad o capacidad claramente definida.
- **Nombra por intención, no por tecnología:** El nombre de la interfaz debe reflejar su propósito, no la tecnología subyacente (por ejemplo, `INotificationSender` en lugar de `ISmtpClient`).
- **Separa lectura y escritura:** Cuando tenga sentido, divide las operaciones de lectura y escritura en interfaces distintas (por ejemplo, `IReader` e `IWriter`).
- **Separa operaciones raras u onerosas:** Si una operación es poco común o costosa/riesgosa, colócala en una interfaz separada (por ejemplo, `IAdmin`, `IBulkOperation`, `IMigration`).
- **Divide si una implementación no puede cumplir todo:** Si una clase no puede implementar todos los métodos de una interfaz, es señal de que la interfaz debe dividirse, evitando así romper el Principio de Sustitución de Liskov (LSP).

Estas heurísticas ayudan a mantener interfaces pequeñas, cohesivas y alineadas con las verdaderas necesidades de los clientes.

## Antipatrones relacionados con el ISP

- **Antipatrón: Interfaz "gorda":** Una interfaz que agrupa métodos poco relacionados, obligando a las clases a implementar métodos innecesarios.

    **Solución:** Extraer puertos (interfaces pequeñas y específicas) y mover los detalles de implementación a adaptadores concretos. Así, cada clase implementa solo las capacidades que necesita, manteniendo la cohesión y evitando dependencias innecesarias.

- **Antipatrón: Puertos declarados en infraestructura:** Definir las interfaces (puertos) directamente en la capa de infraestructura, en lugar de en el dominio, acopla el dominio a detalles técnicos y dificulta la independencia y reutilización.

    **Solución:** Declara los puertos en la capa de dominio. Así, el dominio define sus propias necesidades y contratos, y la infraestructura solo provee implementaciones concretas. Esto mantiene el dominio independiente de detalles técnicos y facilita la evolución y el testeo del sistema.

- **Antipatrón: Interface leakage (fuga de interfaz):** Ocurre cuando una interfaz expone detalles de implementación o dependencias que no son relevantes para el cliente, obligándolo a conocer aspectos internos que deberían estar encapsulados.

    **Solución:** Diseña interfaces enfocadas en el comportamiento y las necesidades del cliente, evitando exponer detalles técnicos o de implementación. Utiliza abstracciones que representen claramente las capacidades requeridas sin revelar cómo se logran.

- **Antipatrón: Interfaces demasiado específicas:** Crear interfaces extremadamente detalladas para cada pequeña variación de comportamiento puede llevar a una proliferación de interfaces, complicando el diseño y la gestión del código.

    **Solución:** Encuentra un equilibrio entre la especificidad y la generalidad. Agrupa métodos relacionados en interfaces coherentes que representen capacidades significativas, evitando la creación de interfaces triviales o redundantes.

> **Nota:** Un **puerto** es una interfaz que define un punto de entrada o salida para la comunicación entre el dominio de la aplicación y el mundo exterior (por ejemplo, bases de datos, servicios externos, interfaces de usuario). Los puertos abstraen las operaciones que el dominio necesita, sin depender de detalles técnicos.  
> **¿Cómo identificarlos?** Observa las necesidades del dominio: un puerto suele representar una acción o capacidad que el dominio requiere (como guardar datos, enviar notificaciones o consultar información). Si una interfaz define una operación relevante para el dominio, pero no su implementación concreta, probablemente es un puerto.

## Errores comunes y cómo corregirlos

A continuación se describen algunos errores frecuentes al aplicar el ISP y cómo solucionarlos:

### 1. Interfaces por entidad, no por capacidad

**Error:** Crear una interfaz general para una entidad, como `IUserService`, que agrupa muchos métodos para distintos casos de uso.

```csharp
public interface IUserService {
    void CrearUsuario();
    void ActualizarUsuario();
    void CambiarPassword();
    void EnviarNotificacion();
    // ...muchos métodos más
}
```

**Problema:** Las implementaciones pueden verse forzadas a definir métodos que no necesitan.

**Corrección:** Divide la interfaz por capacidades o escenarios:

```csharp
public interface IUserReader {
    Usuario ObtenerUsuarioPorId(int id);
}

public interface IUserWriter {
    void CrearUsuario(Usuario usuario);
    void ActualizarUsuario(Usuario usuario);
}

public interface IUserSecurity {
    void CambiarPassword(int id, string nuevoPassword);
}
```

---

### 2. Dividir por capas, no por capacidades

**Error:** Crear interfaces como `IRepository` que mezclan operaciones CRUD, reportes y tareas costosas (como importaciones masivas).

```csharp
public interface IRepository {
    void Agregar();
    void Eliminar();
    IEnumerable<Reporte> ObtenerReportes();
    void ImportarDatos();
}
```

**Problema:** Se mezclan responsabilidades y se obliga a implementar métodos innecesarios.

**Corrección:** Crea repositorios específicos y servicios separados para operaciones costosas:

```csharp
public interface IUsuarioRepository {
    void Agregar(Usuario usuario);
    void Eliminar(int id);
}

public interface IReporteService {
    IEnumerable<Reporte> ObtenerReportes();
}

public interface IImportacionService {
    void ImportarDatos(IEnumerable<Datos> datos);
}
```

---

### 3. Segregar sin criterio (sobre-ingeniería)

**Error:** Crear muchas interfaces minúsculas para métodos que siempre se usan juntos.

```csharp
public interface ICrearUsuario {
    void CrearUsuario();
}
public interface IActualizarUsuario {
    void ActualizarUsuario();
}
public interface IEliminarUsuario {
    void EliminarUsuario();
}
```

**Problema:** Proliferación de interfaces triviales y fragmentación innecesaria.

**Corrección:** Agrupa métodos que cambian juntos y segrega solo lo que es opcional o evoluciona aparte:

```csharp
public interface IUserWriter {
    void CrearUsuario(Usuario usuario);
    void ActualizarUsuario(Usuario usuario);
    void EliminarUsuario(int id);
}
```

---

> **Resumen:** Aplica el ISP con criterio: divide interfaces por capacidades o escenarios de uso, no solo por entidad o capa. Evita tanto las interfaces "gordas" como la sobre-segregación innecesaria.