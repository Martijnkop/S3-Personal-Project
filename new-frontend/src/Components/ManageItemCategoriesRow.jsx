import { useSortable } from "@dnd-kit/sortable";
import { Edit } from "@mui/icons-material";
import { Switch, Table, TableBody, TableCell, TableRow } from "@mui/material";
import {CSS} from '@dnd-kit/utilities';

function ManageItemCategoriesRow({itemCategory}) {
    const {
        attributes,
        listeners,
        setNodeRef,
        transform,
        transition,
      } = useSortable({id: itemCategory.id});

      const style = {
        transform: CSS.Transform.toString(transform),
        transition,
      };

      return (
        <div ref={setNodeRef} {...attributes} {...listeners} style={style} className="w100">
            <Table className="itemcategory-table">
                    <TableBody>
                        <TableRow className="table-content" key={itemCategory.id}>
                            <TableCell className="table-name">
                            {itemCategory.name}
                            </TableCell>
                            <TableCell className="table-active">
                            </TableCell>
                            <TableCell className="table-active">
                                <Switch checked={itemCategory.active} onChange={(e) => changeActive(itemCategory, e)} />
                            </TableCell>
                            <TableCell className="table-edit curp">
                                <Edit color="secondary" onClick={() => navigate("/edit-itemcategory", {state: {...itemCategory}})} />
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
        </div>
      )
}

export default ManageItemCategoriesRow