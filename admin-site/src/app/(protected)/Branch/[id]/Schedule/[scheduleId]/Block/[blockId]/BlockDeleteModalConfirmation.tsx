"use client";

import { useState } from "react";
import { Button, Stack } from "@mui/material";
import { Modal } from "@/components/Modal/Modal";
import { ModalFooter } from "@/components/Modal/ModalFooter";
import { ModalHeader } from "@/components/Modal/ModalHeader";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { useRouter } from "next/navigation";
import { UseDeleteScheduleBlock } from "@/hooks/api-beyou/branch/schedule/block/UseDeleteScheduleBlockById";

interface BlockDeleteModalConfirmationProps {
  isModalOpen: boolean;
  toggleIsOpen: () => void;
  blockId: number;
  title: string;
}

export const BlockDeleteModalConfirmation = ({
  isModalOpen,
  toggleIsOpen,
  blockId,
  title,
}: BlockDeleteModalConfirmationProps) => {
  const router = useRouter();
  const [loading, setLoading] = useState(false);

  const { mutate: deleteBlock } = UseDeleteScheduleBlock(
    UseMutationCallbacks("Bloqueo eliminado exitosamente", "/Block", () => {
      toggleIsOpen();
      router.replace("/Block");
    })
  );

  const handleConfirm = () => {
    setLoading(true);
    deleteBlock(blockId);
  };

  return (
    <Modal isOpen={isModalOpen} toggleIsOpen={toggleIsOpen}>
      <ModalHeader
        toggleIsOpen={toggleIsOpen}
        title="¿Está seguro que desea eliminar el bloqueo?"
        subTitle={title}
      />
      <ModalFooter>
        <Stack direction="row" spacing={2}>
          <Button variant="outlined" onClick={toggleIsOpen}>
            Cancelar
          </Button>
          <Button
            variant="contained"
            onClick={handleConfirm}
            disabled={loading}
          >
            Confirmar
          </Button>
        </Stack>
      </ModalFooter>
    </Modal>
  );
};
