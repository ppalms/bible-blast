import { Organization } from './organization';
import { Kid } from './kid';

export interface User {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    organization: Organization;
    userRoles: string[];
    kids: Kid[];
}
