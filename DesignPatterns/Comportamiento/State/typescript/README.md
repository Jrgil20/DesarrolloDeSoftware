# State - Implementación en TypeScript

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - State TypeScript**
- **Enlace**: [State](https://refactoring.guru/design-patterns/state/typescript/example)

---

## 💡 Ejemplo

```typescript
interface State {
    clickLock(player: AudioPlayer): void;
    clickPlay(player: AudioPlayer): void;
}

class LockedState implements State {
    clickLock(player: AudioPlayer): void {
        player.changeState(new ReadyState());
    }
    
    clickPlay(player: AudioPlayer): void {
        // No hacer nada
    }
}

class ReadyState implements State {
    clickLock(player: AudioPlayer): void {
        player.changeState(new LockedState());
    }
    
    clickPlay(player: AudioPlayer): void {
        player.startPlayback();
        player.changeState(new PlayingState());
    }
}

class AudioPlayer {
    private state: State = new ReadyState();
    
    changeState(state: State): void {
        this.state = state;
    }
    
    clickLock(): void {
        this.state.clickLock(this);
    }
    
    clickPlay(): void {
        this.state.clickPlay(this);
    }
}
```

---

## 🙏 Créditos
- **Refactoring Guru**

[← Volver](../README.md)
