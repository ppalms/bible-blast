import { Organization } from './organization';

export interface User {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    organization: Organization;
    userRoles: string[];
}
