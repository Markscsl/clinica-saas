export interface IUserToken {
    unique_name: string,
    email: string,
    role: string,
    exp?: number
}

export interface ILoginResponse{
    token: string
}

