import { useRef } from 'react'
import Login from './Views/Login'
import Nav from './Views/Nav'
import Auth from './Util/Auth'
import { useSelector } from 'react-redux'
import {
  BrowserRouter, Routes, Route
} from "react-router-dom";
import Home from './Pages/Home'
import Products from './Pages/Products'

function App() {
  const accessToken = useSelector((state) => state.auth.value.accessToken) 
  console.log(accessToken)

  const authRef = useRef()

  function HandleLogin(email, password) {
    authRef.current.HandleLogin(email, password)
  }

  return (
    
    <Auth ref={authRef}>
      {(accessToken == '') && (<Login submit={HandleLogin}/>)}
        <BrowserRouter>
      <Nav />
          <Routes>
              <Route path="/" element = {<Home /> }/>
              <Route path="/products" element = {<Products />} />
          </Routes>
        </BrowserRouter>
    </Auth>
  )
}

export default App
