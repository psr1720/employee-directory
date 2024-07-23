export interface RoleRequest {
    id: number;
    name?: string| null;
    departmentId?: number| null;
    locationId?: number| null;
    description?: string| null;
}