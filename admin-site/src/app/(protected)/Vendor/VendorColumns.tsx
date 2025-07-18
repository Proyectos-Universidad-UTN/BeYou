import { GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import { Vendor } from "@/types/api-beyou";
import { MenuActions } from "./MenuActions";

export const VendorColumns: GridColDef<Vendor>[] = [
  { field: "id", headerName: "ID", flex: 0.5, minWidth: 100 },
  { field: "name", headerName: "Nombre Comercial", flex: 1, minWidth: 150 },
  { field: "socialReason", headerName: "Razón Social", flex: 1.2, minWidth: 180 },
  { field: "fiscalNumber", headerName: "Número Fiscal", flex: 1, minWidth: 150 },
  { field: "telephone", headerName: "Teléfono", flex: 0.8, minWidth: 130 },
  { field: "email", headerName: "Correo Electrónico", flex: 1.2, minWidth: 200 },
  {
    field: "actions",
    headerName: "Acciones",
    sortable: false,
    filterable: false,
    width: 120,
    renderCell: (params: GridRenderCellParams<Vendor>) => (
      <MenuActions id={params.row.id!} name={params.row.name!} />
    ),
  },
];
