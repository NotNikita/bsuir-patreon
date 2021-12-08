import { Action, Reducer } from 'redux';
import { SetUserAction } from './user.actions';
import { UserActionTypes, UserProps } from './user.types';

export interface UserState {
    currentUser: UserProps | null;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
// | InterfaceOfAction2 | InterfaceOfAction3
export type UserKnownAction = SetUserAction;


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<UserState> = (state: UserState | undefined, incomingAction: Action): UserState => {
    if (state === undefined) {
        return { currentUser: null };
    }

    const action = incomingAction as UserKnownAction;
    switch (action.type) {
        case UserActionTypes.SET_CURRENT_USER:
            return { currentUser: action.payload };
        default:
            return state;
    }
};
