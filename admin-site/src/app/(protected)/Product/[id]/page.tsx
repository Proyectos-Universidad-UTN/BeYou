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
import { ConvertToProductSchema, ProductFormType, initialProductValues } from "../components/ProductSchema";
import { ProductDeleteModalConfirmation } from "./ProductDeleteModalConfirmation";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { getErrorMessage } from "@/utils/util";

const EditProductPage = () => {
  const router = useRouter();
  const params = useParams();
  const productIdRaw = params?.id;
  const productId = productIdRaw && !isNaN(Number(productIdRaw)) ? String(productIdRaw) : undefined;

  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const [loading, setLoading] = useState(false);
  const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

  const closeLoading = () => setLoading(false);

  const { data, isLoading, isError, error } = UseGetProductById(productId);

  const { mutate: putProduct } = UsePutProduct(
    UseMutationCallbacks("Producto actualizado con éxito", "/Product", closeLoading)
  );

  useEffect(() => {
    if (isError) {
      setSnackbarMessage(getErrorMessage(error), "error");
      router.replace("/Product");
    }
  }, [isError, error, router, setSnackbarMessage]);

  const handleSubmit = (data: ProductFormType) => {
    if (!productId) return;
    setLoading(true);
    putProduct({
      id: Number(productId),
      ...data,
    });
  };

  if (isLoading) return <CircularLoadingProgress />;

  return (
    <Page
      header={
        <PageHeader
          title={`Editar Producto Nº ${data?.id}`}
          subtitle="Actualiza los datos del producto"
          backPath="/Product"
          backText="Productos"
          actionButton={
            <Button
              className="!bg-red-500 hover:bg-red-600"
              variant="contained"
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
        defaultValues={data ? ConvertToProductSchema(data) : initialProductValues}
        onSubmit={handleSubmit}
        isLoading={loading}
        isEdit
      />

      <ProductDeleteModalConfirmation
        isModalOpen={openModalConfirmation}
        toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
        productId={data?.id ?? 0}
        title={data?.name ?? ""}
      />
    </Page>
  );
};

export default EditProductPage;
