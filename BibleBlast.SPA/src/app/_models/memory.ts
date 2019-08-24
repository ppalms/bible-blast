export class MemoryListItem {
    memoryId: number;
    memoryName: string;
    memoryDescription: string;
    points: number;
}

export class MemoryCategory {
    categoryId: number;
    categoryName: string;
    memories: MemoryListItem[];
}

export class KidMemoryListItem extends MemoryListItem {
    dateCompleted: Date;
}

export class KidMemoryCategory extends MemoryCategory {
    memories: KidMemoryListItem[];
}
