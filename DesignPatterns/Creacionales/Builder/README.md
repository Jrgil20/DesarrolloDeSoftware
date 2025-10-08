# Builder (Constructor)

## Categoría
**Patrón Creacional**

---

## Propósito

Separa la construcción de un objeto complejo de su representación, permitiendo que el mismo proceso de construcción pueda crear diferentes representaciones.

---

## Problema

Imagina que necesitas crear objetos complejos con muchas partes y configuraciones:

```python
# ❌ Código problemático con constructor telescópico
casa = Casa(
    ventanas=4,
    puertas=2,
    pisos=2,
    garaje=True,
    piscina=False,
    jardin=True,
    color="blanco",
    techo="tejas",
    sistema_calefaccion="gas",
    tiene_sotano=True,
    num_habitaciones=3,
    num_banos=2
)
```

**Problemas**:
1. Constructor con demasiados parámetros
2. Difícil de leer y mantener
3. Muchos parámetros opcionales
4. Orden de parámetros fácil de confundir
5. Difícil añadir nuevos parámetros

---

## Solución

El patrón Builder sugiere extraer la construcción del objeto a una clase separada llamada **Builder**, que construye el objeto paso a paso.

**Características**:
- Construcción paso a paso
- Métodos fluidos (method chaining)
- Validación durante construcción
- Separación clara de construcción y representación

---

## Estructura UML

### Diagrama de Clases

```class diagram
┌──────────────────────┐
│      Director        │
├──────────────────────┤
│ - builder: Builder   │
├──────────────────────┤
│ + construct()        │
└──────┬───────────────┘
       │ usa
       ▼
┌──────────────────────┐
│   «interface»        │
│      Builder         │
├──────────────────────┤
│ + reset()            │
│ + buildStepA()       │
│ + buildStepB()       │
│ + getResult()        │
└──────────────────────┘
         △
         │ implementa
    ┌────┴─────┐
    │          │
┌─────────┐ ┌─────────┐
│Builder1 │ │Builder2 │ 
├─────────┤ ├─────────┤
│+build...│ │+build...│
│+getRes..│ │+getRes..│
└────┬────┘ └────┬────┘
     │           │
     │ construye │ construye
     ▼           ▼
┌─────────┐ ┌─────────┐
│Product1 │ │Product2 │
└─────────┘ └─────────┘
```

---

## Componentes

1. **Builder**: Interfaz que declara pasos de construcción
2. **ConcreteBuilder**: Implementa pasos específicos de construcción
3. **Product**: Objeto complejo que se está construyendo
4. **Director** (opcional): Define el orden de los pasos de construcción

---

## Implementaciones por Lenguaje

Este patrón ha sido implementado en múltiples lenguajes. A continuación encontrarás ejemplos de código en carpetas organizadas por lenguaje:

### 📁 Ejemplos Disponibles

