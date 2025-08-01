"use client";

import { useState } from "react";
import { Page } from "@/components/Shared/Page";
import { PageHeader } from "@/components/Shared/PageHeader";
import { ReservationForm } from "../components/ReservationForm";
import { initialReservationValues, ReservationFormType } from "../components/ReservationSchema";
import { UsePostReservation } from "@/hooks/api-beyou/reservation/UsePostReservation";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UseGetCustomer } from "@/hooks/api-beyou/customer/UseGetCustomer";

const NewReservationPage = () => {
  const [loading, setLoading] = useState(false);

  // Obtén los clientes para poder buscar el nombre por id
  const { data: customers = [], isLoading: loadingCustomers } = UseGetCustomer();

  const closeLoading = () => setLoading(false);

  const { mutate: postReservation } = UsePostReservation(
    UseMutationCallbacks("Reserva creada exitosamente", "/Reservation", closeLoading)
  );

  const handleSubmit = (data: ReservationFormType) => {
    setLoading(true);


    const selectedCustomer = customers.find(c => c.id === data.customerId);

    const customerName = selectedCustomer
      ? `${selectedCustomer.firstName} ${selectedCustomer.lastName}`.trim()
      : "Nombre genérico";

    const payload = {
      ...data,
      branchId: 1,
      customerName,
      reservationQuestion: [],
      reservationDetails: [],
      id: 0,
    };

    console.log("📤 Payload enviado a la API:", payload);

    postReservation(payload);
  };

  return (
    <Page
      header={
        <PageHeader
          title="Crear Reserva"
          subtitle="Agrega una nueva reserva al sistema"
        />
      }
    >
      <ReservationForm
        defaultValues={initialReservationValues}
        onSubmit={handleSubmit}
        isLoading={loading}
        onCancel={() => {}}
      />
    </Page>
  );
};

export default NewReservationPage;
