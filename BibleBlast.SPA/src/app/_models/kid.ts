export interface Kid {
    id: number;
    firstName: string;
    lastName: string;
    gender: string;
    grade: string;
    birthday: Date;
    completedMemories: CompletedMemory[];
    parents: any[];
    organizationId: number;
    isActive: boolean;
}

export interface CompletedMemory {
    memoryId: number;
    categoryId: number;
    points: number;
    dateCompleted: Date;
}
