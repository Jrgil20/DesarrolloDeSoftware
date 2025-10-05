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
