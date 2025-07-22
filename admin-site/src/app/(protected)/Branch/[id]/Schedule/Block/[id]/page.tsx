"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { Button } from "@mui/material";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { getErrorMessage } from "@/utils/util";
import { useSnackbar } from "@/stores/useSnackbar";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { BlockForm } from "../components/BlockForm";
import { BlockForm as BlockFormType } from "../components/BlockSchema";
import { BlockDeleteModalConfirmation } from "./BlockDeleteModalConfirmation";
import { UsePutScheduleBlock } from "@/hooks/api-beyou/branch/schedule/block/UsePutScheduleBlock";
import { UseGetScheduleBlockById } from "@/hooks/api-beyou/branch/schedule/block/UseGetScheduleBlockById";

const EditBlockPage = () => {
  const router = useRouter();
  const params = useParams();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);
  const [loading, setLoading] = useState(false);
  const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

  const blockIdRaw = params?.id;
  const blockId =
    blockIdRaw && !isNaN(Number(blockIdRaw)) ? Number(blockIdRaw) : undefined;

  const closeLoading = () => setLoading(false);

  const {
    data: blockData,
    isLoading,
    isError,
    error,
  } = UseGetScheduleBlockById(blockId);

  const { mutate: putBlock } = UsePutScheduleBlock({
    blockId: blockId!,
    ...UseMutationCallbacks(
      "Bloque actualizado exitosamente",
      "/Block",
      closeLoading
    ),
  });

  useEffect(() => {
    if (isError) {
      setSnackbarMessage(getErrorMessage(error), "error");
      router.replace("/Block"); 
    }
  }, [isError, error, setSnackbarMessage, router]);

  const handleSubmit = (data: BlockFormType) => {
    setLoading(true);
    putBlock({
      ...data, 
    });
  };

  if (isLoading) return <CircularLoadingProgress />;

  return (
    <Page
      header={
        <PageHeader
          title={`Editar Bloque Nº ${blockData?.id}`}
          subtitle="Actualiza los datos del bloque"
          backPath="/Block"
          backText="Bloques"
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
      <BlockForm
        defaultValues={{
          id: blockData?.id || 0,
          branchScheduleId: blockData?.branchScheduleId || 0,
          startHour: blockData?.startHour || "",
          endHour: blockData?.endHour || "",
        }}
        onSubmit={handleSubmit}
        isLoading={loading}
      />

      <BlockDeleteModalConfirmation
        isModalOpen={openModalConfirmation}
        toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
        blockId={blockData?.id ?? 0}
        title={`Bloque ${blockData?.id}`}
      />
    </Page>
  );
};

export default EditBlockPage;
