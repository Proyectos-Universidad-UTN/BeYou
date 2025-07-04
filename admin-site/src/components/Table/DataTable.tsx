"use client";

import { isEmpty } from "lodash";
import { useState } from "react";
import { MobileDataTable } from "./MobileDataTable";
import {
  DataGrid,
  DataGridProps,
  GridRenderCellParams,
  GridSortModel,
  GridValidRowModel,
} from "@mui/x-data-grid";
import { getNestedField } from "@/utils/util";
import { NoDataIndicador } from "../NoDataIndicator/NoDataIndicador";
import { useIsMobile } from "@/hooks/UseIsMobile";

export const DataTable = <RowModelT extends GridValidRowModel>({
  sortFieldName,
  sort,
  noDataMessage = "No se encontró información",
  forceDesktopTableInMobile = false,
  emptyDataImageSource,
  ...props
}: DataGridProps<RowModelT> & {
  sortFieldName?: string;
  sort?: "asc" | "desc";
  noDataMessage?: string;
  forceDesktopTableInMobile?: boolean;
  emptyDataImageSource?: string;
}) => {
  const isMobile = useIsMobile();
  const {
    rows,
    columns,
    onRowClick,
    checkboxSelection,
    onRowSelectionModelChange,
    rowSelectionModel,
  } = props;

  const [sortModel, setSortModel] = useState<GridSortModel>([
    {
      field: sortFieldName ?? "name",
      sort: sort ?? "asc",
    },
  ]);

  if (isEmpty(rows)) {
    return (
      <NoDataIndicador
        noDataMessage={noDataMessage}
        imageSource={emptyDataImageSource}
      />
    );
  }

  // Si deseas que se vea versión móvil, puedes descomentar esto:
  // if (isMobile && !forceDesktopTableInMobile) {
  //   return (
  //     <MobileDataTable
  //       columns={columns}
  //       rows={rows}
  //       onRowClick={onRowClick}
  //       checkboxSelection={checkboxSelection}
  //       rowSelectionModel={rowSelectionModel}
  //       onRowSelectionModelChange={onRowSelectionModelChange}
  //     />
  //   );
  // }

  const modifiedColumns = columns.map((col) => {
    if (!col.renderCell) {
      return {
        ...col,
        renderCell: (params: GridRenderCellParams<RowModelT>) => {
          if (typeof col.field === "string" && col.field.includes(".")) {
            const value = getNestedField(params.row, col.field);
            return value ?? "No disponible";
          }
          return params.row[col.field];
        },
      };
    }

    return col;
  });

  return (
    <DataGrid<RowModelT>
      sx={{
        // Encabezados
        "& .MuiDataGrid-columnHeaders": {
          backgroundColor: "#f3f4f6",
          color: "#1f2937",
          fontWeight: 600,
          fontSize: "0.875rem",
          borderBottom: "1px solid #e5e7eb",
        },

        // Celdas
        "& .MuiDataGrid-cell": {
          color: "#374151",
          fontSize: "0.875rem",
          borderBottom: "1px solid #e5e7eb",
        },

        // Filas
        "& .MuiDataGrid-row": {
          backgroundColor: "#ffffff",
        },

        "& .MuiDataGrid-row:hover": {
          backgroundColor: "#f9fafb",
        },

        // Selección
        "& .MuiDataGrid-row.Mui-selected": {
          backgroundColor: "#e0f2fe",
        },

        // Footer
        "& .MuiDataGrid-footerContainer": {
          backgroundColor: "#f9fafb",
          color: "#4b5563",
          fontSize: "0.875rem",
          borderTop: "1px solid #e5e7eb",
        },

        // Checkboxes
        "& .MuiCheckbox-root": {
          color: "#6b7280",
        },

        "& .MuiTablePagination-root": {
          fontSize: "0.875rem",
          color: "#4b5563",
        },

        borderRadius: "0.75rem",
        border: "1px solid #e5e7eb",
        boxShadow: "0 1px 2px rgba(0, 0, 0, 0.05)",
      }}

      checkboxSelection={checkboxSelection}
      disableRowSelectionOnClick
      disableColumnMenu
      sortModel={sortModel}
      onSortModelChange={(model) => setSortModel(model)}
      initialState={{
        pagination: {
          paginationModel: { page: 0, pageSize: 25 },
        },
      }}
      pageSizeOptions={[25, 50, 100]}
      hideFooter={false} 
      {...props}
      columns={modifiedColumns}
    />
  );
};
