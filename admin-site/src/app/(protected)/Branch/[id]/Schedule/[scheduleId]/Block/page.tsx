"use client";

import { Button } from "@mui/material";
import { useEffect, useState } from "react";
import { Page } from "@/components/Shared/Page";
import { useSnackbar } from "@/stores/useSnackbar";
import { useParams, useRouter } from "next/navigation";
import { PageHeader } from "@/components/Shared/PageHeader";
import { getErrorMessage } from "@/utils/util";
import { UseGetScheduleBlocks } from "@/hooks/api-beyou/branch/schedule/block/UseGetScheduleBlocks";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { BlockTable } from "./BlockTable";
import Link from "next/link";
import AddIcon from "@mui/icons-material/Add";
import { BlockForm } from "./components/BlockSchema";

const BlockPage = () => {
  const router = useRouter();
  const params = useParams();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const branchIdRaw = params?.branchId;
  const scheduleIdRaw = params?.scheduleId;

  const branchId =
    branchIdRaw && !isNaN(Number(branchIdRaw)) ? String(branchIdRaw) : undefined;
  const scheduleId =
    scheduleIdRaw && !isNaN(Number(scheduleIdRaw)) ? String(scheduleIdRaw) : undefined;

  const { data: blocksData, isLoading, isError, error } = UseGetScheduleBlocks(scheduleId);

  const [loadingDelete, setLoadingDelete] = useState(false);

  useEffect(() => {
    if (!branchId || !scheduleId) {
      setSnackbarMessage("ID de sucursal o horario inválido", "error");
    }
  }, [branchId, scheduleId, router, setSnackbarMessage]);

  useEffect(() => {
    if (isError) {
      const message = getErrorMessage(error) || "Error desconocido";
      setSnackbarMessage(message, "error");
    }
  }, [isError, error, router, setSnackbarMessage]);

  console.log("blocksData:", blocksData);
  console.log("isLoading:", isLoading, "isError:", isError);

  if (isLoading) return <CircularLoadingProgress />;

  if (!blocksData || !Array.isArray(blocksData)) {
    return (
      <Page
        header={
          <PageHeader
            title="Bloqueos de Horario"
            subtitle={`Sucursal: ${branchId} - Horario: ${scheduleId}`}
            backPath={`/Branch/${branchId}/Schedule`}
            backText="Horarios"
          />
        }
      >
        <div>
          No se encontraron datos de bloqueos o están corruptos.
          <pre>{JSON.stringify(blocksData, null, 2)}</pre>
        </div>
      </Page>
    );
  }

  const blocksFiltered: BlockForm[] = blocksData.filter(
    (b): b is BlockForm =>
      b &&
      typeof b.id === "number" &&
      typeof b.branchScheduleId === "number" &&
      typeof b.startHour === "string" &&
      typeof b.endHour === "string"
  );

  return (
    <Page
      header={
        <PageHeader
          title="Bloqueos de Horario"
          subtitle={`Sucursal: ${branchId} - Horario: ${scheduleId}`}
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
      <BlockTable blocks={blocksFiltered} />
    </Page>
  );
};

export default BlockPage;
