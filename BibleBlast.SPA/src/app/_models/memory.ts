export class KidMemoryListItem {
    memoryId: number;
    memoryName: string;
    memoryDescription: string;
    points: number;
    dateCompleted: Date;
}

export interface KidMemoryCategory {
    categoryId: number;
    categoryName: string;
    memories: KidMemoryListItem[];
}
