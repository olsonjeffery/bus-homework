import {createTheme, ThemeOptions}  from '@mui/material/styles'

const themeOptions: ThemeOptions = {
  
  palette: {
    background:{default: '#ffffff', paper: '#ffffff'},
    primary: {
      main: '#5626c4',
    },
    secondary: {
      main: '#f50057',
    },
    text: {
      primary: '#121212',
      disabled: 'rgba(18,18,18,0.36)',
      secondary: 'rgba(12,12,12,0.54)',
    },
    warning: {
      main: '#facd3d',
    },
    info: {
      main: '#2cccc3',
    },
    success: {
      main: '#2cac68',
    },
  },
};

export const theme = createTheme(themeOptions);
