import * as React from 'react';
import { connect } from 'react-redux';
import { useAuth, apiHostname, authFetch } from '../auth';
import { Post } from './post/post.types';
import styled from '@emotion/styled';

const PostsTitle = styled.p({
  boxSizing: 'border-box',
  display: 'flex',
  width: '100%',
  justifyContent: 'center',
  fontSize: '2rem',
  backgroundColor: '#eeeaea'
});

const EmptyComponent = styled.div({
  boxSizing: 'border-box',
  display: 'flex',
  width: '100%',
  height: '350px',
  justifyContent: 'center',
  alignItems: 'center',
  fontSize: '1.3rem',
  fontWeight: 'bold',
  backgroundColor: '#fff'
})


const Posts = () => {
  const [logged] = useAuth();
  const [posts, setPosts] = React.useState<Post[]>([]);

  React.useEffect(() => {
    logged && authFetch(apiHostname + 'api/Post', {
      headers: {
        "Transfer-Encoding": "buffered",
        'accept': 'application/json'
      }
    })
      .then(r => r.json())
      .then(_posts => {

        console.log('posts: ', _posts)
        setPosts(_posts)
      })
  }, [logged]);

  return (
    <div>
      <PostsTitle>Current posts, visible for you:</PostsTitle>
      <div>
        {posts.length ? posts.map(post =>
          <>
            <div key={post.id}>
              {`postid: ${post.content}`}
            </div>
            <div key={post.content}>
              {`content: ${post.content}`}
            </div>
          </>)
          :
          <EmptyComponent>Unfortunately, the data is not available to you</EmptyComponent>}
      </div>
    </div>
  )
};

export default connect()(Posts);
