import { useState } from "react"
import { useSelector } from 'react-redux'
import { useNavigate } from "react-router-dom"
import './Home.css'
import HtmlRequest from "../Util/HtmlRequest"


function Home() {
  const accessToken = useSelector((state) => state.auth.value.accessToken)

  const navigate = useNavigate();

  const [list, setList] = useState('')
  const [name, setName] = useState('')
  const [id, setId] = useState('')


    function HandleInput(e) {
        let text = e.target.value
        setName(text)
        setId('')
        if (text != '') {
          const fetchInput = async () => {
            const response = await HtmlRequest(
              'POST',
              'https://api.localhost:6840/accounts/list',
              {
                filter: text
              }, 
              accessToken)

          if (response.status == 404) return;

            const d = await response.json()
            setList(d.body)
          }
    
          fetchInput()
        } else {
          setList('')
        }
      }
    
      function HandleSearch(e) {
        console.log(id)
        if (id == '') return;
        navigate('/products', {state: {id: id}})
      }

    return (
      <main className="home">
        <div className='searchContainer'>
          <input id="nameSearcher" onInput={(e) => HandleInput(e)} value={name}></input>
          {list != '' && (
            <ul className='namelist'>
              {list.map((item) => <li onClick={() => {
                setName(item.name)
                setId(item.id)
                setList('')
                }} key={item.id}>{item.name}</li>)}
            </ul>
          )}
          <button className='search' onClick={(e) => HandleSearch(e)}>search!</button>
        </div>
      </main>
    )

}

export default Home