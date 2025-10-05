# sistemas de control de versiones

Los sistemas de control de versiones (VCS, por sus siglas en inglés) son herramientas esenciales en el desarrollo de software que permiten gestionar los cambios en el código fuente a lo largo del tiempo. Estos sistemas facilitan la colaboración entre desarrolladores, el seguimiento de modificaciones y la recuperación de versiones anteriores del código.

## Tipos de sistemas de control de versiones

Existen dos tipos principales de sistemas de control de versiones:

1. **Sistemas de control de versiones centralizados (CVCS)**: En este modelo, existe un único repositorio central donde se almacenan todas las versiones del código. Los desarrolladores realizan cambios y actualizaciones directamente en este repositorio. Ejemplos de CVCS incluyen Subversion (SVN) y Concurrent Versions System (CVS).

2. **Sistemas de control de versiones distribuidos (DVCS)**: En este modelo, cada desarrollador tiene una copia completa del repositorio en su máquina local. Los cambios se realizan localmente y luego se sincronizan con el repositorio central o con otros desarrolladores. Ejemplos populares de DVCS incluyen Git y Mercurial.

## Estrategias de control de versiones

Algunas estrategias comunes para utilizar sistemas de control de versiones incluyen:

- **Modelos de ramificación**: Existen diferentes estrategias para gestionar ramas, como Git Flow, GitHub Flow o trunk-based development. Cada modelo define cómo y cuándo crear, fusionar o eliminar ramas según el flujo de trabajo del equipo y el ciclo de vida del proyecto.

- **Convenciones de commits**: Es recomendable seguir convenciones para los mensajes de commit, como Conventional Commits, que ayudan a mantener un historial claro y facilitan la automatización de tareas como el versionado y la generación de changelogs.

- **Pull, Merge, Rebase, Pull Requests y Checklist**: Las operaciones como pull, merge y rebase permiten integrar cambios entre ramas. Los Pull Requests (PRs) son revisiones colaborativas antes de fusionar cambios a la rama principal, y suelen incluir checklists para asegurar la calidad y el cumplimiento de las políticas del repositorio.

- **Reglas de protección y políticas**: Se pueden establecer reglas de protección en ramas críticas (como main/master) para requerir revisiones, aprobaciones o la ejecución exitosa de pruebas automáticas antes de permitir fusiones.

- **Versionado y releases**: El versionado semántico (SemVer) es una práctica común para etiquetar lanzamientos y comunicar cambios. Las releases permiten distribuir versiones estables y documentar nuevas funcionalidades, correcciones y mejoras.

- **Integración y entrega continua (CI/CD)**: La automatización de pruebas, builds y despliegues mediante pipelines de CI/CD mejora la calidad y la velocidad de entrega del software.

- **Higiene del repositorio**: Mantener el repositorio limpio implica eliminar ramas obsoletas, documentar procesos, organizar archivos y asegurar que el historial sea comprensible.

- **Decisión arquitectónica (Monorepo vs Multirepo)**: Elegir entre un único repositorio para varios proyectos (monorepo) o repositorios separados (multirepo) depende de factores como la escala del equipo, la modularidad del código y las necesidades de integración.

### GitFlow

GitFlow es una estrategia de ramificación popular que define un flujo de trabajo estructurado para gestionar el desarrollo de software utilizando Git. Fue propuesto por Vincent Driessen y se basa en la creación de ramas específicas para diferentes propósitos dentro del ciclo de vida del desarrollo.

Los elementos clave de GitFlow incluyen:

- **Rama principal (main)**: Esta es la rama estable que siempre refleja el estado de producción del software.

- **Rama de desarrollo (develop)**: Esta rama se utiliza para integrar características en desarrollo. Es la base para las ramas de características y se fusiona en la rama principal cuando se prepara un lanzamiento.

- **Ramas de características (feature)**: Se crean a partir de la rama de desarrollo para trabajar en nuevas funcionalidades. Una vez completadas, se fusionan de nuevo en la rama de desarrollo.

