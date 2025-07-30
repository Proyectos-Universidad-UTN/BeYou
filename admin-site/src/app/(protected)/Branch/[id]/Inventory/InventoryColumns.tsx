import { GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import { Inventory } from "@/types/api-beyou";
import { MenuActions } from "./MenuActionsInventory";


const InventoryColumns: GridColDef[] = [
  {
    field: "id",
    headerName: "ID",
    flex: 0.5,
    minWidth: 100,
  },
  {
    field: "branchId",
    headerName: "Sucursal",
    flex: 0.8,
    minWidth: 130,
  },
  {
    field: "name",
    headerName: "Nombre",
    flex: 1.2,
    minWidth: 160,
  },
  {
    field: "typeInventory",
    headerName: "Tipo",
    flex: 1,
    minWidth: 140,
  },
  {
    field: "active",
    headerName: "Activo",
    flex: 0.6,
    minWidth: 100,
    renderCell: (params: GridRenderCellParams<Inventory>) => (params.value ? "Sí" : "No"),
  },
  {
    field: "actions",
    headerName: "Acciones",
    sortable: false,
    filterable: false,
    width: 120,
    renderCell: (params: GridRenderCellParams<Inventory>) => <MenuActions id={params.row.id!} />,
  },
];

export default InventoryColumns;
