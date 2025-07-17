"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { Edit, Delete } from "@mui/icons-material";
import { Menu, MenuItem } from "@mui/material";
import { OptionsBullet } from "@/components/Table/OptionsBullet";
import { ConfirmModal } from "@/components/ConfirmModal/ConfirmModal";
import { useSnackbar } from "@/stores/useSnackbar";
import { UseDeleteVendor } from "@/hooks/api-beyou/vendor/UseDeleteVendorById";
import { getErrorMessage } from "@/utils/util";

interface MenuActionsProps {
  id: number;
  name: string;
}

export const MenuActions = ({ id, name }: MenuActionsProps) => {
  const router = useRouter();
  const { setMessage } = useSnackbar();

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedRowId, setSelectedRowId] = useState<number | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleMenuOpen = (event: React.MouseEvent<HTMLButtonElement>, rowId: number) => {
    event.stopPropagation();
    setAnchorEl(event.currentTarget);
    setSelectedRowId(rowId);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedRowId(null);
  };

  const handleConfirmDelete = () => {
    if (selectedRowId !== null) {
      deleteVendor(selectedRowId);
    }
  };

  const { mutate: deleteVendor } = UseDeleteVendor({
    onSuccess: () => {
      setMessage("Proveedor eliminado exitosamente", "success");
      setIsModalOpen(false);
    },
    onError: (error) => {
      const message = getErrorMessage(error);
      setMessage(message, "error");
    },
  });

  return (
    <>
      <OptionsBullet handleMenuOpen={(e) => handleMenuOpen(e, id)} />
      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={handleMenuClose}
        anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
        transformOrigin={{ vertical: "top", horizontal: "right" }}
        onClick={(e) => e.stopPropagation()}
      >
        <MenuItem
          onClick={() => {
            handleMenuClose();
            router.push(`/Vendor/${selectedRowId}`);
          }}
        >
          <Edit fontSize="small" style={{ marginRight: 8 }} />
          Editar
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose();
            setIsModalOpen(true);
          }}
        >
          <Delete fontSize="small" style={{ marginRight: 8 }} />
          Eliminar
        </MenuItem>
      </Menu>

      <ConfirmModal
        isModalOpen={isModalOpen}
        toggleIsOpen={() => setIsModalOpen(false)}
        onConfirm={handleConfirmDelete}
        onCancel={() => setIsModalOpen(false)}
        confirmMessage="Confirmar Eliminación"
        secondaryMessage={`¿Estás seguro de que quieres eliminar al proveedor ${name}? Esta acción no se puede deshacer.`}
      />
    </>
  );
};
