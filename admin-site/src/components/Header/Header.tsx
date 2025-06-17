"use client";

import React, { useState } from "react";
import {
  IconButton,
  Avatar,
  Menu,
  MenuItem,
  Typography,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import {
  Menu as MenuIcon,
  Notifications as NotificationsIcon,
  Person,
  Settings as SettingsIcon,
  ExitToApp as ExitToAppIcon,
} from "@mui/icons-material";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useIsMobile } from "@/hooks/UseIsMobile";

interface HeaderProps {
  onMenuClick?: () => void;
}

export default function Header({ onMenuClick }: HeaderProps) {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const router = useRouter();
  const isMobile = useIsMobile();

  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    handleClose();
    router.push("/Login");
  };

  return (
    <div className="flex items-center justify-between h-16 px-4 shadow-sm bg-white border-b border-gray-200">
      {isMobile && (
        <IconButton
          edge="start"
          aria-label="menu"
          onClick={onMenuClick}
          sx={{ mr: 2, color: "#b8860b" }}
        >
          <MenuIcon />
        </IconButton>
      )}

      <div className="flex-grow flex items-center">
        <Link href="/" passHref>
          <Typography
            variant="h6"
            component="a"
            sx={{
              fontWeight: "bold",
              color: "#b8860b",
              textDecoration: "none",
              fontSize: isMobile ? "1rem" : "1.25rem",
              whiteSpace: "nowrap",
              overflow: "hidden",
              textOverflow: "ellipsis",
            }}
          >
            Beauty and Style App
          </Typography>
        </Link>
      </div>

      <div className="hidden md:flex flex-grow mx-4" />

      <div className="flex items-center">
        <IconButton sx={{ color: "#666" }}>
          <NotificationsIcon />
        </IconButton>

        <IconButton
          size="large"
          aria-label="account of current user"
          aria-controls="menu-appbar"
          aria-haspopup="true"
          onClick={handleMenu}
        >
          <Avatar sx={{ bgcolor: "#ffb3c6", color: "#b8860b" }}>U</Avatar>
        </IconButton>

        <Menu
          id="menu-appbar"
          anchorEl={anchorEl}
          anchorOrigin={{ vertical: "top", horizontal: "right" }}
          keepMounted
          transformOrigin={{ vertical: "top", horizontal: "right" }}
          open={Boolean(anchorEl)}
          onClose={handleClose}
        >
          <MenuItem onClick={handleClose}>
            <ListItemIcon>
              <Person fontSize="small" />
            </ListItemIcon>
            <ListItemText primary="Mi Perfil" />
          </MenuItem>
          <MenuItem onClick={handleClose}>
            <ListItemIcon>
              <SettingsIcon fontSize="small" />
            </ListItemIcon>
            <ListItemText primary="Configuración" />
          </MenuItem>
          <MenuItem onClick={handleLogout}>
            <ListItemIcon>
              <ExitToAppIcon fontSize="small" />
            </ListItemIcon>
            <ListItemText primary="Cerrar Sesión" />
          </MenuItem>
        </Menu>
      </div>
    </div>
  );
}
