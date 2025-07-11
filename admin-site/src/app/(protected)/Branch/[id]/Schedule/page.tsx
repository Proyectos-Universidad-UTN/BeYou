// /Branch/[id]/Schedule/page.tsx

"use client";

import { useParams, useRouter } from "next/navigation";
import AddIcon from "@mui/icons-material/Add";
import { useEffect } from "react";
import { ErrorProcess } from "@/components/Error/ErrorProcess";
import { useSnackbar } from "@/stores/useSnackbar";
import { getErrorMessage } from "@/utils/util";
import { UseGetBranchById } from "@/hooks/api-beyou/branch/UseGetBranchById";
import Link from "next/link";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { ScheduleTable } from "./ScheduleTable";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { Button } from "@mui/material";


const BranchSchedulePage = () => {
  const router = useRouter();
  const params = useParams();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const branchIdRaw = params?.id
  const branchId = branchIdRaw && !isNaN(Number(branchIdRaw)) ? String(branchIdRaw) : undefined


  const {
    data: branchData,
    isLoading,
    isError,
    error,
  } = UseGetBranchById(branchId);

  useEffect(() => {
    if (!branchId || isError) {
      setSnackbarMessage(getErrorMessage(error), "error");
      router.push("/Branch");
    }
  }, [branchId, isError, error, router, setSnackbarMessage]);

  if (isLoading || !branchData) return <CircularLoadingProgress />;
  if (isError) return <ErrorProcess />;

  return (
    <Page
      header={
        <PageHeader
          title="Horarios de la Sucursal"
          subtitle={branchData.name!}
          actionButton={
            <Link href={`/Branch/${branchId}/Schedule/Gestion`}>
              <Button variant="contained" startIcon={<AddIcon />}>
                Gestión de horario
              </Button>
            </Link>
          }
          backPath="/Branch"
          backText="Sucursales"
        />
      }
    >
      <ScheduleTable
        schedules={branchData.branchSchedules ?? []}
      />
    </Page>
  );
};

export default BranchSchedulePage;
