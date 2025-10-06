# Principio de Inversión de Dependencias (DIP)

> “Los módulos de alto nivel no deberían depender de módulos de bajo nivel. Ambos deben depender de abstracciones.”
> “Las abstracciones no deberían depender de detalles; los detalles deberían depender de abstracciones.”

**Explicación práctica:**  
El principio DIP sugiere que las partes principales de tu aplicación (como la lógica de negocio) no deben estar acopladas a detalles concretos de implementación, como bases de datos, servicios de correo o frameworks. En su lugar, la lógica de negocio depende de interfaces o abstracciones, y son las implementaciones concretas las que se adaptan a esas interfaces. Así, puedes cambiar detalles técnicos sin afectar el núcleo de tu sistema, facilitando el mantenimiento, la escalabilidad y las pruebas.

## Conceptos clave en DIP

- **Alto nivel (dominio/casos de uso):**  
    Son los módulos que orquestan las reglas del negocio y definen el comportamiento principal de la aplicación. No deberían depender de detalles técnicos, sino de abstracciones que representan lo que necesitan para funcionar.

- **Bajo nivel (infraestructura):**  
    Incluye los detalles concretos como bases de datos, servicios HTTP, archivos, sistemas de caché o colas de mensajes. Estos módulos implementan la funcionalidad necesaria para interactuar con el mundo exterior.

- **Abstracciones (puertos):**  
    Son interfaces o contratos definidos por el dominio que especifican cómo los módulos de alto nivel esperan interactuar con los servicios externos. Permiten desacoplar la lógica de negocio de los detalles de implementación.

- **Detalles (adaptadores):**  
    Son las implementaciones concretas de los puertos. Adaptan los servicios de infraestructura (bajo nivel) para que cumplan con las abstracciones requeridas por el dominio.

**Explicación:**  
El DIP promueve que el dominio y los casos de uso (alto nivel) dependan solo de abstracciones (puertos), y que los detalles técnicos (bajo nivel) se adapten a esas abstracciones mediante adaptadores. Así, los cambios en la infraestructura no afectan la lógica de negocio, y se facilita la evolución y prueba del sistema.

## Señales de alerta que indican violaciones al DIP

- **Clases de dominio que crean instancias de detalles concretos:**  
    Si ves que una clase de dominio hace `new` de objetos como `HttpClient`, `SmtpClient` o `SqlConnection`, está dependiendo directamente de detalles de infraestructura.

- **Pruebas que dependen de servicios externos:**  
    Si los tests requieren una base de datos real o servicios externos para ejecutarse correctamente, la lógica de negocio no está desacoplada de los detalles técnicos.

- **Dificultad para cambiar proveedores:**  
    Si cambiar un proveedor (por ejemplo, de base de datos o de servicio de correo) implica modificar la lógica de negocio, hay un acoplamiento indebido entre alto y bajo nivel.

- **Controladores con lógica de dominio y acceso directo a infraestructura:**  
    Si los controladores contienen lógica de negocio y realizan llamadas directas a servicios de infraestructura, no se está respetando la separación de responsabilidades ni el DIP.

> **Nota:**  
> Para distinguir si una clase pertenece al dominio o a la infraestructura, observa su propósito principal:
>
> - **Dominio:** Se encarga de las reglas de negocio, entidades, casos de uso o lógica central de la aplicación. No debe conocer detalles técnicos ni depender de frameworks, librerías externas o servicios externos.
> - **Infraestructura:** Implementa detalles técnicos como acceso a bases de datos, servicios externos, envío de correos, almacenamiento, etc. Suele depender de librerías, frameworks o APIs externas y su función es servir de soporte al dominio, nunca mezclar lógica de negocio.
>
> **Importante:**  
> Dominio y infraestructura deben estar claramente separados, idealmente en archivos o clases diferentes. No deben convivir en el mismo documento ni compartir responsabilidades.


## Guía práctica para aplicar el DIP

1. **Define interfaces (puertos) en el dominio:**  
   Crea interfaces que representen las operaciones que el dominio necesita, como `IUserRepository`, `IEmailService`, etc. Estas interfaces deben estar en el módulo de alto nivel (dominio).
2. **Implementa adaptadores en la infraestructura:**
    Crea clases concretas que implementen las interfaces definidas en el dominio, como `SqlUserRepository`, `SmtpEmailService`, etc. Estas clases deben estar en el módulo de bajo nivel (infraestructura).
3. **Inyecta dependencias:**  
   Utiliza inyección de dependencias para proporcionar las implementaciones concretas a las clases de dominio. Esto puede hacerse mediante constructores, setters o frameworks de inyección de dependencias.
