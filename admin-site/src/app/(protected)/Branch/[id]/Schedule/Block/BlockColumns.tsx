import { GridColDef } from "@mui/x-data-grid";
import { BlockForm } from "./components/BlockSchema";

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
];

export default BlockColumns;
