import './Product.jsx.css'

function Product({ item, click }) {
    return (
        <div className='product' onClick={() => click(item)}>
            <img src={"http://localhost:9000/" + item.filePath} className='product-image' />
            <div className='product-info-wrapper'>

                <div className='product-info'>
                    <p className='product-text'>{item.name}</p>
                    <p className='product-text'>€{item.activePrice.amount}</p>
                </div>
            </div>
            {/* <div className='product-content'>
                <div className='product-info-wrapper'>
                    <div className='product-info'>
                        <div className='product-name'>
                            {item.name}
                        </div>
                        <div className='product-price'>
                            €1.00
                        </div>
                    </div>
                </div>
            </div> */}
        </div>
    )
}

export default Product