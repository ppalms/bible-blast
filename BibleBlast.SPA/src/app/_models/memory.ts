export class MemoryListItem {
    id: number;
    name: string;
    description: string;
    points: number;
}

export class MemoryCategory {
    id: number;
    name: string;
    memories: MemoryListItem[];
}

export class KidMemoryListItem extends MemoryListItem {
    dateCompleted: Date;
}

export class KidMemoryCategory extends MemoryCategory {
    memories: KidMemoryListItem[];
}
