"use client";

import { useState } from "react";
import {
  Checkbox,
  FormControlLabel,
  Stack,
  Button,
  CircularProgress,
} from "@mui/material";
import { Modal } from "@/components/Modal/Modal";
import { ModalHeader } from "@/components/Modal/ModalHeader";
import { ModalBody } from "@/components/Modal/ModalBody";
import { ModalFooter } from "@/components/Modal/ModalFooter";
import { UseGetSchedules } from "@/hooks/api-beyou/schedule/UseGetSchedules";
import { UsePostBranchSchedules } from "@/hooks/api-beyou/branch/schedule/UsePostBranchSchedules";
import { useSnackbar } from "@/stores/useSnackbar";

interface Props {
  isOpen: boolean;
  toggleIsOpen: () => void;
  branchId: number;
}

export const AddExistingSchedulesModal = ({
  isOpen,
  toggleIsOpen,
  branchId,
}: Props) => {
  const { data: schedules, isLoading } = UseGetSchedules();
  const [selected, setSelected] = useState<number[]>([]);
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

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

  const handleSave = () => {
    const payload = selected.map((id) => ({ scheduleId: id }));
    postSchedules.mutate(payload);
  };

  return (
    <Modal isOpen={isOpen} toggleIsOpen={toggleIsOpen} showIconClose>
      <ModalHeader title="Asignar Horario" toggleIsOpen={toggleIsOpen} showIconClose />
      <ModalBody heightModal="60">
        {isLoading ? (
          <CircularProgress />
        ) : (
          <Stack spacing={1}>
            {schedules?.map((schedule) => (
              <FormControlLabel
                key={schedule.id}
                control={
                  <Checkbox
                    checked={selected.includes(schedule.id!)}
                    onChange={() => toggle(schedule.id!)}
                  />
                }
                label={`${schedule.day} | ${schedule.startHour} - ${schedule.endHour}`}
              />
            ))}
          </Stack>
        )}
      </ModalBody>
      <ModalFooter>
        <Button onClick={toggleIsOpen}>Cancelar</Button>
        <Button
          variant="contained"
          onClick={handleSave}
          disabled={selected.length === 0 || postSchedules.status === "pending"}
        >
          {postSchedules.status === "pending" ? "Asignando..." : "Asignar horarios"}
        </Button>
      </ModalFooter>
    </Modal>
  );
};
