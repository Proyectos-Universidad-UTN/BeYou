"use client";

import { useState } from "react";
import { useSearchParams, useRouter } from "next/navigation";
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
  const scheduleId = searchParams.get("scheduleId");

  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const { mutate: postBlock } = UsePostScheduleBlock({
    onSuccess: (_, __) => {
      setSnackbarMessage("Bloqueo creado exitosamente", "success");
      router.push(`/Schedule/${scheduleId}/Block`);
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
    if (!scheduleId) return;
    setLoading(true);
    postBlock({
      ...data,
      branchScheduleId: Number(scheduleId),
    });
  };

  return (
    <Page
      header={
        <PageHeader
          title="Crear Bloqueo de Horario"
          subtitle="Agrega un nuevo bloqueo a la programación"
        />
      }
    >
      <BlockForm
        defaultValues={{
          ...BlockDefaultValues,
          branchScheduleId: Number(scheduleId),
        }}
        onSubmit={handleSubmit}
        isLoading={loading}
      />
    </Page>
  );
};

export default NewBlockPage;
