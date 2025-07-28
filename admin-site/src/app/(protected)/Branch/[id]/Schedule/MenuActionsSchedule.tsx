"use client";

import { useState } from "react";
import { useRouter, useParams } from "next/navigation";
import { Lock } from "@mui/icons-material";
import { Menu, MenuItem } from "@mui/material";
import { OptionsBullet } from "@/components/Table/OptionsBullet";

interface MenuActionsScheduleProps {
  id: number; // este es scheduleId
}

export const MenuActionsSchedule = ({ id }: MenuActionsScheduleProps) => {
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
            router.push(`/Branch/${params.id}/Schedule/${id}/Block`);
          }}
        >
          <Lock fontSize="small" style={{ marginRight: 8 }} />
          Bloqueo
        </MenuItem>
      </Menu>
    </>
  );
};
