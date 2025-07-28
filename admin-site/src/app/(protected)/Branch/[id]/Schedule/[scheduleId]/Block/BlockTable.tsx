"use client";

import DataTableWrapper from "@/components/Table/DataTableWrapper";
import BlockColumns from "./BlockColumns";
import { BranchScheduleBlock } from "@/types/api-beyou";

interface BlockTableProps {
  blocks?: BranchScheduleBlock[];
}

export const BlockTable = ({ blocks }: BlockTableProps) => {
  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={BlockColumns}
      data={blocks?? []} 
    />
  );
};
