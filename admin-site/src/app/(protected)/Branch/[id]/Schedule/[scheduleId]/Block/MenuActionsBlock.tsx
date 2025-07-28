"use client";

import { useState } from "react";
import { useRouter, useParams } from "next/navigation";
import { Edit } from "@mui/icons-material";
import { Menu, MenuItem } from "@mui/material";
import { OptionsBullet } from "@/components/Table/OptionsBullet";

interface MenuActionsBlockProps {
  id: number; 
}

export const MenuActionsBlock = ({ id }: MenuActionsBlockProps) => {
  const router = useRouter();
  const params = useParams(); 
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
        <MenuItem
          onClick={() => {
            handleMenuClose();
            router.push(`/Branch/${params.id}/Schedule/${params.scheduleId}/Block/${id}`);
          }}
        >
          <Edit fontSize="small" style={{ marginRight: 8 }} />
          Editar
        </MenuItem>
      </Menu>
    </>
  );
};
