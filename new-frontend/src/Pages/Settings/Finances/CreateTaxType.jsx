import { Button, InputAdornment, TextField } from "@mui/material";
import CardPage from "../../../Components/CardPage";

import '../Create.css'
import { useState } from "react";
import HtmlRequest from "../../../Util/HtmlRequest";
import { useLocation } from "react-router-dom";

function HandleConfirm(name, number, state) {
    // TODO: Update

    // check for errors from frontend
    // send request
    // if 200
        // redirect to home
        // show success snackbar
    // if not 200
        // give correct response / error

    const createTaxTypeRequest = async() => {
        const response = await HtmlRequest(
            'POST',
            'https://api.localhost:6840/api/price/taxtype/' + (name),
            {
                percentage: number
            }
        )
    
        console.log(response)
        if (response.status == 200) {
            const data = await response.json()
            console.log(data)
        }
    }

    const editTaxTypeRequest = async() => {
        const response = await HtmlRequest(
            'PUT',
            'https://api.localhost:6840/api/price/taxtype/edit/' + (state.id),
            name,
        )
    
        console.log(response)
        const data = await response.json()
        console.log(data)
        if (response.status == 200) {
        }
    }

    if (state == null) createTaxTypeRequest()
    else editTaxTypeRequest()
}

function CreateTaxType() {
    const [name, setName] = useState('')
    const [percentage, setPercentage] = useState(0)
    const [create, setCreate] = useState(true)

    const data = useLocation()
    console.log(data)
    console.log(create)
    useState(() => {
        if (data.state != null && data.state.id != null) {
            setCreate(false)
            setName(data.state.name)
        }
    }, [])

    return (
        <CardPage header = {create ? "Create TaxType" : "Edit TaxType"}>
            <form className="w100">
                <h2>Name:</h2>
                <TextField label="Name" variant = "outlined" value={name} required fullWidth onChange={(event) => {
                    setName(event.target.value)
                }}/>
                {create && <>
                <h2>Percentage:</h2>
                <TextField label="Number" variant = "outlined" type="number" required fullWidth onChange={(event) => {
                    setPercentage(event.target.value)
                }}/>
                </>}
            </form>
            <div className="create-confirm-wrapper w100">
                <Button variant='contained' onClick = {() => HandleConfirm(name, percentage, data.state)}>Confirm</Button>
            </div>
        </CardPage>
    )
}

export default CreateTaxType;