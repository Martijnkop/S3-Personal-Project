import { useLocation, useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { useEffect, useState } from "react";

import HtmlRequest from "../Util/HtmlRequest";

import './Products.css'

function Products() {
    const navigate = useNavigate();
    const accessToken = useSelector((state) => state.auth.value.accessToken)

    const [selected, setSelected] = useState(null);
    const [itemList, setItemList] = useState([]);

    const data = useLocation();
    let id = data.state.id

    if (accessToken == '' || id == undefined || id == '') navigate('/')

    useEffect(() => {
        const fetchData = async () => {
        const response = await HtmlRequest(
            'GET',
            'https://api.localhost:6840/items/list',
            null, 
            accessToken)
        let body = await response.json()

        setItemList(body.body)

        }

        if (accessToken) fetchData()
    },[])

    function addItem(i) {
        let map;
        if (selected == null) {
            map = {}
        } else {
            map = selected
        }
        if (map[i.id]) {
            map[i.id] = selected[i.id] + 1
        } else {
            map[i.id] = 1
        }
        setSelected({...map})
        console.log(map)
    }

    if (selected) console.log(Object.keys(selected))

    return (
    <main className="products-page">
        <div className="test2">

    <div className="test">
        {itemList && itemList.map(item =>
            <div className="test-item" key={item.id} onClick={() => {addItem({...item})}}>{item.name}</div>
            )}
    </div>
            </div>
            <section className="cart">

            {(selected != null) && Object.keys(selected).map(item => {
                let i = itemList.find((i) => i.id == item)
                return <div key={i.id}>{i.name} - {selected[item]}</div>})}
            </section>
    </main>)
}

export default Products;