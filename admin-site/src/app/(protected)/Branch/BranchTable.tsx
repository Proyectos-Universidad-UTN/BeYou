"use client";

import { Branch } from "@/types/api-beyou";
import { useRouter } from "next/navigation";
import BranchColumns from "./BranchColumns";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import { GridEventListener, GridRowParams } from "@mui/x-data-grid";

interface BranchTableProps {
  branches: Branch[];
  isLoading: boolean;
  isError: boolean;
}

export const BranchTable = ({ branches, isLoading, isError }: BranchTableProps) => {
  const router = useRouter()

  const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
    router.push(`/Branch/${params.id}`)
  }

  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={BranchColumns}
      data={branches}
      loading={isLoading}
      error={isError}
      onRowClick={selectRow}
    />
  );
};
