# Prototype (Prototipo)

## Categoría
**Patrón Creacional**

---

## Propósito

Permite copiar objetos existentes sin que el código dependa de sus clases concretas. Especifica los tipos de objetos a crear mediante una instancia prototípica y crea nuevos objetos copiando este prototipo.

---

## Problema

Imagina que necesitas crear una copia exacta de un objeto existente:

**Problemas sin Prototype**:
1. Necesitas conocer la clase concreta del objeto
2. El objeto puede tener campos privados no accesibles
3. Tu código queda acoplado a clases concretas
4. Crear objetos complejos desde cero es costoso

```python
# ❌ Problema: Crear copia manual
objeto_original = ObjetoComplejo(
    param1, param2, param3, ...
)

# Necesitas saber todos los detalles internos
objeto_copia = ObjetoComplejo(
    objeto_original.param1,
    objeto_original.param2,
    objeto_original.param3,
    ...
)
```

---

## Solución

El patrón Prototype delega el proceso de clonación a los propios objetos que están siendo clonados. Declara una interfaz común para todos los objetos que soportan clonación, permitiendo clonar un objeto sin acoplar el código a su clase.

**Características**:
- Clonación mediante método `clone()`
- Copia superficial vs profunda
- Registro de prototipos (opcional)
- Reutilización de objetos configurados

---

## Estructura UML

### Diagrama de Clases

```class diagram
┌──────────────────────┐
│   «interface»        │
│     Prototype        │
├──────────────────────┤
│ + clone(): Prototype │
└──────────────────────┘
          △
          │ implementa
    ┌─────┴─────┐
    │           │
┌─────────────┐ ┌─────────────┐
│ Prototype1  │ │ Prototype2  │
├─────────────┤ ├─────────────┤
│ - field1    │ │ - fieldA    │
│ - field2    │ │ - fieldB    │
├─────────────┤ ├─────────────┤
│ + clone()   │ │ + clone()   │
└─────────────┘ └─────────────┘


┌─────────────────────────┐
│ PrototypeRegistry       │ (Opcional)
├─────────────────────────┤
│ - prototypes: Map       │
├─────────────────────────┤
│ + register(key, proto)  │
│ + unregister(key)       │
│ + getClone(key)         │
└─────────────────────────┘
```

---

## Componentes

1. **Prototype**: Interfaz que declara el método `clone()`
2. **ConcretePrototype**: Implementa el método de clonación
3. **PrototypeRegistry** (opcional): Gestiona prototipos disponibles
4. **Client**: Crea nuevos objetos clonando prototipos

---

## Implementaciones por Lenguaje

Este patrón ha sido implementado en múltiples lenguajes. A continuación encontrarás ejemplos de código en carpetas organizadas por lenguaje:

### 📁 Ejemplos Disponibles

