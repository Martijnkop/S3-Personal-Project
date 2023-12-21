import { Button, TextField } from "@mui/material";
import {Icon} from "@mui/material";
import CardPage from "../../Components/CardPage";

import './Create.css'
import { useEffect, useState } from "react";
import HtmlRequest from "../../Util/HtmlRequest";
import { useLocation } from "react-router-dom";

import useIcons from "../../Util/useIcon";



function HandleConfirm(name, icon, oldItemCategory) {
    // TODO: Update

    // check for errors from frontend
    // send request
    // if 200
        // redirect to home
        // show success snackbar
    // if not 200
        // give correct response / error

    const createItemCategoryRequest = async() => {
        const response = await HtmlRequest(
            'POST',
            'https://api.localhost:6840/api/itemcategory/' + (name),
            icon
        )
    
        console.log(response)
        const data = await response.json()
        console.log(data)
        if (response.status == 200) {
        }
    }

    const editItemCategoryRequest = async() => {
        const response = await HtmlRequest(
            'PUT',
            'https://api.localhost:6840/api/itemcategory/edit/' + (oldItemCategory.id),
            {name, icon},
        )
    
        console.log(response)
        const data = await response.json()
        console.log(data)
        if (response.status == 200) {
        }
    }

    if (oldItemCategory == null) createItemCategoryRequest()
    else editItemCategoryRequest()
}

function CreateOrEditItemCategory() {
    const [name, setName] = useState('')
    const [icon, setIcon] = useState('')

    const [iconDisplay, setIconDisplay] = useState(<></>)

    const [iconValid, setIconValid] = useState(false)

    function displayIcon(i) {
        const DynIcon = useIcons(i, setIconValid)
        return (
            <DynIcon />
        )
    }

    const data = useLocation();
    let state = data.state;

    console.log(iconValid)

    useEffect(() => {
        if (state != null && state.id != null && state.name != null) {
            setName(state.name)
        }
        setIconDisplay(displayIcon(""))
    },[])

    return (
        <CardPage header = "Create ItemCategory">
            <form className="w100">
                <h2>Name:</h2>
                <TextField label="Name" variant = "outlined" required value={name} fullWidth onChange={(event) => {
                    setName(event.target.value)
                }}/>
                <h2>Name:</h2>
                <h2>Preview</h2>
                {iconDisplay}
                {/* <Icon >{icon}</Icon> */}
                <TextField label="Name" variant = "outlined" required value={icon} fullWidth onChange={(event) => {
                    setIconDisplay(displayIcon(event.target.value))
                    setIcon(event.target.value)
                }}/>
            </form>
            <div className="create-confirm-wrapper w100">
                <Button variant='contained' onClick = {() => {if (iconValid) HandleConfirm(name, icon, state)}}>Confirm</Button>
            </div>
        </CardPage>
    )
}

export default CreateOrEditItemCategory;