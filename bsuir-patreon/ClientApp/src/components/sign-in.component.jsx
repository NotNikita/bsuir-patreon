import React, { useState } from 'react'
import FormInput from './form-input.component'
import CustomButton from './custom-button.component'
import styled from '@emotion/styled'
import { login, apiHostname } from '../auth';


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

const SignIn = () => {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    const handleSubmit = event => {
        if (event) event.preventDefault()

        try {
            fetch(apiHostname + 'api/Authenticate/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    username: email,
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
                })

            setEmail('')
            setPassword('')
        } catch (error) {
            console.log('error occured in sign-in handleSubmit ' + error.message)
        }
    }
    const handleChange = event => {
        const { value, name } = event.target;
        // if there will be more than 2 fiels, this will be bad
        name === 'email' ? setEmail(value) : setPassword(value)
    }

    return (
        <SignInDiv>
            <SignInTitle>I already have an account</SignInTitle>
            <span>Sign in with your username and password</span>

            <form onSubmit={handleSubmit}>
                <FormInput name='email' type='text' label='username'
                    handleChange={handleChange} value={email} required />

                <FormInput name='password' type='password' label='password'
                    handleChange={handleChange} value={password} required />

                <SignInButtons>
                    <CustomButton type='submit'>Sign in</CustomButton>
                </SignInButtons>
            </form>
        </SignInDiv>
    )
}

export default SignIn
