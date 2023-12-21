import { useEffect, useState } from "react";
import CardPage from "../../Components/CardPage";

import HtmlRequest from "../../Util/HtmlRequest";
import { Fab, Switch, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";

import './ManageItemCategories.jsx.css'
import { Add, Edit } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import { DndContext } from "@dnd-kit/core";
import { SortableContext, arrayMove } from "@dnd-kit/sortable";
import ManageItemCategoriesRow from "../../Components/ManageItemCategoriesRow";

import useIcons from "../../Util/useIcon";

function useIcon(icon) {
    const DynIcon = useIcons(icon)
    return (
        <DynIcon />
    )
}

function ManageItemCategories() {
    const navigate = useNavigate()

    const [itemCategories, setItemCategories] = useState([])

    const getItemCategories = async() => {
        const response = await HtmlRequest(
            'GET',
            'https://api.localhost:6840/api/itemcategory'
        )
    
        const data = await response.json()
        console.log(data)
        if (response.status == 200) {
            setItemCategories(data)
        }
    }

    useEffect(() => {
        getItemCategories()
    }, [])

    function changeActive(itemCategory, event) {
        const changeItemCategoryActiveRequest = async() => {
            const response = await HtmlRequest(
                'PUT',
                'https://api.localhost:6840/api/itemcategory/setactive/' + (itemCategory.id),
                event.target.checked,
                null,
                true
            )
        
            if (response.status == 200) {
                const data = await response.json()
                console.log(data)
                let tempItemCategories = itemCategories
                let index = tempItemCategories.indexOf(tempItemCategories.find(itemCategory => itemCategory.id == data.id))
                tempItemCategories[index].active = data.active;
                setItemCategories([...tempItemCategories])
            }
        }
    
        changeItemCategoryActiveRequest()
    }

    function updateOrder(order) {
        const changeItemCategoryOrderRequest = async() => {
            const response = await HtmlRequest(
                'PUT',
                'https://api.localhost:6840/api/itemcategory/setorder',
                order
            )
        
            if (response.status == 200) {
            }
        }
    
        changeItemCategoryOrderRequest()
    }

    const onDragEnd = (event) => {
        console.log(event)
        const {active, over} = event
        if (over == null || active.id === over.id) return
        const oldIndex = itemCategories.findIndex(itemCategory => itemCategory.id == active.id)
        const newIndex = itemCategories.findIndex(itemCategory => itemCategory.id == over.id)
        setItemCategories(itemCategories => {
            return arrayMove(itemCategories, oldIndex, newIndex)
        })

        updateOrder(arrayMove(itemCategories, oldIndex, newIndex))
    }

    return (
        <CardPage header = "Manage Item Categories">
            <TableContainer>
                <Table className="itemcategory-table">
                    <TableHead>
                        <TableRow>
                            <TableCell className="table-name">Name</TableCell>
                            <TableCell className="table-icon">Icon</TableCell>
                            <TableCell className="table-active">Active</TableCell>
                            <TableCell className="table-edit">Edit</TableCell>
                        </TableRow>
                    </TableHead>
                </Table>
            </TableContainer>
                    <DndContext onDragEnd={onDragEnd}>
                        <SortableContext items = {itemCategories}>
                            {itemCategories.map(itemCategory => {
                                return <ManageItemCategoriesRow itemCategory={itemCategory} key={itemCategory.id} />
                            })}
                        </SortableContext>
                    </DndContext>

                    <TableContainer>
                {/* <Table className="itemcategory-table">
                    <TableBody>

                    {itemCategories.map(itemCategory => {
                        return <TableRow className="table-content" key={itemCategory.id}>
                            <TableCell className="table-name">
                            {itemCategory.name}
                            </TableCell>
                            <TableCell className="table-active">
                                <Switch checked={itemCategory.active} onChange={(e) => changeActive(itemCategory, e)} />
                            </TableCell>
                            <TableCell className="table-edit curp">
                                <Edit color="secondary" onClick={() => navigate("/edit-itemcategory", {state: {...itemCategory}})} />
                            </TableCell>
                        </TableRow>
                    })}
                    </TableBody>
                </Table> */}
            </TableContainer>
            <Fab color="primary" className="add-itemcategory" onClick={() => navigate("/edit-itemcategory")}>
                <Add />
            </Fab>
        </CardPage>
    )
}

export default ManageItemCategories;