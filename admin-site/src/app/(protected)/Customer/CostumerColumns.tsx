"use client";

import { GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import { Customer } from "@/types/api-beyou";
import { MenuActions } from "./MenuActions";

export const CustomerColumns: GridColDef<Customer>[] = [
  { field: "id", headerName: "ID", flex: 0.5, minWidth: 100 },
  { field: "firstName", headerName: "Nombre", flex: 1, minWidth: 150 },
  { field: "lastName", headerName: "Apellido", flex: 1, minWidth: 150 },
  { field: "telephone", headerName: "Teléfono", flex: 0.8, minWidth: 130 },
  { field: "email", headerName: "Correo Electrónico", flex: 1.2, minWidth: 200 },
  {
    field: "actions",
    headerName: "Acciones",
    sortable: false,
    filterable: false,
    width: 120,
    renderCell: (params: GridRenderCellParams<Customer>) => (
      <MenuActions
        id={params.row.id!}
        name={`${params.row.firstName ?? ""} ${params.row.lastName ?? ""}`}
      />
    ),
  },
];
