import * as React from 'react';
import { connect } from 'react-redux';
import { apiHostname, authFetch } from '../auth';
import { Post } from './post/post.types';

const Posts = () => {
  const [posts, setPosts] = React.useState<Post[]>([]);

  React.useEffect(() => {
    authFetch(apiHostname + 'api/Post', {
      headers: {
        "Transfer-Encoding": "buffered",
        'Content-Type': 'application/json; charset=UTF-8'
      }
    })
      .then(r => r.json())
      .then(_posts => setPosts(_posts))
  }, []);

  return (
    <div>
      <h1>Hello, world!</h1>
      <p>Welcome to your new single-page application, built with:</p>
      <ul>
        <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
        <li><a href='https://facebook.github.io/react/'>React</a> and <a href='https://redux.js.org/'>Redux</a> for client-side code</li>
        <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
      </ul>
      <p>To help you get started, we've also set up:</p>
      <div>
        {posts.map(post =>
          <>
            <div key={post.id}>
              {`postid: ${post.content}`}
            </div>
            <div key={post.content}>
              {`content: ${post.content}`}
            </div>
          </>)}
      </div>
      <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>
    </div>
  )
};

export default connect()(Posts);
