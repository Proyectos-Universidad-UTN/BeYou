"use client";

import { useSearchParams } from "next/navigation";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CreateButton } from "@/components/Button/CreateButton";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { ErrorProcess } from "@/components/Error/ErrorProcess";
import { UseGetInventory } from "@/hooks/api-beyou/inventory/UseGetInventory";
import { InventoryTable } from "./InventoryTable";
import { UseGetBranchInventories } from "@/hooks/api-beyou/branch/Inventory/UseGetBranchInventory";

export default function InventoryPage() {
  const searchParams = useSearchParams();
 const branchId = searchParams.get("branchId") ?? undefined;

const { data, isLoading, isError } = UseGetBranchInventories(branchId);

  if (isLoading) return <CircularLoadingProgress />;
  if (isError) return <ErrorProcess />;

  return (
    <Page
      header={
        <PageHeader
          title="Lista de inventario"
          actionButton={<CreateButton href={`Inventory/new?branchId=${branchId}`} />}
        />
      }
    >
      <InventoryTable inventories={data ?? []} isLoading={isLoading} isError={isError} />
    </Page>
  );
}
