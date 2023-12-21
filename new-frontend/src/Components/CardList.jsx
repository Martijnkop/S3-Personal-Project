import { Button, Card, Divider } from "@mui/material"
import { useNavigate } from "react-router-dom";

function CardList({title, items}) {
    const navigate = useNavigate();

    return (
        <div className="cardlist-wrapper">

            <h2 className='cardlist-header'>{ title }</h2>
            <Card className='cardlist-card'>
                {items.map(item => {
                    return (
                        <div key={item.uri} className="cardlist-item">
                            <Divider className="cardlist-divider"/>
                            <Button className='w100' onClick={() => navigate(`/${item.uri}`)}>{item.name}</Button>
                        </div>
                    )
                })}
            </Card>
        </div>
    )
}

export default CardList