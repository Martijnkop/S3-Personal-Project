import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material"
import HtmlRequest from "../Util/HtmlRequest"
import { useEffect, useState } from "react"

function FinancialOverviewPage() {
    const [taxTypes, setTaxTypes] = useState([])
    const [orders, setOrders] = useState([])

    const totalPrice = () => {
        let sum = 0;
        orders.forEach(order => {
            order.orderedItems.forEach(orderedItem => {
                sum += orderedItem.item.activePrice.amount * orderedItem.amount
            })
        })
        return <div>
            Totale transacties:
            <br />€{sum}
        </div>;
    }

    function sum() {
        let sum = 0;
        orders.forEach(order => {
            order.orderedItems.forEach(orderedItem => {
                let percentage = orderedItem.item.activeInstance.percentage / 100
                sum += orderedItem.item.activePrice.amount * orderedItem.amount / (1 + percentage)
            })
        })
        return <TableCell>{sum}</TableCell>
    }

    function sum2() {
        let sum = 0;
        orders.forEach(order => {
            order.orderedItems.forEach(orderedItem => {
                let percentage = orderedItem.item.activeInstance.percentage / 100
                sum += orderedItem.item.activePrice.amount * orderedItem.amount / (1 + percentage) * percentage
            })
        })
        return <TableCell>{sum}</TableCell>
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

    const getOrders = async() => {
        const response = await HtmlRequest(
            'GET',
            'https://api.localhost:6840/api/order'
        )
    
        if (response.status == 200) {
            const data = await response.json()
            console.log(data)
            setOrders(data)
        }
    }

    useEffect(() => {
        getTaxTypes()
        getOrders()
    },[])

    return (
        <div>
            
            {totalPrice()}
            <div>
                BTW uitsplitsing:
                <br />
                <TableContainer>
                    <Table className="itemcategory-table">
                        <TableHead>
                            <TableRow>
                                <TableCell />
                                {taxTypes && taxTypes.map(taxType => {
                                    return <TableCell key={taxType.id}>{taxType.name}</TableCell>
                                })}
                                <TableCell>Totaal</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            <TableRow>
                                <TableCell>Exclusief BTW</TableCell>
                                {taxTypes && taxTypes.map(taxType => {
                                    let sum = 0;
                                    orders.forEach(order => {
                                        order.orderedItems.forEach(orderedItem => {
                                            if (orderedItem.item.taxType.id == taxType.id) {
                                                let percentage = orderedItem.item.activeInstance.percentage / 100
                                                sum += orderedItem.item.activePrice.amount * orderedItem.amount / (1 + percentage)
                                            }
                                        })
                                    })
                                    return <TableCell key={taxType.id}>{sum}</TableCell>
                                })}
                                {sum()}
                            </TableRow>
                            <TableRow>
                                <TableCell>BTW</TableCell>
                                {taxTypes && taxTypes.map(taxType => {
                                    let sum = 0;
                                    orders.forEach(order => {
                                        order.orderedItems.forEach(orderedItem => {
                                            if (orderedItem.item.taxType.id == taxType.id) {
                                                let percentage = orderedItem.item.activeInstance.percentage / 100
                                                sum += orderedItem.item.activePrice.amount * orderedItem.amount / (1 + percentage) * percentage
                                            }
                                        })
                                    })
                                    return <TableCell key={taxType.id}>{sum}</TableCell>
                                })}
                                {sum2()}
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </div>
        </div>
    )
}

export default FinancialOverviewPage