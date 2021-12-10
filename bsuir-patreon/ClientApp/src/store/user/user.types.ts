// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export type UserProps = {
    username: string;
    password: string;
}

export type PasswordChangingProps = {
    oldPassword: string;
    newPassword: string;
}

export const UserActionTypes = {
    SET_CURRENT_USER: 'SET_CURRENT_USER',
    CHANGE_PASSWORD_USER: 'CHANGE_PASSWORD_USER',
}