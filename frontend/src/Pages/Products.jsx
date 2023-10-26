import { useLocation, useNavigate } from "react-router-dom";
import './Products.css'
import { useState } from "react";

function Products() {
    const navigate = useNavigate();
    const [selected, setSelected] = useState(null);

    const data = useLocation();
    let id = data.state.id

    if (id == undefined || id == '') navigate('/')

    function addItem(i) {
        let map;
        if (selected == null) {
            map = {}
        } else {
            map = selected
        }
        if (map[i]) {
            map[i] = selected[i] + 1
        } else {
            map[i] = 1
        }
        setSelected({...map})
        console.log(map)
    }

    let arr = [
        'item1',
        'item2',
        'item3',
        'item4',
        'item5',
        'item6',
        'item7',
        'item8',
        'item9',
        'item10',
        'item11',
        'item12',
        'item13',
        'item14',
        'item15',
        'item16',
        'item17',
        'item18',
        'item19',
        'item20',
        'item21',
        'item22',
        'item23',
        'item24',
        'item25',
        'item26',
        'item27',
        'item28',
        'item29',
        'item30',
        'item31',
        'item32',
        'item33',
        'item34',
        'item35',
        'item36',
        'item37',
        'item38',
        'item39',
    ]

    return (
    <main className="products-page">
        <div className="test2">

    <div className="test">
        {arr.map(item =>
            <div className="test-item" key={item} onClick={() => {addItem(item)}}>{item}</div>
            )}
    </div>
            </div>
            {(selected != null) && Object.keys(selected).map(item => <div key={item}>{item} - {selected[item]}</div>)}
    </main>)
}

export default Products;