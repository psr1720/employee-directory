export interface EmployeeRequest {
    id: number;
    employeeId?: string | null;
    password?: string | null;
    firstName?: string | null;
    lastName?: string | null;
    dob?: string | null;
    emailID?: string | null;
    phoneNo?: string | null;
    joinDate?: string | null;
    locationId?: number| null;
    jobTitleId?: number| null;
    managerId?: number| null;
    projectId?: number| null;
    profilePicture?: string | null;
}