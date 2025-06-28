"use client";

import { useMemo, useState } from "react";
import { Box, Button, Menu, MenuItem } from "@mui/material";
import { type GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import { OptionsBullet } from "@/components/Table/OptionsBullet";
import { Edit, Delete } from "@mui/icons-material";
import { ConfirmModal } from "@/components/ConfirmModal/ConfirmModal";
import { getErrorMessage } from "@/utils/util";
import { useSnackbar } from "@/stores/useSnackbar";
import { UseDeleteUser } from "@/hooks/api-beyou/user/UseDeleteUserById";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";

// Mapas para roles y géneros
const GENDER_MAP: Record<number, string> = {
  1: "Hombre",
  2: "Mujer",
  3: "Otro",
};

const ROLE_MAP: Record<number, string> = {
  1: "Admin",
  2: "Usuario",
  3: "Editor",
};

export interface UserTableItem {
  id: string | number | undefined; 
  firstName?: string | null;
  lastName?: string | null;
  email?: string | null;
  telephone?: number;
  roleId?: number;
  genderId?: number;
}

interface UserTableProps {
  users: UserTableItem[];
}

export const UserTable = ({ users }: UserTableProps) => {
  const { openSnackbar } = useSnackbar() as any;

  const { mutate: deleteUser, isPending: isDeleting } = UseDeleteUser();
  const [openModal, setOpenModal] = useState(false);
  const [userToDelete, setUserToDelete] = useState<UserTableItem | null>(null);

  // Estado para menú contextual
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedUser, setSelectedUser] = useState<UserTableItem | null>(null);

  const handleDeleteClick = (user: UserTableItem) => {
    setUserToDelete(user);
    setOpenModal(true);
  };

  const handleConfirmDelete = async () => {
    if (userToDelete) {
      try {
        await deleteUser(Number(userToDelete.id));
        openSnackbar("Usuario eliminado con éxito", "success");
      } catch (error) {
        openSnackbar(getErrorMessage(error as any), "error");
      } finally {
        setOpenModal(false);
        setUserToDelete(null);
      }
    }
  };

  const handleCloseModal = () => {
    setOpenModal(false);
    setUserToDelete(null);
  };

  const handleMenuOpen = (
    event: React.MouseEvent<HTMLButtonElement>,
    user: UserTableItem
  ) => {
    event.stopPropagation();
    setAnchorEl(event.currentTarget);
    setSelectedUser(user);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedUser(null);
  };

  const columns: GridColDef<UserTableItem>[] = useMemo(
    () => [
      { field: "firstName", headerName: "Nombre", flex: 1, minWidth: 150 },
      { field: "lastName", headerName: "Apellido", flex: 1, minWidth: 150 },
      {
        field: "email",
        headerName: "Correo Electrónico",
        flex: 1.5,
        minWidth: 200,
      },
      { field: "telephone", headerName: "Teléfono", flex: 1, minWidth: 150 },
      {
        field: "roleId",
        headerName: "Rol",
        flex: 0.8,
        minWidth: 120,
        valueFormatter: (params: { value: number }) =>
          ROLE_MAP[params.value] || "N/A",
      },
      {
        field: "genderId",
        headerName: "Género",
        flex: 0.8,
        minWidth: 120,
        valueFormatter: (params: { value: number }) =>
          GENDER_MAP[params.value] || "N/A",
      },
      {
        field: "actions",
        headerName: "Acciones",
        sortable: false,
        filterable: false,
        width: 80,
        renderCell: (params: GridRenderCellParams<UserTableItem>) => (
          <OptionsBullet
            handleMenuOpen={(e) => handleMenuOpen(e, params.row)}
          />
        ),
      },
    ],
    []
  );

  if (isDeleting) {
    return <CircularLoadingProgress />;
  }

  return (
    <>
      <DataTableWrapper columns={columns} data={users} />
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
            if (selectedUser) {
              window.location.href = `/User/${selectedUser.id}`;
            }
          }}
        >
          <Edit fontSize="small" style={{ marginRight: 8 }} />
          Editar
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose();
            if (selectedUser) handleDeleteClick(selectedUser);
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
        secondaryMessage={`¿Estás seguro de que quieres eliminar al usuario ${userToDelete?.firstName} ${userToDelete?.lastName}? Esta acción no se puede deshacer.`}
      />
    </>
  );
};
