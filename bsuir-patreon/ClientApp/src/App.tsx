import * as React from 'react';
import { Route, Switch, Redirect } from 'react-router';
import Layout from './components/Layout';
import Posts from './components/Posts';
import Counter from './components/Counter';
import Profile from './components/profile/profile.component';
import FetchData from './components/FetchData';
import SignInAndSignUpPage from './pages/signin-signup/signin-signup.component';

import { Dispatch } from 'redux';
import { connect } from 'react-redux';
import { ApplicationState } from './store';
import { UserProps } from './store/user/user.types';
import { actionCreators } from './store/user/user.actions';
import { UserKnownAction, UserState } from './store/user/user.reducer';

import './custom.css'
import { useAuth } from './auth';


type AppProps =
    UserState // ... state we've requested from the Redux store
    & typeof actionCreators // ... plus action creators we've requested


const App = (props: AppProps) => {
    // setCurrentUser
    const { currentUser } = props;
    const [logged] = useAuth();
    // const [userToken, setUserToken] = React.useState<string>('');

    React.useCallback(
        () => {
            if (currentUser) {
                console.log(currentUser)
            }
            if (logged) {
                console.log(logged)
            }
        },
        [currentUser, logged],
    )

    return (
        <Layout>
            <Switch>
                <Route exact path='/' component={Posts} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
                <Route exact path='/signin' render={() =>
                    logged ? (
                        <Redirect to='/' />
                    ) : (
                        <SignInAndSignUpPage />
                    )
                }
                />
                <Route path='/profile' component={Profile} />
            </Switch>
        </Layout>
    )
};

const mapStateToProps = (state: ApplicationState) => {
    return state.user ? { currentUser: state.user.currentUser } : {
        currentUser: null
    };
};
const mapDispatchToProps = (dispatch: Dispatch<UserKnownAction>) => ({
    setCurrentUser: (user: UserProps) => dispatch({
        type: 'SET_CURRENT_USER',
        payload: user
    })
})

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(App);
