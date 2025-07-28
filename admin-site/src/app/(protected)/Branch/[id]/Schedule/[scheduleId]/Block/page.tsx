"use client";

import { Button } from "@mui/material";
import { useEffect, useState } from "react";
import { Page } from "@/components/Shared/Page";
import { useSnackbar } from "@/stores/useSnackbar";
import { useParams, useRouter } from "next/navigation";
import { PageHeader } from "@/components/Shared/PageHeader";
import { getErrorMessage, weekDaysSpanish } from "@/utils/util";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { BlockTable } from "./BlockTable";
import Link from "next/link";
import AddIcon from "@mui/icons-material/Add";
import { UseGetScheduleBlocksByBranchSchedule } from "@/hooks/api-beyou/branch/schedule/block/UseGetScheduleBlocksByBranchSchedule";
import { UseGetBranchScheduleById } from "@/hooks/api-beyou/branch/schedule/UseGetBranchScheduleById";

const BlockPage = () => {
  const router = useRouter();
  const params = useParams();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const branchIdRaw = params?.id;
  const scheduleIdRaw = params?.scheduleId;

  const branchId =
    branchIdRaw && !isNaN(Number(branchIdRaw))
      ? String(branchIdRaw)
      : undefined;
  const scheduleId =
    scheduleIdRaw && !isNaN(Number(scheduleIdRaw))
      ? String(scheduleIdRaw)
      : undefined;

  const {
    data: blocksData,
    isLoading,
    isError,
    error,
  } = UseGetScheduleBlocksByBranchSchedule(scheduleId);

  const {
    data: BranchScheduleData,
    isLoading: isLoadingBranchSchedule,
    isError: isErrorBranchSchedule,
    error: errorBranchSchedule,
  } = UseGetBranchScheduleById(scheduleId);

  useEffect(() => {
    if (!branchId || !scheduleId) {
      setSnackbarMessage("ID de sucursal o horario inválido", "error");
      router.replace(
        (!branchId && !scheduleId) || !branchId
          ? "/Branch"
          : `/Branch/${branchId}/Schedule`
      );
    }
  }, [branchId, scheduleId, router, setSnackbarMessage]);

  useEffect(() => {
    if (isError) {
      const message = getErrorMessage(error);
      setSnackbarMessage(message, "error");
      router.replace(`/Branch/${branchId}/Schedule`);
    }
  }, [isError, error, router, setSnackbarMessage]);

  useEffect(() => {
    if (isErrorBranchSchedule) {
      const message = getErrorMessage(errorBranchSchedule);
      setSnackbarMessage(message, "error");
      router.replace("/Branch");
    }
  }, [isErrorBranchSchedule, errorBranchSchedule, router, setSnackbarMessage]);

  useEffect(() => {
    if (BranchScheduleData?.branchId != branchId) {
      setSnackbarMessage(
        "El horario sucursal no pertenece a la sucursal",
        "error"
      );
      router.replace("/Branch");
    }
  });

  if (isLoading || isLoadingBranchSchedule) return <CircularLoadingProgress />;

  return (
    <Page
      header={
        <PageHeader
          title="Bloqueos de Horario"
          subtitle={`Sucursal: ${BranchScheduleData?.branch?.name} - Horario: ${weekDaysSpanish[BranchScheduleData?.schedule?.day!]} `}
          backPath={`/Branch/${branchId}/Schedule`}
          backText="Horarios"
          actionButton={
            <Link href={`/Branch/${branchId}/Schedule/${scheduleId}/Block/new`}>
              <Button variant="outlined" startIcon={<AddIcon />} size="large">
                Nuevo Bloqueo
              </Button>
            </Link>
          }
        />
      }
    >
      <BlockTable blocks={blocksData} />
    </Page>
  );
};

export default BlockPage;
