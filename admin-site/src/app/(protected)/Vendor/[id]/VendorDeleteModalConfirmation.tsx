"use client";

import { useState } from "react";
import { Button, Stack } from "@mui/material";
import { Modal } from "@/components/Modal/Modal";
import { ModalFooter } from "@/components/Modal/ModalFooter";
import { ModalHeader } from "@/components/Modal/ModalHeader";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UseDeleteVendor } from "@/hooks/api-beyou/vendor/UseDeleteVendorById";

interface VendorDeleteModalConfirmationProps {
  isModalOpen: boolean;
  toggleIsOpen: () => void;
  vendorId: number;
  title: string;
}

export const VendorDeleteModalConfirmation = ({
  isModalOpen,
  toggleIsOpen,
  vendorId,
  title,
}: VendorDeleteModalConfirmationProps) => {
  const [loading, setLoading] = useState(false);

  const { mutate: deleteVendor } = UseDeleteVendor(
    UseMutationCallbacks("Proveedor eliminado exitosamente", "/Vendor", toggleIsOpen)
  );

  const handleConfirm = () => {
    setLoading(true);
    deleteVendor(vendorId);
  };

  return (
    <Modal
      isOpen={isModalOpen}
      toggleIsOpen={toggleIsOpen}
      sx={{
        width: { xs: "90vw", sm: "50%" },
        height: "auto",
      }}
    >
      <ModalHeader
        toggleIsOpen={toggleIsOpen}
        title="¿Está seguro que desea eliminar el proveedor?"
        subTitle={`Nombre: ${title}`}
      />
      <ModalFooter>
        <Stack direction="row" spacing={2}>
          <Button variant="outlined" onClick={toggleIsOpen}>
            Cancelar
          </Button>
          <Button
            loading={loading}
            loadingPosition="start"
            variant="contained"
            onClick={handleConfirm}
          >
            Confirmar
          </Button>
        </Stack>
      </ModalFooter>
    </Modal>
  );
};
