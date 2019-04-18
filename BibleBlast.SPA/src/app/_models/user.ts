import { Organization } from './organization';
import { Kid } from './kid';

export interface User {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    organization: Organization;
    userRole: string;
    kids: Kid[];
}
