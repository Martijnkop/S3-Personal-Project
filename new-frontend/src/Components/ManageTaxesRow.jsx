import { Add, Edit, KeyboardArrowDown, KeyboardArrowUp } from "@mui/icons-material"
import { Collapse, Divider, IconButton, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material"
import { useState } from "react"

import './ManageTaxesRow.jsx.css'

function ManageTaxesRow({taxType, navigate}) {
    const [open, setOpen] = useState(false)
return (
    <>
    <TableRow className="table-content" >
        <TableCell>
            <IconButton
                aria-label="expand row"
                size="small"
                onClick={() => setOpen(!open)}
            >
                {open ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
            </IconButton>
        </TableCell>
        <TableCell className="table-name">
            {taxType.name}
        </TableCell>
        <TableCell />
        <TableCell className="table-edit curp">
            <Edit color="primary" onClick={() => navigate("/edit-tax", {state: {...taxType}})} />
        </TableCell>
    </TableRow>
    {open && <TableRow>
        <TableCell colSpan={4} >
            <Collapse in={open} unmountOnExit>
                <div className="taxtype-instance-header">
                    <h4>Instances:</h4>
                    <Add color="secondary" className="curp"  onClick={() => navigate("/edit-tax-instance" , {state: {taxType}})}  />
                </div>
                <Divider />
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Percentage</TableCell>
                            <TableCell>Begin Time</TableCell>
                            <TableCell>End Time</TableCell>
                            <TableCell>Edit</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {taxType.instances.map(instance => {
                            console.log(instance)
                            return (
                                <TableRow key={instance.id}>
                                    <TableCell>{instance.percentage} %</TableCell>
                                    <TableCell>{instance.beginTime ?? "-"}</TableCell>
                                    <TableCell>{instance.endTime ?? "-"}</TableCell>
                                    <TableCell><Edit className="curp" color="secondary" onClick={() => navigate("/edit-tax-instance", {state: {taxType, instance}})} /></TableCell>
                                </TableRow>
                            )
                        })}
                    </TableBody>
                </Table>
            </Collapse>
        </TableCell>
    </TableRow>
    }
    </>
)
}

export default ManageTaxesRow