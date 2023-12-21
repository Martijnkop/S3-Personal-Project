import { Button, TextField } from "@mui/material";
import CardPage from "../../Components/CardPage";

import './CreateAccount.jsx.css'
import { useState } from "react";
import HtmlRequest from "../../Util/HtmlRequest";

function HandleConfirm(name) {
    // TODO: Update

    // check for errors from frontend
    // send request
    // if 200
        // redirect to home
        // show success snackbar
    // if not 200
        // give correct response / error

    const createAccountRequest = async() => {
        const response = await HtmlRequest(
            'POST',
            'https://api.localhost:6840/api/users/add/' + (name)
        )
    
        console.log(response)
        if (response.status == 200) {
            const data = await response.json()
            console.log(data)
        }
    }

    createAccountRequest()
}

function CreateAccount() {
    const [name, setName] = useState('')

    return (
        <CardPage header = "Create Account">
            <form className="w100">
                <h2>Name:</h2>
                <TextField label="Name" variant = "outlined" required fullWidth onChange={(event) => {
                    setName(event.target.value)
                }}/>
            </form>
            <div className="create-account-confirm-wrapper w100">
                <Button variant='contained' onClick = {() => HandleConfirm(name)}>Confirm</Button>
            </div>
        </CardPage>
    )
}

export default CreateAccount;