import { Action, Reducer } from 'redux';
import { UserProps, UserActionTypes } from "./user.types";

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

export interface SetUserAction {
    type: 'SET_CURRENT_USER',
    payload: UserProps
}

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    setCurrentUser: (user: UserProps) => ({ type: 'DECREMENT_COUNT', payload: user })
};
