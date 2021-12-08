import React from 'react'
import styled from '@emotion/styled'


const CustomButtonElement = styled.button({
  marginTop: 10,
  minWidth: 165,
  width: 'auto',
  height: 50,
  letterSpacing: 0.5,
  lineHeight: '50px',
  padding: '0 35px 0 35px',
  fontSize: 15,
  backgroundColor: 'black',
  color: 'white',
  textTransform: 'uppercase',
  fontFamily: 'Open Sans Condensed',
  fontWeight: 'bolder',
  border: 'none',
  cursor: 'pointer',
  '&:hover': {
    backgroundColor: 'white',
    color: 'black',
    border: '1px solid black'
  }
});

const CustomButton = ({ children, ...otherProps }) => {
  return (
    <CustomButtonElement {...otherProps}>
      {children}
    </CustomButtonElement>
  )
}

export default CustomButton
