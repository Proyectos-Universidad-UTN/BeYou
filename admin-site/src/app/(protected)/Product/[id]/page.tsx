"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { Button } from "@mui/material";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { useSnackbar } from "@/stores/useSnackbar";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";

import { UseGetProductById } from "@/hooks/api-beyou/product/UseGetProductById";
import { UsePutProduct } from "@/hooks/api-beyou/product/UsePutProduct";

import { ProductForm } from "../components/ProductForm";
import { ProductFormType, initialProductValues } from "../components/ProductSchema";

export default function EditProductPage() {
  const router = useRouter();
  const params = useParams();
  const productIdRaw = params?.id;
  const productId = productIdRaw && !isNaN(Number(productIdRaw)) ? String(productIdRaw) : undefined;

  const { setMessage } = useSnackbar();

  const [loading, setLoading] = useState(false);
  const [openModalConfirmation, setOpenModalConfirmation] = useState(false);
  const [defaultValues, setDefaultValues] = useState<ProductFormType>(initialProductValues);

  const { data: productData, isLoading, isError, error } = UseGetProductById(productId);

  const putProduct = UsePutProduct({
    onSuccess: () => {
      setMessage("Producto actualizado con éxito", "success");
      router.push("/Product");
    },
    onError: (error) => {
      setMessage("Error al actualizar producto", "error");
      console.error(error);
      setLoading(false);
    },
  });

  useEffect(() => {
    if (isError) {
      setMessage(error?.message ?? "Error al cargar producto", "error");
      router.replace("/Product");
    }
  }, [isError, error, router, setMessage]);

  useEffect(() => {
    if (productData) {
      setDefaultValues({
        name: productData.name || "",
        brand: productData.brand || "",
        price: productData.price || 0,
        sku: productData.sku || "",
        categoryId: productData.categoryId || 1,
        unitMeasureId: productData.unitMeasureId || 1,
        active: productData.active ?? false,
        description: productData.description || "",
      });
    }
  }, [productData]);

  const handleSubmit = (data: ProductFormType) => {
    if (!productId) return;
    setLoading(true);
    putProduct.mutate({
      id: Number(productId),
      ...data,
    }, {
      onSettled: () => setLoading(false),
    });
  };

  if (isLoading) return <CircularLoadingProgress />;

  return (
    <Page
      header={
        <PageHeader
          title={`Editar Producto Nº ${productId}`}
          subtitle="Actualiza los datos del producto"
          backPath="/Product"
          backText="Productos"
          actionButton={
            <Button
              variant="contained"
              color="error"
              size="large"
              fullWidth
              onClick={() => setOpenModalConfirmation(true)}
            >
              Eliminar
            </Button>
          }
        />
      }
    >
      <ProductForm
        defaultValues={defaultValues}
        onSubmit={handleSubmit}
        isLoading={loading}
        isEdit={true}
      />

      {/* Aquí puedes agregar tu modal de confirmación para eliminar producto si lo tienes */}
      {/* <ProductDeleteModalConfirmation
          isModalOpen={openModalConfirmation}
          toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
          productId={Number(productId)}
          title={defaultValues.name}
      /> */}
    </Page>
  );
}
