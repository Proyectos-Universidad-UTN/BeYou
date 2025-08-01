"use client";

import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { CreateButton } from "@/components/Button/CreateButton";
import { Agenda } from "./agenda";


export default function ReservationPage() {
  return (
    <Page
      header={
        <PageHeader
          title="Agenda de Reservas"
          actionButton={<CreateButton href="/Reservation/new" />}
        />
      }
    >
      <Agenda />
    </Page>
  );
}
