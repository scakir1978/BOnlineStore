import { Role } from './role';

export class User {
  id: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  avatar: string;
  role: Role;
  token?: string;
  language?: string;
}