- **Ramas de lanzamiento (release)**: Se crean a partir de la rama de desarrollo cuando se está listo para preparar un nuevo lanzamiento. Permiten realizar pruebas finales y correcciones de errores antes de fusionarse en la rama principal.

- **Ramas de hotfix (hotfix)**: Se utilizan para corregir errores críticos en la rama principal. Se crean a partir de la rama principal y, una vez solucionado el problema, se fusionan de nuevo en la rama principal y en la rama de desarrollo.

GitFlow proporciona un marco claro para gestionar el desarrollo de software, facilitando la colaboración entre equipos y la implementación de nuevas funcionalidades de manera controlada.

### Buenas prácticas recomendadas

- **Rama principal protegida**: La rama `main` (y `develop`, si existe) debe estar protegida. Esto implica requerir Pull Requests (PR) y al menos una aprobación antes de permitir fusiones.
- **Ramas por feature**: Cada nueva funcionalidad o corrección debe desarrollarse en una rama independiente creada a partir de `develop` o `main`.
- **Pull Requests y despliegue continuo**: Los cambios se integran mediante PRs, que activan pipelines de integración y despliegue continuo (CI/CD).
- **Commits atómicos y descriptivos**: Cada commit debe representar un cambio coherente y estar claramente descrito.
- **Convenciones de commits**: Se recomienda usar Conventional Commits, por ejemplo:  
        - `feat[#Tarea]: Mensaje` para nuevas funcionalidades  
        - `fix[#Tarea]: Mensaje` para correcciones  

    En estas convenciones, `[#Tarea]` hace referencia al identificador único de la tarea, incidencia o requerimiento en el sistema de gestión de proyectos (por ejemplo, Jira, Azure Boards, Trello, etc.). Incluir este identificador en el mensaje de commit permite establecer una trazabilidad clara entre los cambios realizados en el código y las tareas o historias de usuario correspondientes. Así, es posible rastrear fácilmente qué cambios se relacionan con cada requerimiento, facilitando auditorías, revisiones y el seguimiento del avance del proyecto.

    Por ejemplo:
        - `feat[#123]: Añadir autenticación de usuarios`
        - `fix[#456]: Corregir error en el cálculo de totales`

    Además, se recomienda utilizar comandos como `amend` y `rebase` para mantener un historial de commits limpio y organizado, evitando mensajes innecesarios o commits intermedios irrelevantes. Esto contribuye a una mejor comprensión del historial y facilita la colaboración entre los miembros del equipo.

        - `git commit --amend` se utiliza para modificar el último commit realizado. Es útil cuando necesitas corregir el mensaje del commit o agregar/quitar archivos que olvidaste incluir. En lugar de crear un nuevo commit, `amend` reemplaza el commit anterior, manteniendo el historial más limpio y evitando commits innecesarios por pequeños errores o ajustes.

        - `git rebase` permite reorganizar, editar o combinar commits antes de integrarlos en otra rama. Es especialmente útil para mantener un historial lineal y comprensible, eliminando commits intermedios irrelevantes o resolviendo conflictos antes de fusionar ramas. A diferencia de `merge`, que conserva la historia de ramificación, `rebase` reescribe la historia para que los cambios parezcan aplicados directamente sobre la rama de destino.

    Ambas herramientas ayudan a mantener un historial de commits claro, ordenado y fácil de entender, lo que facilita la colaboración y la revisión de código.

- **PRs pequeños y claros**: Idealmente, los PRs deben ser de menos de 300 líneas, incluir una descripción, evidencia (capturas de pantalla, tests) y criterios de aceptación.
- **Checklist de aprobación**: Antes de aprobar un PR, verificar que:
        - El código compila
        - Los tests pasan
        - No hay credenciales expuestas
        - Los nombres son claros
        - Se ha considerado el impacto en la documentación y notas de release
- **.gitignore adecuado**: Configurar el archivo `.gitignore` según el stack tecnológico para evitar subir archivos innecesarios o sensibles.

