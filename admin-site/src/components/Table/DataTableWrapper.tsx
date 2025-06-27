"use client"

import { GridColDef, GridEventListener } from "@mui/x-data-grid";
import { DataTable } from "./DataTable";
import { SchemaData } from "@/types/api-beyou";
import { ErrorProcess } from "../Error/ErrorProcess";
import { CircularLoadingProgress } from "../LoadingProgress/CircularLoadingProcess";

interface DataTableWrapperProps {
    columns: GridColDef[];
    data: SchemaData[];
    loading?: boolean;
    error?: boolean;
    onRowClick?: GridEventListener<'rowClick'>;
    sortFieldName?: string;
    sort?: "desc" | "asc";
}

const DataTableWrapper = ({
    columns,
    data,
    loading = false,
    error = false,
    onRowClick,
    sortFieldName = "id",
    sort = "desc"
}: DataTableWrapperProps) => {
    if (loading) {
        return <CircularLoadingProgress />;
    }

    if (error) {
        return <ErrorProcess />;
    }

    return (
        <DataTable
            sortFieldName={sortFieldName}
            sort={sort}
            columns={columns}
            rows={data}
            onRowClick={onRowClick}
        />
    );
};

export default DataTableWrapper;
