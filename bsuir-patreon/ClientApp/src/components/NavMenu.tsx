import * as React from 'react';
import styled from '@emotion/styled';
import { Link } from 'react-router-dom';
import { PersonCircle } from 'react-bootstrap-icons';
import { ReactComponent as Logo } from '../assets/svg/crown.svg';

import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { UserState } from '../store/user/user.reducer';
import { useAuth, logout } from '../auth';

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


const NavMenu = (props: UserState) => {
    const [logged] = useAuth();

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
                    <Option onClick={() => logout()}>
                        SIGN OUT
                    </Option>
                ) : (
                    <Option>
                        <Link to='/signin'>
                            SIGN IN
                        </Link>
                    </Option>
                )}
                <Link className='option' to='/profile'>
                    <PersonCircle size={30} />
                </Link>
            </Options>
        </Header>
    )
};

const mapStateToProps = (state: ApplicationState) => {
    return state.user ? { currentUser: state.user.currentUser } : {
        currentUser: null
    };
};

export default connect(
    mapStateToProps
)(NavMenu);