- **[Java](./java/)** - Implementación con clonación superficial y profunda
- **[C#](./csharp/)** - Implementación .NET con ICloneable y MemberwiseClone
- **[TypeScript](./typescript/)** - Implementación type-safe con clonación

Cada carpeta contiene:
- ✅ Diferencias entre clonación superficial y profunda
- ✅ Prototype Registry para gestionar prototipos
- ✅ Solución a problemas de referencias circulares
- ✅ Referencias a repositorios reconocidos
- ✅ Comparación de rendimiento vs creación directa

---

## Implementación (Vista previa)

### Python

```python
import copy
from abc import ABC, abstractmethod

# Prototype Interface
class Prototype(ABC):
    @abstractmethod
    def clone(self):
        """Crea una copia del objeto"""
        pass

# Concrete Prototype 1
class Person(Prototype):
    def __init__(self, name: str, age: int, address: dict):
        self.name = name
        self.age = age
        self.address = address  # Objeto mutable
    
    def clone(self):
        """Clonación profunda"""
        return copy.deepcopy(self)
    
    def shallow_clone(self):
        """Clonación superficial"""
        return copy.copy(self)
    
    def __str__(self):
        return f"Person(name={self.name}, age={self.age}, address={self.address})"

# Concrete Prototype 2
class Shape(Prototype):
    def __init__(self, x: int, y: int, color: str):
        self.x = x
        self.y = y
        self.color = color
    
    def clone(self):
        return copy.deepcopy(self)
    
    def __str__(self):
        return f"{self.__class__.__name__}(x={self.x}, y={self.y}, color={self.color})"

class Circle(Shape):
    def __init__(self, x: int, y: int, color: str, radius: int):
        super().__init__(x, y, color)
        self.radius = radius
    
    def __str__(self):
        return f"Circle(x={self.x}, y={self.y}, color={self.color}, radius={self.radius})"

class Rectangle(Shape):
    def __init__(self, x: int, y: int, color: str, width: int, height: int):
        super().__init__(x, y, color)
        self.width = width
        self.height = height
    
    def __str__(self):
        return f"Rectangle(x={self.x}, y={self.y}, color={self.color}, width={self.width}, height={self.height})"

# Prototype Registry (opcional)
class ShapeRegistry:
    def __init__(self):
        self._prototypes = {}
    
    def register(self, key: str, prototype: Prototype):
        """Registra un prototipo"""
        self._prototypes[key] = prototype
    
    def unregister(self, key: str):
        """Elimina un prototipo"""
        del self._prototypes[key]
    
    def get_clone(self, key: str):
        """Obtiene un clon del prototipo"""
        prototype = self._prototypes.get(key)
        if prototype:
            return prototype.clone()
        return None
    
    def list_prototypes(self):
        """Lista todos los prototipos registrados"""
        return list(self._prototypes.keys())

# Uso
if __name__ == "__main__":
    # Ejemplo 1: Clonación simple
    print("=== Ejemplo 1: Clonación Simple ===")
    person1 = Person("Juan", 30, {"city": "Madrid", "country": "España"})
    person2 = person1.clone()
    
    print(f"Original: {person1}")
    print(f"Clon: {person2}")
    print(f"¿Son el mismo objeto? {person1 is person2}")
    print(f"¿Tienen el mismo contenido? {person1.name == person2.name}")
    
    # Modificar el clon no afecta al original
    person2.name = "María"
    person2.address["city"] = "Barcelona"
    
    print(f"\nDespués de modificar el clon:")
    print(f"Original: {person1}")
    print(f"Clon modificado: {person2}")
    
    # Ejemplo 2: Clonación superficial vs profunda
    print("\n=== Ejemplo 2: Shallow vs Deep Copy ===")
    person3 = Person("Pedro", 25, {"city": "Valencia", "country": "España"})
    person4_shallow = person3.shallow_clone()
    person5_deep = person3.clone()
    
    # Modificar direcciones
    person4_shallow.address["city"] = "Sevilla"
    person5_deep.address["city"] = "Bilbao"
    
    print(f"Original: {person3}")
    print(f"Shallow clone: {person4_shallow}")
    print(f"Deep clone: {person5_deep}")
    
    # Ejemplo 3: Registry
    print("\n=== Ejemplo 3: Prototype Registry ===")
    registry = ShapeRegistry()
    
    # Registrar prototipos
    registry.register("red_circle", Circle(10, 10, "red", 5))
    registry.register("blue_rectangle", Rectangle(20, 20, "blue", 100, 50))
    
    # Clonar desde el registro
    shape1 = registry.get_clone("red_circle")
    shape2 = registry.get_clone("red_circle")
    shape3 = registry.get_clone("blue_rectangle")
    
    print(f"Shape 1: {shape1}")
    print(f"Shape 2: {shape2}")
    print(f"Shape 3: {shape3}")
    
    # Modificar clones
    shape1.x = 50
    shape2.radius = 15
    
    print(f"\nDespués de modificaciones:")
    print(f"Shape 1: {shape1}")
    print(f"Shape 2: {shape2}")
    
    # Listar prototipos disponibles
    print(f"\nPrototipos disponibles: {registry.list_prototypes()}")
```

### Java

```java
import java.util.*;

// Prototype Interface
interface Prototype extends Cloneable {
    Prototype clone();
}

// Concrete Prototype
class Shape implements Prototype {
    protected int x;
    protected int y;
    protected String color;
    
    public Shape() {}
    
    public Shape(Shape source) {
        if (source != null) {
            this.x = source.x;
            this.y = source.y;
            this.color = source.color;
        }
    }
    
    @Override
    public Shape clone() {
        return new Shape(this);
    }
    
    @Override
    public boolean equals(Object obj) {
        if (!(obj instanceof Shape)) return false;
        Shape shape = (Shape) obj;
        return shape.x == x && shape.y == y && 
               Objects.equals(shape.color, color);
    }
}

class Circle extends Shape {
    private int radius;
    
    public Circle() {}
    
    public Circle(Circle source) {
        super(source);
        if (source != null) {
            this.radius = source.radius;
        }
    }
    
    @Override
    public Circle clone() {
        return new Circle(this);
    }
    
    @Override
    public boolean equals(Object obj) {
        if (!(obj instanceof Circle) || !super.equals(obj)) return false;
        Circle circle = (Circle) obj;
        return circle.radius == radius;
    }
    
    @Override
    public String toString() {
        return String.format("Circle(x=%d, y=%d, color=%s, radius=%d)",
                           x, y, color, radius);
    }
}

class Rectangle extends Shape {
    private int width;
    private int height;
    
    public Rectangle() {}
    
    public Rectangle(Rectangle source) {
        super(source);
        if (source != null) {
            this.width = source.width;
            this.height = source.height;
        }
    }
    
    @Override
    public Rectangle clone() {
        return new Rectangle(this);
    }
    
    @Override
    public String toString() {
        return String.format("Rectangle(x=%d, y=%d, color=%s, width=%d, height=%d)",
                           x, y, color, width, height);
    }
}

// Prototype Registry
class ShapeRegistry {
    private Map<String, Shape> cache = new HashMap<>();
    
    public void register(String key, Shape prototype) {
        cache.put(key, prototype);
    }
    
    public Shape getClone(String key) {
        Shape prototype = cache.get(key);
        return (prototype != null) ? prototype.clone() : null;
    }
    
    public void loadCache() {
        Circle circle = new Circle();
        circle.x = 10;
        circle.y = 10;
        circle.color = "red";
        register("red_circle", circle);
        
        Rectangle rectangle = new Rectangle();
        rectangle.x = 20;
        rectangle.y = 20;
        rectangle.color = "blue";
        register("blue_rectangle", rectangle);
    }
}

// Demo
public class PrototypeDemo {
    public static void main(String[] args) {
        ShapeRegistry registry = new ShapeRegistry();
        registry.loadCache();
        
        Shape shape1 = registry.getClone("red_circle");
        Shape shape2 = registry.getClone("red_circle");
        Shape shape3 = registry.getClone("blue_rectangle");
        
        System.out.println("Shape 1: " + shape1);
        System.out.println("Shape 2: " + shape2);
        System.out.println("Shape 3: " + shape3);
        
        System.out.println("¿shape1 == shape2? " + (shape1 == shape2));
        System.out.println("¿shape1.equals(shape2)? " + shape1.equals(shape2));
    }
}
```

### TypeScript

```typescript
// Prototype Interface
interface Prototype {
    clone(): this;
}

// Concrete Prototype
class Shape implements Prototype {
    public x: number = 0;
    public y: number = 0;
    public color: string = "";
    
    constructor(source?: Shape) {
        if (source) {
            this.x = source.x;
            this.y = source.y;
            this.color = source.color;
        }
    }
    
    public clone(): this {
        return Object.create(this);
    }
}

class Circle extends Shape {
    public radius: number = 0;
    
    constructor(source?: Circle) {
        super(source);
        if (source) {
            this.radius = source.radius;
        }
    }
    
    public clone(): this {
        return new Circle(this) as this;
    }
    
    public toString(): string {
        return `Circle(x=${this.x}, y=${this.y}, color=${this.color}, radius=${this.radius})`;
    }
}

class Rectangle extends Shape {
    public width: number = 0;
    public height: number = 0;
    
    constructor(source?: Rectangle) {
        super(source);
        if (source) {
            this.width = source.width;
            this.height = source.height;
        }
    }
    
    public clone(): this {
        return new Rectangle(this) as this;
    }
    
    public toString(): string {
        return `Rectangle(x=${this.x}, y=${this.y}, color=${this.color}, width=${this.width}, height=${this.height})`;
    }
}

// Prototype Registry
class ShapeRegistry {
    private cache: Map<string, Shape> = new Map();
    
    public register(key: string, prototype: Shape): void {
        this.cache.set(key, prototype);
    }
    
    public getClone(key: string): Shape | undefined {
        const prototype = this.cache.get(key);
        return prototype ? prototype.clone() : undefined;
    }
    
    public loadCache(): void {
        const circle = new Circle();
        circle.x = 10;
        circle.y = 10;
        circle.color = "red";
        circle.radius = 5;
        this.register("red_circle", circle);
        
        const rectangle = new Rectangle();
        rectangle.x = 20;
        rectangle.y = 20;
        rectangle.color = "blue";
        rectangle.width = 100;
        rectangle.height = 50;
        this.register("blue_rectangle", rectangle);
    }
}

// Uso
const registry = new ShapeRegistry();
registry.loadCache();

const shape1 = registry.getClone("red_circle");
const shape2 = registry.getClone("red_circle");
const shape3 = registry.getClone("blue_rectangle");

console.log("Shape 1:", shape1?.toString());
console.log("Shape 2:", shape2?.toString());
console.log("Shape 3:", shape3?.toString());

console.log("¿shape1 === shape2?", shape1 === shape2); // false
```

---

## Diagrama de Secuencia

```sequence diagram
:Cliente      :PrototypeRegistry    :Prototype    :NewObject
   │                 │                   │              │
   │─getClone(key)──>▌                   │              │
   │                 ▌─get(key)─────────>▌              │
   │                 ▌<┄┄prototype┄┄┄┄┄ ▌              │
   │                 ▌                   │              │
   │                 ▌─clone()──────────>▌              │
   │                 ▌                   ▌─┐            │
   │                 ▌                   ▌ │ copiar     │
   │                 ▌                   ▌ │ campos     │
   │                 ▌                   ▌<┘            │
   │                 ▌                   ▌── «create» ──> ┌──────────┐
   │                 ▌                   ▌               │:NewObject│
   │                 ▌                   ▌               └──────────┘
   │                 ▌<┄┄┄clone┄┄┄┄┄┄┄┄ ▌                    │
   │<┄┄clone┄┄┄┄┄┄┄ ▌                   │                    │
   │                 │                   │                    │
   │─usar()─────────────────────────────────────────────────>▌
   │                 │                   │                    │
```

---

## Ventajas ✅

1. **Clona objetos sin acoplar** el código a sus clases
2. **Elimina código de inicialización repetitivo**
3. **Produce objetos complejos** más convenientemente
4. **Alternativa a herencia** para manejar configuraciones
5. **Mejora el rendimiento**: Evita costosas inicializaciones

---

## Desventajas ❌

1. **Clonar objetos con referencias circulares** es complicado
2. **Problemas con clonación profunda**: Requiere cuidado especial
3. **Puede ser confuso**: Especialmente la diferencia entre shallow y deep copy
4. **No todos los objetos** pueden clonarse fácilmente

---

## Cuándo Usar

✅ **Usa Prototype cuando:**

- Necesitas copiar objetos sin depender de sus clases concretas
- La creación de objetos es costosa (consultas BD, cálculos complejos)
- Quieres reducir el número de subclases que solo difieren en inicialización
- Necesitas configuraciones predefinidas de objetos

❌ **Evita Prototype cuando:**

- Los objetos son simples y baratos de crear
- No necesitas múltiples instancias similares
- La clonación es más compleja que la creación directa

---

## Casos de Uso Reales

### 1. **Editor Gráfico**
```python
# Clonar formas con configuración específica
circle_prototype = Circle(x=0, y=0, color="blue", radius=10)
circle1 = circle_prototype.clone()
circle2 = circle_prototype.clone()
```

### 2. **Sistema de Documentos**
```python
# Plantillas de documentos predefinidas
template_registry = DocumentRegistry()
template_registry.register("invoice", invoice_template)
template_registry.register("report", report_template)

new_invoice = template_registry.get_clone("invoice")
```

### 3. **Configuraciones de Juego**
```python
# Enemigos con configuraciones predefinidas
enemy_registry = EnemyRegistry()
enemy_registry.register("goblin", goblin_prototype)
enemy_registry.register("dragon", dragon_prototype)

enemy1 = enemy_registry.get_clone("goblin")
enemy2 = enemy_registry.get_clone("dragon")
```

### 4. **Cache de Objetos**
```python
# Cache de objetos pesados
cache = ObjectCache()
obj = cache.get_or_clone("heavy_object")
```

---

## Shallow Copy vs Deep Copy

### Shallow Copy (Copia Superficial)
```python
import copy

original = {"name": "Juan", "address": {"city": "Madrid"}}
shallow = copy.copy(original)

shallow["address"]["city"] = "Barcelona"
# ⚠️ Modifica también el original porque comparten la referencia
print(original["address"]["city"])  # "Barcelona"
```

### Deep Copy (Copia Profunda)
```python
import copy

original = {"name": "Juan", "address": {"city": "Madrid"}}
deep = copy.deepcopy(original)

deep["address"]["city"] = "Barcelona"
# ✅ No afecta al original
print(original["address"]["city"])  # "Madrid"
print(deep["address"]["city"])      # "Barcelona"
```

---

## Relación con Otros Patrones

- **Abstract Factory**: Puede almacenar un conjunto de prototipos
- **Composite + Prototype**: Útil para clonar estructuras complejas
- **Decorator + Prototype**: Puede clonar objetos decorados
- **Memento**: A veces usa Prototype para guardar snapshots

---

## Relación con Principios SOLID

| Principio | Cómo lo cumple |
|-----------|----------------|
| **SRP** | Delega la clonación al propio objeto |
| **OCP** | Puedes añadir nuevos prototipos sin modificar código cliente |
| **LSP** | Los clones son sustituibles por los originales |
| **DIP** | El cliente depende de la interfaz Prototype |

---

## Ejercicios Prácticos

### Ejercicio 1: Sistema de Configuración
Crea un sistema que clone configuraciones predefinidas de aplicación.

### Ejercicio 2: Editor de Imágenes
Implementa un sistema de capas en un editor que permita clonar capas con todos sus efectos.

### Ejercicio 3: Juego de Estrategia
Diseña un sistema de unidades donde puedas clonar ejércitos completos con sus configuraciones.

---

## Referencias

- [Refactoring Guru - Prototype](https://refactoring.guru/design-patterns/prototype)
- Gang of Four - Design Patterns
- [SourceMaking - Prototype](https://sourcemaking.com/design_patterns/prototype)
- [Python copy module](https://docs.python.org/3/library/copy.html)

---

[← Volver a Patrones Creacionales](../Creacionales.md)
