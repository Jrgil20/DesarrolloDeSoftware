# State - Implementación en Java

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [State](https://github.com/iluwatar/java-design-patterns/tree/master/state)
- **Ejemplo**: Mammoth states (Peaceful, Angry, Dead)

### 2. **Refactoring Guru - State Java**
- **Enlace**: [State](https://refactoring.guru/design-patterns/state/java/example)
- **Ejemplo**: Audio player states

---

## 💡 Ejemplo

```java
interface State {
    void clickLock(AudioPlayer player);
    void clickPlay(AudioPlayer player);
}

class LockedState implements State {
    public void clickLock(AudioPlayer player) {
        player.changeState(new ReadyState());
    }
    public void clickPlay(AudioPlayer player) {
        // No hacer nada cuando está bloqueado
    }
}

class ReadyState implements State {
    public void clickLock(AudioPlayer player) {
        player.changeState(new LockedState());
    }
    public void clickPlay(AudioPlayer player) {
        player.startPlayback();
        player.changeState(new PlayingState());
    }
}

class AudioPlayer {
    private State state = new ReadyState();
    
    public void changeState(State state) {
        this.state = state;
    }
    
    public void clickLock() {
        state.clickLock(this);
    }
    
    public void clickPlay() {
        state.clickPlay(this);
    }
}
```

---

## 🙏 Créditos
- **iluwatar/java-design-patterns**
- **Refactoring Guru**

[← Volver](../README.md)
