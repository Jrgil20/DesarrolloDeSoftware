# Singleton (Instancia Única)

## Categoría
**Patrón Creacional**

---

## Propósito

Garantiza que una clase tenga una única instancia y proporciona un punto de acceso global a ella.

---

## Problema

Algunas clases deben tener exactamente una instancia:

- **Configuración global** de la aplicación
- **Pool de conexiones** a base de datos
- **Logger** centralizado
- **Gestor de caché** global

Si permites crear múltiples instancias de estas clases, podrías tener:
- Comportamiento inconsistente
- Desperdicio de recursos
- Conflictos de estado

Además, necesitas acceso global a esa instancia desde cualquier parte del código.

---

## Solución

El patrón Singleton resuelve ambos problemas:

1. **Constructor privado**: Previene que otras clases usen `new` con la clase Singleton
2. **Método estático getInstance()**: Crea la instancia si no existe, o devuelve la existente
3. **Variable estática privada**: Almacena la única instancia

---

## Estructura UML

### Diagrama de Clases

```class diagram
┌────────────────────────┐
│      Singleton         │
├────────────────────────┤
│ - instance: Singleton  │ {static}
├────────────────────────┤
│ - Singleton()          │ ← Constructor privado
│ + getInstance()        │ {static}
│ + businessLogic()      │
└────────────────────────┘
```

---

## Componentes

1. **Constructor privado**: Previene instanciación externa
2. **Variable estática**: Almacena la única instancia
3. **Método getInstance()**: Proporciona acceso controlado a la instancia

---

## Implementaciones por Lenguaje

Este patrón ha sido implementado en múltiples lenguajes. A continuación encontrarás ejemplos de código en carpetas organizadas por lenguaje:

### 📁 Ejemplos Disponibles