4. **Evita referencias directas a detalles en el dominio:** 
   Asegúrate de que las clases de dominio no hagan `new` de objetos concretos ni dependan de librerías o frameworks específicos.
5. **Prueba con mocks o stubs:**  
   Al tener interfaces, puedes crear implementaciones simuladas (mocks o stubs) para las pruebas unitarias, evitando la necesidad de servicios externos.6. **Revisa y refactoriza regularmente:**  
   A medida que el proyecto evoluciona, revisa el código para asegurarte de que el DIP se sigue respetando y refactoriza cuando sea necesario.

## ¿Por qué aplicar el DIP?

Aplicar el Principio de Inversión de Dependencias aporta beneficios clave:

- **Mantenibilidad:**  
    Permite cambiar detalles de infraestructura (por ejemplo, pasar de SMTP a SendGrid para el envío de correos) sin modificar la lógica de negocio.

- **Testabilidad:**  
    Al depender de interfaces, puedes usar mocks o stubs en las pruebas unitarias, logrando tests rápidos y sin necesidad de acceso a servicios externos o IO.

- **Evolución:**  
    Facilita agregar nuevas integraciones o proveedores sin afectar el núcleo de la aplicación, ya que solo necesitas implementar nuevas adaptaciones a las interfaces existentes.

- **Arquitectura limpia / Hexagonal:**  
    El núcleo de la aplicación permanece independiente de frameworks, librerías o detalles técnicos, siguiendo principios de arquitectura limpia o hexagonal.

Estos beneficios hacen que el sistema sea más flexible, fácil de mantener y escalar, y menos propenso a errores al evolucionar.

## Anti-patrones comunes al aplicar DIP

### 1. Servicios que conocen todo

**Problema:**  
Servicios de dominio que acceden directamente a múltiples detalles de infraestructura, mezclando lógica de negocio con detalles técnicos.

**Solución:**  
Extraer puertos (interfaces) en el dominio y mover los detalles concretos a adaptadores en la infraestructura. Así, el dominio solo conoce abstracciones.

**Ejemplo incorrecto:**

```csharp
public class UserService
{
    private readonly SqlConnection _db;
    private readonly SmtpClient _smtp;

    public UserService()
    {
        _db = new SqlConnection("...");
        _smtp = new SmtpClient("...");
    }

    // Lógica de negocio mezclada con detalles técnicos
}
```

**Ejemplo correcto:**

```csharp
public interface IUserRepository
{
    void Save(User user);
}

public interface IEmailService
{
    void SendWelcomeEmail(User user);
}

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IEmailService _email;

    public UserService(IUserRepository repo, IEmailService email)
    {
        _repo = repo;
        _email = email;
    }

    // Lógica de negocio desacoplada de detalles técnicos
}
```

---

### 2. Puertos declarados en infraestructura

**Problema:**  
Interfaces (puertos) que se definen en la capa de infraestructura, haciendo que el dominio dependa de detalles técnicos.

**Solución:**  
Define los puertos en el dominio. La infraestructura solo debe implementar esas interfaces, nunca definirlas.

**Ejemplo incorrecto:**

```
/* En infraestructura */
public interface IUserRepository { ... }
/* El dominio depende de infraestructura */
```

**Ejemplo correcto:**

```
/* En dominio */
public interface IUserRepository { ... }
/* En infraestructura */
public class SqlUserRepository : IUserRepository { ... }
```

---

### 3. Falsos puertos tecnológicos

**Problema:**  
Interfaces que solo reflejan detalles técnicos (por ejemplo, `ISmtpClient`), en vez de expresar necesidades del dominio.

**Solución:**  
Crea puertos con intención de negocio (por ejemplo, `IEmailService`). El puerto debe expresar lo que el dominio necesita, no cómo se implementa.

**Ejemplo incorrecto:**

```csharp
public interface ISmtpClient
{
    void Send(string to, string body);
}
```

**Ejemplo correcto:**

```csharp
public interface IEmailService
{
    void SendWelcomeEmail(User user);
}
```

---

### 4. Dominio usando el contenedor de inyección de dependencias (DI)

**Problema:**  
Clases de dominio que acceden directamente al contenedor de DI para resolver dependencias, acoplando el dominio a detalles de infraestructura.

**Solución:**  
Utiliza inyección explícita de dependencias (por constructor o método). El dominio no debe conocer ni depender del contenedor de DI.

**Ejemplo incorrecto:**

```csharp
public class UserService
{
    public void Register(User user)
    {
        var repo = Container.Resolve<IUserRepository>();
        repo.Save(user);
    }
}
```

**Ejemplo correcto:**

```csharp
public class UserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public void Register(User user)
    {
        _repo.Save(user);
    }
}
```
