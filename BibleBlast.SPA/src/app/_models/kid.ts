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

export interface KidAward {
    kidId: number;
    awardId: number;
    datePresented: Date;
}

export interface KidAwardListItem {
    id: number;
    firstName: string;
    lastName: string;
    datePresented: Date;
}
