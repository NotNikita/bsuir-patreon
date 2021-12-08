import * as React from 'react';

export interface Post {
    id: number;
    content: string;
    author: string;
    fileUrl: string;
    likes: null; // Like
    comments: null; // Comment
};