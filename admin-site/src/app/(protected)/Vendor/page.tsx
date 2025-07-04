"use client";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { UseGetVendor } from "@/hooks/api-beyou/vendor/UseGetVendor";
import { VendorTable } from "./VendorTable";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { type VendorTableItem } from "./VendorTable";

export default function VendorPage() {
  const { data, isLoading, error } = UseGetVendor();

  const vendors: VendorTableItem[] = (data ?? [])
    .filter((vendor) => vendor.id !== undefined)
    .map((vendor) => ({
      id: vendor.id!,
      name: vendor.name ?? "",
      socialReason: vendor.socialReason ?? "",
      fiscalNumber: vendor.fiscalNumber ?? "", 
      telephone: vendor.telephone ?? 0,
      email: vendor.email ?? "",
      districtId: vendor.districtId ?? 0,
    }));

  return (
    <Page >
      {isLoading ? (
        <CircularLoadingProgress />
      ) : error ? (
        <p>Error al cargar proveedores</p>
      ) : vendors.length === 0 ? (
        <p>No se encontró información</p>
      ) : (
        <VendorTable vendors={vendors} />
      )}
    </Page>
  );
}
