# Repaso de UML para Patrones de Diseño

## Introducción a UML

**UML (Unified Modeling Language)** es un lenguaje de modelado visual estándar para especificar, visualizar, construir y documentar sistemas de software. Para el estudio de patrones de diseño, nos enfocaremos en dos diagramas fundamentales:

- **Diagramas de Clase**: Representan la estructura estática del sistema.
- **Diagramas de Secuencia**: Representan las interacciones dinámicas entre objetos.

---

## 1. Diagramas de Clase

Los diagramas de clase muestran las clases del sistema, sus atributos, métodos y las relaciones entre ellas.

### 1.1 Estructura de una Clase

``` class diagram
┌─────────────────────────┐
│      NombreClase        │  ← Nombre de la clase
├─────────────────────────┤
│  - atributoPrivado      │  ← Atributos (con visibilidad)
│  + atributoPublico      │
│  # atributoProtegido    │
│  ~ atributoPaquete      │
├─────────────────────────┤
│  + metodoPublico()      │  ← Métodos/Operaciones
│  - metodoPrivado()      │
│  # metodoProtegido()    │
└─────────────────────────┘
```

#### Notación de Atributos:

``` class diagram
visibilidad nombre: tipo [multiplicidad] = valorInicial {propiedades}
```

**Ejemplos:**

- `- edad: int = 0`
- `+ nombre: String`
- `# saldo: double {readOnly}`
- `+ items: List<Item> [0..*]`

#### Notación de Métodos:

``` class diagram
visibilidad nombre(parametros): tipoRetorno {propiedades}
```

**Ejemplos:**

- `+ calcularTotal(): double`
- `- validar(dato: String): boolean`
- `+ obtenerNombre(): String {query}`
- `# procesar(items: List): void`

### 1.2 Símbolos de Visibilidad

| Símbolo | Visibilidad | Significado |
|---------|-------------|-------------|
| `+` | **public** | Accesible desde cualquier clase |
| `-` | **private** | Accesible solo dentro de la clase |
| `#` | **protected** | Accesible desde la clase y sus subclases |
| `~` | **package** | Accesible solo dentro del mismo paquete |

### 1.3 Tipos de Clases Especiales

#### Clase Abstracta:

``` class diagram
┌─────────────────────────┐
│   «abstract»            │
│     FiguraGeometrica    │  ← Nombre en cursiva o con estereotipo
├─────────────────────────┤
│  # color: String        │
├─────────────────────────┤
│  + calcularArea()       │  ← Método abstracto en cursiva
└─────────────────────────┘
```

#### Interfaz:

``` class diagram
┌─────────────────────────┐
│   «interface»           │
│     Ordenable           │
├─────────────────────────┤
│  + comparar(): int      │
│  + ordenar(): void      │
└─────────────────────────┘
```

#### Clase Estática (Static Class):

``` class diagram
┌─────────────────────────┐
│   «static»              │
│     Utilidades          │
├─────────────────────────┤
│  + {static} PI: double  │
├─────────────────────────┤
│  + {static} max(): int  │
└─────────────────────────┘
```

### 1.4 Relaciones entre Clases

#### 1.4.1 Asociación (Association)

**Símbolo**: Línea simple `──────`

Representa una relación estructural entre clases.

``` class diagram
Persona ──────── Dirección
  1                 1..*
```

**Elementos de una asociación:**

- **Nombre**: Describe la relación (opcional)
- **Multiplicidad**: Indica cuántas instancias participan
- **Roles**: Nombres en los extremos que describen el papel de cada clase

**Multiplicidades comunes:**

- `1` - Exactamente uno
- `0..1` - Cero o uno (opcional)
- `*` o `0..*` - Cero o muchos
- `1..*` - Uno o muchos
- `n..m` - Desde n hasta m
- `n` - Exactamente n

**Ejemplo con nombre y roles:**
``` class diagram
Persona ────── trabaja en ────── Empresa
           1                 1..*
       empleado             empleador
```

#### 1.4.2 Asociación Direccional

**Símbolo**: Línea con flecha `──────>`

Indica que la relación es navegable solo en una dirección.

``` class diagram
Cliente ──────> Pedido
   1              0..*
```

Significa: El Cliente conoce sus Pedidos, pero el Pedido no conoce necesariamente a su Cliente.

