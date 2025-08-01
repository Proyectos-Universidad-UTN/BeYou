"use client";


import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CreateButton } from "@/components/Button/CreateButton";
import { UseGetCustomer } from "@/hooks/api-beyou/customer/UseGetCustomer";
import { CustomerTable } from "./CostumerTable";


export default function CustomerPage() {
  const { data, isLoading, error } = UseGetCustomer();

  return (
    <Page
      header={
        <PageHeader
          title="Lista de Clientes"
          actionButton={<CreateButton href="/Customer/new" />}
        />
      }
    >
      <CustomerTable
        customers={data ?? []}
        isLoading={isLoading}
        isError={!!error}
      />
    </Page>
  );
}
