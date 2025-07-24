"use client";

import { useState } from "react";
import { useSearchParams, useRouter, useParams } from "next/navigation";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import {
  BlockDefaultValues,
  BlockForm as BlockFormType,
} from "../components/BlockSchema";
import { UsePostScheduleBlock } from "@/hooks/api-beyou/branch/schedule/block/UsePostScheduleBlock";
import { useSnackbar } from "@/stores/useSnackbar";
import { getErrorMessage } from "@/utils/util";
import { BlockFormComponent } from "../components/BlockForm";

const NewBlockPage = () => {
  const [loading, setLoading] = useState(false);
  const searchParams = useSearchParams();
  const router = useRouter();
  const params = useParams();

  const branchId = params.id;

  const scheduleId = params.scheduleId;

  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const { mutate: postBlock } = UsePostScheduleBlock({
    onSuccess: () => {
      setSnackbarMessage("Bloqueo creado exitosamente", "success");
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

  const backPath = `/Branch/${branchId}/Schedule/${scheduleId}/Block`;

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
      <BlockFormComponent
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
