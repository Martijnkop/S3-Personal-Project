import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import Product from "../Components/Product";
import SelectedProducts from "../Views/SelectedProducts";

const products = ["asdf","abcd", "asdf", "123", "456", "7890","asdf","abcd", "asdf", "123", "456", "7890"]

import './ProductsPage.jsx.css'
import HtmlRequest from "../Util/HtmlRequest";

const fetchData = async () => {
    const response = await HtmlRequest(
        'GET',
        'https://api.localhost:6840/api/item/allwithprice/fbfabd7a-2f6f-4927-9386-9cda393e36af')

    let data = await response.json()
    return data
}

function ProductsPage() {
    const [itemList, setItemList] = useState()
    const [selected, setSelected] = useState({})
    const [totalPrice, setTotalPrice] = useState(0)

    function HandleClick(item) {
        console.log(item)
    }

    function addItem(i) {
        let map = selected
        if (map[i.id]) {
            map[i.id] = selected[i.id] + 1;
        } else {
            map[i.id] = 1
        }
        setSelected({...map})
        updateTotalPrice()
    }

    function removeItem(i) {
        let map;
        if (selected == null) return;
        else map = selected
        if (map[i]) {
            if (map[i] > 1) map[i] -- ;
            else delete map[i]
        }
        setSelected({...map})
        updateTotalPrice()
    }

    function updateTotalPrice() {
        let sum = 0;
        Object.keys(selected).forEach(id => {
            let item = itemList.find(item => item.id == id);
            sum += item.activePrice.amount * selected[id]
        });
        setTotalPrice(sum)
    }

    const navigate = useNavigate();

    const data = useLocation();
    let state = data.state;

    useEffect(() => {
        if (state == undefined || state.id == undefined || state.id == '') navigate('/')

        if (!itemList) {
            async function getData() {
                let items = await fetchData()
                setItemList(items)
            }

            getData()
        }
    })

    return (
        <main className="product-page w100">
            <section className="products-wrapper">
            {itemList != null && itemList.map((item) => {
                return <Product key={item.id} item={item} click={addItem} />
            })}
            </section>
        <SelectedProducts selected={selected} itemList={itemList} totalPrice={totalPrice} removeItem={removeItem} />
        </main>
    )
}

export default ProductsPage;