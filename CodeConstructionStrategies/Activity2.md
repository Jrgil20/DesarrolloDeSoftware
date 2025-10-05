# Actividad 2: comprobabilidad

Este servicio calcula la caducidad de un token leyendo directamente el tiempo del sistema y un
archivo de configuración.

Objetivo: mejorar comprobabilidad (testabilidad).

    Extrae un reloj (IClock) y un lector de archivos (IFileReader).
    Haz que el servicio sea determinista en tests.
    Escribe una prueba unitaria que valide la expiración.

```csharp
public class TokenService
{
    public Token Issue( string userId )
    {
        if ( string.IsNullOrWhiteSpace( userId ) ) throw new ArgumentException( "userId is required", nameof( userId ) );

        var json = File.ReadAllText( "tokenSettings.json" );
        var cfg=JsonSerializer.Deserialize<TokenSettings>( json )!;

        var now = DateTime.UtcNow;
        return new Token (userId,now,now.AddMinutes(cfg.ExpirationMinutes));
    }
}

public record Token( string UserId, DateTime IssuedAt, DateTime ExpiresAt );
public record TokenSettings( int ExpirationMinutes );
```

## Refactorización paso a paso

### Plan de acción

1. **Extraer dependencias**: Crear interfaces `IClock` y `IFileReader` para abstraer la obtención del tiempo y la lectura de archivos.
2. **Inyectar dependencias**: Modificar el constructor de `TokenService` para aceptar instancias de `IClock` y `IFileReader`.
3. **Implementar clases concretas**: Crear implementaciones concretas de `IClock` y `IFileReader` para uso en producción.
4. **Modificar el método `Issue`**: Utilizar las interfaces inyectadas en lugar de llamadas directas a `DateTime.UtcNow` y `File.ReadAllText`.
5. **Escribir pruebas unitarias**: Crear pruebas unitarias que utilicen implementaciones simuladas (mocks) de `IClock` y `IFileReader` para validar la lógica de expiración del token.

```csharp
public interface IClock
{
    DateTime UtcNow { get; }
}

public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}

public interface IFileReader
{
    string ReadAllText(string path);
}

public class FileReader : IFileReader
{
    public string ReadAllText(string path) => File.ReadAllText(path);
}

public class TokenService
{
    private readonly IClock _clock;
    private readonly IFileReader _fileReader;

    public TokenService(IClock clock, IFileReader fileReader)
    {
        _clock = clock;
        _fileReader = fileReader;
    }

    public Token Issue(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("userId is required", nameof(userId));

        var json = _fileReader.ReadAllText("tokenSettings.json");
        var cfg = JsonSerializer.Deserialize<TokenSettings>(json)!;

        var now = _clock.UtcNow;
        return new Token(userId, now, now.AddMinutes(cfg.ExpirationMinutes));
    }
}

public record Token(string UserId, DateTime IssuedAt, DateTime ExpiresAt);
public record TokenSettings(int ExpirationMinutes);
```
