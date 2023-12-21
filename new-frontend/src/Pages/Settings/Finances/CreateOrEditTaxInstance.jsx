import { Button, InputAdornment, TextField } from "@mui/material";
import CardPage from "../../../Components/CardPage";

import '../Create.css'
import { useState } from "react";
import HtmlRequest from "../../../Util/HtmlRequest";
import { useLocation, useNavigate } from "react-router-dom";
import { DateTimePicker } from "@mui/x-date-pickers";

import './CreateOrEditTaxInstance.jsx.css'

function HandleConfirm(name, number, beginDate, endDate, state, redirect) {
    // TODO: Update

    // check for errors from frontend
    // send request
    // if 200
        // redirect to home
        // show success snackbar
    // if not 200
        // give correct response / error

    const createTaxTypeInstanceRequest = async() => {
        const response = await HtmlRequest(
            'POST',
            'https://api.localhost:6840/api/price/taxtypeinstance/' + (state.taxType.id),
            {
                Percentage: number,
                BeginTime: beginDate,
                EndTime: endDate
            }
        )
    
        console.log(response)
        const data = await response.json()
        console.log(data)
        if (response.status == 200) {
            redirect('/manage-taxes')
        }
    }

    const editTaxTypeInstanceRequest = async() => {
        const response = await HtmlRequest(
            'PUT',
            'https://api.localhost:6840/api/price/taxtypeinstance/edit/' + (state.instance.id),
            { 
                BeginTime: beginDate,
                EndTime: endDate
            }
        )
    
        console.log(response)
        const data = await response.json()
        console.log(data)
        if (response.status == 200) {
            redirect('/manage-taxes')

        }
    }

    if (state.instance == null) createTaxTypeInstanceRequest()
    else editTaxTypeInstanceRequest()
}

function CreateOrEditTaxTypeInstance() {
    const [name, setName] = useState('')
    const [percentage, setPercentage] = useState()
    const [create, setCreate] = useState(true)
    const [beginDate, setBeginDate] = useState()
    const [endDate, setEndDate] = useState()

    const navigate = useNavigate();

    const data = useLocation()
    console.log(data)
    console.log(create)
    useState(() => {
        if (data.state != null && data.state.instance != null && data.state.instance.id != null) {
            setCreate(false)
            setName(data.state.taxType.name)
            setPercentage(data.state.instance.percentage)
        }
    }, [])

    return (
        <CardPage header = {create ? "Create TaxTypeInstance" : "Edit TaxTypeInstance"}>
            <form className="w100 taxinstance-card">
                <h2>Percentage:</h2>
                <TextField label="Number" defaultValue={percentage} disabled = {!create} variant = "outlined" type="number" required fullWidth onChange={(event) => {
                    setPercentage(event.target.value)
                }}/>
                <div className="begindate">
                    <h2>Begin date:</h2>
                    <DateTimePicker className="w100i" onChange={(newValue) => setBeginDate(newValue)} />
                </div>
                <div className="enddate">
                    <h2>End date:</h2>
                    <DateTimePicker className="w100i" onChange={(newValue) => setEndDate(newValue)}/>
                </div>
            </form>
            <div className="create-confirm-wrapper w100">
                <Button variant='contained' onClick = {() => HandleConfirm(name, percentage, beginDate, endDate, data.state, navigate)}>Confirm</Button>
            </div>
        </CardPage>
    )
}

export default CreateOrEditTaxTypeInstance;