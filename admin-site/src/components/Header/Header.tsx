"use client";

import React from "react";
import { IconButton, Avatar, Menu, MenuItem, Typography, ListItemIcon, ListItemText } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import NotificationsIcon from "@mui/icons-material/Notifications";
// AccountCircle no está siendo usado directamente en este componente, pero lo mantengo si lo necesitas para el futuro.
import AccountCircle from "@mui/icons-material/AccountCircle";
import SettingsIcon from "@mui/icons-material/Settings";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
import Link from "next/link";
import { useMediaQuery, useTheme } from "@mui/material";
import { Person } from "@mui/icons-material";
import { useRouter } from "next/navigation"; // Importamos useRouter para la navegación

interface HeaderProps {
  onMenuClick?: () => void;
}

export default function Header({ onMenuClick }: HeaderProps) {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const theme = useTheme();
  const router = useRouter(); // Inicializamos el router para la navegación programática

  const isSmallScreen = useMediaQuery(theme.breakpoints.down("md"));

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
      {isSmallScreen && (
        <IconButton
          edge="start"
          color="inherit"
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
              fontSize: isSmallScreen ? "1rem" : "1.25rem", 
              whiteSpace: "nowrap", 
              overflow: "hidden", 
              textOverflow: "ellipsis",
            }}
          >
            Beauty and Style App
          </Typography>
        </Link>
      </div>

      {!isSmallScreen && (
        <div className="flex-grow mx-4">
        </div>
      )}

      <div className="flex items-center">
        <IconButton color="inherit" sx={{ color: "#666" }}>
          <NotificationsIcon />
        </IconButton>

        <IconButton
          size="large"
          aria-label="account of current user"
          aria-controls="menu-appbar"
          aria-haspopup="true"
          onClick={handleMenu}
          color="inherit"
        >
          <Avatar sx={{ bgcolor: "#ffb3c6", color: "#b8860b" }}>U</Avatar>{" "}
        </IconButton>
        <Menu
          id="menu-appbar"
          anchorEl={anchorEl}
          anchorOrigin={{
            vertical: "top",
            horizontal: "right",
          }}
          keepMounted
          transformOrigin={{
            vertical: "top",
            horizontal: "right",
          }}
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