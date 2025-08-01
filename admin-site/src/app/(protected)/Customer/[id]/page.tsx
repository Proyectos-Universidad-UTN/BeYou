"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { Button } from "@mui/material";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { removePhoneMask, getErrorMessage } from "@/utils/util";
import { useSnackbar } from "@/stores/useSnackbar";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UseGetCustomerById } from "@/hooks/api-beyou/customer/UseGetCustomerById";
import { UsePutCustomer } from "@/hooks/api-beyou/customer/UsePutCustomer";
import { CustomerFormType } from "../components/CustomerSchema";
import { CustomerForm } from "../components/CustomerForm";
import { CustomerDeleteModalConfirmation } from "./CustomerDeleteModalConfirmation";

const EditCostumerPage = () => {
  const router = useRouter();
  const params = useParams();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);
  const [loading, setLoading] = useState(false);
  const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

  const customerIdRaw = params?.id;
  const customerId =
    customerIdRaw && !isNaN(Number(customerIdRaw))
      ? String(customerIdRaw)
      : undefined;

  const closeLoading = () => setLoading(false);

  const {
    data: customerData,
    isLoading,
    isError,
    error,
  } = UseGetCustomerById(customerId);

  const { mutate: putCustomer } = UsePutCustomer(
    UseMutationCallbacks(
      "Cliente actualizado exitosamente",
      "/Customer",
      closeLoading
    )
  );

  useEffect(() => {
    if (isError) {
      setSnackbarMessage(getErrorMessage(error), "error");
      router.replace("/Customer");
    }
  }, [isError, error, setSnackbarMessage, router]);

  const handleSubmit = (data: CustomerFormType) => {
    setLoading(true);
    putCustomer({
      ...data,
      id: Number(customerId),
      telephone: removePhoneMask(data.telephone),
    });
  };

  if (isLoading) return <CircularLoadingProgress />;

  return (
    <Page
      header={
        <PageHeader
          title={`Editar Cliente Nº ${customerData?.id}`}
          subtitle="Actualiza los datos del cliente"
          backPath="/Customer"
          backText="Clientes"
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
      <CustomerForm
        defaultValues={{
          firstName: customerData?.firstName || "",
          lastName: customerData?.lastName || "",
          telephone: String(customerData?.telephone || ""),
          email: customerData?.email || "",
          districtId: customerData?.districtId || 0,
          address: customerData?.address || "",
        }}
        onSubmit={handleSubmit}
        isLoading={loading}
        isEdit={true}
      />

      <CustomerDeleteModalConfirmation
        isModalOpen={openModalConfirmation}
        toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
        customerId={customerData?.id ?? 0}
        title={customerData?.firstName ?? ""}
      />
    </Page>
  );
};

export default EditCostumerPage;
