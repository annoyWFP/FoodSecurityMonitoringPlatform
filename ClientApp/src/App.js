import React, { Component } from 'react';
import './custom.css';
import Dashboard from './components/Dashboard'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
        <div className="App">
            <h1>Food Security Monitoring Dashboard</h1>
            <Dashboard />
        </div>
    );
  }
}
