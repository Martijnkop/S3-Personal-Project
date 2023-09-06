import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [count, setCount] = useState(0)
  const [data, setData] = useState("waiting...")

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch("https://localhost:7091/HelloWorld")
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
