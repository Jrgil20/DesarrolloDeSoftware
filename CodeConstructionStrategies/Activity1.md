# Actividad 1: God Method

Tienes este “método dios” que mezcla validaciones, lógica y efectos.

Refactoriza para mejorar legibilidad:
    Nombres con intención.
    Guard clauses.
    Extrae funciones por responsabilidad.
    Un solo nivel de abstracción por método.

```csharp
public async Task Process(Order order)
{
    if (order != null)
    {
        if (order.Items != null && order.Items.Count > 0)
        {
            foreach (var item in order.Items)
            {
                if (item.Price > 0)
                {
                    await _inventory.ReserveAsync(i.sku, i.quantity);
                    _logger.LogInformation("Reserced {sku}", i.sku);
                }
                else
                {
                    throw new Exception("Invalid price");
                }
            }

            var shippingCost = 0m;
            if (order.Address != null && order.Address.Country == "US")shippingCost = 10m;
            else
                shippingCost = 25m;

            await _shipping.DispatchAsync(order, shippingCost);
            _logger.LogInformation("Order {orderId} processed", order.Id);
    
        }

    }
}
```

## Refactorización paso a paso

### Plan de acción

1. **Separar validaciones** en métodos o interfaces dedicadas.
2. **Dividir responsabilidades**: 
    - Validación de la orden.
    - Reserva de inventario.
    - Cálculo de costos de envío.
    - Despacho del pedido.
3. **Renombrar variables y métodos** para mayor claridad.
4. **Agregar bloques try-catch** para manejo de errores.
5. **Aplicar guard clauses** para simplificar la lógica.
6. **Extraer funciones** para mantener un solo nivel de abstracción por método.

---

### Lo que debería hacerse

- [/] Separar validaciones en interfaces/métodos dedicados
- [/] Dividir responsabilidades (orden, inventario, envío, despacho)
- [/] Renombrar variables y métodos para mayor claridad
- [/] Agregar bloques try-catch para manejo de errores
- [/] Aplicar guard clauses
- [/] Extraer funciones por responsabilidad

---

### Lo que tengo correcto

- [/] Identificar la necesidad de separar validaciones
- [/] Proponer dividir responsabilidades en interfaces
- [/] Sugerir nombres más descriptivos

---

### Lo que me faltó

- [x] Especificar la extracción de funciones por responsabilidad
- [x] Mencionar el uso de guard clauses explícitamente
- [x] Incluir el manejo de errores con try-catch en el plan

---

### Ejemplo de refactorización

```csharp
public async Task ProcessOrderAsync(Order order)
{
    try
    {
        ValidateOrder(order);

        await ReserveInventoryAsync(order.Items);

        var shippingCost = CalculateShippingCost(order.Address);

        await DispatchOrderAsync(order, shippingCost);

        _logger.LogInformation("Order {orderId} processed", order.Id);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error processing order {orderId}", order?.Id);
        throw;
    }
}

private void ValidateOrder(Order order)
{
    if (order == null)
        throw new ArgumentNullException(nameof(order));
    if (order.Items == null || !order.Items.Any())
        throw new ArgumentException("Order must have items");
    foreach (var item in order.Items)
    {
        if (item.Price <= 0)
            throw new ArgumentException($"Invalid price for item {item.Sku}");
    }
}

private async Task ReserveInventoryAsync(IEnumerable<Item> items)
{
    foreach (var item in items)
    {
        await _inventory.ReserveAsync(item.Sku, item.Quantity);
        _logger.LogInformation("Reserved {sku}", item.Sku);
    }
}

private decimal CalculateShippingCost(Address address)
{
    if (address == null)
        throw new ArgumentNullException(nameof(address));
    return address.Country == "US" ? 10m : 25m;
}

private async Task DispatchOrderAsync(Order order, decimal shippingCost)
{
    await _shipping.DispatchAsync(order, shippingCost);
}
```

**Cambios realizados:**
- Se aplicaron guard clauses para validaciones.
- Se extrajeron funciones por responsabilidad.
- Se renombraron variables y métodos para mayor claridad.
- Se agregó manejo de errores con try-catch.
- Se mantiene un solo nivel de abstracción por método.