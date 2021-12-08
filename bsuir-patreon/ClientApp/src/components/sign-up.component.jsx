import React from 'react'

import FormInput from './form-input.component'
import CustomButton from './custom-button.component'
import styled from '@emotion/styled'
import { apiHostname } from '../auth';


const SignUpDiv = styled.div({
    display: 'flex',
    flexDirection: 'column',
    width: 380
});
const SignUpTitle = styled.div({
    fontWeight: 600,
    fontSize: '1.3rem',
    margin: '10px 0'
});


const SignUp = () => {
    const [credentials, setCredentials] = React.useState({
        displayName: '',
        name: '',
        surname: '',
        email: '',
        password: '',
        confirmPassword: '',
    })

    const handleSubmit = async event => {
        if (event) event.preventDefault()

        const { displayName, email, password, confirmPassword, name, surname } = credentials

        if (password !== confirmPassword) {
            alert("passwords don't match")
            return
        }

        try {
            fetch(apiHostname + 'api/Authenticate/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    username: displayName,
                    name: name,
                    surname: surname,
                    email: email,
                    password: password,
                    confirmPassword: confirmPassword
                })
            })
                .then(r => r.json())
                .then(response => {
                    console.log('sign-up status: ', response.status)
                    alert(response.message)
                })

            setCredentials({
                displayName: '',
                name: '',
                surname: '',
                email: '',
                password: '',
                confirmPassword: '',
            });
        } catch (error) {
            console.error(error)
        }
    }

    const handleChange = event => {
        const { name, value } = event.target
        setCredentials({
            ...credentials,
            [name]: value
        })
    }

    return (
        <SignUpDiv>
            <SignUpTitle>I dont have an account</SignUpTitle>
            <span>Sign up</span>
            <form onSubmit={handleSubmit} className="sign-up-form">
                <FormInput
                    type='text'
                    name='displayName'
                    value={credentials.displayName}
                    onChange={handleChange}
                    label='Display name'
                    required
                />
                <FormInput
                    type='text'
                    name='name'
                    value={credentials.name}
                    onChange={handleChange}
                    label='Your name'
                    required
                />
                <FormInput
                    type='text'
                    name='surname'
                    value={credentials.surname}
                    onChange={handleChange}
                    label='Your name surname'
                    required
                />
                <FormInput
                    type='email'
                    name='email'
                    value={credentials.email}
                    onChange={handleChange}
                    label='Email'
                    required
                />
                <FormInput
                    type='password'
                    name='password'
                    value={credentials.password}
                    onChange={handleChange}
                    label='Password'
                    required
                />
                <FormInput
                    type='password'
                    name='confirmPassword'
                    value={credentials.confirmPassword}
                    onChange={handleChange}
                    label='Confirm password'
                    required
                />
                <CustomButton type='submit'>SIGN UP</CustomButton>
            </form>
        </SignUpDiv>
    )
}

export default SignUp;