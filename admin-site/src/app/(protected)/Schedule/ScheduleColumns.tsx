import { GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import { Schedule } from "@/types/api-beyou";
import { getDayInSpanish } from "@/utils/util";
import { MenuActions } from "./MenuActions";


const ScheduleColumns: GridColDef[] = [
  {
    field: "id",
    headerName: "ID",
    flex: 0.5,
    minWidth: 100,
  },
  {
    field: "day",
    headerName: "Día",
    flex: 1,
    minWidth: 150,
    renderCell: (params: GridRenderCellParams<Schedule>) => getDayInSpanish(params.row.day),
  },
  {
    field: "startHour",
    headerName: "Hora de inicio",
    flex: 1,
    minWidth: 150,
  },
  {
    field: "endHour",
    headerName: "Hora de fin",
    flex: 1,
    minWidth: 150,
  },
  {
    field: "actions",
    headerName: "Acciones",
    sortable: false,
    filterable: false,
    width: 120,
    renderCell: (params: GridRenderCellParams<Schedule>) => <MenuActions id={params.row.id!} />,
  },
];

export default ScheduleColumns;
