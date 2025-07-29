// src/app/(protected)/Reservation/agenda.tsx
"use client";

import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import esLocale from "@fullcalendar/core/locales/es";
import { Box, Typography } from "@mui/material";
import { UseGetReservations } from "@/hooks/api-beyou/reservation/UseGetReservations";
import { Modal } from "@/components/Modal/Modal";
import { Reservation } from "@/types/api-beyou";
import { useState } from "react"; // Importar useState

// Importar el nuevo componente
import { ReservationDetailModal } from "./components/ReservationDetailModal";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";

export const Agenda = () => {
  const { data, isLoading, error, refetch } = UseGetReservations(); // Añadir refetch aquí

  const [isDetailModalOpen, setIsDetailModalOpen] = useState(false);
  const [selectedReservationId, setSelectedReservationId] = useState<string | null>(null);

  const getEventColor = (status?: string) => {
    switch (status) {
      case "C":
        return "green";
      case "X":
        return "red";
      case "P":
      default:
        return "#2196f3";
    }
  };

  const events =
    data?.map((reservation: Reservation) => {
      const eventDate = reservation.date ? new Date(`${reservation.date}T${reservation.hour}`) : null; // Combinar fecha y hora para un Date adecuado
      let formattedTime = "";

      if (eventDate && !isNaN(eventDate.getTime())) {
        formattedTime = eventDate.toLocaleTimeString("es-ES", {
          hour: "2-digit",
          minute: "2-digit",
        });
      }

      const eventTitle = `${formattedTime ? formattedTime + ' - ' : ''}${reservation.customerName ?? "Reserva sin nombre"}`;

      return {
        id: reservation.id?.toString(),
        title: eventTitle,
        start: `${reservation.date}T${reservation.hour}`, // FullCalendar espera un formato ISO para fecha y hora
        backgroundColor: getEventColor(reservation.status ?? undefined),
        borderColor: getEventColor(reservation.status ?? undefined),
      };
    }) ?? [];

  const handleEventClick = (clickInfo: any) => {
    setSelectedReservationId(clickInfo.event.id);
    setIsDetailModalOpen(true);
  };

  const handleCloseDetailModal = () => {
    setIsDetailModalOpen(false);
    setSelectedReservationId(null);
  };

  // Función para refrescar los datos del calendario después de una edición/eliminación
  const handleReservationChanged = () => {
    refetch(); // Vuelve a cargar las reservas para actualizar el calendario
  };


  if (isLoading) {
    return (
      <Box display="flex" justifyContent="center" py={6}>
        <CircularLoadingProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Box display="flex" justifyContent="center" py={6}>
        <Typography color="error">Error al cargar las reservas</Typography>
      </Box>
    );
  }

  return (
    <Box p={2}>
      <FullCalendar
        plugins={[dayGridPlugin]}
        initialView="dayGridMonth"
        locale={esLocale}
        height="auto"
        events={events}
        eventClick={handleEventClick}
      />

+      <Modal isOpen={isDetailModalOpen} toggleIsOpen={handleCloseDetailModal}> {/* <-- CAMBIO AQUÍ */}
        {selectedReservationId && (
          <ReservationDetailModal
            reservationId={selectedReservationId}
            onClose={handleCloseDetailModal}
            onReservationChanged={handleReservationChanged} // Pasar la función de refresco
          />
        )}
      </Modal>
    </Box>
  );
};