import { useEffect, useState } from "react";
import CardPage from "../../../Components/CardPage";

import HtmlRequest from "../../../Util/HtmlRequest";
import { Fab, Switch, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";

import './ManagePriceTypes.jsx.css'
import { Add, Edit } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

function ManagePriceTypes() {
    const navigate = useNavigate()

    const [priceTypes, setPriceTypes] = useState([])

    const getPriceTypes = async() => {
        const response = await HtmlRequest(
            'GET',
            'https://api.localhost:6840/api/price/pricetype/'
        )
    
        if (response.status == 200) {
            const data = await response.json()
            setPriceTypes(data)
        }
    }

    useEffect(() => {
        getPriceTypes()
    }, [])

    function changeActive(priceType, event) {
        const changePriceTypeActiveRequest = async() => {
            const response = await HtmlRequest(
                'PUT',
                'https://api.localhost:6840/api/price/pricetype/setactive/' + (priceType.id),
                event.target.checked,
                null,
                true
            )
        
            if (response.status == 200) {
                const data = await response.json()
                console.log(data)
                let tempPriceTypes = priceTypes
                let index = tempPriceTypes.indexOf(tempPriceTypes.find(priceType => priceType.id == data.id))
                tempPriceTypes[index].active = data.active;
                setPriceTypes([...tempPriceTypes])
            }
        }
    
        changePriceTypeActiveRequest()
    }

    return (
        <CardPage header = "Manage PriceTypes">
            <TableContainer>
                <Table className="pricetype-table">
                    <TableHead>
                        <TableRow>
                            <TableCell className="table-name">Name</TableCell>
                            <TableCell className="table-active">Active</TableCell>
                            <TableCell className="table-edit curp">Edit</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                    {priceTypes.map(priceType => {
                        return <TableRow className="table-content" key={priceType.id}>
                            <TableCell className="table-name">
                            {priceType.name}
                            </TableCell>
                            <TableCell className="table-active">
                                <Switch checked={priceType.active} onChange={(e) => changeActive(priceType, e)} />
                            </TableCell>
                            <TableCell className="table-edit curp">
                                <Edit color="secondary" onClick={() => navigate("/edit-pricetype", {state: {...priceType}})} />
                            </TableCell>
                        </TableRow>
                    })}
                    </TableBody>
                </Table>
            </TableContainer>
            <Fab color="primary" className="add-pricetype" onClick={() => navigate("/edit-pricetype")}>
                <Add />
            </Fab>
        </CardPage>
    )
}

export default ManagePriceTypes;