"use client";

import { useMemo, useState } from "react";
import { Box, Typography, Menu, MenuItem } from "@mui/material";
import { CreateButton } from "@/components/Button/CreateButton";
import { type GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import { OptionsBullet } from "@/components/Table/OptionsBullet";
import { Edit, Delete } from "@mui/icons-material";
import { ConfirmModal } from "@/components/ConfirmModal/ConfirmModal";
import { useSnackbar } from "@/stores/useSnackbar";
import { UseDeleteVendor } from "@/hooks/api-beyou/vendor/UseDeleteVendorById";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { getErrorMessage } from "@/utils/util";

export interface VendorTableItem {
  id: number;
  name: string;
  socialReason: string;
  fiscalNumber: string;
  telephone: number;
  email: string;
  districtId: number;
}

interface VendorTableProps {
  vendors: VendorTableItem[];
}

export const VendorTable = ({ vendors }: VendorTableProps) => {
  const { setMessage } = useSnackbar();

  const [openModal, setOpenModal] = useState(false);
  const [vendorToDelete, setVendorToDelete] = useState<VendorTableItem | null>(null);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedVendor, setSelectedVendor] = useState<VendorTableItem | null>(null);

  const { mutate: deleteVendor, isPending: isDeleting } = UseDeleteVendor({
    onSuccess: () => {
      setMessage("Proveedor eliminado exitosamente", "success");
      setOpenModal(false);
      setVendorToDelete(null);
    },
    onError: (error) => {
      const message = getErrorMessage(error);
      setMessage(message, "error");
    },
  });

  const handleDeleteClick = (vendor: VendorTableItem) => {
    setVendorToDelete(vendor);
    setOpenModal(true);
  };

  const handleConfirmDelete = () => {
    if (vendorToDelete) {
      deleteVendor(vendorToDelete.id);
    }
  };

  const handleCloseModal = () => {
    setOpenModal(false);
    setVendorToDelete(null);
  };

  const handleMenuOpen = (
    event: React.MouseEvent<HTMLButtonElement>,
    vendor: VendorTableItem
  ) => {
    event.stopPropagation();
    setAnchorEl(event.currentTarget);
    setSelectedVendor(vendor);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedVendor(null);
  };

  const columns: GridColDef<VendorTableItem>[] = useMemo(
    () => [
      { field: "name", headerName: "Nombre Comercial", flex: 1, minWidth: 150 },
      { field: "socialReason", headerName: "Razón Social", flex: 1.2, minWidth: 180 },
      { field: "fiscalNumber", headerName: "Número Fiscal", flex: 1, minWidth: 150 },
      { field: "telephone", headerName: "Teléfono", flex: 0.8, minWidth: 130 },
      { field: "email", headerName: "Correo Electrónico", flex: 1.2, minWidth: 200 },
      {
        field: "actions",
        headerName: "Acciones",
        sortable: false,
        filterable: false,
        width: 80,
        renderCell: (params: GridRenderCellParams<VendorTableItem>) => (
          <OptionsBullet handleMenuOpen={(e) => handleMenuOpen(e, params.row)} />
        ),
      },
    ],
    []
  );

  if (isDeleting) return <CircularLoadingProgress />;

  return (
    <>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
        <Typography variant="h6" fontWeight="bold">
          Lista de Proveedores
        </Typography>
        <CreateButton href="/Vendor/new" label="Crear" />
      </Box>

      <DataTableWrapper columns={columns} data={vendors} />

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
            if (selectedVendor) {
              window.location.href = `/Vendor/${selectedVendor.id}`;
            }
          }}
        >
          <Edit fontSize="small" style={{ marginRight: 8 }} />
          Editar
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose();
            if (selectedVendor) handleDeleteClick(selectedVendor);
          }}
        >
          <Delete fontSize="small" style={{ marginRight: 8 }} />
          Eliminar
        </MenuItem>
      </Menu>

      <ConfirmModal
        isModalOpen={openModal}
        toggleIsOpen={handleCloseModal}
        onConfirm={handleConfirmDelete}
        onCancel={handleCloseModal}
        confirmMessage="Confirmar Eliminación"
        secondaryMessage={`¿Estás seguro de que quieres eliminar al proveedor ${vendorToDelete?.name}? Esta acción no se puede deshacer.`}
      />
    </>
  );
};
