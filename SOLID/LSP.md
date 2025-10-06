# Liskov Substitution Principle (LSP)

> “Si S es subtipo de T, entonces los objetos de tipo T en un programa pueden ser reemplazados por objetos de tipo S sin alterar las propiedades deseables del programa.” — Barbara Liskov
> En términos simples: cualquier clase hija debe poder usarse en lugar de su clase padre sin que el programa falle o se comporte de manera inesperada.

## ¿Qué implica realmente LSP?

Para que una subclase (`S`) pueda sustituir a su clase base (`T`) sin problemas, deben cumplirse varias condiciones clave:

### Precondiciones

- **Definición:** Son los requisitos que una clase impone a quienes la usan.
- **Regla:** La subclase no puede exigir más que la clase base.
- ✔️ Puede exigir igual o menos, facilitando el uso.

### Postcondiciones

- **Definición:** Son las garantías que la clase ofrece tras ejecutar sus métodos.
- **Regla:** La subclase no puede prometer menos que la base.
- ✔️ Puede prometer igual o más, nunca menos.

### Invariantes

- **Definición:** Son condiciones que siempre deben cumplirse para el objeto.
- **Regla:** La subclase no puede romper los invariantes definidos por la clase base.

### Efectos observables

- **Definición:** Son los resultados y cambios que produce el método (estado, salidas, etc.).
- **Regla:** La subclase debe mantener la misma semántica general, sin alterar de forma inesperada el sentido, orden o magnitud de los efectos (por ejemplo, respetar la idempotencia).

### Excepciones y errores

- **Definición:** Son las condiciones de error que un método puede lanzar.
- **Regla:** No se deben introducir nuevas excepciones inesperadas ni modificar las condiciones normales de error de la clase base.

**En resumen:** LSP implica que las subclases deben comportarse de manera coherente con sus clases base, respetando contratos y expectativas, para evitar errores sutiles y mantener la robustez del sistema.

## ¿Cómo se aplica LSP en la práctica?

Para aplicar el Principio de Sustitución de Liskov en tus diseños y código:

- **Diseña interfaces y clases base claras:** Define contratos (métodos, precondiciones, postcondiciones) bien especificados.
- **Evita sobrescribir métodos de forma incompatible:** Si una subclase modifica el comportamiento de un método, asegúrate de que sigue cumpliendo las expectativas de la clase base.
- **No restrinjas más las precondiciones:** Las subclases no deben requerir más que la clase base para funcionar correctamente.
- **No debilites las garantías:** Las subclases deben cumplir al menos las mismas postcondiciones e invariantes que la clase base.
- **Maneja errores y excepciones de forma coherente:** No introduzcas nuevas excepciones inesperadas en las subclases.
- **Prueba con polimorfismo:** Usa objetos de la subclase donde se espera la clase base y verifica que el comportamiento sea correcto y predecible.

**Ejemplo sencillo en código:**

```python
class Bird:
    def fly(self):
        pass

class Sparrow(Bird):
    def fly(self):
        print("Sparrow flies")

class Ostrich(Bird):
    def fly(self):
        raise Exception("Ostrich can't fly")  # Viola LSP: Ostrich no puede sustituir a Bird sin errores
```

En este ejemplo, `Ostrich` rompe LSP porque no puede usarse donde se espera un `Bird` que pueda volar. Para cumplir LSP, deberías rediseñar la jerarquía o el contrato de la clase base.

## Señales de violación del LSP

Algunos indicios comunes de que el Principio de Sustitución de Liskov está siendo violado incluyen:

- **Tests que pasan con la clase base pero fallan con la subclase:** Si al sustituir una subclase en pruebas diseñadas para la clase base aparecen fallos, probablemente hay una violación de LSP.
- **Subclase que restringe entradas válidas:** Si la subclase exige precondiciones más estrictas que la clase base, limita el uso polimórfico.
- **Subclase que garantiza menos o cambia efectos:** Si la subclase debilita postcondiciones o altera los efectos esperados (por ejemplo, cambia el resultado, el orden, o introduce efectos secundarios inesperados).
- **Introducción de nuevas excepciones inesperadas:** Si la subclase lanza excepciones que la clase base no contemplaba, puede romper el contrato esperado.
- **Cambios de semántica observable:** Alterar el significado, el orden de ejecución, la idempotencia o los efectos secundarios de los métodos respecto a la clase base.

Detectar estas señales a tiempo ayuda a mantener la robustez y previsibilidad del sistema orientado a objetos.

## Estrategias para cumplir LSP

- **Diseña contratos explícitos:** Documenta precondiciones, postcondiciones e invariantes usando comentarios, documentación XML o tests de contrato.
- **Documenta entradas, salidas y errores:** Asegúrate de que la interfaz o clase base especifique claramente qué entradas son válidas, qué salidas se garantizan y qué errores pueden ocurrir.
- **Prefiere interfaces pequeñas y específicas (ISP):** No hagas que las clases prometan más de lo que realmente pueden cumplir todas las implementaciones.
- **Evalúa la relación “es-un”:** Usa herencia solo si la relación entre la subclase y la clase base es genuinamente de tipo “es-un”. Si tienes dudas, prefiere composición o delegación.
- **Mantén invariantes con inmutabilidad o setters controlados:** Limita el uso de setters peligrosos y considera la inmutabilidad para asegurar que los invariantes se mantengan.
- **Unifica el manejo de errores:** Todas las subclases deben lanzar las mismas excepciones o usar una jerarquía común de errores.
- **Pruebas de contrato:** Ejecuta el mismo conjunto de pruebas para todas las implementaciones y verifica que todas cumplan el contrato definido por la clase base o interfaz.

Estas estrategias ayudan a garantizar que tus subclases sean verdaderos sustitutos de sus clases base, manteniendo la robustez y previsibilidad del sistema.