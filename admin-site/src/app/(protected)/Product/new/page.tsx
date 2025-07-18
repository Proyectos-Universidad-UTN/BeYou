"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";

import { UsePostProduct } from "@/hooks/api-beyou/product/UsePostProduct";
import { ProductFormType, initialProductValues } from "../components/ProductSchema";
import { useSnackbar } from "@/stores/useSnackbar";
import { ProductForm } from "../components/ProductForm";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";

export default function NewProductPage() {
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const { setMessage } = useSnackbar();

  const postProduct = UsePostProduct({
    onSuccess: () => {
      setMessage("Producto creado exitosamente", "success");
      router.push("/Product");
    },
    onError: () => {
      setMessage("Error al crear producto", "error");
    },
  });

  const onSubmit = (data: ProductFormType) => {
    setIsLoading(true);
    postProduct.mutate(data, {
      onSettled: () => setIsLoading(false),
    });
  };

  return (
    <Page
      header={
        <PageHeader
          title="Crear Producto"
          subtitle="Agrega un nuevo producto"
        />
      }
    >
      <ProductForm
        onSubmit={onSubmit}
        defaultValues={initialProductValues}
        isLoading={isLoading}
      />
    </Page>
  );
}
