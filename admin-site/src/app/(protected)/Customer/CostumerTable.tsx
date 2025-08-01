"use client";

import { Customer } from "@/types/api-beyou";
import { useRouter } from "next/navigation";
import { GridEventListener, GridRowParams } from "@mui/x-data-grid";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import { CustomerColumns } from "./CostumerColumns";


interface CustomerTableProps {
  customers: Customer[];
  isLoading: boolean;
  isError: boolean;
}

export const CustomerTable = ({ customers, isLoading, isError }: CustomerTableProps) => {
  const router = useRouter();

  const selectRow: GridEventListener<"rowClick"> = (params: GridRowParams) => {
    router.push(`/Customer/${params.id}`);
  };

  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={CustomerColumns}
      data={customers}
      loading={isLoading}
      error={isError}
      onRowClick={selectRow}
    />
  );
};
