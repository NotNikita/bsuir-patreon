import React, { useState } from 'react';
import FormInput from './form-input.component';
import CustomButton from './custom-button.component';
import styled from '@emotion/styled';
import { login, apiHostname } from '../auth';

import { Dispatch } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/user/user.actions';
import { UserProps } from '../store/user/user.types';
import { UserKnownAction } from '../store/user/user.reducer';


const SignInDiv = styled.div({
    width: 380,
    display: 'flex',
    flexDirection: 'column'
});
const SignInTitle = styled.div({
    fontWeight: 600,
    fontSize: '1.3rem',
    margin: '10px 0'
});
const SignInButtons = styled.div({
    display: 'flex',
    justifyContent: 'space-between'
});

const SignIn = (props: typeof actionCreators) => {
    const [displayName, setDisplayName] = useState('')
    const [password, setPassword] = useState('')

    const handleSubmit = (event: React.SyntheticEvent): void => {
        if (event) event.preventDefault();

        try {
            fetch(apiHostname + 'api/Authenticate/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    username: displayName,
                    password: password
                })
            })
                .then(r => r.json())
                .then(token => {
                    console.log('signin token received: ', token)
                    login({
                        ...token,
                        accessToken: token.token,
                        refreshToken: ''
                    })
                });

            props.setCurrentUser({
                username: displayName,
                password: password
            })
            setDisplayName('');
            setPassword('');
        } catch (err) {
            console.log('error occured in sign-in handleSubmit ');
        }
    }
    const handleChange = (event: React.FormEvent<HTMLInputElement>) => {
        const { value, name } = event.currentTarget;
        // if there will be more than 2 fiels, this will be bad
        name === 'displayName' ? setDisplayName(value) : setPassword(value);
    };

    return (
        <SignInDiv>
            <SignInTitle>I already have an account</SignInTitle>
            <span>Sign in with your username and password</span>

            <form onSubmit={handleSubmit}>
                <FormInput name='displayName' type='text' label='Display name'
                    handleChange={handleChange} value={displayName} required />

                <FormInput name='password' type='password' label='password'
                    handleChange={handleChange} value={password} required />

                <SignInButtons>
                    <CustomButton type='submit'>Sign in</CustomButton>
                </SignInButtons>
            </form>
        </SignInDiv>
    )
}

const mapDispatchToProps = (dispatch: Dispatch<UserKnownAction>) => ({
    setCurrentUser: (user: UserProps) => dispatch({
        type: 'SET_CURRENT_USER',
        payload: user
    })
});

export default connect(
    null,
    mapDispatchToProps
)(SignIn);
