import User from './User';

export interface ApplicationState {
    isAuthenticated: boolean,
    user?: User,
    count: number
}