export class MemoryItem {
    id: number;
    name: string;
    description: string;
    points: number;
}

export class MemoryCategory {
    id: number;
    name: string;
    memories: MemoryItem[];
}

export class KidMemoryItem extends MemoryItem {
    dateCompleted: Date;
}

export class KidMemoryCategory extends MemoryCategory {
    memories: KidMemoryItem[];
}
