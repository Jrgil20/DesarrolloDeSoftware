# Composite - Implementación en TypeScript

## 📖 Descripción

Referencias a implementaciones del patrón Composite en TypeScript con type safety y recursión.

---

## 🌟 Repositorios Recomendados

### 1. **Refactoring Guru - Composite TypeScript**
- **Enlace**: [Composite en TypeScript](https://refactoring.guru/design-patterns/composite/typescript/example)

### 2. **torokmark/design_patterns_in_typescript**
- **Enlace**: [GitHub](https://github.com/torokmark/design_patterns_in_typescript)

---

## 💡 Ejemplo de Referencia

```typescript
// Component Interface
interface FileSystemItem {
    name: string;
    getSize(): number;
    print(indent?: string): void;
}

// Leaf
class File implements FileSystemItem {
    constructor(
        public readonly name: string,
        private readonly size: number
    ) {}
    
    public getSize(): number {
        return this.size;
    }
    
    public print(indent: string = ""): void {
        console.log(`${indent}📄 ${this.name} (${this.size} bytes)`);
    }
}

// Composite
class Folder implements FileSystemItem {
    private items: FileSystemItem[] = [];
    
    constructor(public readonly name: string) {}
    
    public add(item: FileSystemItem): void {
        this.items.push(item);
    }
    
    public remove(item: FileSystemItem): void {
        const index = this.items.indexOf(item);
        if (index > -1) {
            this.items.splice(index, 1);
        }
    }
    
    public getSize(): number {
        return this.items.reduce((total, item) => total + item.getSize(), 0);
    }
    
    public print(indent: string = ""): void {
        console.log(`${indent}📁 ${this.name}/`);
        this.items.forEach(item => item.print(indent + "  "));
    }
    
    public getItems(): readonly FileSystemItem[] {
        return [...this.items];
    }
}

// Client
const root = new Folder("root");

const documents = new Folder("Documents");
documents.add(new File("resume.pdf", 150_000));
documents.add(new File("cover-letter.doc", 80_000));

const photos = new Folder("Photos");
photos.add(new File("vacation.jpg", 2_000_000));
photos.add(new File("family.jpg", 1_500_000));

root.add(documents);
root.add(photos);
root.add(new File("readme.txt", 5_000));

root.print();

console.log(`\nTotal size: ${root.getSize()} bytes`);
console.log(`Documents size: ${documents.getSize()} bytes`);
```

---

## 🔧 Características TypeScript

### 1. Type Guards para Seguridad de Tipos
```typescript
function isFile(item: FileSystemItem): item is File {
    return item instanceof File;
}

function isFolder(item: FileSystemItem): item is Folder {
    return item instanceof Folder;
}

class Folder implements FileSystemItem {
    public getAllFiles(): File[] {
        const files: File[] = [];
        
        for (const item of this.items) {
            if (isFile(item)) {
                files.push(item);
            } else if (isFolder(item)) {
                files.push(...item.getAllFiles());
            }
        }
        
        return files;
    }
}
```

### 2. Generics con Constraints
```typescript
interface Component {
    getName(): string;
}

class Composite<T extends Component> {
    private children: T[] = [];
    
    public add(component: T): void {
        this.children.push(component);
    }
    
    public getChildren(): readonly T[] {
        return this.children;
    }
    
    public find(predicate: (item: T) => boolean): T | undefined {
        return this.children.find(predicate);
    }
}
```

### 3. Async Operations en Composite
```typescript
interface AsyncFileSystemItem {
    name: string;
    getSizeAsync(): Promise<number>;
}

class AsyncFolder implements AsyncFileSystemItem {
    private items: AsyncFileSystemItem[] = [];
    
    constructor(public readonly name: string) {}
    
    public add(item: AsyncFileSystemItem): void {
        this.items.push(item);
    }
    
    public async getSizeAsync(): Promise<number> {
        const sizes = await Promise.all(
            this.items.map(item => item.getSizeAsync())
        );
        return sizes.reduce((total, size) => total + size, 0);
    }
}

// Uso
const root = new AsyncFolder("root");
root.add(new AsyncFile("file1.txt", 1000));
root.add(new AsyncFile("file2.txt", 2000));

const totalSize = await root.getSizeAsync(); // 3000
```

### 4. Readonly y Immutability
```typescript
class ImmutableFolder implements FileSystemItem {
    private readonly items: readonly FileSystemItem[];
    
    constructor(
        public readonly name: string,
        items: FileSystemItem[] = []
    ) {
        this.items = Object.freeze([...items]);
    }
    
    public withItem(item: FileSystemItem): ImmutableFolder {
        return new ImmutableFolder(
            this.name,
            [...this.items, item]
        );
    }
    
    public getSize(): number {
        return this.items.reduce((sum, item) => sum + item.getSize(), 0);
    }
}

// Uso funcional
const folder1 = new ImmutableFolder("docs");
const folder2 = folder1.withItem(new File("readme.md", 100));
const folder3 = folder2.withItem(new File("license.txt", 200));
```

### 5. Visitor Pattern con Composite
```typescript
interface FileSystemVisitor {
    visitFile(file: File): void;
    visitFolder(folder: Folder): void;
}

interface FileSystemItem {
    accept(visitor: FileSystemVisitor): void;
}

class SizeCalculatorVisitor implements FileSystemVisitor {
    private totalSize: number = 0;
    
    public visitFile(file: File): void {
        this.totalSize += file.getSize();
    }
    
    public visitFolder(folder: Folder): void {
        folder.getItems().forEach(item => item.accept(this));
    }
    
    public getTotalSize(): number {
        return this.totalSize;
    }
}

// Uso
const visitor = new SizeCalculatorVisitor();
root.accept(visitor);
console.log(`Total size: ${visitor.getTotalSize()}`);
```

---

## 📚 Recursos

- [TypeScript Handbook - Advanced Types](https://www.typescriptlang.org/docs/handbook/2/types-from-types.html)
- [Patterns.dev - Composite Pattern](https://www.patterns.dev/)
- [TypeScript Design Patterns](https://refactoring.guru/design-patterns/typescript)

---

## 🙏 Créditos

- **Refactoring Guru** - Alexander Shvets
- **torokmark** - Mark Torok
- **Patterns.dev** - Addy Osmani

---

[← Volver a Composite](../README.md)
