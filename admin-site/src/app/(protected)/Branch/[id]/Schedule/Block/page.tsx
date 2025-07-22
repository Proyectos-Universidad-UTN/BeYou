"use client";

import { useParams } from "next/navigation";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CreateButton } from "@/components/Button/CreateButton";
import { BlockTable } from "./BlockTable";
import { UseGetScheduleBlocks } from "@/hooks/api-beyou/branch/schedule/block/UseGetScheduleBlocks";
import { BlockForm } from "./components/BlockSchema";

export default function BlockPage() {
  const { id } = useParams();
  const scheduleId = typeof id === "string" ? id : undefined;

  const { data, isLoading, error } = UseGetScheduleBlocks(scheduleId);

  const validBlocks: BlockForm[] = (data ?? [])
    .filter(
      (item): item is Required<Pick<BlockForm, "id" | "branchScheduleId" | "startHour" | "endHour">> =>
        item.id !== undefined &&
        item.branchScheduleId !== undefined &&
        item.startHour !== undefined &&
        item.endHour !== undefined
    )
    .map((item) => ({
      id: item.id!,
      branchScheduleId: item.branchScheduleId!,
      startHour: item.startHour!,
      endHour: item.endHour!,
    }));

  return (
    <Page
      header={
        <PageHeader
          title="Lista de Bloqueos"
          actionButton={<CreateButton href={`/Block/new?scheduleId=${scheduleId}`} />}
        />
      }
    >
      <BlockTable blocks={validBlocks} />
    </Page>
  );
}
