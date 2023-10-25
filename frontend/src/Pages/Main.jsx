import { useState } from "react"

function MainPage() {
  const [list, setList] = useState('')
  const [name, setName] = useState('')
  const [id, setId] = useState('')

    function HandleInput(e) {
        let text = e.target.value
        setName(text)
        setId('')
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
      }

    return (
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
    )

}

export default MainPage