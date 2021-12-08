import React from "react";
import UserDetails from "./UserDetails";

import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { UserState } from '../../store/user/user.reducer';
import { apiHostname } from '../../auth';

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

const Profile = (props: UserState) => {
    const { currentUser } = props;
    const [user, setUser] = React.useState<UserProfile>();

    React.useEffect(() => {
        currentUser && fetch(apiHostname + 'api/User/' + currentUser.username, {
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

    return user ? (
        <UserDetails
            userProp={user}
        />
    )
        : (<span style={{ display: 'flex', justifyContent: 'center' }}>NO USER IS SELECTED</span>);
}


const mapStateToProps = (state: ApplicationState) => {
    return state.user ?
        { currentUser: state.user.currentUser } :
        { currentUser: null };
};

export default connect(
    mapStateToProps
)(Profile);