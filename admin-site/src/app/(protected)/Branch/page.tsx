"use client";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { BranchTable } from "./BranchTable";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { type BranchTableItem } from "./BranchTable";
import { UseGetBranches } from "@/hooks/api-beyou/branch/UseGetBranches";

export default function BranchPage() {
  const { data, isLoading, error } = UseGetBranches();

  const branches: BranchTableItem[] = (data ?? [])
    .filter((branch) => branch.id !== undefined)
    .map((branch) => ({
      id: branch.id!,
      name: branch.name ?? "",
      description: branch.description ?? "",
      telephone: branch.telephone ?? 0,
      email: branch.email ?? "",
      districtId: branch.districtId ?? 0,
      address: branch.address ?? "",
    }));

  return (
    <Page>
      {isLoading ? (
        <CircularLoadingProgress />
      ) : error ? (
        <p>Error al cargar sucursales</p>
      ) : branches.length === 0 ? (
        <p>No se encontró información</p>
      ) : (
        <BranchTable branches={branches} />
      )}
    </Page>
  );
}
