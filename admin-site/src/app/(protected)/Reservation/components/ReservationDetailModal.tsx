// src/app/(protected)/Reservation/components/ReservationDetailModal.tsx
"use client";

import { Box, Typography, Button, CircularProgress, DialogActions } from "@mui/material";
import { UseGetDetailReservationById } from "@/hooks/api-beyou/detailReservation/UseGetDetailReservationById"; // Usaremos este hook
import { UsePutReservation } from "@/hooks/api-beyou/reservation/UsePutReservation";
import { UseDeleteReservation } from "@/hooks/api-beyou/reservation/UseDeleteReservation";
import { useState } from "react";
import { useSnackbar } from "@/stores/useSnackbar";
import { getErrorMessage } from "@/utils/util";
import { Reservation } from "@/types/api-beyou"; // Interfaz de la API
import { ReservationFormType } from "./ReservationSchema";

interface ReservationDetailModalProps {
  reservationId: string; // El ID que viene del evento del calendario
  onClose: () => void;
  onReservationChanged: () => void; // Para que el calendario pueda refrescarse
}

export const ReservationDetailModal = ({
  reservationId,
  onClose,
  onReservationChanged,
}: ReservationDetailModalProps) => {
  const { openSnackbar } = useSnackbar(); // Asumo que ya corregiste el tipado de useSnackbar

  // Cargar la reserva
  const { data: reservation, isLoading, error, refetch } = UseGetDetailReservationById(reservationId);

  // Hooks para mutaciones
  const { mutate: updateReservation, isPending: isUpdating } = UsePutReservation();
  const { mutate: deleteReservation, isPending: isDeleting } = UseDeleteReservation();

  // Estado para el modal de confirmación de eliminación
  const [isConfirmDeleteModalOpen, setIsConfirmDeleteModalOpen] = useState(false);

  // Manejador del submit del formulario
  const handleSubmit = (formData: ReservationFormType) => {
    const reservationToUpdate: Reservation = {
        ...formData,
        id: parseInt(reservationId), // Asegurar que el ID esté en el payload para el PUT
        // customerName no se debería enviar en el PUT/POST si solo se usa para display
        // Si la API lo requiere, asegúrate que formData.customerName tenga valor
        // Los arreglos reservationQuestion y reservationDetails deberán ser manejados si son parte del PUT
        reservationQuestion: formData.reservationQuestion,
        reservationDetails: formData.reservationDetails,
    } as Reservation; // Casteo temporal, ajusta los tipos para que esto no sea necesario

    updateReservation(reservationToUpdate, {
      onSuccess: () => {
        openSnackbar("Reserva actualizada con éxito", "success");
        onReservationChanged(); // Notificar al calendario para que refresque
        onClose(); // Cerrar el modal
      },
      onError: (err) => {
        openSnackbar(`Error al actualizar reserva: ${getErrorMessage(err)}`, "error");
      },
    });
  };

  // Manejador para iniciar la eliminación
  const handleDeleteClick = () => {
    setIsConfirmDeleteModalOpen(true);
  };

  // Manejador para confirmar la eliminación
  const handleConfirmDelete = () => {
    deleteReservation(reservationId, { // Asegúrate de que tu hook delete recibe el ID correcto
      onSuccess: () => {
        openSnackbar("Reserva eliminada con éxito", "success");
        onReservationChanged(); // Notificar al calendario para que refresque
        onClose(); // Cerrar el modal principal
        setIsConfirmDeleteModalOpen(false); // Cerrar el modal de confirmación
      },
      onError: (err) => {
        openSnackbar(`Error al eliminar reserva: ${getErrorMessage(err)}`, "error");
        setIsConfirmDeleteModalOpen(false);
      },
    });
  };

  if (isLoading) {
    return (
      <Box p={4} display="flex" flexDirection="column" alignItems="center" justifyContent="center" minHeight="200px">
        <CircularProgress />
        <Typography mt={2}>Cargando detalles de la reserva...</Typography>
      </Box>
    );
  }

  if (error || !reservation) {
    return (
      <Box p={4}>
        <Typography color="error">Error al cargar la reserva o reserva no encontrada.</Typography>
        <DialogActions>
            <Button onClick={onClose} variant="outlined">Cerrar</Button>
        </DialogActions>
      </Box>
    );
  }

  const defaultFormValues: ReservationFormType = {
    id: reservation.id,
    customerName: reservation.customerName,
    customerId: reservation.customerId,
    date: reservation.date, // Formato "YYYY-MM-DD" esperado
    hour: reservation.hour, // Formato "HH:mm" esperado
    branchId: reservation.branchId,
    status: reservation.status,
    reservationQuestion: reservation.reservationQuestion,
    reservationDetails: reservation.reservationDetails,
  };


  return (
    <Box p={4}>
      <Typography variant="h5" mb={3}>
        Detalle y Edición de Reserva
      </Typography>
      <ReservationForm
        defaultValues={defaultFormValues}
        onSubmit={handleSubmit}
        isLoading={isUpdating}
        isEdit={true}
        // Pasamos datos mock o reales para los selectores si tuvieran
        customers={[{ id: reservation.customerId, name: reservation.customerName }]} // Solo el cliente de esta reserva por ahora
        branches={[{ id: reservation.branchId, name: `Sucursal ${reservation.branchId}` }]} // Mock
        services={[{ id: 1, name: "Manicura" }, { id: 2, name: "Pedicura" }]} // Mock de servicios
      />
      <DialogActions sx={{ mt: 2 }}>
        <Button onClick={onClose} variant="outlined">
          Cerrar
        </Button>
        <Button
          onClick={handleDeleteClick}
          color="error"
          variant="contained"
          disabled={isDeleting || isUpdating}
        >
          Eliminar Reserva
        </Button>
      </DialogActions>

      <ConfirmModal
        open={isConfirmDeleteModalOpen}
        onClose={() => setIsConfirmDeleteModalOpen(false)}
        onConfirm={handleConfirmDelete}
        title="Confirmar Eliminación"
        message={`¿Estás seguro de que quieres eliminar la reserva de ${reservation.customerName} el ${reservation.date} a las ${reservation.hour}? Esta acción no se puede deshacer.`}
        isLoading={isDeleting}
      />
    </Box>
  );
};