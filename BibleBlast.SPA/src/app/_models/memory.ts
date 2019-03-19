export class Memory {
    id: number;
    name: string;
    category: MemoryCategory;
}

export interface MemoryCategory {
    id: number;
    name: string;
    memories: Memory[];
}
