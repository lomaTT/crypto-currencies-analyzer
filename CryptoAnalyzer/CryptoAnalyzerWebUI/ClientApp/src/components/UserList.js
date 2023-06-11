import React, { Component } from 'react';

export class UserList extends Component {
  static displayName = UserList.name;

  constructor(props) {
    super(props);
    this.state = { users: [], loading: true };
  }

  componentDidMount() {
    this.fetchUserList();
  }

  renderUserList(users) {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
          </tr>
        </thead>
        <tbody>
          {users.map(user => (
            <tr key={user.id}>
              <td>{user.id}</td>
              <td>{user.name}</td>
              <td>{user.email}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    const { users, loading } = this.state;

    let contents = loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderUserList(users)
    );

    return (
      <div>
        <h1 id="tableLabel">User List</h1>
        {contents}
      </div>
    );
  }

  async fetchUserList() {
    try {
      const token = localStorage.getItem('token'); // Assuming the JWT token is stored in localStorage
      const response = await fetch('/api/database/users', {
        headers: {
          Authorization: `Bearer ${token}` // Include the JWT token in the Authorization header
        }
      });

      const data = await response.json();

      if (response.ok) {
        this.setState({ users: data, loading: false });
      } else {
        throw new Error('Failed to fetch user list');
      }
    } catch (error) {
      console.error(error);
    }
  }
}
