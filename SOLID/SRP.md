# Single Responsibility Principle (SRP)

> “Una clase, módulo o función debe tener una única razón de cambio.”  
> Es decir, cada componente del software debe estar enfocado en un solo propósito o responsabilidad. Si un cambio en un aspecto del sistema requiere modificar un componente, ese debe ser el único motivo válido para alterarlo.

## Claves prácticas

- **Propósito único y claro para cada componente:** Cada clase, módulo o función debe estar diseñada para cumplir una tarea específica y bien definida. Esto facilita su comprensión y mantenimiento.
- **Separación de dominios y responsabilidades técnicas:** Los cambios relacionados con la lógica de negocio (dominio) deben afectar solo a los componentes encargados de esa lógica, mientras que los cambios técnicos (infraestructura) deben impactar únicamente a los componentes de soporte.
- **Evitar responsabilidades múltiples:** Cuando un componente asume varias responsabilidades, aumenta el riesgo de efectos secundarios inesperados al modificarlo, dificultando la evolución del sistema.

### Aclaración de términos

- **Dominio:** Se refiere a la lógica central del negocio, es decir, las reglas y procesos que aportan valor directo al usuario o al objetivo principal de la aplicación.
- **Infraestructura:** Engloba los mecanismos de soporte como bases de datos, servicios externos, mensajería, frameworks y cualquier tecnología que permita ejecutar los casos de uso del dominio.
- **Responsabilidad única:** Es el conjunto de tareas que comparten un propósito común y coherente. Una clase puede realizar varias acciones, siempre que todas respondan al mismo motivo de cambio.

Distinguir claramente entre dominio e infraestructura ayuda a decidir qué clase debe modificarse ante un cambio. Si el cambio es de negocio, afecta al dominio; si es técnico, afecta a la infraestructura. Así se evita confundir responsabilidad con tareas aisladas y se mantiene la cohesión interna de los componentes.

## Beneficios inmediatos

- **Menos bugs:** Los cambios se limitan a áreas concretas, reduciendo el riesgo de errores colaterales.
- **Pruebas sencillas:** Componentes pequeños y enfocados son más fáciles de testear y validar.
- **Evolución ágil:** Es más sencillo reemplazar, modificar o mejorar partes específicas del sistema sin afectar el resto.
- **Diseños robustos:** Se logra alta cohesión (los elementos de una clase están fuertemente relacionados) y bajo acoplamiento (las clases dependen poco entre sí).

## ¿Cómo aplicarlo?

1. **Identifica cada motivo de cambio posible:** Analiza qué factores pueden requerir modificaciones en un componente.
2. **Divide responsabilidades:** Si detectas más de un motivo de cambio, separa las responsabilidades en diferentes clases o módulos.
3. **Usa nombres descriptivos:** El nombre de cada componente debe reflejar claramente su propósito único.
4. **Refactoriza cuando sea necesario:** Si una clase empieza a recibir cambios por motivos distintos, es momento de dividirla y reorganizar su código.

### Señales de alerta

- **“Clases navaja suiza”:** Son clases que hacen demasiadas cosas a la vez: validan datos, transforman información, llaman APIs externas y formatean resultados, todo en un solo bloque de código.  
    **¿Qué hacer?** Divide la clase en componentes más pequeños, cada uno con una única responsabilidad. Por ejemplo, crea una clase para validación, otra para acceso a APIs y otra para formateo de datos.

- **Concentración de motivos de cambio:** Si una clase contiene lógica de negocio, manejo de caché, logging y transporte de datos, cualquier cambio en uno de estos aspectos puede afectar la clase entera.  
    **¿Qué hacer?** Separa cada motivo de cambio en diferentes clases o servicios. Así, modificar el sistema de logging no afectará la lógica de negocio, por ejemplo.

- **Métodos extensos y complejos:** Métodos con muchos `switch` o `if` para manejar diferentes variantes de lógica suelen indicar que la clase está asumiendo demasiadas responsabilidades.  
    **¿Qué hacer?** Extrae cada variante de lógica en métodos o clases independientes. Considera usar patrones como Estrategia o Polimorfismo para manejar variantes.

- **Cambios divergentes:** Si una clase cambia frecuentemente por razones distintas (por ejemplo, cambios en reglas de negocio y cambios en el formato de salida), es probable que termine siendo una "God Class", difícil de mantener y entender.  
    **¿Qué hacer?** Refactoriza la clase para que cada una tenga un solo motivo de cambio. Así, cada clase será más pequeña, clara y fácil de mantener.

**Resumen:**  
El SRP recomienda que cada clase o módulo tenga una única razón para cambiar. Para lograrlo, identifica las distintas responsabilidades y sepáralas en componentes independientes. Esto mejora la mantenibilidad, facilita las pruebas y reduce errores al modificar el código.

## Criterios para detectar responsabilidades

Al aplicar el SRP, es útil pensar en los diferentes **ejes de cambio** que pueden afectar una clase o módulo. Pregúntate:

> **¿Qué parte del mundo externo cambia y me obliga a modificar esta clase?**

Identificar estos ejes ayuda a separar responsabilidades y mantener el código cohesivo. Algunos ejes típicos de cambio son:

- **Reglas de negocio:** Cambios en tarifas, impuestos, políticas, o cualquier lógica que afecte directamente al dominio.
- **Persistencia:** Modificaciones en la forma de almacenar datos, como cambios en Entity Framework, SQL, o colecciones en memoria.
- **Integraciones:** Adaptaciones necesarias para interactuar con servicios externos, como APIs HTTP/REST, autenticación con Keycloak, o notificaciones vía Firebase.
- **Presentación/Formato:** Cambios en la forma de exponer o transformar datos, como DTOs, mapeos, o plantillas de email.
- **Transversal:** Aspectos que cruzan varias capas, como logging, caching, políticas de reintentos (retry) o métricas.

**Conclusión:**  
Detectar y separar estos ejes de cambio permite diseñar componentes con una única responsabilidad, facilitando la evolución y el mantenimiento del sistema.
