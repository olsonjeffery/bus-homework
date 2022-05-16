import React from 'react';
import logo from './logo.svg';
import { Counter } from './features/counter/Counter';
import './App.css';
import {theme} from './theme-options';
import {ThemeProvider} from '@mui/material/styles';
import {Site} from './features/site/Site';

function App() {
  return (
    <div className="App">
      <ThemeProvider theme={theme}>
        <Site></Site>
      </ThemeProvider>
    </div>
  );
}

export default App;
