import { useLocation, useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { useEffect, useState } from "react";

import HtmlRequest from "../Util/HtmlRequest";

import Product from "../Components/Product";
import SelectedProduct from "../Components/SelectedProduct";

import './Products.css'

function Products() {
    const navigate = useNavigate();
    const accessToken = useSelector((state) => state.auth.value.accessToken)

    const [selected, setSelected] = useState(null);
    const [itemList, setItemList] = useState(null);

    const data = useLocation();
    let id =data.state.id

    
    useEffect(() => {
        if (accessToken == '' || id == undefined || id == '') navigate('/')
        const fetchData = async () => {
        const response = await HtmlRequest(
            'GET',
            'https://api.localhost:6840/items/list',
            null, 
            accessToken)
        let body = await response.json()

        setItemList(body.body)
        console.log(body.body)

        }

        if (!itemList) fetchData()
    }, [itemList, accessToken])


    function addItem(i) {
        let map;
        if (selected == null) {
            map = {}
        } else {
            map = selected
        }
        if (map[i.id]) {
            map[i.id] = selected[i.id] + 1;
        } else {
            map[i.id] = 1
        }
        setSelected({...map})
        console.log(map)
    }

    function removeItem(i) {
        let map;
        if (selected == null) return;
        else map = selected
        if (map[i.id]) {
            if (map[i.id] > 1) map[i.id] -- ;
            else delete map[i.id]
        }
        setSelected({...map})
        console.log(map)
    }

    async function order() {
        await HtmlRequest("POST",
        "https://api.localhost:6840/Orders/create",
        {
            AccountOrdered: id,
            OrderItems: selected
        },
        accessToken)
        navigate('/')
    }

    if (selected) console.log(Object.keys(selected))

    return (
    <main className="products-page">
        <section className="products-list">

            <div className="products-wrapper">
                {itemList && itemList.map(item =>
                    <Product item = {item} add = {addItem} />    
                )}
            </div>

        </section>

        <section className="cart">

            {(selected != null) && Object.keys(selected).map(item => {
                let i = itemList.find((i) => i.id == item)
                return <SelectedProduct item = {i} remove = {removeItem} count = {selected[item]} />
            })}
            
            <div className="order">
                <div className="order-button" onClick={() => {order()}}>
                    Order
                </div>
            </div>

        </section>
    </main>)
}

export default Products;