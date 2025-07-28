import { GridColDef, GridRenderCellParams } from "@mui/x-data-grid";

import { BranchScheduleBlock } from "@/types/api-beyou"; // Ajusta si es otro tipo
import { MenuActionsBlock } from "./MenuActionsBlock";

const BlockColumns: GridColDef[] = [
  { field: "id", headerName: "ID", flex: 0.5, minWidth: 100 },
  {
    field: "startHour",
    headerName: "Hora de inicio",
    flex: 0.5,
    minWidth: 100,
  },
  {
    field: "endHour",
    headerName: "Hora de fin",
    flex: 0.5,
    minWidth: 100,
  },
  {
    field: "actions",
    headerName: "Acciones",
    sortable: false,
    filterable: false,
    width: 120,
    renderCell: (params: GridRenderCellParams<BranchScheduleBlock>) => {
      return <MenuActionsBlock id={params.row.id!} />;
    },
  },
];

export default BlockColumns;
