import { KidAwardListItem } from './kid';

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

export class AwardEarned {
    id: number;
    categoryId: number;
    itemDescription: string;
    timing: boolean;
    kids: KidAwardListItem[];
    ordinal: number;
}
