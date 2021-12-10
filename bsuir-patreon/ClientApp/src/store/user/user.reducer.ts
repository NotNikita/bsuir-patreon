import { Action, Reducer } from 'redux';
import { SetUserAction, ChangePasswordAction } from './user.actions';
import { UserActionTypes, UserProps } from './user.types';

export interface UserState {
    currentUser: UserProps | null;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
// | InterfaceOfAction2 | InterfaceOfAction3
export type UserKnownAction = SetUserAction | ChangePasswordAction;


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<UserState> = (state: UserState | undefined, incomingAction: Action): UserState => {
    if (state === undefined) {
        return { currentUser: null };
    }

    const action = incomingAction as UserKnownAction;
    switch (action.type) {
        case UserActionTypes.SET_CURRENT_USER:
            const trueAction1 = action as SetUserAction;
            return { currentUser: trueAction1.payload };
        case UserActionTypes.CHANGE_PASSWORD_USER:
            const trueAction2 = action as ChangePasswordAction;
            if (state.currentUser && trueAction2.payload.oldPassword === state.currentUser.password) {
                return {
                    currentUser: {
                        ...state.currentUser,
                        password: trueAction2.payload.newPassword
                    } as UserProps
                };
            } else
                return state;
        default:
            return state;
    }
};
