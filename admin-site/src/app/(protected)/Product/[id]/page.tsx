"use client";

import { useEffect, useState } from "react";
import { useRouter, usePathname } from "next/navigation";
import { UseGetProductById } from "@/hooks/api-beyou/product/UseGetProductById";
import { UsePutProduct } from "@/hooks/api-beyou/product/UsePutProduct";
import { useSnackbar } from "@/stores/useSnackbar";
import {
  ProductFormType,
  initialProductValues,
} from "../components/ProductSchema";
import { ProductForm } from "../components/ProductForm";

export default function EditProductPage() {
  const router = useRouter();
  const { setMessage } = useSnackbar();

  const pathname = usePathname();
  const idStr = pathname?.split("/").pop();  

  const [isLoading, setIsLoading] = useState(false);
  const [defaultValues, setDefaultValues] =
    useState<ProductFormType>(initialProductValues);

  const {
    data: productData,
    isLoading: loadingProduct,
    error,
  } = UseGetProductById(idStr);

  const putProduct = UsePutProduct({
    onSuccess: () => {
      setMessage("Producto actualizado con éxito", "success");
      router.push("/Product");
    },
    onError: (error) => {
      setMessage("Error al actualizar producto", "error");
      console.error(error);
    },
  });

  useEffect(() => {
    if (productData) {
      setDefaultValues({
        name: productData.name || "",
        brand: productData.brand || "",
        price: productData.price || 0,
        sku: productData.sku || "",
        categoryId: productData.categoryId || 1,
        unitMeasureId: productData.unitMeasureId || 1,
        active: productData.active || false,
        description: productData.description || "",
      });
    }
  }, [productData]);

  const onSubmit = (data: ProductFormType) => {
    if (!idStr) return; 

    setIsLoading(true);

    const payload = {
      id: Number(idStr),
      ...data,
    };

    putProduct.mutate(payload, {
      onSettled: () => setIsLoading(false),
    });
  };

  if (loadingProduct) return <p>Cargando producto...</p>;
  if (error) return <p>Error al cargar producto.</p>;

  return (
    <div className="p-4 max-w-4xl mx-auto">
      <h1 className="text-2xl font-semibold mb-6">Editar Producto</h1>
      <ProductForm
        onSubmit={onSubmit}
        defaultValues={defaultValues}
        isLoading={isLoading}
        isEdit={true}
      />
    </div>
  );
}
