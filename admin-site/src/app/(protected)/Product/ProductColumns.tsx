import { GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import { Product } from "@/types/api-beyou";
import { ProductMenuActions } from "./MenuActions";

const ProductColumns: GridColDef[] = [
  { field: "id", headerName: "ID", flex: 0.5, minWidth: 100 },
  { field: "name", headerName: "Nombre", flex: 1.2, minWidth: 150 },
  { field: "description", headerName: "Descripción", flex: 1.5, minWidth: 180 },
  {
    field: "category",
    headerName: "Categoría",
    flex: 1,
    minWidth: 140,
    renderCell: (params: GridRenderCellParams<Product>) => (
      <span>{params.row.category?.name || "—"}</span>
    ),
  },
  //   {
  //     field: "unitOfMeasure",
  //     headerName: "Unidad",
  //     flex: 1,
  //     minWidth: 140,
  //     valueGetter: (params) => params.row.unitOfMeasure?.name || "—",
  //   },
  {
    field: "price",
    headerName: "Precio",
    flex: 0.8,
    minWidth: 120,
    valueFormatter: (params: { value: number | undefined }) =>
      params.value !== undefined ? `₡ ${params.value.toFixed(2)}` : "—",
  },
  {
    field: "actions",
    headerName: "Acciones",
    sortable: false,
    filterable: false,
    width: 120,
    renderCell: (params: GridRenderCellParams<Product>) => {
      return <ProductMenuActions id={params.row.id!} />;
    },
  },
];

export default ProductColumns;
