import { useRef } from 'react'
import './App.css'
import Login from './Views/Login'
import Nav from './Views/Nav'
import Auth from './Util/Auth'
import { useSelector } from 'react-redux'
import MainPage from './Pages/Main'

function App() {
  const accessToken = useSelector((state) => state.auth.value.accessToken)

  const authRef = useRef()

  function HandleLogin(email, password) {
    authRef.current.HandleLogin(email, password)
  }

  return (
    <>
      <Auth ref={authRef}>
      {(accessToken == '') && (<Login submit={HandleLogin}/>)}
        <Nav />
        <main>
          <MainPage />
        </main>
      </Auth>
    </>
  )
}

export default App
