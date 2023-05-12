import React, { useState } from 'react';
import './App.css';
import {Routes, Link, Route, BrowserRouter as Router} from 'react-router-dom';
import AppNavBar from "./components/appNavBar";
import Register from "./components/register";
import Login from "./components/login";
import Logout from './components/logout';
import PlayLists from './components/playList/playLists';
import PlayListView from './components/playList/playListView'
import Profile from './components/user/profile';
import { Provider } from 'react-redux';
import { store } from './store/store';
import PlayListEditor from './components/playList/playListEditor';
import GamePage from './components/game/gamePage';

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false); // создаем переменную, которая будет хранить статус авторизации пользователя

  return (
    <Provider store={store}>
    <Router>
      <div className="App">
        <AppNavBar />
        <Routes>
          <Route exact path="/" element={<PlayLists />} />
          <Route exact path="/playlists" element={<PlayLists />} />
          <Route path = "/playlist/:id" element={<PlayListView />}></Route>
          <Route path="/my" render={() => <><PlayLists displayMode="my"/></>} />
          <Route path="/register" element={<Register />} /> 
          <Route path="/login" element={<Login />} /> 
          <Route path="/profile/:id" element={<Profile />} /> 
          <Route path="/create" element={<PlayListEditor />} /> 
          <Route path="/edit/:id" element={<PlayListEditor displayMode="edit"/>} />
          <Route path="/game/:id" element={<GamePage />} /> 
          {/* <Route path="/logout" element={<Logout />} />  */}

         </Routes>
      </div>
    </Router>
    </Provider>
  );
}

export default App;