- **[Java](./java/)** - Implementaciones thread-safe con diferentes variantes
- **[C#](./csharp/)** - Implementación .NET con lazy initialization
- **[TypeScript](./typescript/)** - Implementación con control de instancia única

Cada carpeta contiene:
- ✅ Múltiples variantes del patrón (lazy, eager, thread-safe)
- ✅ Ejemplos del mundo real (Logger, Config, ConnectionPool)
- ✅ Solución a problemas comunes (reflexión, serialización, multi-threading)
- ✅ Referencias a repositorios reconocidos
- ✅ Tests de unicidad de instancia

### ⚠️ Consideración Importante

El patrón Singleton es considerado **controversial** en el desarrollo moderno. Antes de usarlo, considera alternativas como **Dependency Injection**. Consulta las carpetas de cada lenguaje para ver análisis detallado de pros/cons y alternativas.

---

## Diagrama de Secuencia

```sequence diagram
:Cliente1        :Singleton        :Cliente2
   │                 │                 │
   │─getInstance()──>▌                 │
   │                 ▌─┐               │
   │                 ▌ │ ¿existe?      │
   │                 ▌ │ No, crear     │
   │                 ▌<┘               │
   │<┄instancia┄┄┄┄ ▌                 │
   │                 │                 │
   │                 │                 │
   │                 │<─getInstance()──│
   │                 ▌─┐               │
   │                 ▌ │ ¿existe?      │
   │                 ▌ │ Sí, retornar  │
   │                 ▌<┘               │
   │                 ▌┄┄instancia┄┄┄┄>│
   │                 │                 │
   │─businessLogic()->▌                 │
   │<┄resultado┄┄┄┄ ▌                 │
   │                 │                 │
```

---

## Ventajas ✅

1. **Una única instancia garantizada**
2. **Acceso global controlado** a esa instancia
3. **Inicialización perezosa** (lazy initialization)
4. **Ahorro de recursos**: No se crean instancias innecesarias
5. **Estado global consistente**

---

## Desventajas ❌

1. **Viola Single Responsibility Principle**: Controla su creación y lógica de negocio
2. **Dificulta testing**: Difícil de mockear en pruebas unitarias
3. **Acoplamiento global**: Crea dependencias ocultas
4. **Problemas de concurrencia**: Requiere cuidado especial en entornos multi-hilo
5. **Puede enmascarar mal diseño**: A veces indica que deberías usar inyección de dependencias

---

## Cuándo Usar

✅ **Usa Singleton cuando:**

- Necesitas exactamente una instancia de una clase
- Requieres acceso global a esa instancia
- La inicialización debe ser perezosa (lazy)
- Ejemplos: configuración, logging, caché, pool de conexiones

❌ **Evita Singleton cuando:**

- Puedes usar inyección de dependencias
- Necesitas testear fácilmente (Singletons son difíciles de mockear)
- La clase puede necesitar múltiples instancias en el futuro
- Estás tentado a usarlo solo por "conveniencia"

---

## Casos de Uso Reales

### 1. **Sistema de Logging**
```python
class Logger(Singleton):
    def __init__(self):
        self.log_file = "app.log"
    
    def log(self, message):
        with open(self.log_file, 'a') as f:
            f.write(f"{message}\n")
```

### 2. **Configuración de Aplicación**
```python
class AppConfig(Singleton):
    def __init__(self):
        self.config = self.load_config()
    
    def get(self, key):
        return self.config.get(key)
```

### 3. **Pool de Conexiones**
```python
class ConnectionPool(Singleton):
    def __init__(self):
        self.connections = []
        self.max_size = 10
    
    def get_connection(self):
        # Lógica de pool
        pass
```

### 4. **Caché Global**
```python
class Cache(Singleton):
    def __init__(self):
        self.data = {}
    
    def set(self, key, value):
        self.data[key] = value
    
    def get(self, key):
        return self.data.get(key)
```

---

## Variantes del Patrón

### 1. **Eager Initialization**
```python
class EagerSingleton:
    _instance = EagerSingleton()  # Creada inmediatamente
    
    def __init__(self):
        pass
```

### 2. **Lazy Initialization**
```python
class LazySingleton:
    _instance = None
    
    def __new__(cls):
        if cls._instance is None:
            cls._instance = super().__new__(cls)
        return cls._instance
```

### 3. **Thread-Safe**
```python
from threading import Lock

class ThreadSafeSingleton:
    _instance = None
    _lock = Lock()
    
    def __new__(cls):
        if cls._instance is None:
            with cls._lock:
                if cls._instance is None:
                    cls._instance = super().__new__(cls)
        return cls._instance
```

---

## Problemas Comunes y Soluciones

### Problema 1: Multi-threading
**Solución**: Double-check locking con lock

### Problema 2: Serialización
**Solución**: Implementar métodos especiales de serialización

### Problema 3: Reflexión
**Solución**: Lanzar excepción en constructor si ya existe instancia

### Problema 4: Testing
**Solución**: Proporcionar método reset() para tests o usar inyección de dependencias

---

## Alternativas Modernas

En lugar de Singleton, considera:

1. **Inyección de Dependencias**: Frameworks como Spring, Angular
2. **Context Managers**: En Python
3. **Módulos**: En lenguajes que los soportan nativamente
4. **Service Locator**: Patrón alternativo

---

## Relación con Otros Patrones

- **Abstract Factory**: Las factories suelen implementarse como Singleton
- **Facade**: Las facades suelen ser Singletons
- **State**: Los estados suelen ser Singletons

---

## Relación con Principios SOLID

| Principio | Cumplimiento |
|-----------|--------------|
| **SRP** | ❌ Viola: Controla creación y lógica de negocio |
| **OCP** | ✅ Cerrado para modificación |
| **LSP** | ✅ No aplica herencia típicamente |
| **ISP** | ✅ Interfaz simple |
| **DIP** | ⚠️ Depende: Puede crear acoplamiento |

---

## Ejercicios Prácticos

### Ejercicio 1: Logger Thread-Safe
Implementa un logger que escriba en archivo de forma thread-safe.

### Ejercicio 2: Game Manager
Crea un gestor de juego que controle puntuación, nivel y estado.

### Ejercicio 3: Database Manager
Diseña un gestor de base de datos con pool de conexiones limitado.

---

## Referencias

- [Refactoring Guru - Singleton](https://refactoring.guru/design-patterns/singleton)
- Gang of Four - Design Patterns
- [SourceMaking - Singleton](https://sourcemaking.com/design_patterns/singleton)
- [Why Singletons are Controversial](https://stackoverflow.com/questions/137975/what-is-so-bad-about-singletons)

---

[← Volver a Patrones Creacionales](../Creacionales.md)