#### 1.4.3 Agregación (Aggregation)

**Símbolo**: Diamante blanco `◇──────`

Representa una relación "tiene-un" débil, donde las partes pueden existir independientemente del todo.

``` class diagram
Universidad ◇────── Estudiante
     1                 1..*
```

Significado: Una Universidad tiene Estudiantes, pero si la Universidad desaparece, los Estudiantes siguen existiendo.

#### 1.4.4 Composición (Composition)

**Símbolo**: Diamante negro `◆──────`

Representa una relación "tiene-un" fuerte, donde las partes no pueden existir sin el todo.

``` class diagram
Casa ◆────── Habitación
  1            1..*
```

Significado: Una Casa tiene Habitaciones, si la Casa se destruye, las Habitaciones también se destruyen.

**Diferencia clave:**

- **Agregación**: Relación débil, ciclo de vida independiente
- **Composición**: Relación fuerte, ciclo de vida dependiente

#### 1.4.5 Herencia/Generalización (Inheritance)

**Símbolo**: Flecha con triángulo blanco `────▷`

Representa relación "es-un" (is-a).

``` class diagram
        Animal
          △
          │
    ┌─────┴─────┐
    │           │
   Perro      Gato
```

Notación alternativa:
```
Perro ────▷ Animal
Gato  ────▷ Animal
```

#### 1.4.6 Implementación/Realización

**Símbolo**: Flecha punteada con triángulo blanco `┄┄┄▷`

Indica que una clase implementa una interfaz.

``` class diagram
              «interface»
              Volador
                △
                ┊
    ┌───────────┼───────────┐
    ┊           ┊           ┊
  Pajaro     Avion      Superman
```

#### 1.4.7 Dependencia (Dependency)

**Símbolo**: Flecha punteada `┄┄┄┄>`

Indica que una clase usa o depende de otra temporalmente.

``` class diagram
Controlador ┄┄┄┄> ServicioEmail
```

Significado: Controlador usa ServicioEmail (por ejemplo, como parámetro de método o variable local), pero no lo contiene como atributo.

**Cuándo usar:**

- Parámetro de método
- Variable local
- Llamada a método estático
- Retorno de método

### 1.5 Resumen Visual de Relaciones

``` class diagram
TIPO                SÍMBOLO          SIGNIFICADO

Asociación         ──────           Relación estructural
Asociación         ──────>          Relación navegable
Direccional

Agregación         ◇──────          "Tiene-un" débil
                                    (partes independientes)

Composición        ◆──────          "Tiene-un" fuerte
                                    (partes dependientes)

Herencia           ────▷            "Es-un"
                                    (generalización)

Implementación     ┄┄┄▷             Implementa interfaz

Dependencia        ┄┄┄┄>            Usa temporalmente
```

### 1.6 Ejemplo Completo de Diagrama de Clase

``` class diagram
┌──────────────────────┐
│   «interface»        │
│     Pago             │
├──────────────────────┤
│ + procesar(): void   │
└──────────────────────┘
          △
          ┊
    ┌─────┴─────┐
    ┊           ┊
┌─────────┐  ┌──────────┐
│ PagoTC  │  │ PagoEfec │
└─────────┘  └──────────┘
          
┌──────────────────────┐         ┌───────────────────┐
│     Cliente          │         │      Pedido       │
├──────────────────────┤         ├───────────────────┤
│ - nombre: String     │1      * │ - fecha: Date     │
│ - email: String      │◆────────│ - total: double   │
├──────────────────────┤         ├───────────────────┤
│ + realizarPedido()   │         │ + calcularTotal() │
│ + consultarPedidos() │         │ + agregarItem()   │
└──────────────────────┘         └───────────────────┘
                                          │
                                          │ 1
                                          │
                                          │ *
                                    ┌───────────┐
                                    │ ItemPedido│
                                    ├───────────┤
                                    │-cantidad  │
                                    │-precio    │
                                    └───────────┘
```

---

## 2. Diagramas de Secuencia

Los diagramas de secuencia muestran cómo los objetos interactúan entre sí a lo largo del tiempo, enfocándose en el orden de los mensajes.

### 2.1 Elementos Básicos

#### 2.1.1 Actores y Objetos

**Actor (Usuario externo):**

``` sequence diagram
    👤
  Usuario
```

**Objeto:**

