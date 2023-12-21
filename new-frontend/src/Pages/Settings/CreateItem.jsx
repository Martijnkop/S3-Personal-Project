import { Button, Divider, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import CardPage from "../../Components/CardPage";

import './CreateItem.jsx.css'
import { useEffect, useState } from "react";
import HtmlRequest from "../../Util/HtmlRequest";
import logReadableStream from "../../Util/LogReadableStream";

function HandleConfirm(name, taxType, itemCategory, priceTypes, image) {
    // TODO: Update

    // check for errors from frontend
    // send request
    // if 200
        // redirect to home
        // show success snackbar
    // if not 200
        // give correct response / error

    let priceTypeMap = priceTypes.reduce((map, priceType) => {
        map[priceType.id] = priceType.price
        return map;
    }, {})

    const createItemRequest = async() => {
        const formData = new FormData()
        formData.append("Image", image)
        formData.append("Name", name)
        formData.append("TaxTypeId", taxType.id)
        formData.append("CategoryId", itemCategory.id)
        for (const [key, value] of Object.entries(priceTypeMap)) {
            formData.append(`ItemPrices[${key}]`, value);
        }
        const response = await HtmlRequest(
            'POST',
            'https://api.localhost:6840/api/item/' + (name),
            formData,
            null,
            true,
            true                       
        )
    
        console.log(response)
        logReadableStream(response.body)
        // const data = await response.json()
        // console.log(data)
        if (response.status == 200) {
        }
    }

    createItemRequest()
}

function CreateItem() {
    const [name, setName] = useState('')
    const [priceTypes, setPriceTypes] = useState([])
    const [taxTypes, setTaxTypes] = useState([])
    const [taxType, setTaxType] = useState('')
    const [itemCategories, setItemCategories] = useState([])
    const [itemCategory, setItemCategory] = useState('')
    const [image, setImage] = useState()

    function saveFile(e) {
        console.log(e.target.files[0])
        setImage(e.target.files[0])
    }

    function HandleChangeTaxType(e) {
        console.log(e.target)
        setTaxType(e.target.value)
    }

    function HandleChangeItemCategory(e) {
        console.log(e.target)
        setItemCategory(e.target.value)
    }

    useEffect(() => {
        const getPriceTypes = async() => {
            const response = await HtmlRequest(
                'GET',
                'https://api.localhost:6840/api/price/pricetype'
            )
        
            if (response.status == 200) {
                const data = await response.json()
                setPriceTypes(data)
                console.log(data)
            }
        }

        const getTaxTypes = async() => {
            const response = await HtmlRequest(
                'GET',
                'https://api.localhost:6840/api/price/taxtype'
            )
        
            if (response.status == 200) {
                const data = await response.json()
                console.log(data)
                setTaxTypes(data)
            }
        }

        const getItemCategories = async() => {
            const response = await HtmlRequest(
                'GET',
                'https://api.localhost:6840/api/itemcategory'
            )
        
            const data = await response.json()
            console.log(data)
            if (response.status == 200) {
                setItemCategories(data)
            }
        }

        getPriceTypes()
        getTaxTypes()
        getItemCategories()
    },[])

    return (
        <CardPage header = "Create Item">
            <form className="w100 create-item-form">
                <div>
                    <h2>Name:</h2>
                    <TextField label="Name" variant = "outlined" required fullWidth onChange={(event) => {
                        setName(event.target.value)
                    }}/>
                </div>
                <div className="item-form-other-options">
                    <div className="option">
                        <h2 className="option-header">Tax Type:</h2>
                        <InputLabel id="taxtype-select" className="w100">Tax Type</InputLabel>
                        <Select labelId="taxtype-select" value={taxType} onChange={(e) => HandleChangeTaxType(e)} className="w100">
                            {taxTypes.map(taxType => {
                                return <MenuItem value={taxType}>{taxType.name}</MenuItem>
                            })}
                        </Select>
                    </div>
                    <Divider className="option-divider"/>
                    <div className="option">
                        <h2 className="option-header">Item Category:</h2>
                        <InputLabel id="itemcategory-select" className="w100">Item Category</InputLabel>
                        <Select labelId="itemcategory-select" value={itemCategory} onChange={(e) => HandleChangeItemCategory(e)} className="w100">
                            {itemCategories.map(itemCategory => {
                                return <MenuItem value={itemCategory}>{itemCategory.name}</MenuItem>
                            })}
                        </Select>
                    </div>
                    <Divider className="option-divider"/>
                    <div className="option">
                        <h2 className="option-header">Icon:</h2>
                        <input type="file" onChange={(e) => saveFile(e)} />
                    </div>

                    <div className="option">
                        <h2 className="option-header">Prices</h2>
                        <div className="prices-wrapper">
                            {priceTypes.map(priceType => {
                                return <><h3 key={priceType.id}>{priceType.name}</h3>
                                <TextField label={priceType.name} variant = "outlined" required fullWidth onChange={(event) => {
                                    console.log(event.target.value)
                                    let oldPriceTypes = priceTypes
                                    let index = oldPriceTypes.indexOf(priceType)
                                    oldPriceTypes[index].price = event.target.value
                                    setPriceTypes(oldPriceTypes)
                                }}/>
                                </>
                            })}
                        </div>
                    </div>
                </div>
            </form>
            <div className="create-item-confirm-wrapper w100">
                <Button variant='contained' onClick = {() => HandleConfirm(name, taxType, itemCategory, priceTypes, image)}>Confirm</Button>
            </div>
        </CardPage>
    )
}

export default CreateItem;