"use client";

import { UseGetReservationById } from "@/hooks/api-beyou/reservation/UseGetReservationById";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import {
  Card,
  CardContent,
  Typography,
  Box,
  Divider,
  Button,
  Stack,
} from "@mui/material";
import {
  CalendarToday,
  AccessTime,
  Person,
  CheckCircleOutline,
  Edit as EditIcon,
  Close as CloseIcon,
} from "@mui/icons-material";
import { useRouter } from "next/navigation";

interface Props {
  reservationId: string;
  onClose: () => void;
  onReservationChanged?: () => void;
}

export const ReservationDetailModal = ({
  reservationId,
  onClose,
  onReservationChanged,
}: Props) => {
  const {
    data: reservation,
    isLoading,
    error,
  } = UseGetReservationById(reservationId);

  const router = useRouter();

  const STATUS_LABELS: Record<string, string> = {
    P: "Pendiente",
    C: "Confirmada",
    X: "Cancelada",
  };

  const handleEdit = () => {
    router.push(`/Reservation/${reservation?.id}`);
  };

  if (isLoading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" py={6}>
        <CircularLoadingProgress />
      </Box>
    );
  }

  if (error || !reservation) {
    return (
      <Box p={4}>
        <Typography color="error">No se pudo cargar la reserva.</Typography>
        <Box mt={2} textAlign="right">
          <Button
            onClick={onClose}
            variant="contained"
            startIcon={<CloseIcon />}
          >
            Cerrar
          </Button>
        </Box>
      </Box>
    );
  }

  return (
    <Card className="rounded-2xl shadow-xl dark:bg-zinc-900">
      <CardContent className="p-6 space-y-4">
        <Typography
          variant="h5"
          className="text-center font-semibold text-zinc-800 dark:text-white"
        >
          Detalle de la Reserva
        </Typography>

        <Typography
          variant="body2"
          className="text-center text-zinc-500 dark:text-zinc-400"
        >
          Consulta rápida de la información
        </Typography>

        <Divider className="my-2" />

        <Stack spacing={2}>
          <Box className="flex items-center justify-between">
            <Box className="flex items-center gap-2">
              <Person className="text-blue-500" />
              <Typography className="text-sm font-medium">Cliente</Typography>
            </Box>
            <Typography className="text-sm text-zinc-700 dark:text-zinc-100">
              {reservation.customerName}
            </Typography>
          </Box>

          <Box className="flex items-center justify-between">
            <Box className="flex items-center gap-2">
              <CalendarToday className="text-green-500" fontSize="small" />
              <Typography className="text-sm font-medium">Fecha</Typography>
            </Box>
            <Typography className="text-sm">{reservation.date}</Typography>
          </Box>

          <Box className="flex items-center justify-between">
            <Box className="flex items-center gap-2">
              <AccessTime className="text-purple-500" fontSize="small" />
              <Typography className="text-sm font-medium">Hora</Typography>
            </Box>
            <Typography className="text-sm">{reservation.hour}</Typography>
          </Box>

          <Box className="flex items-center justify-between">
            <Box className="flex items-center gap-2">
              <CheckCircleOutline
                className="text-orange-500"
                fontSize="small"
              />
              <Typography className="text-sm font-medium">Estado</Typography>
            </Box>
            <Typography className="text-sm capitalize">
             {STATUS_LABELS[reservation.status ?? ""] || reservation.status || "Desconocido"}
            </Typography>
          </Box>
        </Stack>

        <Box className="pt-6 flex justify-end gap-4">
          <Button
            variant="outlined"
            color="primary"
            onClick={handleEdit}
            startIcon={<EditIcon />}
          >
            Editar
          </Button>

          <Button
            onClick={onClose}
            variant="contained"
            color="primary"
            startIcon={<CloseIcon />}
          >
            Cerrar
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
};