``` sequence diagram
┌─────────────┐
│ :NombreClase│  ← Objeto anónimo de la clase
└─────────────┘
      │
      │  ← Línea de vida
      │
```

**Objeto con nombre:**

``` sequence diagram
┌──────────────────┐
│ objetoNombre:Clase│  ← Objeto específico
└──────────────────┘
         │
```

#### 2.1.2 Línea de Vida (Lifeline)

Línea vertical punteada que representa la existencia del objeto a lo largo del tiempo.

``` sequence diagram
  :Objeto
     │
     │  ← Representa el tiempo (de arriba hacia abajo)
     │
     │
```

#### 2.1.3 Activación (Activation Box)

Rectángulo delgado sobre la línea de vida que indica cuándo el objeto está activo/ejecutando.

``` sequence diagram
  :Objeto
     │
     ▌  ← Barra de activación
     ▌
     │
```

### 2.2 Tipos de Mensajes

#### 2.2.1 Mensaje Síncrono

**Símbolo**: Flecha sólida `───────>`

El emisor espera la respuesta antes de continuar.

``` sequence diagram
:Objeto1          :Objeto2
   ▌                 │
   ▌───metodo()────> ▌
   ▌                 ▌
```

#### 2.2.2 Mensaje Asíncrono

**Símbolo**: Flecha abierta `───────>>`

El emisor no espera respuesta, continúa inmediatamente.

``` sequence diagram
:Objeto1          :Objeto2
   ▌                 │
   ▌──notificar()──>>▌
   ▌                 ▌
```

#### 2.2.3 Mensaje de Retorno

**Símbolo**: Flecha punteada `┄┄┄┄┄>`

Indica el valor de retorno.

``` sequence diagram
:Objeto1          :Objeto2
   ▌                 ▌
   ▌───getData()───> ▌
   ▌ <┄┄datos┄┄┄┄┄┄┄ ▌
```

#### 2.2.4 Mensaje a sí mismo (Self-call)

``` sequence diagram
:Objeto
   ▌
   ▌─┐
   ▌ │ metodoInterno()
   ▌<┘
```

#### 2.2.5 Creación de Objeto

**Símbolo**: Flecha punteada hacia el objeto con estereotipo `«create»`

``` sequence diagram
:Factory              :Producto
   ▌                      
   ▌─── «create» ────> ┌──────────┐
   ▌                   │:Producto │
                       └──────────┘
```

#### 2.2.6 Destrucción de Objeto

**Símbolo**: X grande al final de la línea de vida

``` sequence diagram
:Objeto
   │
   ▌
   ▌────destroy()─────> ×
```

### 2.3 Fragmentos de Interacción

Los fragmentos permiten expresar lógica condicional, bucles, etc.

#### 2.3.1 Alt (Alternativa - if/else)

``` class diagram
┌────────────────────────────────────────┐
│ alt [condición verdadera]              │
├────────────────────────────────────────┤
│    :A ────metodo1()───> :B             │
├────────────────────────────────────────┤
│ else [condición falsa]                 │
├────────────────────────────────────────┤
│    :A ────metodo2()───> :B             │
└────────────────────────────────────────┘
```

#### 2.3.2 Opt (Opcional - if)

``` class diagram
┌────────────────────────────────────────┐
│ opt [si saldo > 0]                     │
├────────────────────────────────────────┤
│    :Cliente ──retirar()─> :Cuenta      │
└────────────────────────────────────────┘
```

#### 2.3.3 Loop (Bucle - for/while)

``` class diagram
┌────────────────────────────────────────┐
│ loop [para cada item]                  │
├────────────────────────────────────────┤
│    :Carrito ──agregar()─> :Item        │
└────────────────────────────────────────┘
```

#### 2.3.4 Par (Paralelo)

``` class diagram
┌────────────────────────────────────────┐
│ par                                    │
├────────────────────────────────────────┤
│    :A ────tarea1()───> :B              │
├────────────────────────────────────────┤
│    :A ────tarea2()───> :C              │
└────────────────────────────────────────┘
```

#### 2.3.5 Ref (Referencia a otro diagrama)

``` class diagram
┌────────────────────────────────────────┐
│ ref                                    │
│  ProcesarPago                          │
└────────────────────────────────────────┘
```

### 2.4 Tabla de Símbolos en Diagramas de Secuencia

