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
