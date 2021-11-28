import * as React from 'react';
import styled from '@emotion/styled';

import './Footer.css';


const CommentCard = styled.div({
    boxSizing: 'border-box',
    display: 'flex',
    width: '100%',
    justifyContent: 'center',
    fontSize: '2.4rem',
    backgroundColor: 'grey'
});

const Footer = () => {
    return (
        <footer>
            <CommentCard>
                This is footer
            </CommentCard>
        </footer>
    );
}

export default Footer
