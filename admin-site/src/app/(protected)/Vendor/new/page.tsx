"use client";

import { useState } from "react";
import { removePhoneMask } from "@/utils/util";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { VendorForm } from "../components/VendorForm";
import { initialVendorValues, VendorFormType } from "../components/VendorSchema";
import { UsePostVendor } from "@/hooks/api-beyou/vendor/UsePostVendor";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";

const NewVendorPage = () => {
  const [loading, setLoading] = useState(false);

  const closeLoading = () => setLoading(false);

  const { mutate: postVendor } = UsePostVendor(
    UseMutationCallbacks("Proveedor creado exitosamente", "/Vendor", closeLoading)
  );

  const handleSubmit = (data: VendorFormType) => {
    setLoading(true);
    postVendor({
      ...data,
      telephone: removePhoneMask(data.telephone),
    });
  };

  return (
    <Page
      header={
        <PageHeader
          title="Crear Proveedor"
          subtitle="Agrega un nuevo proveedor al sistema"
        />
      }
    >
      <VendorForm
        defaultValues={initialVendorValues}
        onSubmit={handleSubmit}
        isLoading={loading}
      />
    </Page>
  );
};

export default NewVendorPage;
