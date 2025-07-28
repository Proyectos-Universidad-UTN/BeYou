"use client";

import { useState, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Checkbox,
  FormControlLabel,
  Stack,
  Button,
  CircularProgress,
  Typography,
  Box,
} from "@mui/material";
import { UseGetSchedules } from "@/hooks/api-beyou/schedule/UseGetSchedules";
import { UsePostBranchSchedules } from "@/hooks/api-beyou/branch/schedule/UsePostBranchSchedules";
import { useSnackbar } from "@/stores/useSnackbar";

interface Props {
  isOpen: boolean;
  toggleIsOpen: () => void;
  branchId: number;
}


const dayNames = [
  "", 
  "Lunes",
  "Martes",
  "Miércoles",
  "Jueves",
  "Viernes",
  "Sábado",
  "Domingo",
];

export const AddExistingSchedulesModal = ({ isOpen, toggleIsOpen, branchId }: Props) => {
  const { data: schedules, isLoading } = UseGetSchedules();
  const [selected, setSelected] = useState<number[]>([]);
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);


  useEffect(() => {
    if (!isOpen) setSelected([]);
  }, [isOpen]);

  const postSchedules = UsePostBranchSchedules({
    branchId,
    onSuccess: () => {
      setSnackbarMessage("Horarios asignados correctamente", "success");
      toggleIsOpen();
    },
    onError: () => {
      setSnackbarMessage("Error al asignar horarios", "error");
    },
  });

  const toggle = (id: number) => {
    setSelected((prev) =>
      prev.includes(id) ? prev.filter((s) => s !== id) : [...prev, id]
    );
  };

  const handleSave = async () => {
    const payload = selected.map((id) => ({ scheduleId: id }));
    await postSchedules.mutate(payload);
  };

  return (
    <Dialog open={isOpen} onClose={toggleIsOpen} maxWidth="sm" fullWidth>
      <DialogTitle sx={{ fontWeight: "bold", fontSize: "1.5rem" }}>
        Asignar Horarios
      </DialogTitle>
      <DialogContent dividers>
        {isLoading ? (
          <Box display="flex" justifyContent="center" py={4}>
            <CircularProgress />
          </Box>
        ) : (
          <Stack spacing={2}>
            {schedules?.length === 0 && (
              <Typography variant="body1" color="text.secondary">
                No hay horarios disponibles.
              </Typography>
            )}
            {schedules?.map((schedule) => (
              <Box
                key={schedule.id}
                sx={{
                  p: 2,
                  borderRadius: 1,
                  border: "1px solid",
                  borderColor: selected.includes(schedule.id!) ? "primary.main" : "divider",
                  backgroundColor: selected.includes(schedule.id!) ? "rgba(25, 118, 210, 0.1)" : "transparent",
                  cursor: "pointer",
                  "&:hover": { backgroundColor: "rgba(25, 118, 210, 0.15)" },
                }}
                onClick={() => toggle(schedule.id!)}
              >
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={selected.includes(schedule.id!)}
                      onChange={() => toggle(schedule.id!)}
                    />
                  }
                  label={
                    <Typography>
                      <strong>{dayNames[schedule.day ?? 0]}</strong> — {schedule.startHour} - {schedule.endHour}
                    </Typography>
                  }
                />
              </Box>
            ))}
          </Stack>
        )}
      </DialogContent>
      <DialogActions sx={{ px: 3, py: 2 }}>
        <Button onClick={toggleIsOpen} color="inherit">
          Cancelar
        </Button>
        <Button
          variant="contained"
          onClick={handleSave}
          disabled={selected.length === 0 || postSchedules.status === "pending"}
        >
          {postSchedules.status === "pending" ? "Asignando..." : "Asignar horarios"}
        </Button>
      </DialogActions>
    </Dialog>
  );
};
