function SelectedProduct ({item, remove, count}) {
    return (
        <div key={item.id} onClick={() => {remove(item)}} className="cart-item">
            {item.name} - {count}
        </div>
    )
}

export default SelectedProduct;