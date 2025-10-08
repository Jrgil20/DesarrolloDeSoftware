# Facade - Implementación en TypeScript

## 📖 Descripción

Referencias a implementaciones del patrón Facade en TypeScript con módulos y APIs simplificadas.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Facade TypeScript**
- **Enlace**: [Facade en TypeScript](https://refactoring.guru/design-patterns/facade/typescript/example)

### 2. **torokmark/design_patterns_in_typescript**
- **Enlace**: [GitHub](https://github.com/torokmark/design_patterns_in_typescript)

---

## 💡 Ejemplo de Referencia

```typescript
// Subsistema complejo
class VideoFile {
    constructor(public filename: string) {}
}

class AudioMixer {
    public fix(video: VideoFile): void {
        console.log("AudioMixer: fixing audio...");
    }
}

class CodecFactory {
    public static extract(file: VideoFile): Codec {
        const extension = file.filename.split('.').pop();
        return extension === 'mp4' ? new MPEG4Codec() : new OggCodec();
    }
}

interface Codec {
    type: string;
}

class MPEG4Codec implements Codec {
    type = "mp4";
}

class OggCodec implements Codec {
    type = "ogg";
}

class BitrateReader {
    public read(file: VideoFile, codec: Codec): VideoFile {
        console.log("BitrateReader: reading file...");
        return file;
    }
    
    public convert(buffer: VideoFile, codec: Codec): VideoFile {
        console.log("BitrateReader: converting...");
        return buffer;
    }
}

// FACADE: Simplifica el subsistema
class VideoConverterFacade {
    public convert(filename: string, format: string): File {
        console.log("VideoConverter: Starting conversion...");
        
        const file = new VideoFile(filename);
        const sourceCodec = CodecFactory.extract(file);
        
        const destinationCodec = format === "mp4" 
            ? new MPEG4Codec() 
            : new OggCodec();
        
        const reader = new BitrateReader();
        const buffer = reader.read(file, sourceCodec);
        const result = reader.convert(buffer, destinationCodec);
        
        const audioMixer = new AudioMixer();
        audioMixer.fix(result);
        
        console.log("VideoConverter: Conversion complete!");
        return new File(`output.${format}`);
    }
}

// Cliente usa interfaz simple
const converter = new VideoConverterFacade();
const file = converter.convert("video.ogg", "mp4");
```

---

## 🔧 Características TypeScript

### 1. Module Pattern como Facade
```typescript
// api-facade.ts
import { DatabaseService } from './db';
import { CacheService } from './cache';
import { ValidationService } from './validation';
import { LoggingService } from './logging';

// Facade simplifica el uso de múltiples servicios
export class ApiFacade {
    private db = new DatabaseService();
    private cache = new CacheService();
    private validator = new ValidationService();
    private logger = new LoggingService();
    
    public async getData(id: string): Promise<Data> {
        this.logger.log(`Fetching data for ${id}`);
        
        // Check cache first
        const cached = this.cache.get(id);
        if (cached) return cached;
        
        // Fetch from DB
        const data = await this.db.query(`SELECT * FROM data WHERE id = ?`, [id]);
        
        // Validate
        if (!this.validator.validate(data)) {
            throw new Error('Invalid data');
        }
        
        // Cache for future
        this.cache.set(id, data);
        
        return data;
    }
}

// Uso
import { ApiFacade } from './api-facade';
const api = new ApiFacade();
const data = await api.getData('123');
```

### 2. Facade con Async/Await
```typescript
class PaymentFacade {
    private stripe = new StripeService();
    private paypal = new PayPalService();
    private bank = new BankService();
    private logger = new Logger();
    
    public async processPayment(
        amount: number, 
        method: 'stripe' | 'paypal' | 'bank'
    ): Promise<PaymentResult> {
        this.logger.info(`Processing ${method} payment for $${amount}`);
        
        try {
            switch (method) {
                case 'stripe':
                    await this.stripe.authenticate();
                    return await this.stripe.charge(amount);
                case 'paypal':
                    await this.paypal.connect();
                    return await this.paypal.sendMoney(amount);
                case 'bank':
                    await this.bank.authorize();
                    return await this.bank.transfer(amount);
            }
        } catch (error) {
            this.logger.error(`Payment failed: ${error}`);
            throw error;
        }
    }
}
```

---

## 📚 Recursos

- [TypeScript Module Pattern](https://www.typescriptlang.org/docs/handbook/modules.html)
- [Patterns.dev - Module Pattern](https://www.patterns.dev/posts/module-pattern/)

---

## 🙏 Créditos

- **Refactoring Guru** - Alexander Shvets
- **torokmark** - Mark Torok

---

[← Volver a Facade](../README.md)
