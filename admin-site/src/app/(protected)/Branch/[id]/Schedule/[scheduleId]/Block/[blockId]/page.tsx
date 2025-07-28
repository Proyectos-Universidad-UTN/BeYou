"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { Button } from "@mui/material";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { useSnackbar } from "@/stores/useSnackbar";
import { getErrorMessage } from "@/utils/util";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";

import { UseGetScheduleBlockById } from "@/hooks/api-beyou/branch/schedule/block/UseGetScheduleBlockById";
import { UsePutScheduleBlock } from "@/hooks/api-beyou/branch/schedule/block/UsePutScheduleBlock";

import { BlockDeleteModalConfirmation } from "./BlockDeleteModalConfirmation";
import { BlockFormComponent } from "../components/BlockForm";
import { components } from "@/api/clients/beyou/api";
import { BlockForm } from "../components/BlockSchema";

const EditBlockPage = () => {
  const router = useRouter();
  const params = useParams();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const [loading, setLoading] = useState(false);
  const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

  const blockIdRaw = params?.blockId;
  const scheduleIdRaw = params?.scheduleId;
  const branchIdRaw = params?.id;

  const blockId =
    blockIdRaw && !isNaN(Number(blockIdRaw)) ? Number(blockIdRaw) : undefined;
  const scheduleId =
    scheduleIdRaw && !isNaN(Number(scheduleIdRaw))
      ? String(scheduleIdRaw)
      : undefined;
  const branchId =
    branchIdRaw && !isNaN(Number(branchIdRaw))
      ? String(branchIdRaw)
      : undefined;

  const backPath = `/Branch/${branchId}/Schedule/${scheduleId}/Block`;

  const { data, isLoading, isError, error } = UseGetScheduleBlockById(blockId);

  const { mutate: putBlock } = UsePutScheduleBlock({
    blockId: blockId!,
    onSuccess: () => {
      setSnackbarMessage("Bloqueo actualizado exitosamente", "success");
      router.push(backPath);
    },
    onError: (error) => {
      setSnackbarMessage(getErrorMessage(error), "error");
      setLoading(false);
    },
    onSettled: () => {
      setLoading(false);
    },
  });

  useEffect(() => {
    if (isError) {
      setSnackbarMessage(getErrorMessage(error), "error");
      router.replace(backPath);
    }
  }, [isError, error, router, setSnackbarMessage]);

  const handleSubmit = (data: BlockForm) => {
    if (!scheduleId || !blockId) {
      setSnackbarMessage("ID inválido para el horario o el bloqueo", "error");
      return;
    }

    setLoading(true);
    putBlock({
      ...data,
      branchScheduleId: Number(scheduleId),
    });
  };

  if (isLoading || !data) return <CircularLoadingProgress />;

  const ConvertToBlockSchema = (
    data: components["schemas"]["ResponseBranchScheduleBlockDto"]
  ): BlockForm => ({
    id: data.id!,
    startHour: data.startHour ?? "",
    endHour: data.endHour ?? "",
    branchScheduleId: data.branchScheduleId!,
  });

  return (
    <Page
      header={
        <PageHeader
          title={`Editar Bloqueo #${blockId}`}
          subtitle="Actualiza los datos del bloqueo"
          backPath={backPath}
          backText="Bloqueos"
          actionButton={
            <Button
              className="!bg-red-500 hover:bg-red-600"
              variant="contained"
              size="large"
              fullWidth
              onClick={() => setOpenModalConfirmation(true)}
            >
              Eliminar
            </Button>
          }
        />
      }
    >
      <BlockFormComponent
        defaultValues={ConvertToBlockSchema(data)}
        onSubmit={handleSubmit}
        isLoading={loading}
        backPath={backPath}
      />

      {blockId !== undefined && (
        <BlockDeleteModalConfirmation
          isModalOpen={openModalConfirmation}
          toggleIsOpen={() =>
            setOpenModalConfirmation(!openModalConfirmation)
          }
          blockId={blockId}
          title={`Bloqueo ${blockId}`}
        />
      )}
    </Page>
  );
};

export default EditBlockPage;
