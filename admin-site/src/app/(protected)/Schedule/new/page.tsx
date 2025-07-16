"use client";

import { useState } from "react";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { ScheduleFormComponent } from "../../(protected)/Branch/Schedule/components/ScheduleForm";
import {
  ScheduleDefaultValues,
  ScheduleForm,
} from "../../(protected)/Branch/Schedule/components/ScheduleSchema";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UsePostSchedule } from "@/hooks/api-beyou/schedule/UsePostSchedule";

const NewSchedulePage = () => {
  const [loading, setLoading] = useState(false);

  const closeLoading = () => setLoading(false);

  const { mutate: postSchedule } = UsePostSchedule(
    UseMutationCallbacks(
      "Horario creado exitosamente",
      "/Branch/Schedule",
      closeLoading
    )
  );

  const handleSubmit = (data: ScheduleForm) => {
    setLoading(true);
    postSchedule({
      day: data.day as 1 | 2 | 3 | 4 | 5 | 6 | 7,
      startHour: data.startHour,
      endHour: data.endHour,
    });
  };

  return (
    <Page
      header={
        <PageHeader
          title="Crear Horario"
          subtitle="Selecciona un horario para registrar"
        />
      }
    >
      <ScheduleFormComponent
        defaultValues={ScheduleDefaultValues}
        onSubmit={handleSubmit}
        isLoading={loading}
      />
    </Page>
  );
};

export default NewSchedulePage;
