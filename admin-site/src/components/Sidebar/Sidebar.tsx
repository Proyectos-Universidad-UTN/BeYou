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
  Settings,
  ExpandLess,
  ExpandMore,
  Menu,
  Build,
  Info,
} from "@mui/icons-material";
import Link from "next/link";

interface MenuItem {
  label: string;
  icon: React.ReactNode;
  path?: string;
  children?: MenuItem[];
}

const menuItems: MenuItem[] = [
  {
    label: "Inicio",
    icon: <Home />,
    path: "/",
  },
  {
    label: "Configuración",
    icon: <Settings />,
    children: [
      { label: "Perfil", icon: <Info />, path: "/configuracion/perfil" },
      { label: "Seguridad", icon: <Build />, path: "/configuracion/seguridad" },
    ],
  },
];

export default function Sidebar() {
  const pathname = usePathname();
  const [open, setOpen] = useState(false);
  const [expandedMenus, setExpandedMenus] = useState<{
    [key: string]: boolean;
  }>({});

  const handleToggle = (label: string) => {
    setExpandedMenus((prev) => ({ ...prev, [label]: !prev[label] }));
  };

  const renderMenuItem = (item: MenuItem) => {
    const isActive = pathname === item.path;

    if (item.children) {
      const open = expandedMenus[item.label];
      return (
        <>
          <ListItem onClick={() => handleToggle(item.label)}>
            <ListItemIcon>{item.icon}</ListItemIcon>
            <ListItemText primary={item.label} />
            {open ? <ExpandLess /> : <ExpandMore />}
          </ListItem>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>
              {item.children.map((child) => (
                <Link href={child.path!} key={child.label}>
                  <ListItem className="pl-8">
                    <ListItemIcon>{child.icon}</ListItemIcon>
                    <ListItemText primary={child.label} />
                  </ListItem>
                </Link>
              ))}
            </List>
          </Collapse>
        </>
      );
    }

    return (
      <Link href={item.path!} key={item.label}>
        <ListItem>
          <ListItemIcon>{item.icon}</ListItemIcon>
          <ListItemText primary={item.label} />
        </ListItem>
      </Link>
    );
  };

  return (
    <div className="md:w-64">
      <div className="md:hidden p-2">
        <IconButton onClick={() => setOpen(true)}>
          <Menu />
        </IconButton>
      </div>
      <Drawer
        variant="temporary"
        open={open}
        onClose={() => setOpen(false)}
        className="md:hidden"
        classes={{ paper: "w-64" }}
      >
        <List>{menuItems.map(renderMenuItem)}</List>
      </Drawer>
      <div className="hidden md:block fixed h-full bg-white shadow w-64">
        <List>{menuItems.map(renderMenuItem)}</List>
      </div>
    </div>
  );
}
