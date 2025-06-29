"use client";

import { useMemo, useState } from "react";
import { Box, Typography, Menu, MenuItem } from "@mui/material";
import { CreateButton } from "@/components/Button/CreateButton";
import { type GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import DataTableWrapper from "@/components/Table/DataTableWrapper";
import { OptionsBullet } from "@/components/Table/OptionsBullet";
import { Edit, Delete } from "@mui/icons-material";
import { ConfirmModal } from "@/components/ConfirmModal/ConfirmModal";
import { getErrorMessage } from "@/utils/util";
import { useSnackbar } from "@/stores/useSnackbar";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { UseDeleteProductById } from "@/hooks/api-beyou/product/UseDeleteProductById";

const CATEGORY_MAP: Record<number, string> = {
  1: "Ropa Deportiva",
  2: "Ropa Casual",
};

const UNIT_MEASURE_MAP: Record<number, string> = {
  1: "un",
  2: "kg",
};

export interface ProductTableItem {
  id: number;
  name: string;
  description: string;
  brand: string;
  price: number;
  sku: string;
  categoryId: number;
  unitMeasureId: number;
  active: boolean;
}

interface ProductTableProps {
  products: ProductTableItem[];
}

export const ProductTable = ({ products }: ProductTableProps) => {
  const { openSnackbar } = useSnackbar() as any;

  const { mutate: deleteProduct, isPending: isDeleting } =
    UseDeleteProductById();
  const [openModal, setOpenModal] = useState(false);
  const [productToDelete, setProductToDelete] =
    useState<ProductTableItem | null>(null);

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedProduct, setSelectedProduct] =
    useState<ProductTableItem | null>(null);

  const handleDeleteClick = (product: ProductTableItem) => {
    setProductToDelete(product);
    setOpenModal(true);
  };

  const handleConfirmDelete = async () => {
    if (productToDelete) {
      try {
        await deleteProduct(productToDelete.id);
        openSnackbar("Producto eliminado con éxito", "success");
      } catch (error: unknown) {
        openSnackbar(getErrorMessage(error as any), "error");
      } finally {
        setOpenModal(false);
        setProductToDelete(null);
      }
    }
  };

  const handleCloseModal = () => {
    setOpenModal(false);
    setProductToDelete(null);
  };

  const handleMenuOpen = (
    event: React.MouseEvent<HTMLButtonElement>,
    product: ProductTableItem
  ) => {
    event.stopPropagation();
    setAnchorEl(event.currentTarget);
    setSelectedProduct(product);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedProduct(null);
  };

  const columns: GridColDef<ProductTableItem>[] = useMemo(
    () => [
      { field: "name", headerName: "Nombre", flex: 1, minWidth: 150 },
      {
        field: "description",
        headerName: "Descripción",
        flex: 2,
        minWidth: 200,
      },
      { field: "brand", headerName: "Marca", flex: 1, minWidth: 120 },
      { field: "sku", headerName: "SKU", flex: 1, minWidth: 120 },
      {
        field: "price",
        headerName: "Precio",
        flex: 1,
        minWidth: 120,
        valueFormatter: (params: { value: number | null | undefined }) =>
          `₡${(params.value ?? 0).toLocaleString("es-CR", {
            minimumFractionDigits: 2,
          })}`,
      },
      {
        field: "categoryId",
        headerName: "Categoría",
        flex: 1,
        minWidth: 150,
        valueFormatter: (params: { value: number }) =>
          CATEGORY_MAP[params.value] || "N/A",
      },
      {
        field: "unitMeasureId",
        headerName: "Unidad",
        flex: 0.5,
        minWidth: 100,
        valueFormatter: (params: { value: number }) =>
          UNIT_MEASURE_MAP[params.value] || "N/A",
      },
      {
        field: "active",
        headerName: "Activo",
        flex: 0.5,
        minWidth: 100,
        valueFormatter: (params: { value: boolean }) =>
          params.value ? "Sí" : "No",
      },
      {
        field: "actions",
        headerName: "Acciones",
        sortable: false,
        filterable: false,
        width: 80,
        renderCell: (params: GridRenderCellParams<ProductTableItem>) => (
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
      <Box
        display="flex"
        justifyContent="space-between"
        alignItems="center"
        mb={2}
      >
        <Typography variant="h6" fontWeight="bold">
          Lista de Productos
        </Typography>
        <CreateButton href="/Product/new" label="Crear" />
      </Box>

      <DataTableWrapper columns={columns} data={products} />

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
            if (selectedProduct) {
              window.location.href = `/Product/${selectedProduct.id}`;
            }
          }}
        >
          <Edit fontSize="small" style={{ marginRight: 8 }} />
          Editar
        </MenuItem>
        <MenuItem
          onClick={() => {
            handleMenuClose();
            if (selectedProduct) handleDeleteClick(selectedProduct);
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
        secondaryMessage={`¿Estás seguro de que quieres eliminar el producto ${productToDelete?.name}? Esta acción no se puede deshacer.`}
      />
    </>
  );
};
