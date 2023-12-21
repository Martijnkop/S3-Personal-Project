import { Card } from "@mui/material"

import './CardPage.jsx.css'

function CardPage({header, children}) {
    return (
        <main className='cardpage-main'>
          <h1 className='cardpage-header'>{ header }</h1>
          <Card className='cardpage-card'>
            { children }
          </Card>
        </main>
    )
}

export default CardPage