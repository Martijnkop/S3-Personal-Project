import { Cancel } from '@mui/icons-material';
import './SelectedProducts.jsx.css'
import { Button } from '@mui/material';
import HtmlRequest from '../Util/HtmlRequest';
import logReadableStream from '../Util/LogReadableStream';

function SelectedProducts({ selected, itemList, totalPrice, removeItem }) {
    function placeOrder() {
        const placeOrderRequest = async() => {
            const response = await HtmlRequest(
                'POST',
                'https://api.localhost:6840/api/order',
                {
                    itemIdsWithAmounts: selected,
                    priceTypeId: 'fbfabd7a-2f6f-4927-9386-9cda393e36af'
                }
            )
        
            console.log(response)
            logReadableStream(response.body)
            const data = await response.json()
            console.log(data)
            if (response.status == 200) {
            }
        }
    
        placeOrderRequest()
    }

    return (
        <section className="selected-products">
            <div className='item-list w100'>
                <h2>Bestelling</h2>
            {selected && Object.keys(selected).map(id => {
                let item = itemList.find(item => item.id == id)
                return <div className="selected-item w100" onClick={() => removeItem(id)} key={id}>

                    {item.name}: {selected[id]}
                <Cancel color="error" className='curp selected-item-icon' />
                </div>
            })}
            </div>
            <div className='selected-products-actions w100'>€{totalPrice}</div>
            <Button variant='contained' className='products-confirm' onClick={(e) => placeOrder()}>Place Order</Button>
        </section>
    )
}

export default SelectedProducts;