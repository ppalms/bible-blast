import { MemoryCategory } from './memory';

export interface Kid {
    id: number;
    firstName: string;
    lastName: string;
    gender: string;
    grade: string;
    completedMemories: CompletedMemory[];
    isActive: boolean;
}

export interface CompletedMemory {
    memoryId: number;
    name: string;
    description: string;
    points: number;
    categoryId: number;
    dateCompleted: Date;
}
