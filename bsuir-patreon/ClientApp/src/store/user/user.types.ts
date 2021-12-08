// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export type UserProps = {
    username: string;
    password: string;
}

export const UserActionTypes = {
    SET_CURRENT_USER: 'SET_CURRENT_USER'
}