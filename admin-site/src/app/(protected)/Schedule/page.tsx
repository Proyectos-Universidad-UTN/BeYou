"use client";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { ErrorProcess } from "@/components/Error/ErrorProcess";
import { UseGetSchedules } from "@/hooks/api-beyou/schedule/UseGetSchedules";
import { CreateButton } from "@/components/Button/CreateButton";
import { ScheduleTable } from "./ScheduleTable";

const SchedulePage = () => {
  const { data, isLoading, isError } = UseGetSchedules();

  if (isLoading) return <CircularLoadingProgress />;
  if (isError) return <ErrorProcess />;

  return (
    <Page
      header={
        <PageHeader
          title="Lista de Horarios"
          backPath="/Branch"
          backText="Sucursales"
          actionButton={<CreateButton href="/Branch/Schedule/new" />}
        />
      }
    >
      <ScheduleTable 
        schedules={data ?? []}
        isLoading={isLoading}
        isError={isError}
      />
    </Page>
  );
};

export default SchedulePage;
