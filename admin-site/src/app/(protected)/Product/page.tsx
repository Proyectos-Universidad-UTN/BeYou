"use client";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { ProductTable } from "./ProductTable";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { ProductTableItem } from "./ProductTable";
import { UseGetProduct } from "@/hooks/api-beyou/product/UseGetProduct";

export default function ProductPage() {
  const { data, isLoading, error } = UseGetProduct();

  const products: ProductTableItem[] = (data ?? [])
  .filter((product) => product.id !== undefined)
  .map((product) => ({
    id: product.id!,
    name: product.name ?? "",                      // asegurado como string
    description: product.description ?? "",        // string
    brand: product.brand ?? "",                    // string
    price: product.price ?? 0,                     // number
    sku: product.sku ?? "",                        // string
    categoryId: product.categoryId ?? 0,           // number
    unitMeasureId: product.unitMeasureId ?? 0,     // number
    active: product.active ?? false,               // boolean
  }));


  return (
    <Page header={<PageHeader title="Productos" />}>
      {isLoading ? (
        <CircularLoadingProgress />
      ) : error ? (
        <p>Error al cargar productos</p>
      ) : products.length === 0 ? (
        <p>No se encontró información</p>
      ) : (
        <ProductTable products={products} />
      )}
    </Page>
  );
}
