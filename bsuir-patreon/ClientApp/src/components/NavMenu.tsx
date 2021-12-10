import * as React from 'react';
import styled from '@emotion/styled';
import { Link } from 'react-router-dom';
import { PersonCircle } from 'react-bootstrap-icons';
import { ReactComponent as Logo } from '../assets/svg/crown.svg';

import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { UserKnownAction, UserState } from '../store/user/user.reducer';
import { useAuth, logout } from '../auth';
import { actionCreators } from '../store/user/user.actions';
import { Dispatch } from 'redux';
import { PasswordChangingProps, UserProps } from '../store/user/user.types';

const Header = styled.header({
    height: '70px',
    width: '100%',
    display: 'flex',
    justifyContent: 'space-between',
    marginBottom: 25
});
const LogoContainer = styled.div({
    height: '100%',
    width: 70,
    padding: 25
});
const Options = styled.div({
    width: '50%',
    height: '100%',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end'
});
const Option = styled.div({
    padding: '10px 15px',
    cursor: 'pointer'
});
const ProfileUsername = styled.span({
    marginLeft: 5,
    fontWeight: 500
});


const NavMenu = (props: UserState & typeof actionCreators) => {
    const [logged] = useAuth();
    const { currentUser } = props;
    const [displayName, setDisplayName] = React.useState<string>(currentUser ? currentUser.username : '');

    React.useEffect(() => {
        if (currentUser) setDisplayName(currentUser.username);
    }, [currentUser])

    return (
        <Header>
            <LogoContainer>
                <Link to='/'>
                    <Logo className='logo' />
                </Link>
            </LogoContainer>
            <Options>
                <Option>
                    <Link to='/counter'>
                        COUNTER
                    </Link>
                </Option>
                <Option>
                    <Link to='/fetch-data'>
                        FETCH DATA
                    </Link>
                </Option>

                {logged ? (
                    <Option onClick={() => {
                        logout();
                        props.setCurrentUser({ username: '', password: '' } as UserProps)
                    }}>
                        SIGN OUT
                    </Option>
                ) : (
                    <Option>
                        <Link to='/signin'>
                            SIGN IN
                        </Link>
                    </Option>
                )}
                {logged && currentUser && (
                    <Link className='option' to='/profile'>
                        <PersonCircle size={30} />
                        <ProfileUsername key={displayName}>{displayName}</ProfileUsername>
                    </Link>
                )}

            </Options>
        </Header>
    )
};

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
)(NavMenu);