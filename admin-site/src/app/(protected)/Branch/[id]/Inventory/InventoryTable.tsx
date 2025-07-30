"use client";

import { Inventory } from "@/types/api-beyou";
import { useRouter } from "next/navigation";
import InventoryColumns from "./InventoryColumns";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import { GridEventListener, GridRowParams } from "@mui/x-data-grid";

interface InventoryTableProps {
  inventories: Inventory[];
  isLoading: boolean;
  isError: boolean;
}

export const InventoryTable = ({ inventories, isLoading, isError }: InventoryTableProps) => {
  const router = useRouter();

  const selectRow: GridEventListener<"rowClick"> = (params: GridRowParams) => {
    console.log("Click en inventario con ID:", params.id);
  };

  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={InventoryColumns}
      data={inventories}
      loading={isLoading}
      error={isError}
      onRowClick={selectRow}
    />
  );
};