- **[Java](./java/)** - Implementación con fluent interface y Director opcional
- **[C#](./csharp/)** - Implementación .NET con method chaining
- **[TypeScript](./typescript/)** - Implementación type-safe con builder pattern

Cada carpeta contiene:
- ✅ Construcción paso a paso de objetos complejos
- ✅ Ejemplos con múltiples configuraciones opcionales
- ✅ Patrón Director (opcional)
- ✅ Referencias a repositorios reconocidos
- ✅ Comparación con constructores telescópicos

---

## Implementación (Vista previa)

### Python

```python
from abc import ABC, abstractmethod
from typing import Optional

# Product
class House:
    def __init__(self):
        self.walls: Optional[str] = None
        self.windows: int = 0
        self.doors: int = 0
        self.roof: Optional[str] = None
        self.garage: bool = False
        self.pool: bool = False
        self.garden: bool = False
    
    def __str__(self):
        return f"""
        Casa:
        - Paredes: {self.walls}
        - Ventanas: {self.windows}
        - Puertas: {self.doors}
        - Techo: {self.roof}
        - Garaje: {'Sí' if self.garage else 'No'}
        - Piscina: {'Sí' if self.pool else 'No'}
        - Jardín: {'Sí' if self.garden else 'No'}
        """

# Builder Interface
class HouseBuilder(ABC):
    @abstractmethod
    def reset(self):
        pass
    
    @abstractmethod
    def build_walls(self, material: str):
        pass
    
    @abstractmethod
    def build_windows(self, count: int):
        pass
    
    @abstractmethod
    def build_doors(self, count: int):
        pass
    
    @abstractmethod
    def build_roof(self, type: str):
        pass
    
    @abstractmethod
    def build_garage(self):
        pass
    
    @abstractmethod
    def build_pool(self):
        pass
    
    @abstractmethod
    def build_garden(self):
        pass
    
    @abstractmethod
    def get_house(self) -> House:
        pass

# Concrete Builder
class ConcreteHouseBuilder(HouseBuilder):
    def __init__(self):
        self.reset()
    
    def reset(self):
        self._house = House()
    
    def build_walls(self, material: str):
        self._house.walls = material
        return self  # Para method chaining
    
    def build_windows(self, count: int):
        self._house.windows = count
        return self
    
    def build_doors(self, count: int):
        self._house.doors = count
        return self
    
    def build_roof(self, type: str):
        self._house.roof = type
        return self
    
    def build_garage(self):
        self._house.garage = True
        return self
    
    def build_pool(self):
        self._house.pool = True
        return self
    
    def build_garden(self):
        self._house.garden = True
        return self
    
    def get_house(self) -> House:
        house = self._house
        self.reset()  # Preparar para nueva construcción
        return house

# Director (opcional)
class HouseDirector:
    def __init__(self, builder: HouseBuilder):
        self._builder = builder
    
    def build_minimal_house(self):
        """Casa mínima viable"""
        return (self._builder
                .build_walls("Ladrillo")
                .build_windows(2)
                .build_doors(1)
                .build_roof("Chapa")
                .get_house())
    
    def build_luxury_house(self):
        """Casa de lujo"""
        return (self._builder
                .build_walls("Piedra")
                .build_windows(8)
                .build_doors(3)
                .build_roof("Tejas Premium")
                .build_garage()
                .build_pool()
                .build_garden()
                .get_house())
    
    def build_family_house(self):
        """Casa familiar"""
        return (self._builder
                .build_walls("Ladrillo")
                .build_windows(6)
                .build_doors(2)
                .build_roof("Tejas")
                .build_garage()
                .build_garden()
                .get_house())

# Uso
if __name__ == "__main__":
    # Construcción manual (sin Director)
    builder = ConcreteHouseBuilder()
    casa1 = (builder
             .build_walls("Madera")
             .build_windows(4)
             .build_doors(2)
             .build_roof("Tejas")
             .build_garage()
             .get_house())
    
    print("Casa personalizada:")
    print(casa1)
    
    # Construcción con Director
    director = HouseDirector(builder)
    
    print("\nCasa mínima:")
    casa_minimal = director.build_minimal_house()
    print(casa_minimal)
    
    print("\nCasa de lujo:")
    casa_lujo = director.build_luxury_house()
    print(casa_lujo)
    
    print("\nCasa familiar:")
    casa_familiar = director.build_family_house()
    print(casa_familiar)
```

### Java

```java
// Product
class House {
    private String walls;
    private int windows;
    private int doors;
    private String roof;
    private boolean garage;
    private boolean pool;
    private boolean garden;
    
    // Getters y setters
    public void setWalls(String walls) { this.walls = walls; }
    public void setWindows(int windows) { this.windows = windows; }
    public void setDoors(int doors) { this.doors = doors; }
    public void setRoof(String roof) { this.roof = roof; }
    public void setGarage(boolean garage) { this.garage = garage; }
    public void setPool(boolean pool) { this.pool = pool; }
    public void setGarden(boolean garden) { this.garden = garden; }
    
    @Override
    public String toString() {
        return String.format("""
            Casa:
            - Paredes: %s
            - Ventanas: %d
            - Puertas: %d
            - Techo: %s
            - Garaje: %s
            - Piscina: %s
            - Jardín: %s
            """, walls, windows, doors, roof, 
                 garage ? "Sí" : "No",
                 pool ? "Sí" : "No",
                 garden ? "Sí" : "No");
    }
}

// Builder Interface
interface HouseBuilder {
    HouseBuilder buildWalls(String material);
    HouseBuilder buildWindows(int count);
    HouseBuilder buildDoors(int count);
    HouseBuilder buildRoof(String type);
    HouseBuilder buildGarage();
    HouseBuilder buildPool();
    HouseBuilder buildGarden();
    House getHouse();
}

// Concrete Builder
class ConcreteHouseBuilder implements HouseBuilder {
    private House house;
    
    public ConcreteHouseBuilder() {
        this.reset();
    }
    
    public void reset() {
        this.house = new House();
    }
    
    @Override
    public HouseBuilder buildWalls(String material) {
        house.setWalls(material);
        return this;
    }
    
    @Override
    public HouseBuilder buildWindows(int count) {
        house.setWindows(count);
        return this;
    }
    
    @Override
    public HouseBuilder buildDoors(int count) {
        house.setDoors(count);
        return this;
    }
    
    @Override
    public HouseBuilder buildRoof(String type) {
        house.setRoof(type);
        return this;
    }
    
    @Override
    public HouseBuilder buildGarage() {
        house.setGarage(true);
        return this;
    }
    
    @Override
    public HouseBuilder buildPool() {
        house.setPool(true);
        return this;
    }
    
    @Override
    public HouseBuilder buildGarden() {
        house.setGarden(true);
        return this;
    }
    
    @Override
    public House getHouse() {
        House result = this.house;
        this.reset();
        return result;
    }
}

// Director
class HouseDirector {
    private HouseBuilder builder;
    
    public HouseDirector(HouseBuilder builder) {
        this.builder = builder;
    }
    
    public House buildMinimalHouse() {
        return builder
            .buildWalls("Ladrillo")
            .buildWindows(2)
            .buildDoors(1)
            .buildRoof("Chapa")
            .getHouse();
    }
    
    public House buildLuxuryHouse() {
        return builder
            .buildWalls("Piedra")
            .buildWindows(8)
            .buildDoors(3)
            .buildRoof("Tejas Premium")
            .buildGarage()
            .buildPool()
            .buildGarden()
            .getHouse();
    }
}

// Uso
public class BuilderDemo {
    public static void main(String[] args) {
        HouseBuilder builder = new ConcreteHouseBuilder();
        
        // Construcción manual
        House casa1 = builder
            .buildWalls("Madera")
            .buildWindows(4)
            .buildDoors(2)
            .buildRoof("Tejas")
            .buildGarage()
            .getHouse();
        
        System.out.println(casa1);
        
        // Con Director
        HouseDirector director = new HouseDirector(builder);
        House casaMinimal = director.buildMinimalHouse();
        House casaLujo = director.buildLuxuryHouse();
        
        System.out.println(casaMinimal);
        System.out.println(casaLujo);
    }
}
```

### TypeScript

```typescript
// Product
class House {
    public walls?: string;
    public windows: number = 0;
    public doors: number = 0;
    public roof?: string;
    public garage: boolean = false;
    public pool: boolean = false;
    public garden: boolean = false;
    
    public toString(): string {
        return `
        Casa:
        - Paredes: ${this.walls}
        - Ventanas: ${this.windows}
        - Puertas: ${this.doors}
        - Techo: ${this.roof}
        - Garaje: ${this.garage ? 'Sí' : 'No'}
        - Piscina: ${this.pool ? 'Sí' : 'No'}
        - Jardín: ${this.garden ? 'Sí' : 'No'}
        `;
    }
}

// Builder Interface
interface HouseBuilder {
    buildWalls(material: string): this;
    buildWindows(count: number): this;
    buildDoors(count: number): this;
    buildRoof(type: string): this;
    buildGarage(): this;
    buildPool(): this;
    buildGarden(): this;
    getHouse(): House;
}

// Concrete Builder
class ConcreteHouseBuilder implements HouseBuilder {
    private house: House;
    
    constructor() {
        this.reset();
    }
    
    public reset(): void {
        this.house = new House();
    }
    
    public buildWalls(material: string): this {
        this.house.walls = material;
        return this;
    }
    
    public buildWindows(count: number): this {
        this.house.windows = count;
        return this;
    }
    
    public buildDoors(count: number): this {
        this.house.doors = count;
        return this;
    }
    
    public buildRoof(type: string): this {
        this.house.roof = type;
        return this;
    }
    
    public buildGarage(): this {
        this.house.garage = true;
        return this;
    }
    
    public buildPool(): this {
        this.house.pool = true;
        return this;
    }
    
    public buildGarden(): this {
        this.house.garden = true;
        return this;
    }
    
    public getHouse(): House {
        const result = this.house;
        this.reset();
        return result;
    }
}

// Director
class HouseDirector {
    private builder: HouseBuilder;
    
    constructor(builder: HouseBuilder) {
        this.builder = builder;
    }
    
    public buildMinimalHouse(): House {
        return this.builder
            .buildWalls("Ladrillo")
            .buildWindows(2)
            .buildDoors(1)
            .buildRoof("Chapa")
            .getHouse();
    }
    
    public buildLuxuryHouse(): House {
        return this.builder
            .buildWalls("Piedra")
            .buildWindows(8)
            .buildDoors(3)
            .buildRoof("Tejas Premium")
            .buildGarage()
            .buildPool()
            .buildGarden()
            .getHouse();
    }
}

// Uso
const builder = new ConcreteHouseBuilder();

const casa1 = builder
    .buildWalls("Madera")
    .buildWindows(4)
    .buildDoors(2)
    .buildRoof("Tejas")
    .buildGarage()
    .getHouse();

console.log(casa1.toString());

const director = new HouseDirector(builder);
const casaMinimal = director.buildMinimalHouse();
console.log(casaMinimal.toString());
```

---

## Diagrama de Secuencia

```sequence diagram
:Cliente      :Director     :Builder       :Product
   │              │             │              │
   │─new()───────>▌             │              │
   │              │             │              │
   │─construct()─>▌             │              │
   │              ▌─buildPartA()->▌             │
   │              ▌             ▌── «create» ──> ┌─────────┐
   │              ▌             ▌               │:Product │
   │              ▌             ▌               └─────────┘
   │              ▌─buildPartB()->▌                  │
   │              ▌             ▌──setPart()───────> ▌
   │              ▌─buildPartC()->▌                  │
   │              ▌             ▌──setPart()───────> ▌
   │              ▌             │                    │
   │─getResult()───────────────>▌                    │
   │              │             ▌──────────────────> ▌
   │              │             ▌<┄┄product┄┄┄┄┄┄┄┄ ▌
   │<┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄┄ ▌                    │
   │              │             │                    │
```

---

## Ventajas ✅

1. **Construcción paso a paso** de objetos complejos
2. **Código más legible** que constructores telescópicos
3. **Reutilización** del mismo código de construcción
4. **Aislamiento** del código complejo de construcción
5. **Single Responsibility Principle**: Separa construcción de representación
6. **Validación durante construcción**

---

## Desventajas ❌

1. **Complejidad aumentada**: Requiere crear múltiples clases
2. **Más código**: Para casos simples puede ser excesivo
3. **Inmutabilidad**: Difícil crear objetos inmutables

---

## Cuándo Usar

✅ **Usa Builder cuando:**

- El objeto tiene muchos parámetros de construcción (más de 4-5)
- Algunos parámetros son opcionales
- Necesitas diferentes representaciones del mismo objeto
- La construcción requiere varios pasos

❌ **Evita Builder cuando:**

- El objeto es simple
- Todos los parámetros son obligatorios
- Solo hay una forma de construir el objeto

---

## Casos de Uso Reales

### 1. **SQL Query Builder**
```python
query = (QueryBuilder()
         .select("name", "email")
         .from_table("users")
         .where("age > 18")
         .order_by("name")
         .limit(10)
         .build())
```

### 2. **HTTP Request Builder**
```python
request = (HttpRequestBuilder()
           .set_url("https://api.example.com")
           .set_method("POST")
           .add_header("Content-Type", "application/json")
           .set_body({"name": "John"})
           .build())
```

### 3. **Document Builder (HTML/XML)**
```python
html = (HTMLBuilder()
        .add_header("Título")
        .add_paragraph("Contenido")
        .add_image("image.jpg")
        .build())
```

---

## Relación con Otros Patrones

- **Abstract Factory**: Builder construye objetos paso a paso; Factory los devuelve inmediatamente
- **Composite**: Builder puede construir árboles Composite
- **Prototype**: Builder puede usar prototipos para algunos pasos

---

## Relación con Principios SOLID

| Principio | Cómo lo cumple |
|-----------|----------------|
| **SRP** | Separa construcción de representación |
| **OCP** | Puedes añadir nuevos builders sin modificar código existente |
| **LSP** | Diferentes builders pueden sustituirse |
| **ISP** | Interfaces de builder específicas |
| **DIP** | Cliente depende de interfaz Builder, no de implementación |

---

## Ejercicios Prácticos

### Ejercicio 1: Pizza Builder
Crea un builder para pizzas con ingredientes, tamaño y masa personalizables.

### Ejercicio 2: Email Builder
Diseña un builder para emails con destinatarios, asunto, cuerpo y adjuntos.

### Ejercicio 3: Computer Builder
Implementa un builder para computadoras con CPU, RAM, almacenamiento, etc.

---

## Referencias

- [Refactoring Guru - Builder](https://refactoring.guru/design-patterns/builder)
- Gang of Four - Design Patterns
- [SourceMaking - Builder](https://sourcemaking.com/design_patterns/builder)

---

[← Volver a Patrones Creacionales](../Creacionales.md)
