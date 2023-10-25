import { useEffect, useState, useRef } from 'react'
import './App.css'
import Login from './Views/Login'
import Nav from './Views/Nav'
import Auth from './Util/Auth'
import Products from './Pages/Products'

function App() {
  const [refreshToken, setRefreshToken] = useState(null)
  const [accessToken, setAccessToken] = useState(null)
  const [list, setList] = useState('')
  const [name, setName] = useState('')
  const [id, setId] = useState('')

  const authRef = useRef()

  function HandleLogin(email, password) {
    authRef.current.HandleLogin(email, password)
  }

  function HandleInput(e) {
    let text = e.target.value
    setName(text)
    if (text != '') {
      const fetchInput = async () => {
        const response = await fetch('https://api.localhost:6840/accounts/list', {
        method: 'POST',
        body: JSON.stringify({
          filter: text
        }),
        headers: {'Content-Type': 'application/json',
                  'Authorization': `Bearer ${accessToken}`}
      });

      if (response.status == 404) return;
      console.log(response)
      const d = await response.json()
      setList(d.body)
      }

      fetchInput()
    } else {
      setList('')
    }
  }

  function HandleSearch(e) {
    console.log(e)
    console.log(id)
  }

  return (
    <>
      <Auth ref={authRef} setAccessToken={setAccessToken} setRefreshToken={setRefreshToken}>
      {(accessToken == null || accessToken == undefined	) && (<Login submit={HandleLogin}/>)}
        <Nav />
        <main>
          <div className='searchContainer'>

          <input id="nameSearcher" onInput={(e) => HandleInput(e)} value={name}></input>
          {list != '' && (
            <ul className='namelist'>
              {list.map((item) => <li onClick={() => {
                setName(item.name)
                setId(item.id)
                setList('')
                }}>{item.name}</li>)}
            </ul>
          )}
          <button className='search' onClick={(e) => HandleSearch(e)}>search!</button>
          </div>
        </main>
      </Auth>
    </>
  )
}

export default App
