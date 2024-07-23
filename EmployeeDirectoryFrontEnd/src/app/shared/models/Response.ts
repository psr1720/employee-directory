export interface Response<T> {
    isSuccess: boolean;
    statusCode: number;
    data: T;
    errorMessage?: string;
}