"use client";

import { useState, useEffect, useMemo, useCallback } from "react";
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
import { UseGetBranchSchedules } from "@/hooks/api-beyou/branch/schedule/UseGetBranchSchedules";
import { UsePostBranchSchedules } from "@/hooks/api-beyou/branch/schedule/UsePostBranchSchedules";
import { useSnackbar } from "@/stores/useSnackbar";
import { BranchScheduleRequest } from "@/types/api-beyou";

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

export const AddExistingSchedulesModal = ({
  isOpen,
  toggleIsOpen,
  branchId,
}: Props) => {
  const { data: allSchedules, isLoading: isLoadingAll } = UseGetSchedules();
  const { data: assignedSchedules, isLoading: isLoadingAssigned } =
    UseGetBranchSchedules(branchId.toString());

  const [selected, setSelected] = useState<number[]>([]);
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  useEffect(() => {
    if (isOpen && Array.isArray(assignedSchedules)) {
      const assignedIds = assignedSchedules
        .map((s) => s.scheduleId)
        .filter((id): id is number => typeof id === "number");
      setSelected(assignedIds);
    } else if (!isOpen) {
      setSelected([]);
    }
  }, [isOpen, assignedSchedules]);

  const assignedScheduleIds = useMemo(
    () =>
      Array.isArray(assignedSchedules)
        ? assignedSchedules
            .map((s) => s.scheduleId)
            .filter((id): id is number => typeof id === "number")
        : [],
    [assignedSchedules]
  );

  const toggle = useCallback((id: number) => {
    setSelected((prev) =>
      prev.includes(id) ? prev.filter((s) => s !== id) : [...prev, id]
    );
  }, []);

  const postSchedules = UsePostBranchSchedules({
    branchId,
    onSuccess: () => {
      setSnackbarMessage("✅ Horarios asignados correctamente", "success");
      toggleIsOpen();
    },
    onError: (error) => {
      console.error("❌ Error al asignar horarios:", error);
      setSnackbarMessage("❌ Error al asignar horarios", "error");
    },
  });

  const handleSave = useCallback(async () => {
    const newSchedulesToAssign = selected.filter(
      (id) => !assignedScheduleIds.includes(id)
    );
    const payload: BranchScheduleRequest[] = newSchedulesToAssign.map((id) => ({
      scheduleId: id,
      branchId,
      branchScheduleBlocks: [], // 👈 esto evita que EF Core asuma null y requiera bloques
    }));
    if (payload.length > 0) {
      try {
        await postSchedules.mutateAsync(payload);
      } catch (err) {
        console.error("🚨 Error en mutateAsync:", err);
      }
    } else {
      setSnackbarMessage("ℹ️ No hay horarios nuevos para asignar", "info");
      toggleIsOpen();
    }
  }, [
    selected,
    assignedScheduleIds,
    postSchedules,
    setSnackbarMessage,
    toggleIsOpen,
    branchId,
  ]);

  const isLoading = isLoadingAll || isLoadingAssigned;

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
            {(!allSchedules || allSchedules.length === 0) && (
              <Typography variant="body1" color="text.secondary">
                No hay horarios disponibles.
              </Typography>
            )}
            {allSchedules?.map((schedule, idx) => {
              const id = schedule.id;
              if (typeof id !== "number") return null;

              return (
                <Box
                  key={id}
                  onClick={() => toggle(id)}
                  sx={{
                    p: 2,
                    borderRadius: 1,
                    border: "1px solid",
                    borderColor: selected.includes(id)
                      ? "primary.main"
                      : "divider",
                    backgroundColor: selected.includes(id)
                      ? "rgba(25, 118, 210, 0.1)"
                      : "transparent",
                    cursor: "pointer",
                    "&:hover": {
                      backgroundColor: "rgba(25, 118, 210, 0.15)",
                    },
                  }}
                >
                  <FormControlLabel
                    control={<Checkbox checked={selected.includes(id)} />}
                    label={
                      <Typography>
                        <strong>
                          {dayNames[(schedule.day as number) ?? 0]}
                        </strong>{" "}
                        — {schedule.startHour ?? "--:--"} -{" "}
                        {schedule.endHour ?? "--:--"}
                      </Typography>
                    }
                    sx={{ width: "100%", margin: 0 }}
                  />
                </Box>
              );
            })}
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
          disabled={
            selected.length === assignedScheduleIds.length ||
            postSchedules.isPending
          }
        >
          {postSchedules.isPending ? "Asignando..." : "Asignar horarios"}
        </Button>
      </DialogActions>
    </Dialog>
  );
};