| Elemento | Símbolo | Significado |
|----------|---------|-------------|
| **Mensaje síncrono** | `───────>` | Llamada que espera respuesta |
| **Mensaje asíncrono** | `───────>>` | Llamada sin esperar respuesta |
| **Mensaje de retorno** | `┄┄┄┄┄>` | Valor de retorno |
| **Creación** | `┄┄┄> «create»` | Crea una nueva instancia |
| **Destrucción** | `×` | Destruye el objeto |
| **Activación** | `▌` (rectángulo) | Objeto está ejecutando |
| **Línea de vida** | `│` (línea punteada) | Existencia del objeto |

### 2.5 Ejemplo Completo de Diagrama de Secuencia

**Escenario**: Usuario realiza un pedido en línea

``` sequence diagram
👤                :Sistema      :Carrito       :Pago          :BD
Usuario              │             │             │             │
  │                  │             │             │             │
  │──login()───────> ▌             │             │             │
  │                  ▌──validar()────────────────────────────> ▌
  │                  ▌ <┄┄┄OK┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄ ▌
  │ <┄┄sesión┄┄┄┄┄┄ ▌             │             │             │
  │                  │             │             │             │
  │──agregarItem()─> ▌             │             │             │
  │                  ▌──agregar()─>▌             │             │
  │                  │             ▌             │             │
  │                  │             ▌─┐           │             │
  │                  │             ▌ │calcTotal()│             │
  │                  │             ▌<┘           │             │
  │                  ▌<┄total┄┄┄┄ ▌             │             │
  │ <┄┄total┄┄┄┄┄┄┄ ▌             │             │             │
  │                  │             │             │             │
  │──checkout()────> ▌             │             │             │
  │                  ▌───procesar()─────────────>▌             │
  │                  │             │             ▌──guardar()─>▌
  │                  │             │             ▌<┄┄OK┄┄┄┄┄┄ ▌
  │                  ▌<┄┄confirmación┄┄┄┄┄┄┄┄┄┄ ▌             │
  │<┄┄OK┄┄┄┄┄┄┄┄┄┄┄ ▌             │             │             │
  │                  │             │             │             │
```

### 2.6 Ejemplo con Fragmentos

**Escenario**: Transferencia bancaria con validaciones

``` sequence diagram
:Cliente        :Sistema          :Cuenta          :BD
   │               │                 │              │
   │─transferir()─>▌                 │              │
   │               ▌                 │              │
   │    ┌──────────────────────────────────────┐   │
   │    │ opt [validar sesión]                 │   │
   │    ├──────────────────────────────────────┤   │
   │    │  ▌────validarSesion()──────────────────> ▌
   │    │  ▌ <┄┄┄OK┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄▌
   │    └──────────────────────────────────────┘   │
   │               ▌                 │             │
   │    ┌──────────────────────────────────────┐   │
   │    │ alt [saldo suficiente]               │   │
   │    ├──────────────────────────────────────┤   │
   │    │  ▌──verificarSaldo()──> ▌            │   │
   │    │  ▌──retirar()──────────> ▌           │   │
   │    │  ▌                       ▌──update()───> ▌
   │    │  ▌ <┄┄┄success┄┄┄┄┄┄┄┄┄  ▌           │   │
   │    ├──────────────────────────────────────┤   │
   │    │ else [saldo insuficiente]            │   │
   │    ├──────────────────────────────────────┤   │
   │    │  ▌──lanzarError()────────────────────│   │
   │    └──────────────────────────────────────┘   │
   │<┄resultado┄┄ ▌                 │              │
   │              ▌                 │              │
```

---

## 3. Notaciones Adicionales Importantes

### 3.1 Estereotipos

Los estereotipos extienden el vocabulario de UML. Se escriben entre comillas angulares `« »`.

**Ejemplos comunes:**

- `«interface»` - Interfaz
- `«abstract»` - Clase abstracta
- `«create»` - Creación de objeto
- `«destroy»` - Destrucción de objeto
- `«entity»` - Entidad de dominio
- `«controller»` - Controlador
- `«boundary»` - Interfaz de usuario

### 3.2 Notas y Comentarios

``` class diagram
┌─────────────┐        ╔════════════════╗
│   Clase     │        ║ Nota:          ║
└─────────────┘        ║ Esta clase es  ║
      │                ║ importante     ║
      └───────────────>╚════════════════╝
```

