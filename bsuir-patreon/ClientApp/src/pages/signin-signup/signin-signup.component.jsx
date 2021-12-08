import React from 'react'
import styled from '@emotion/styled'

import SignIn from '../../components/sign-in.component'
import SignUp from '../../components/sign-up.component'

const SignInAndSignUpDiv = styled.div({
    width: 850,
    display: 'flex',
    justifyContent: 'space-between',
    margin: '30px auto'
});

const SignInAndSignUpPage = () => {
    return (
        <SignInAndSignUpDiv>
            <SignIn></SignIn>
            <SignUp></SignUp>
        </SignInAndSignUpDiv>
    )
}

export default SignInAndSignUpPage
