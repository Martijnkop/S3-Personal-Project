import './App.css'

import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

import Themer from './Util/Components/Themer';
import Router from './Util/Components/Router';

import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

function App() {


  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <Themer>
        <Router />
      </Themer>
    </LocalizationProvider>
  );
}

export default App;