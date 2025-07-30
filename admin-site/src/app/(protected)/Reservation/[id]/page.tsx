"use client";

import { useRouter } from "next/navigation";
import { use, useState } from "react";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { ReservationForm } from "../components/ReservationForm";
import { UseGetReservationById } from "@/hooks/api-beyou/reservation/UseGetReservationById";
import { UsePutReservation } from "@/hooks/api-beyou/reservation/UsePutReservation";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import {
  initialReservationValues,
  ReservationFormType,
} from "../components/ReservationSchema";

interface Props {
  params: Promise<{ id: string }>;
}

const EditReservationPage = ({ params }: Props) => {
  const resolvedParams = use(params);
  const { id } = resolvedParams;

  const router = useRouter();

  const { data: reservation, isLoading, error } = UseGetReservationById(id);

  const [loading, setLoading] = useState(false);

  const onSuccess = () => {
    setLoading(false);
    router.push("/Reservation");
  };

  const validStatuses = ["P", "C", "X"] as const;

  const { mutate: updateReservation } = UsePutReservation(
    UseMutationCallbacks(
      "Reserva actualizada correctamente",
      "/Reservation",
      onSuccess
    )
  );

  const defaultValues: ReservationFormType = reservation
  ? {
      customerId: reservation.customerId ?? 0,
      customerName: null, 
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
    console.log("🔍 Datos enviados al PUT:", data); // ← ESTE CONSOLE
    setLoading(true);
    updateReservation(data);
  };

  if (isLoading) return <CircularLoadingProgress />;

  if (error || !reservation)
    return <div>Error al cargar la reserva para editar.</div>;

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
