"use client";

import { useState } from "react";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UsePostCustomer } from "@/hooks/api-beyou/customer/UsePostCustomer";
import { CustomerFormType, initialCustomerValues } from "../components/CustomerSchema";
import { CustomerForm } from "../components/CustomerForm";

const NewCustomerPage = () => {
  const [loading, setLoading] = useState(false);

  const closeLoading = () => setLoading(false);

  const { mutate: postCustomer } = UsePostCustomer(
    UseMutationCallbacks("Cliente creado exitosamente", "/Customer", closeLoading)
  );

  const handleSubmit = (data: CustomerFormType) => {
    setLoading(true);
    postCustomer({
      ...data,
    });
  };

  return (
    <Page
      header={
        <PageHeader
          title="Crear Cliente"
          subtitle="Agrega un nuevo cliente al sistema"
        />
      }
    >
      <CustomerForm
        defaultValues={initialCustomerValues}
        onSubmit={handleSubmit}
        isLoading={loading}
      />
    </Page>
  );
};

export default NewCustomerPage;
