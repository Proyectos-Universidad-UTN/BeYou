"use client";

import { Box, Button, Typography, CircularProgress } from "@mui/material";
import { Modal } from "../Modal/Modal";

interface ConfirmModalProps {
  isModalOpen: boolean;
  toggleIsOpen: () => void;
  onConfirm: () => void;
  onCancel: () => void;
  confirmMessage: string;
  secondaryMessage: string;
  isLoading?: boolean;
}

export const ConfirmModal = ({
  isModalOpen,
  toggleIsOpen,
  onConfirm,
  onCancel,
  confirmMessage,
  secondaryMessage,
  isLoading = false,
}: ConfirmModalProps) => {
  return (
    <Modal
      isOpen={isModalOpen}
      toggleIsOpen={toggleIsOpen}
      sx={{
        width: { xs: "90vw", sm: "400px" },
        borderRadius: 2,
        p: 3,
      }}
    >
      <Box
        display="flex"
        flexDirection="column"
        alignItems="center"
        textAlign="center"
        gap={2}
      >
        <Typography variant="h5" fontWeight="bold" color="text.primary">
          {confirmMessage}
        </Typography>

        <Typography variant="body1" color="text.secondary" sx={{ mb: 3 }}>
          {secondaryMessage}
        </Typography>

        <Box
          display="flex"
          gap={2}
          justifyContent="center"
          width="100%"
        >
          <Button
            variant="outlined"
            onClick={onCancel}
            disabled={isLoading}
            fullWidth
          >
            Cancelar
          </Button>

          <Button
            variant="contained"
            onClick={onConfirm}
            disabled={isLoading}
            fullWidth
            startIcon={isLoading ? <CircularProgress size={18} /> : null}
          >
            {isLoading ? "Confirmando..." : "Confirmar"}
          </Button>
        </Box>
      </Box>
    </Modal>
  );
};
