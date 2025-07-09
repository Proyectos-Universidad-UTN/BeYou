"use client";

import { useMemo, useState } from "react";
import {
  Box,
  Typography,
  Menu,
  MenuItem,
  Button,
} from "@mui/material";
import { CreateButton } from "@/components/Button/CreateButton";
import { type GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import { OptionsBullet } from "@/components/Table/OptionsBullet";
import { Edit, Delete, Schedule } from "@mui/icons-material";
import { ConfirmModal } from "@/components/ConfirmModal/ConfirmModal";
import { useSnackbar } from "@/stores/useSnackbar";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { getErrorMessage } from "@/utils/util";
import { UseDeleteBranch } from "@/hooks/api-beyou/branch/UseDeleteBranchById";
import { useRouter } from "next/navigation";

export interface BranchTableItem {
  id: number;
  name: string;
  description: string;
  telephone: number;
  email: string;
  districtId: number;
  address?: string | null;
}

interface BranchTableProps {
  branches: BranchTableItem[];
}

export const BranchTable = ({ branches }: BranchTableProps) => {
  const { setMessage } = useSnackbar();
  const router = useRouter();

  const [openModalDelete, setOpenModalDelete] = useState(false);
  const [branchToDelete, setBranchToDelete] = useState<BranchTableItem | null>(null);

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedBranch, setSelectedBranch] = useState<BranchTableItem | null>(null);

  const { mutate: deleteBranch, isPending: isDeleting } = UseDeleteBranch({
    onSuccess: () => {
      setMessage("Sucursal eliminada exitosamente", "success");
      setOpenModalDelete(false);
      setBranchToDelete(null);
    },
    onError: (error) => {
      const message = getErrorMessage(error);
      setMessage(message, "error");
    },
  });

  const handleDeleteClick = (branch: BranchTableItem) => {
    setBranchToDelete(branch);
    setOpenModalDelete(true);
  };

  const handleConfirmDelete = () => {
    if (branchToDelete) {
      deleteBranch(branchToDelete.id);
    }
  };

  const handleCloseDeleteModal = () => {
    setOpenModalDelete(false);
    setBranchToDelete(null);
  };

  const handleMenuOpen = (
    event: React.MouseEvent<HTMLButtonElement>,
    branch: BranchTableItem
  ) => {
    event.stopPropagation();
    setAnchorEl(event.currentTarget);
    setSelectedBranch(branch);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedBranch(null);
  };

  const columns: GridColDef<BranchTableItem>[] = useMemo(
    () => [
      { field: "name", headerName: "Nombre", flex: 1, minWidth: 150 },
      { field: "description", headerName: "Descripción", flex: 1.2, minWidth: 180 },
      { field: "telephone", headerName: "Teléfono", flex: 0.8, minWidth: 130 },
      { field: "email", headerName: "Correo", flex: 1.2, minWidth: 200 },
      {
        field: "actions",
        headerName: "Acciones",
        sortable: false,
        filterable: false,
        width: 120,
        renderCell: (params: GridRenderCellParams<BranchTableItem>) => (
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
          Lista de Sucursales
        </Typography>
        <CreateButton href="/Branch/new" label="Crear" />
      </Box>

      <DataTableWrapper columns={columns} data={branches} />

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
            if (selectedBranch) {
              router.push(`/Branch/${selectedBranch.id}`);
            }
          }}
        >
          <Edit fontSize="small" style={{ marginRight: 8 }} />
          Editar
        </MenuItem>

        <MenuItem
          onClick={() => {
            handleMenuClose();
            if (selectedBranch) {
              router.push(`/Branch/${selectedBranch.id}/Schedule`);
            }
          }}
        >
          <Schedule fontSize="small" style={{ marginRight: 8 }} />
          Horario
        </MenuItem>

        <MenuItem
          onClick={() => {
            handleMenuClose();
            if (selectedBranch) handleDeleteClick(selectedBranch);
          }}
        >
          <Delete fontSize="small" style={{ marginRight: 8 }} />
          Eliminar
        </MenuItem>
      </Menu>

      <ConfirmModal
        isModalOpen={openModalDelete}
        toggleIsOpen={handleCloseDeleteModal}
        onConfirm={handleConfirmDelete}
        onCancel={handleCloseDeleteModal}
        confirmMessage="Confirmar Eliminación"
        secondaryMessage={`¿Estás seguro de que quieres eliminar la sucursal ${branchToDelete?.name}? Esta acción no se puede deshacer.`}
      />
    </>
  );
};
