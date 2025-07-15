import { GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import { MenuActions } from "./MenuActions";
import { Branch } from "@/types/api-beyou";

const BranchColumns: GridColDef[] = [
    { field: "id", headerName: "ID", flex: 0.5, minWidth: 100 },
    { field: "name", headerName: "Nombre", flex: 1, minWidth: 150 },
    { field: "description", headerName: "Descripción", flex: 1.2, minWidth: 180 },
    { field: "telephone", headerName: "Teléfono", flex: 0.8, minWidth: 130 },
    { field: "email", headerName: "Correo", flex: 1.2, minWidth: 200 },
    {
        field: "actions",
        headerName: "Acciones",
        sortable: false,
        filterable: false,
        width: 120,
        renderCell: (params: GridRenderCellParams<Branch>) => {
            return (
                <MenuActions id={params.row.id!} />
            )
        }
    }
]

export default BranchColumns