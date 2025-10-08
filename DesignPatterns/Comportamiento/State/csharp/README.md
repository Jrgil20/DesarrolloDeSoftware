# State - Implementación en C#

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - State C#**
- **Enlace**: [State](https://refactoring.guru/design-patterns/state/csharp/example)

### 2. **DoFactory - State**
- **Enlace**: [DoFactory](https://www.dofactory.com/net/state-design-pattern)

---

## 💡 Ejemplo

```csharp
public interface IState
{
    void ClickLock(AudioPlayer player);
    void ClickPlay(AudioPlayer player);
}

public class LockedState : IState
{
    public void ClickLock(AudioPlayer player)
    {
        player.ChangeState(new ReadyState());
    }
    
    public void ClickPlay(AudioPlayer player)
    {
        // No hacer nada cuando bloqueado
    }
}

public class AudioPlayer
{
    private IState _state = new ReadyState();
    
    public void ChangeState(IState state) => _state = state;
    
    public void ClickLock() => _state.ClickLock(this);
    public void ClickPlay() => _state.ClickPlay(this);
}
```

---

## 🙏 Créditos
- **Refactoring Guru**
- **DoFactory**

[← Volver](../README.md)
