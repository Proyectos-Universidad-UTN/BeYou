"use client";

import { useState } from "react";
import { useRouter, useParams } from "next/navigation";
import { Lock } from "@mui/icons-material";
import { Menu, MenuItem } from "@mui/material";
import { OptionsBullet } from "@/components/Table/OptionsBullet";

interface MenuActionsScheduleProps {
  id: number;
}

export const MenuActionsSchedule = ({ id }: MenuActionsScheduleProps) => {
  const router = useRouter();
  const params = useParams();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedRowId, setSelectedRowId] = useState<number | null>(null);

  const handleMenuOpen = (
    event: React.MouseEvent<HTMLButtonElement>,
    id: number
  ) => {
    event.stopPropagation();
    setAnchorEl(event.currentTarget);
    setSelectedRowId(id);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setSelectedRowId(null);
  };

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
            router.push(`/Branch/${params.id}/Schedule/Block?id=${selectedRowId}`);
          }}
        >
          <Lock fontSize="small" style={{ marginRight: 8 }} />
          Bloqueo
        </MenuItem>
      </Menu>
    </>
  );
};