### 3.3 Propiedades de Elementos

Se colocan entre llaves `{ }`:

- `{readOnly}` - Solo lectura
- `{abstract}` - Abstracto
- `{static}` - Estático
- `{ordered}` - Colección ordenada
- `{unique}` - Sin duplicados

**Ejemplo:**
```
+ items: List {ordered, unique}
+ calcular(): double {query}
```

---

## 4. Guía Rápida de Referencia

### 4.1 Cuándo usar cada tipo de relación

| Pregunta | Relación | Símbolo |
|----------|----------|---------|
| ¿Clase B es un tipo de A? | Herencia | `────▷` |
| ¿Clase A usa a B temporalmente? | Dependencia | `┄┄┄┄>` |
| ¿Clase A tiene atributo de tipo B? | Asociación | `──────` |
| ¿A contiene a B, pero B puede existir sin A? | Agregación | `◇──────` |
| ¿A contiene a B, y B no existe sin A? | Composición | `◆──────` |
| ¿Clase A implementa interfaz B? | Realización | `┄┄┄▷` |

### 4.2 Diferencias clave

**Asociación vs Dependencia:**

- **Asociación**: Relación permanente (atributo de clase)
- **Dependencia**: Relación temporal (parámetro, variable local)

**Agregación vs Composición:**

- **Agregación**: Las partes sobreviven sin el todo
- **Composición**: Las partes mueren con el todo

**Herencia vs Implementación:**

- **Herencia**: Clase extiende otra clase concreta/abstracta
- **Implementación**: Clase implementa una interfaz

---

## 5. Ejemplos Aplicados a Patrones de Diseño

### 5.1 Patrón Observer (Diagrama de Clase)

``` Sequence diagram
┌────────────────────┐         ┌──────────────────┐
│   «interface»      │         │     Subject      │
│     Observer       │         ├──────────────────┤
├────────────────────┤    *    │ - observers: List│
│ + update(): void   │◇────────│ + attach()       │
└────────────────────┘         │ + detach()       │
         △                     │ + notify()       │
         │                     └──────────────────┘
    ┌────┴────┐                        △
    │         │                        │
┌─────────┐ ┌─────────┐        ┌──────────────┐
│Observer1│ │Observer2│        │ConcreteSubject│
└─────────┘ └─────────┘        └──────────────┘
```

### 5.2 Patrón Factory Method (Diagrama de Secuencia)

``` Sequence diagram
:Cliente         :Factory          :Producto
   │                │                  │
   │─crearProducto()▌                  │
   │                ▌─┐                │
   │                ▌ │decidirTipo()   │
   │                ▌<┘                │
   │                ▌                  │
   │                ▌─── «create» ───> ┌──────────┐
   │                ▌                  │:Producto │
   │                ▌                  └──────────┘
   │                ▌                       │
   │                ▌ <┄┄producto┄┄┄┄┄┄┄┄┄  │
   │<┄┄producto┄┄┄┄ ▌                       │
   │                │                       │
   │──usar()────────────────────────────────▌
   │                │                       │
```

---

## 6. Consejos para Dibujar Diagramas UML

1. **Simplicidad**: No incluyas todos los detalles, solo lo relevante
2. **Consistencia**: Usa la misma notación en todo el diagrama
3. **Claridad**: Evita cruzar líneas innecesariamente
4. **Enfoque**: Un diagrama, un propósito
5. **Nombres descriptivos**: Usa nombres claros para clases y métodos

---

## 7. Herramientas Recomendadas

- **PlantUML**: Diagramas a partir de texto
- **Draw.io / diagrams.net**: Editor visual gratuito
- **Lucidchart**: Herramienta online colaborativa
- **Visual Paradigm**: Herramienta profesional completa
- **StarUML**: Software de modelado UML

---

## Resumen

**Diagramas de Clase** = Estructura estática (qué hay)
**Diagramas de Secuencia** = Comportamiento dinámico (cómo interactúa)

Dominar estas notaciones es fundamental para:

- Comprender patrones de diseño
- Documentar arquitectura de software
- Comunicar ideas de diseño
- Analizar sistemas existentes

¡Con esta guía tienes todo lo necesario para interpretar y crear diagramas UML aplicados a patrones de diseño!

 