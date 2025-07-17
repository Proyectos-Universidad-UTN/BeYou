"use client";

import { VendorTable } from "./VendorTable";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CreateButton } from "@/components/Button/CreateButton";
import { UseGetVendor } from "@/hooks/api-beyou/vendor/UseGetVendor";

export default function VendorPage() {
  const { data, isLoading, error } = UseGetVendor();

  return (
    <Page
      header={
        <PageHeader
          title="Lista de Proveedores"
          actionButton={<CreateButton href="/Vendor/new" />}
        />
      }
    >
      <VendorTable
        vendors={data ?? []}
        isLoading={isLoading}
        isError={!!error}
      />
    </Page>
  );
}
