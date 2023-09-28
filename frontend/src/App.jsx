import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [count, setCount] = useState(0)
  const [data, setData] = useState("waiting...")
  const [refreshToken, setRefreshToken] = useState(null)

  setRefreshToken(localStorage.getItem('refreshToken'))


  useEffect(() => {
    const fetchData = async () => {
      const refreshTokenResponse = await fetch('https://localhost:7091/accounts/validateRefreshToken', {
        method: 'POST',
        body: JSON.stringify({
          refreshToken: refreshToken
        }),
        headers: {'Content-Type': 'application/json'}
      });

      
      const d = await response.json()
      setData(d.message)
    }

    fetchData()
  })

  return (
    <>
      <p>
        {data}
      </p>
    </>
  )
}

export default App
