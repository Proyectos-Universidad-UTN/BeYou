"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { Edit, Delete } from "@mui/icons-material";
import { Menu, MenuItem } from "@mui/material";
import { OptionsBullet } from "@/components/Table/OptionsBullet";

interface ProductMenuActionsProps {
    id: number;
}

export const ProductMenuActions = ({ id }: ProductMenuActionsProps) => {
    const router = useRouter();
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleMenuOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.stopPropagation();
        setAnchorEl(event.currentTarget);
    };

    const handleMenuClose = () => {
        setAnchorEl(null);
    };

    return (
        <>
            <OptionsBullet handleMenuOpen={handleMenuOpen} />
            <Menu
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={handleMenuClose}
                anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
                transformOrigin={{ vertical: "top", horizontal: "right" }}
                onClick={(e) => e.stopPropagation()}
            >
                <MenuItem onClick={() => {
                    handleMenuClose();
                    router.push(`/Product/${id}`);
                }}>
                    <Edit fontSize="small" style={{ marginRight: 8 }} />
                    Editar
                </MenuItem>
                <MenuItem onClick={() => {
                    handleMenuClose();
                    // Lógica para eliminar o mostrar modal
                    alert(`Eliminar producto con ID: ${id}`);
                }}>
                    <Delete fontSize="small" style={{ marginRight: 8 }} />
                    Eliminar
                </MenuItem>
            </Menu>
        </>
    );
};
