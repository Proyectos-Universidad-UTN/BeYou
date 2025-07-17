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
import { UseGetVendorById } from "@/hooks/api-beyou/vendor/UseGetVendorById";
import { UsePutVendor } from "@/hooks/api-beyou/vendor/UsePutVendor";
import { VendorForm } from "../components/VendorForm";
import {
  VendorFormType,
  VendorSchema,
  initialVendorValues,
} from "../components/VendorSchema";
import { VendorDeleteModalConfirmation } from "./VendorDeleteModalConfirmation";


const EditVendorPage = () => {
  const router = useRouter();
  const params = useParams();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);
  const [loading, setLoading] = useState(false);
  const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

  const vendorIdRaw = params?.id;
  const vendorId = vendorIdRaw && !isNaN(Number(vendorIdRaw)) ? String(vendorIdRaw) : undefined;

  const closeLoading = () => setLoading(false);

  const {
    data: vendorData,
    isLoading,
    isError,
    error,
  } = UseGetVendorById(vendorId);

  const { mutate: putVendor } = UsePutVendor(
    UseMutationCallbacks("Proveedor actualizado exitosamente", "/Vendor", closeLoading)
  );

  useEffect(() => {
    if (isError) {
      setSnackbarMessage(getErrorMessage(error), "error");
      router.replace("/Vendor");
    }
  }, [isError, error, setSnackbarMessage, router]);

  const handleSubmit = (data: VendorFormType) => {
    setLoading(true);
    putVendor({
      ...data,
      id: Number(vendorId),
      telephone: removePhoneMask(data.telephone),
    });
  };

  if (isLoading) return <CircularLoadingProgress />;

  return (
    <Page
      header={
        <PageHeader
          title={`Editar Proveedor Nº ${vendorData?.id}`}
          subtitle="Actualiza los datos del proveedor"
          backPath="/Vendor"
          backText="Proveedores"
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
      <VendorForm
        defaultValues={{
          name: vendorData?.name || "",
          fiscalNumber: vendorData?.fiscalNumber || "",
          socialReason: vendorData?.socialReason || "",
          telephone: String(vendorData?.telephone || ""),
          email: vendorData?.email || "",
          districtId: vendorData?.districtId || 0,
          address: vendorData?.address || "",
        }}
        onSubmit={handleSubmit}
        isLoading={loading}
        isEdit={true}
      />

      <VendorDeleteModalConfirmation
        isModalOpen={openModalConfirmation}
        toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
        vendorId={vendorData?.id ?? 0}
        title={vendorData?.name ?? ""}
      />
    </Page>
  );
};

export default EditVendorPage;
