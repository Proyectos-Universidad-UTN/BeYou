"use client";

import { Vendor } from "@/types/api-beyou";
import { useRouter } from "next/navigation";
import { GridEventListener, GridRowParams } from "@mui/x-data-grid";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import { VendorColumns } from "./VendorColumns";

interface VendorTableProps {
  vendors: Vendor[];
  isLoading: boolean;
  isError: boolean;
}

export const VendorTable = ({ vendors, isLoading, isError }: VendorTableProps) => {
  const router = useRouter();

  const selectRow: GridEventListener<"rowClick"> = (params: GridRowParams) => {
    router.push(`/Vendor/${params.id}`);
  };

  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={VendorColumns}
      data={vendors}
      loading={isLoading}
      error={isError}
      onRowClick={selectRow}
    />
  );
};
