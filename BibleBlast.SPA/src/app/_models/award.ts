export class Award {
    id: number;
    itemId: number;
    memories: any[];
}

export class AwardCategory {
    categoryId: number;
    categoryName: string;
    awards: AwardListItem[];
}

export class AwardListItem {
    awardId: number;
    itemDescription: string;
    requirement: string;
    timing: boolean;
    ordinal: number;
}
