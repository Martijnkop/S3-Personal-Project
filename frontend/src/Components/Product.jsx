function Product({item, add}) {
    return (
        <div className="product" key={item.id} onClick={() => {add({...item})}}>
            <div>
                <p>
                    {item.name}
                </p>
                <p className="product-price">€{item.currentPrice}</p>
            </div>
        </div>
    )
}

export default Product