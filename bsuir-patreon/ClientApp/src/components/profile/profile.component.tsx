import React from "react";
import UserDetails from "./UserDetails";

import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { UserKnownAction, UserState } from '../../store/user/user.reducer';
import { apiHostname, authFetch } from '../../auth';
import { PasswordChangingProps, UserProps } from "../../store/user/user.types";
import { Dispatch } from "redux";
import { actionCreators } from "../../store/user/user.actions";

export interface Subscription {
    id: number;
    sub: string;
    user: UserProfile;
    author: UserProfile;
    startTime: string; // luxon .fromISO
    endTime: string;   // luxon .fromISO
};

export interface UserProfile {
    id: string;
    name: string;
    surname: string;
    balance: number;
    subscriptions?: Subscription[];
    followers?: Subscription[];
    userName: string;
    email: string;
    phoneNumber?: string;
    lockoutEnabled: boolean;
};

const Profile = (props: UserState & typeof actionCreators) => {
    const { currentUser, changeUserPassword } = props;
    const [user, setUser] = React.useState<UserProfile>();

    React.useEffect(() => {
        console.log('')
        currentUser && authFetch(apiHostname + 'api/User/' + currentUser.username, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(r => r.json())
            .then(userJson => setUser({
                ...userJson
            }));

    }, [currentUser])
    const handleChangePassword = async (oldP: string, newP: string) => {
        if (currentUser && currentUser.password !== oldP) {
            alert("passwords don't match")
            return
        }

        try {
            authFetch(apiHostname + 'api/Authenticate/changepass', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json-patch+json'
                },
                body: JSON.stringify({
                    oldPassword: oldP,
                    newPassword: newP
                })
            })
                .then(r => r.json())
                .then(response => {
                    console.log('change pass response: ', response)
                    if (response.status === 200) {
                        alert('Password sucessfully changed')
                        changeUserPassword({
                            oldPassword: oldP,
                            newPassword: newP
                        });
                    }
                })

        } catch (error) {
            console.error(error)
        }
    }

    return user ? (
        <UserDetails
            userProp={user}
            onChangePass={handleChangePassword}
        />
    )
        : (<span style={{ display: 'flex', justifyContent: 'center' }}>NO USER IS SELECTED</span>);
}


const mapStateToProps = (state: ApplicationState) => {
    return state.user ?
        { currentUser: state.user.currentUser } :
        { currentUser: null };
};
const mapDispatchToProps = (dispatch: Dispatch<UserKnownAction>) => ({
    setCurrentUser: (user: UserProps) => dispatch({
        type: 'SET_CURRENT_USER',
        payload: user
    }),
    changeUserPassword: (passwords: PasswordChangingProps) => dispatch({
        type: 'CHANGE_PASSWORD_USER',
        payload: passwords
    })
})

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Profile);