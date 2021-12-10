import * as React from 'react';
import { connect } from 'react-redux';
import { useAuth, apiHostname, authFetch } from '../auth';
import { Post } from './post/post.types';
import styled from '@emotion/styled';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Typography from '@material-ui/core/Typography';
import { CardActionArea } from '@material-ui/core';

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
            <Card style={{ maxWidth: 345 }}>
              <CardActionArea>
                <CardMedia
                  component="img"
                  height="140"
                  image={post.fileUrl ? post.fileUrl : 'https://mui.com/static/images/cards/contemplative-reptile.jpg'}
                  alt="green iguana"
                />
                <CardContent>
                  <Typography gutterBottom variant="h5" component="div">
                    {post.author}
                  </Typography>
                  <Typography variant="body2">
                    {post.content}
                  </Typography>
                </CardContent>
              </CardActionArea>
            </Card>
          </>)
          :
          <EmptyComponent>Unfortunately, the data is not available to you</EmptyComponent>}
      </div>
    </div>
  )
};

export default connect()(Posts);
