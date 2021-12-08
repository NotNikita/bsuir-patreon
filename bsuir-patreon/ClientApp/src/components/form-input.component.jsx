import React from 'react'
import styled from '@emotion/styled'


const GroupDiv = styled.div({
    position: 'relative',
    margin: '45px 0'
});
const FormInputElement = styled.input({
    background: 'none',
    backgroundColor: 'white',
    color: 'grey',
    fontSize: 18,
    padding: '10px 10px 10px 5px',
    display: 'block',
    width: '100%',
    border: 'none',
    borderRadius: 0,
    borderBottom: '1px solid grey',
    margin: '5px 0',
    '&:focus': {
        outline: 'none'
    },
    '&:focus ~ .form-input-label': {
        top: -14,
        fontSize: 12,
        color: 'black',
    }
});
const FormInputLabel = styled.label({
    color: 'grey',
    fontSize: '16',
    fontWeight: 'normal',
    position: 'absolute',
    pointerEvents: 'none',
    left: '5',
    top: '10',
    transition: '300ms ease all',
    '&.shrink': {
        top: -18,
        fontSize: '0.8rem',
        color: 'black',
    }
});

const FormInput = ({ handleChange, label, ...otherProps }) => {
    return (
        <GroupDiv>
            <FormInputElement onChange={handleChange} {...otherProps} />
            {label ? (
                <FormInputLabel
                    className={otherProps.value.length ? 'shrink' : ''}
                >
                    {label}
                </FormInputLabel>
            ) : null}
        </GroupDiv>
    )
}

export default FormInput
