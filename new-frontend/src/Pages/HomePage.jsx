import { Autocomplete, Button, Card, TextField, Divider } from '@mui/material';

import { useEffect, useState } from 'react'
import CardPage from '../Components/CardPage';
import HtmlRequest from '../Util/HtmlRequest';
import { useNavigate } from 'react-router-dom';



function HomePage() {
    const navigate = useNavigate();
    
    const [inputValue, setInputValue] = useState('')
    const [options, setList] = useState([{name: ""}])
    const [value, setValue] = useState()

    const fetchInput = async() => {
        let uri = 'https://api.localhost:6840/api/users'
        if (inputValue != undefined && inputValue != null) uri += `/${inputValue}` 
        const response = await HtmlRequest(
            'GET',
            uri
        )
    
        if (response.status == 200) {
            const data = await response.json()
            setList(data)
        }
    }

    function HandleSearch() {
        console.log(value)
        navigate('/products', {state: {id: value.id}})
    }

    useEffect(() => {
        if (!inputValue.includes("/") && value == undefined) fetchInput()
    }, [inputValue])

    return (
        <CardPage header={"Choose account:"}>
            <section className='name-select w100'>
                <Autocomplete
                className='input w100'
                onChange={(event, newValue) => {
                    setValue(newValue);
                }}
                inputValue={inputValue}
                options={options}
                onInputChange={(event, newInputValue) => {
                    setInputValue(newInputValue);
                }}
                getOptionLabel={(option) => option.name}
                getOptionKey={(option) => option.id}
                renderInput={(params) => <TextField {...params} variant="outlined" label="Insert name" autoComplete='none' />}
                />
                <Button variant='contained' onClick={() => HandleSearch()}>Continue</Button>
                <Button variant='contained'>Own account</Button>

            </section>

            <Divider className='divider' />
            <section className='shortcuts w100'>
                <h2 className='header-shortcuts'>Shortcuts:</h2>

                <Button variant='contained'>asdf</Button>
                <Button variant='contained'>asdf</Button>
                <Button variant='contained'>asdf</Button>
                <Button variant='contained'>aasdfasdfadfasdfasdfsdf</Button>
                <Button variant='contained'>asdf</Button>
                <Button variant='contained'>asdf</Button>
                <Button variant='contained'>asdf</Button>
                <Button variant='contained'>asdf</Button>
                <Button variant='contained'>asdf</Button>
                
                <Button variant='contained'>aasdfasdfadfasdfasdfsdf</Button>
                <Button variant='contained'>aasdfasdfadfasdfasdfsdf</Button>
            </section>
        </CardPage>
    )
}

export default HomePage