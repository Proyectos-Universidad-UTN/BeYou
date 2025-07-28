"use client";

import React from "react";
import { BranchSchedule } from "@/types/api-beyou";
import BranchScheduleColumns from "./BranchScheduleColumns";
import DataTableWrapper from "@/components/Table/DataTableWrapper";

interface ScheduleTableProps {
  schedules: BranchSchedule[];
}

export const ScheduleTable = ({ schedules }: ScheduleTableProps) => {
  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={BranchScheduleColumns}
      data={schedules}
    />
  )
 
};

