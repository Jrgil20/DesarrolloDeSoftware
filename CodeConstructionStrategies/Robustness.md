# Robustez del código

La robustez es la capacidad de un sistema para seguir funcionando correctamente, o al menos fallar de forma controlada, ante entradas inválidas, fallos externos (como problemas de red o de entrada/salida), condiciones inesperadas y situaciones de concurrencia. En otras palabras, un software robusto no se rompe fácilmente: detecta problemas temprano y es capaz de recuperarse o informar los errores de manera clara.

**Señales de robustez:**

- Validación explícita de entradas y cumplimiento de contratos (precondiciones): Verifica que los datos recibidos cumplen con los requisitos esperados antes de procesarlos, lanzando excepciones o devolviendo errores claros si no se cumplen las condiciones.
- Protección de invariantes de dominio, evitando que ocurran estados imposibles: Implementa controles internos para asegurar que los objetos y estructuras de datos nunca lleguen a estados inválidos, utilizando métodos privados, constructores controlados o validaciones en setters.
- Manejo coherente de errores, diferenciando entre errores de dominio y técnicos, con mensajes o códigos claros: Separa los errores que surgen por reglas del negocio de los que provienen de fallos técnicos (como IO o red), y proporciona información útil para su diagnóstico y resolución.
- Diseño defensivo frente a valores nulos, colecciones vacías y límites (como desbordamientos o tamaños máximos): Anticipa y gestiona explícitamente casos como referencias nulas, listas vacías o valores fuera de rango, evitando fallos inesperados y asegurando un comportamiento predecible.
- Concurrencia segura, utilizando inmutabilidad o mecanismos de sincronización (locks) cuando corresponde: Protege los datos compartidos entre hilos mediante técnicas como objetos inmutables, sincronización explícita o estructuras concurrentes, previniendo condiciones de carrera y estados inconsistentes.

## Ejemplos y contraejemplos

### 1. Validación explícita de entradas y cumplimiento de contratos

**Ejemplo (Java):**
```java
public void setEdad(int edad) {
    if (edad < 0 || edad > 150) {
        throw new IllegalArgumentException("Edad fuera de rango");
    }
    this.edad = edad;
}
```
**Contraejemplo:**
```java
public void setEdad(int edad) {
    this.edad = edad; // No valida el rango, puede causar errores después
}
```

---

### 2. Protección de invariantes de dominio

**Ejemplo (C#):**
```csharp
public class CuentaBancaria {
    private decimal saldo;
    public void Retirar(decimal monto) {
        if (monto > saldo) throw new InvalidOperationException("Fondos insuficientes");
        saldo -= monto;
    }
}
```
**Contraejemplo:**
```csharp
public class CuentaBancaria {
    public decimal Saldo;
    // Permite modificar el saldo directamente, puede quedar negativo
}
```

---

### 3. Manejo coherente de errores

**Ejemplo (Python):**
```python
try:
    resultado = dividir(a, b)
except ZeroDivisionError:
    print("Error: No se puede dividir por cero")
except Exception as e:
    print(f"Error técnico: {e}")
```
**Contraejemplo:**
```python
resultado = dividir(a, b)  # No maneja posibles errores, puede fallar abruptamente
```

---

### 4. Diseño defensivo frente a valores nulos y límites

**Ejemplo (JavaScript):**
```javascript
function obtenerPrimerElemento(lista) {
    if (!Array.isArray(lista) || lista.length === 0) return null;
    return lista[0];
}
```
**Contraejemplo:**
```javascript
function obtenerPrimerElemento(lista) {
    return lista[0]; // Falla si lista es null o vacía
}
```

---

### 5. Concurrencia segura

**Ejemplo (Java):**
```java
public synchronized void incrementar() {
    contador++;
}
```
**Contraejemplo:**
```java
public void incrementar() {
    contador++; // No es seguro en entornos multihilo
}
```
