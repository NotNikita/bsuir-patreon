import React from 'react'

import FormInput from './form-input.component'
import CustomButton from './custom-button.component'
import styled from '@emotion/styled'


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


class SignUp extends React.Component {
    constructor() {
        super();

        this.state = {
            displayName: '',
            email: '',
            password: '',
            confirmPassword: '',
        }
        this.handleSubmit = this.handleSubmit.bind(this)
    }

    handleSubmit = async event => {
        // event.preventDefault()

        // const { displayName, email, password, confirmPassword } = this.state

        // if (password !== confirmPassword) {
        //     alert("passwords don't match")
        //     return
        // }

        // try {
        //     const { user } = await auth.createUserWithEmailAndPassword(
        //         email,
        //         password
        //     )

        //     await createUserProfileDocument(user, { displayName })

        //     this.setState({
        //         displayName: '',
        //         email: '',
        //         password: '',
        //         confirmPassword: ''
        //     });
        // } catch (error) {
        //     console.error(error)
        // }
    }

    handleChange = event => {
        const { name, value } = event.target
        this.setState({ [name]: value })
    }

    render() {
        const { displayName, email, password, confirmPassword } = this.state

        return (
            <SignUpDiv>
                <SignUpTitle>I dont have an account</SignUpTitle>
                <span>Sign up</span>
                <form onSubmit={this.handleSubmit} className="sign-up-form">
                    <FormInput
                        type='text'
                        name='displayName'
                        value={displayName}
                        onChange={this.handleChange}
                        label='Display name'
                        required
                    />
                    <FormInput
                        type='email'
                        name='email'
                        value={email}
                        onChange={this.handleChange}
                        label='Email'
                        required
                    />
                    <FormInput
                        type='password'
                        name='password'
                        value={password}
                        onChange={this.handleChange}
                        label='Password'
                        required
                    />
                    <FormInput
                        type='password'
                        name='confirmPassword'
                        value={confirmPassword}
                        onChange={this.handleChange}
                        label='Confirm password'
                        required
                    />
                    <CustomButton type='submit'>SIGN UP</CustomButton>
                </form>
            </SignUpDiv>
        )
    }
}

export default SignUp;