import { useEffect, useState } from "react";
import CardPage from "../../../Components/CardPage";

import HtmlRequest from "../../../Util/HtmlRequest";
import { Fab, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";

import './ManageTaxes.jsx.css'
import { Add } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import ManageTaxesRow from "../../../Components/ManageTaxesRow";

function ManageTaxes() {
    const navigate = useNavigate()

    const [taxTypes, setTaxTypes] = useState([])

    const getTaxTypes = async() => {
        const response = await HtmlRequest(
            'GET',
            'https://api.localhost:6840/api/price/taxtype/'
        )
    
        if (response.status == 200) {
            const data = await response.json()
            setTaxTypes(data)
        }
    }

    useEffect(() => {
        getTaxTypes()
    }, [])

    function changeActive(taxType, event) {
        const changeTaxTypeActiveRequest = async() => {
            const response = await HtmlRequest(
                'PUT',
                'https://api.localhost:6840/api/price/taxtype/setactive/' + (taxType.id),
                event.target.checked,
                null,
                true
            )
        
            if (response.status == 200) {
                const data = await response.json()
                console.log(data)
                let tempTaxTypes = taxTypes
                let index = tempTaxTypes.indexOf(tempTaxTypes.find(taxType => taxType.id == data.id))
                tempTaxTypes[index].active = data.active;
                setTaxTypes([...tempTaxTypes])
            }
        }
    
        changeTaxTypeActiveRequest()
    }

    return (
        <CardPage header = "Manage Tax types">
            <div className="table-wrapper w100">
            <TableContainer>
                <Table className="taxtype-table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell className="table-name">Name</TableCell>
                            <TableCell />
                            <TableCell />
                        </TableRow>
                    </TableHead>
                    <TableBody>
                    {taxTypes.map(taxType => {
                        return <ManageTaxesRow taxType = {taxType}  key={taxType.id} navigate = {navigate} />
                    })}
                    </TableBody>
                </Table>
            </TableContainer>
            </div>
            <Fab color="primary" className="add-taxtype" onClick={() => navigate("/edit-tax")}>
                <Add />
            </Fab>
        </CardPage>
    )
}

export default ManageTaxes;