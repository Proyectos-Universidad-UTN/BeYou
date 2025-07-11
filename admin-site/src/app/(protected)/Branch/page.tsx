"use client";

import { BranchTable } from "./BranchTable";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CreateButton } from "@/components/Button/CreateButton";
import { UseGetBranches } from "@/hooks/api-beyou/branch/UseGetBranches";

export default function BranchPage() {
  const { data, isLoading, error } = UseGetBranches();

  return (
    <Page
      header={
        <PageHeader
          title="Lista de sucursales"
          actionButton={<CreateButton href="Branch/new" />}
        />
      }
    >
      <BranchTable branches={data ?? []} isLoading={isLoading} isError={!!error} />
    </Page>
  );
}
