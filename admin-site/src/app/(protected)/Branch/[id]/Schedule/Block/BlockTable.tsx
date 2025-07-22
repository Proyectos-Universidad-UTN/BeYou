"use client";

import { BlockForm } from "./components/BlockSchema";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import BlockColumns from "./BlockColumns";

interface BlockTableProps {
  blocks: BlockForm[];
}

export const BlockTable = ({ blocks }: BlockTableProps) => {
  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={BlockColumns}
      data={blocks}
    />
  );
};
