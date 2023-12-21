import { Button, TextField } from "@mui/material";
import CardPage from "../../../Components/CardPage";

import '../Create.css'
import { useEffect, useState } from "react";
import HtmlRequest from "../../../Util/HtmlRequest";
import { useLocation } from "react-router-dom";

function HandleConfirm(name, oldPriceType) {
    // TODO: Update

    // check for errors from frontend
    // send request
    // if 200
        // redirect to home
        // show success snackbar
    // if not 200
        // give correct response / error

    const createPriceTypeRequest = async() => {
        const response = await HtmlRequest(
            'POST',
            'https://api.localhost:6840/api/price/pricetype/' + (name)
        )
    
        console.log(response)
        if (response.status == 200) {
            const data = await response.json()
            console.log(data)
        }
    }

    const editPriceTypeRequest = async() => {
        const response = await HtmlRequest(
            'PUT',
            'https://api.localhost:6840/api/price/pricetype/edit/' + (oldPriceType.id),
            name,
        )
    
        console.log(response)
        const data = await response.json()
        console.log(data)
        if (response.status == 200) {
        }
    }

    if (oldPriceType == null) createPriceTypeRequest()
    else editPriceTypeRequest()
}

function CreateOrEditPriceType() {
    const [name, setName] = useState('')
    const data = useLocation();
    let state = data.state;

    console.log(name)
    
    useEffect(() => {
        if (state != null && state.id != null && state.name != null) {
            setName(state.name)
        }
    },[])

    return (
        <CardPage header = "Create PriceType">
            <form className="w100">
                <h2>Name:</h2>
                <TextField label="Name" variant = "outlined" required value={name} fullWidth onChange={(event) => {
                    setName(event.target.value)
                }}/>
            </form>
            <div className="create-confirm-wrapper w100">
                <Button variant='contained' onClick = {() => HandleConfirm(name, state)}>Confirm</Button>
            </div>
        </CardPage>
    )
}

export default CreateOrEditPriceType;