// app/(protected)/Branch/Schedule/ScheduleTable.tsx

"use client";

import { useRouter } from "next/navigation";
import { GridEventListener, GridRowParams } from "@mui/x-data-grid";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import ScheduleColumns from "./ScheduleColumns";
import { Schedule } from "@/types/api-beyou";

interface ScheduleTableProps {
  schedules: Schedule[];
  isLoading: boolean;
  isError: boolean;
}

export const ScheduleTable = ({
  schedules,
  isLoading,
  isError,
}: ScheduleTableProps) => {
  const router = useRouter();

  const selectRow: GridEventListener<"rowClick"> = (params: GridRowParams) => {

    console.log("Click en horario con ID:", params.id);
  };

  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={ScheduleColumns}
      data={schedules}
      loading={isLoading}
      error={isError}
      onRowClick={selectRow}
    />
  );
};
