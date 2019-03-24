import { KidMemoryCategory } from './memory';

export interface Kid {
    id: number;
    firstName: string;
    lastName: string;
    gender: string;
    grade: string;
    completedMemories: CompletedMemory[];
    memoriesByCategory: KidMemoryCategory[];
    isActive: boolean;
}

export interface CompletedMemory {
    memoryId: number;
    categoryId: number;
    points: number;
    dateCompleted: Date;
}
