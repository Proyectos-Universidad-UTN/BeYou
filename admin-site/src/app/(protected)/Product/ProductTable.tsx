"use client";

import { Branch, Product } from "@/types/api-beyou";
import { useRouter } from "next/navigation";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import { GridEventListener, GridRowParams } from "@mui/x-data-grid";
import ProductColumns from "./ProductColumns";

interface ProductTableProps {
  products: Product[];
  isLoading: boolean;
  isError: boolean;
}

export const ProductTable = ({ products, isLoading, isError }: ProductTableProps) => {
  const router = useRouter()

  const selectRow: GridEventListener<'rowClick'> = (params: GridRowParams) => {
    router.push(`/Product/${params.id}`)
  }

  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={ProductColumns}
      data={products}
      loading={isLoading}
      error={isError}
      onRowClick={selectRow}
    />
  );
};
