"use client";

import React, { useState } from "react";
import {
  Drawer,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Collapse,
  IconButton,
} from "@mui/material";
import { usePathname } from "next/navigation";
import {
  Home,
  Store,
  EventNote,
  Description,
  LocalShipping,
  Category,
  Build,
  Settings,
  Person,
  Security,
  ExpandLess,
  ExpandMore,
  Menu, 
} from "@mui/icons-material";
import Link from "next/link";
import { useMediaQuery, useTheme } from "@mui/material"; 

interface MenuItem {
  label: string;
  icon?: React.ReactNode;
  path?: string;
  children?: MenuItem[];
}


interface SidebarProps {
  isOpen?: boolean; 
  onClose?: () => void; 
}

const iconMap: { [key: string]: React.ReactNode } = {
  Inicio: <Home />,
  Sucursal: <Store />,
  Sucursales: <Store />,
  Gestiones: <Build />,
  Reservas: <EventNote />,
  Proforma: <Description />,
  Proveedores: <LocalShipping />,
  Productos: <Category />,
  Servicios: <Build />,
  Configuración: <Settings />,
  Perfil: <Person />,
  Seguridad: <Security />,
};

const menuItems: MenuItem[] = [
  { label: "Inicio", path: "/" },
  {
    label: "Sucursal",
    children: [{ label: "Sucursales", path: "/sucursales" }],
  },
  {
    label: "Gestiones",
    children: [
      { label: "Reservas", path: "/gestiones/reservas" },
      { label: "Proforma", path: "/gestiones/proforma" },
    ],
  },
  { label: "Proveedores", path: "/proveedores" },
  { label: "Productos", path: "/productos" },
  { label: "Servicios", path: "/servicios" },
  {
    label: "Configuración",
    children: [
      { label: "Perfil", path: "/configuracion/perfil" },
      { label: "Seguridad", path: "/configuracion/seguridad" },
    ],
  },
];

export default function Sidebar({ isOpen, onClose }: SidebarProps) {
  const pathname = usePathname();
  const theme = useTheme();
  const isSmallScreen = useMediaQuery(theme.breakpoints.down("md"));

  const [expandedMenus, setExpandedMenus] = useState<{
    [key: string]: boolean;
  }>({});

  const handleToggle = (label: string) => {
    setExpandedMenus((prev) => ({ ...prev, [label]: !prev[label] }));
  };

  const parentStyles = {
    cursor: "pointer",
    padding: "12px 16px",
    borderRadius: "8px",
    marginBottom: 1,
    transition: "background-color 0.3s, color 0.3s",
    display: "flex",
    alignItems: "center",
    justifyContent: "space-between",
  };

  const parentHover = {
    backgroundColor: "#ffd966",
    color: "#000000",
  };

  const childStyles = {
    fontSize: "0.85rem",
    borderRadius: "6px",
    marginBottom: 2,
    backgroundColor: "transparent",
    color: "#444444",
    transition: "background-color 0.3s, color 0.3s",
    display: "flex",
    alignItems: "center",
    justifyContent: "flex-start",
    paddingLeft: "24px",
  };

  const childHover = {
    backgroundColor: "#ffe8a1",
    color: "#000000",
  };

  const header = (
    <div
      style={{
        fontWeight: "bold",
        fontSize: "1.25rem",
        textAlign: "center",
        padding: "16px 0",
        color: "#b8860b",
        backgroundColor: "#ffb3c6",
        letterSpacing: "1px",
        borderBottom: "1px solidrgb(255, 255, 255)",
      }}
    >
    </div>
  );

  const renderMenuItem = (item: MenuItem) => {
    const isActive = pathname === item.path;
    const isExpanded = expandedMenus[item.label];
    const icon = iconMap[item.label] || item.icon;

    if (item.children) {
      return (
        <React.Fragment key={item.label}>
          <ListItem
            onClick={() => handleToggle(item.label)}
            sx={{
              ...parentStyles,
              backgroundColor:
                isActive || isExpanded ? "#f9d371" : "transparent",
              color: isActive || isExpanded ? "#222222" : "#333333",
              "&:hover": parentHover,
              userSelect: "none",
            }}
          >
            <ListItemIcon
              sx={{ color: isActive || isExpanded ? "#b8860b" : "#666666" }}
            >
              {icon}
            </ListItemIcon>
            <ListItemText primary={item.label} />
            {isExpanded ? (
              <ExpandLess sx={{ color: "#b8860b" }} />
            ) : (
              <ExpandMore sx={{ color: "#b8860b" }} />
            )}
          </ListItem>
          <Collapse in={isExpanded} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>
              {item.children.map((child) => {
                const childActive = pathname === child.path;
                const childIcon = iconMap[child.label] || child.icon;
                return (
                  <Link
                    href={child.path!}
                    key={child.label}
                    passHref
                    legacyBehavior
                  >
                    <a style={{ textDecoration: "none", display: "block" }}>
                      <ListItem

                        onClick={isSmallScreen ? onClose : undefined}
                        sx={{
                          ...childStyles,
                          backgroundColor: childActive
                            ? "#f9d371"
                            : "transparent",
                          color: childActive ? "#222222" : "#444444",
                          "&:hover": childHover,
                        }}
                      >
                        <ListItemIcon
                          sx={{
                            color: childActive ? "#b8860b" : "#888888",
                            minWidth: 36,
                          }}
                        >
                          {childIcon}
                        </ListItemIcon>
                        <ListItemText
                          primary={child.label}
                          primaryTypographyProps={{ fontSize: "0.85rem" }}
                        />
                      </ListItem>
                    </a>
                  </Link>
                );
              })}
            </List>
          </Collapse>
        </React.Fragment>
      );
    }

    return (
      <Link href={item.path!} key={item.label} passHref legacyBehavior>
        <a style={{ textDecoration: "none" }}>
          <ListItem
  
            onClick={isSmallScreen ? onClose : undefined}
            sx={{
              ...parentStyles,
              backgroundColor: isActive ? "#f9d371" : "transparent",
              color: isActive ? "#222222" : "#333333",
              "&:hover": parentHover,
            }}
          >
            <ListItemIcon sx={{ color: isActive ? "#b8860b" : "#666666" }}>
              {icon}
            </ListItemIcon>
            <ListItemText primary={item.label} />
          </ListItem>
        </a>
      </Link>
    );
  };

  if (isSmallScreen) {
    return (
      <Drawer
        variant="temporary"
        open={isOpen} 
        onClose={onClose} 
        className="md:hidden"
        classes={{ paper: "w-64" }}
        PaperProps={{ style: { backgroundColor: "#ffb3c6", width: "75%" } }}
      >
        {header}
        <List>{menuItems.map(renderMenuItem)}</List>
      </Drawer>
    );
  }

  return (
    <div
      className="fixed h-full shadow w-64" 
      style={{ backgroundColor: "#ffe4ec" }}
    >
      {header}
      <List>{menuItems.map(renderMenuItem)}</List>
    </div>
  );
}