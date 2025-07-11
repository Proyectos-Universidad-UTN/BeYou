import { getDayInSpanish } from "@/utils/util";
import { BranchSchedule } from "@/types/api-beyou";
import { GridColDef, GridRenderCellParams } from "@mui/x-data-grid";

const BranchScheduleColumns: GridColDef[] = [
    { field: "id", headerName: "ID", flex: 0.5, minWidth: 100 },
    {
        field: "schedule.day",
        headerName: "Día",
        flex: 0.5,
        minWidth: 100,
        renderCell: (params: GridRenderCellParams<BranchSchedule>) => {
            return getDayInSpanish(params.row.schedule?.day)
        },
    },
    { field: "schedule.startHour", headerName: "Hora de inicio", flex: 0.5, minWidth: 100 },
    { field: "schedule.endHour", headerName: "Hora de fin", flex: 0.5, minWidth: 100 },
]

export default BranchScheduleColumns