"use client";

import { useState } from "react";
import { useSearchParams, useRouter, useParams } from "next/navigation";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { BlockForm } from "../components/BlockForm";
import {
  BlockDefaultValues,
  BlockForm as BlockFormType,
} from "../components/BlockSchema";
import { UsePostScheduleBlock } from "@/hooks/api-beyou/branch/schedule/block/UsePostScheduleBlock";
import { useSnackbar } from "@/stores/useSnackbar";
import { getErrorMessage } from "@/utils/util";

const NewBlockPage = () => {
  const [loading, setLoading] = useState(false);
  const searchParams = useSearchParams();
  const router = useRouter();
  const params = useParams();

  // branchId viene del path /Branch/[id]/Schedule/Block/new
  const branchId = params.id;

  // scheduleId lo pasamos por query param: ?scheduleId=123
  const scheduleId = searchParams.get("id");

  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const { mutate: postBlock } = UsePostScheduleBlock({
    onSuccess: () => {
      setSnackbarMessage("Bloqueo creado exitosamente", "success");
      router.push(
        `/Branch/${branchId}/Schedule/Block?scheduleId=${scheduleId}`
      );
    },
    onError: (error) => {
      setSnackbarMessage(getErrorMessage(error), "error");
      setLoading(false);
    },
    onSettled: () => {
      setLoading(false);
    },
  });

  const handleSubmit = (data: BlockFormType) => {
    if (!scheduleId) {
      setSnackbarMessage(
        "El ID de la programación (scheduleId) es requerido",
        "error"
      );
      return;
    }
    setLoading(true);
    postBlock({
      ...data,
      branchScheduleId: Number(scheduleId),
    });
  };

  // Ruta para regresar a la lista de bloqueos del schedule
  const backPath = `/Branch/${branchId}/Schedule/Block?scheduleId=${scheduleId}`;

  return (
    <Page
      header={
        <PageHeader
          title="Crear Bloqueo de Horario"
          subtitle="Agrega un nuevo bloqueo a la programación"
          backPath={backPath}
          backText="Bloqueos"
        />
      }
    >
      <BlockForm
        defaultValues={{
          ...BlockDefaultValues,
          branchScheduleId: Number(scheduleId ?? 0),
        }}
        onSubmit={handleSubmit}
        isLoading={loading}
        backPath={backPath}
      />
    </Page>
  );
};

export default NewBlockPage;
