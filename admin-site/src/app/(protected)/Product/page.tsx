"use client";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CreateButton } from "@/components/Button/CreateButton";
import { ProductTable } from "./ProductTable";
import { UseGetProduct } from "@/hooks/api-beyou/product/UseGetProduct";

export default function ProductPage() {
  const { data, isLoading, error } = UseGetProduct();

  return (
    <Page
      header={
        <PageHeader
          title="Lista de productos"
          actionButton={<CreateButton href="Product/new" />}
        />
      }
    >
      <ProductTable
        products={data ?? []}
        isLoading={isLoading}
        isError={!!error}
      />
    </Page>
  );
}
