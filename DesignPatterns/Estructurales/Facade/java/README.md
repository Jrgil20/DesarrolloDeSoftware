# Facade - Implementación en Java

## 📖 Descripción

Referencias a implementaciones del patrón Facade en Java, demostrando simplificación de subsistemas complejos.

---

## 🌟 Repositorios Recomendados

### 1. **iluwatar/java-design-patterns** (⭐ 89,000+)
- **Enlace**: [Facade en java-design-patterns](https://github.com/iluwatar/java-design-patterns/tree/master/facade)
- **Ejemplo**: Dwarf mine operations (goldmine, gold diggers, miners)
- **Características**:
  - ✅ Subsistema complejo con múltiples clases
  - ✅ Facade que simplifica operaciones
  - ✅ Tests incluidos

### 2. **Refactoring Guru - Facade Java**
- **Enlace**: [Facade en Refactoring Guru](https://refactoring.guru/design-patterns/facade/java/example)
- **Ejemplo**: Video conversion library

### 3. **Baeldung - Facade Pattern**
- **Enlace**: [Baeldung Facade](https://www.baeldung.com/java-facade-pattern)

---

## 💡 Ejemplo de Referencia

```java
// Subsistema complejo
class VideoFile {
    private String name;
    public VideoFile(String name) { this.name = name; }
    public String getName() { return name; }
}

class CodecFactory {
    public static Codec extract(VideoFile file) {
        String type = file.getName().substring(file.getName().lastIndexOf('.') + 1);
        return "mp4".equals(type) ? new MPEG4CompressionCodec() : new OggCompressionCodec();
    }
}

interface Codec { }
class MPEG4CompressionCodec implements Codec { }
class OggCompressionCodec implements Codec { }

class BitrateReader {
    public static VideoFile read(VideoFile file, Codec codec) {
        System.out.println("Reading file with codec");
        return file;
    }
    public static VideoFile convert(VideoFile buffer, Codec codec) {
        System.out.println("Converting video");
        return buffer;
    }
}

class AudioMixer {
    public File fix(VideoFile result) {
        System.out.println("Fixing audio");
        return new File("tmp");
    }
}

// FACADE: Simplifica el uso del subsistema
class VideoConverterFacade {
    public File convertVideo(String fileName, String format) {
        System.out.println("VideoConverterFacade: Conversion started.");
        
        VideoFile file = new VideoFile(fileName);
        Codec sourceCodec = CodecFactory.extract(file);
        
        Codec destinationCodec;
        if (format.equals("mp4")) {
            destinationCodec = new MPEG4CompressionCodec();
        } else {
            destinationCodec = new OggCompressionCodec();
        }
        
        VideoFile buffer = BitrateReader.read(file, sourceCodec);
        VideoFile result = BitrateReader.convert(buffer, destinationCodec);
        result = (VideoFile) new AudioMixer().fix(result);
        
        System.out.println("VideoConverterFacade: Conversion completed.");
        return new File("output." + format);
    }
}

// Cliente: Código simple
public class FacadeDemo {
    public static void main(String[] args) {
        VideoConverterFacade converter = new VideoConverterFacade();
        File mp4Video = converter.convertVideo("youtubevideo.ogg", "mp4");
        // ✅ Una línea vs. múltiples llamadas a subsistemas
    }
}
```

---

## 🔧 Características Java

### 1. Home Theater Facade (Ejemplo Clásico)
```java
// Subsistemas
class Amplifier {
    public void on() { System.out.println("Amplifier on"); }
    public void setVolume(int level) { System.out.println("Volume: " + level); }
}

class DVDPlayer {
    public void on() { System.out.println("DVD Player on"); }
    public void play(String movie) { System.out.println("Playing: " + movie); }
}

class Projector {
    public void on() { System.out.println("Projector on"); }
    public void wideScreenMode() { System.out.println("Wide screen mode"); }
}

class Lights {
    public void dim(int level) { System.out.println("Lights dimmed to " + level); }
}

// Facade
class HomeTheaterFacade {
    private Amplifier amp;
    private DVDPlayer dvd;
    private Projector projector;
    private Lights lights;
    
    public HomeTheaterFacade(Amplifier amp, DVDPlayer dvd, 
                            Projector projector, Lights lights) {
        this.amp = amp;
        this.dvd = dvd;
        this.projector = projector;
        this.lights = lights;
    }
    
    public void watchMovie(String movie) {
        System.out.println("Get ready to watch a movie...");
        lights.dim(10);
        projector.on();
        projector.wideScreenMode();
        amp.on();
        amp.setVolume(5);
        dvd.on();
        dvd.play(movie);
    }
    
    public void endMovie() {
        System.out.println("Shutting down theater...");
        lights.dim(100);
        projector.off();
        amp.off();
        dvd.off();
    }
}

// Uso simple
HomeTheaterFacade homeTheater = new HomeTheaterFacade(amp, dvd, projector, lights);
homeTheater.watchMovie("Inception");
// vs.
// lights.dim(10);
// projector.on();
// projector.wideScreenMode();
// amp.on();
// ... muchas líneas más
```

### 2. Facade con Factory
```java
public class DatabaseFacadeFactory {
    public static DatabaseFacade createFacade(String dbType) {
        if ("mysql".equals(dbType)) {
            return new MySQLDatabaseFacade();
        } else if ("postgresql".equals(dbType)) {
            return new PostgreSQLDatabaseFacade();
        }
        throw new IllegalArgumentException("Unknown DB type");
    }
}
```

### 3. Facade con Dependency Injection (Spring)
```java
@Service
public class OrderFacade {
    private final InventoryService inventoryService;
    private final PaymentService paymentService;
    private final ShippingService shippingService;
    private final NotificationService notificationService;
    
    @Autowired
    public OrderFacade(InventoryService inventory, 
                      PaymentService payment,
                      ShippingService shipping,
                      NotificationService notification) {
        this.inventoryService = inventory;
        this.paymentService = payment;
        this.shippingService = shipping;
        this.notificationService = notification;
    }
    
    public void placeOrder(Order order) {
        // Orquesta múltiples servicios
        inventoryService.reserve(order.getItems());
        paymentService.processPayment(order.getPayment());
        shippingService.scheduleDelivery(order);
        notificationService.sendConfirmation(order.getCustomer());
    }
}
```

---

## 📚 Recursos

- [Baeldung - Facade Pattern](https://www.baeldung.com/java-facade-pattern)
- [Head First Design Patterns - Home Theater](https://www.oreilly.com/library/view/head-first-design/0596007124/)

---

## 🙏 Créditos

- **iluwatar/java-design-patterns** - Ilkka Seppälä (MIT License)
- **Refactoring Guru** - Alexander Shvets
- **Baeldung**

---

[← Volver a Facade](../README.md)
