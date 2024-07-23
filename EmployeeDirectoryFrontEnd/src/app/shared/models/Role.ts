import { Employee } from "./Employee";

export interface Role {
    id: number;
    name: string;
    description?: string;
    location: string;
    department: string;
    imgArray: Array<Employee>;
}
