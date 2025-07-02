"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";

import { UsePostProduct } from "@/hooks/api-beyou/product/UsePostProduct";
import { ProductFormType, initialProductValues } from "../components/ProductSchema";
import { useSnackbar } from "@/stores/useSnackbar";
import { ProductForm } from "../components/ProductForm";

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
    <div className="p-4 max-w-4xl mx-auto">
      <h1 className="text-2xl font-semibold mb-6">Crear nuevo producto</h1>
      <ProductForm
        onSubmit={onSubmit}
        defaultValues={initialProductValues}
        isLoading={isLoading}
      />
    </div>
  );
}
