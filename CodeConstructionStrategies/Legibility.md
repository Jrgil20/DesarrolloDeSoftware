# Legibilidad del código

La legibilidad del código es crucial para el mantenimiento y la colaboración en proyectos de software. Factores como la intención, los nombres, la brevedad, la cohesión, la consistencia, la abstracción y la alineación de estilo juegan un papel importante en la creación de código legible y comprensible.

## Factores que afectan la legibilidad del código

1. **Intención**: El código debe expresar claramente su propósito y funcionalidad. La intención debe ser evidente para cualquier desarrollador que lo lea.
2. **Nombres**: Los identificadores (variables, funciones, clases) deben tener nombres descriptivos y coherentes que reflejen su propósito y uso.
3. **Brevidad**: El código debe ser lo más conciso posible sin sacrificar la claridad. Evitar la redundancia y la complejidad innecesaria mejora la legibilidad.
4. **Cohesión**: Las funciones y módulos deben tener una única responsabilidad y estar relacionados entre sí. Esto facilita la comprensión y el mantenimiento del código.
5. **Consistencia**: Seguir convenciones y estilos de codificación consistentes a lo largo del proyecto ayuda a los desarrolladores a familiarizarse con el código más rápidamente.
6. **Abstracción**: Utilizar niveles adecuados de abstracción permite ocultar detalles complejos y resaltar la lógica principal del código.
7. **Alineación de estilo**: Mantener un estilo de codificación uniforme (espaciado, sangrías, uso de mayúsculas/minúsculas) contribuye a la legibilidad general del código.

// Ejemplo de código legible

```python
def calcular_area_circulo(radio):
    import math
    if radio < 0:
        raise ValueError("El radio no puede ser negativo.")
    area = math.pi * (radio ** 2)
    return area
```

// Ejemplo de código menos legible

```python
def calc(r):
    import math
    if r < 0:
        raise ValueError("Neg")
    a = 3.14 * (r * r)
    return a
```

### Comparación entre código legible y menos legible

| Aspecto                | Código legible                                   | Código menos legible                |
|------------------------|--------------------------------------------------|-------------------------------------|
| Intención              | Claramente expresa el propósito de la función    | Propósito poco claro                |
| Nombres                | Nombres descriptivos (`calcular_area_circulo`, `radio`, `area`) | Nombres ambiguos (`calc`, `r`, `a`) |
| Brevidad               | Conciso sin perder claridad                      | Conciso pero sacrifica claridad     |
| Cohesión               | Función realiza una sola tarea                   | Similar, pero menos explícito       |
| Consistencia           | Sigue convenciones de nombres y errores          | Convenciones inconsistentes         |
| Abstracción            | Utiliza constantes y módulos apropiados          | Usa valores mágicos (3.14)          |
| Alineación de estilo   | Estilo uniforme y claro                          | Estilo menos cuidado                |

En resumen, la legibilidad del código es esencial para el desarrollo de software efectivo. Al centrarse en estos factores, los desarrolladores pueden crear código que sea fácil de entender, mantener y colaborar con otros.

## Principios prácticos para mejorar la legibilidad

1. **Nombres que digan la verdad**: Utiliza nombres claros y precisos, alineados con el dominio del problema y el lenguaje ubicuo del equipo.
2. **Funciones cortas y con una sola intención**: Cada función debe realizar una única tarea y mantener un solo nivel de abstracción por bloque de código.
3. **Guard clauses / early return**: Usa condiciones de salida temprana para evitar la indentación excesiva y facilitar la lectura.
4. **Evita comentarios obvios**: Comenta el "por qué" detrás de las decisiones, no el "qué" hace el código.
5. **Formato consistente**: Sigue las convenciones del equipo y utiliza herramientas de análisis estático para mantener la uniformidad.
6. **Baja complejidad ciclomática/cognitiva**: Divide el código en partes pequeñas y manejables para reducir la dificultad de comprensión y mantenimiento.

7. **Evita la negación en condiciones**: Las condiciones positivas son más fáciles de entender que las negativas.
8. **Evita los valores mágicos**: Usa constantes con nombres descriptivos en lugar de números o cadenas literales.
9. **Evita abreviaciones innecesarias**: Los nombres completos son generalmente más claros que las abreviaciones.

## Errores comunes que afectan la legibilidad

### Dios-método (exceso de responsabilidades)

Un "dios-método" es una función o método que asume demasiadas responsabilidades, abarcando múltiples tareas o lógicas dentro de un solo bloque de código. Esto dificulta su comprensión, mantenimiento y reutilización. Los métodos extensos suelen ser difíciles de probar y propensos a errores, ya que cualquier cambio puede afectar varias partes de la funcionalidad.

**Cómo evitarlo:** Divide las funciones grandes en funciones más pequeñas y especializadas, cada una con una única responsabilidad. Aplica el principio de responsabilidad única (SRP) para mejorar la legibilidad y mantenibilidad del código.

### Nulls silenciosos 

**Nulls silenciosos:** Asignar o devolver valores nulos sin un manejo explícito puede ocultar errores y dificultar el rastreo de problemas en el código. Los nulls silenciosos suelen provocar fallos inesperados en tiempo de ejecución y hacen que el flujo de la aplicación sea menos predecible.

**Cómo evitarlo:** Maneja los valores nulos de forma explícita, utilizando validaciones, excepciones o valores por defecto según corresponda. Documenta claramente cuándo y por qué una función puede devolver un valor nulo.

### Nombres genéricos

**Variables con nombres genéricos:** Usar nombres como `data`, `temp`, `item` o `value` dificulta la comprensión del propósito y el contenido de una variable. Esto reduce la claridad y puede llevar a confusiones, especialmente en funciones largas o contextos complejos.

**Cómo evitarlo:** Elige nombres descriptivos que reflejen el significado y el uso de la variable en el contexto del código. Nombres claros facilitan la lectura y el mantenimiento.
