import * as React from 'react';
import styled from '@emotion/styled';

import './Footer.css';


const CommentCard = styled.div({
    boxSizing: 'border-box',
    display: 'flex',
    width: '100%',
    justifyContent: 'center',
    fontSize: '1.5rem',
    backgroundColor: '#eeeaea'
});

const Footer = () => {
    return (
        <footer>
            <CommentCard>
                2021. All right reserved
            </CommentCard>
        </footer>
    );
}

export default Footer
