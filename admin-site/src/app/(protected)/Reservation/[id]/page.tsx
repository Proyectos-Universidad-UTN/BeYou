"use client";

import { useParams, useRouter } from "next/navigation";
import { useState } from "react";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { ReservationForm } from "../components/ReservationForm";
import { UseGetReservationById } from "@/hooks/api-beyou/reservation/UseGetReservationById";
import { UsePutReservation } from "@/hooks/api-beyou/reservation/UsePutReservation";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import {
  initialReservationValues,
  ReservationFormType,
} from "../components/ReservationSchema";
import { UseGetCustomer } from "@/hooks/api-beyou/customer/UseGetCustomer"; // Importar tu hook

const EditReservationPage = () => {
  const params = useParams();
  const router = useRouter();

  let id = params?.id;
  if (Array.isArray(id)) id = id[0];

  const { data: reservation, isLoading, error } = UseGetReservationById(id);
  const { data: customers } = UseGetCustomer(); // Obtenés los clientes

  const [loading, setLoading] = useState(false);

  const onSuccess = () => {
    setLoading(false);
    router.push("/Reservation");
  };

  const validStatuses = ["P", "C", "X"] as const;

  const { mutate: updateReservation } = UsePutReservation(
    UseMutationCallbacks("Reserva actualizada correctamente", "/Reservation", onSuccess)
  );

  if (!id) return <div>No se encontró ID de la reserva</div>;

  const defaultValues: ReservationFormType = reservation
    ? {
        customerId: reservation.customerId ?? 0,
        customerName: reservation.customerName ?? "",
        date: reservation.date ?? "",
        hour: reservation.hour ?? "",
        status: validStatuses.includes(reservation.status as any)
          ? (reservation.status as "P" | "C" | "X")
          : "P",
        branchId: reservation.branchId ?? 0,
        reservationQuestion: reservation.reservationQuestions || [],
        reservationDetails: reservation.reservationDetails || [],
        id: reservation.id ?? 0,
      }
    : initialReservationValues;

  const handleSubmit = (data: ReservationFormType) => {
    const selectedCustomer = customers?.find((c) => c.id === data.customerId);
    const fullName = selectedCustomer
      ? `${selectedCustomer.firstName} ${selectedCustomer.lastName}`
      : "";

    const payload = {
      ...data,
      id: Number(id),
      customerName: fullName,
    };

    console.log("🔍 Datos enviados al PUT:", payload);

    setLoading(true);
    updateReservation(payload);
  };

  if (isLoading) return <CircularLoadingProgress />;
  if (error || !reservation) return <div>Error al cargar la reserva para editar.</div>;

  return (
    <div>
      <h2 className="text-center font-semibold mb-6">Editar Reserva</h2>
      <ReservationForm
        defaultValues={defaultValues}
        onSubmit={handleSubmit}
        isLoading={loading}
        onCancel={() => router.push("/Reservation")}
      />
    </div>
  );
};

export default EditReservationPage;
