"use client";

import React, { useState } from "react";
import {
  Drawer,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Collapse,
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
} from "@mui/icons-material";
import Link from "next/link";
import { useIsMobile } from "@/hooks/UseIsMobile";

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
  const isSmallScreen = useIsMobile();

  const [expandedMenus, setExpandedMenus] = useState<{
    [key: string]: boolean;
  }>({});

  const handleToggle = (label: string) => {
    setExpandedMenus((prev) => ({ ...prev, [label]: !prev[label] }));
  };

  const header = (
    <div className="font-bold text-xl text-center py-4 text-yellow-700 bg-pink-300 tracking-wide border-b">
      {/* Título o logo */}
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
            className={`cursor-pointer px-4 py-3 rounded-lg mb-1 flex items-center justify-between transition-colors duration-300 ${
              isActive || isExpanded
                ? "bg-yellow-300 text-gray-900"
                : "text-gray-800 hover:bg-yellow-200 hover:text-black"
            }`}
          >
            <ListItemIcon
              className={`${
                isActive || isExpanded ? "text-yellow-700" : "text-gray-500"
              }`}
            >
              {icon}
            </ListItemIcon>
            <ListItemText primary={item.label} />
            {isExpanded ? (
              <ExpandLess className="text-yellow-700" />
            ) : (
              <ExpandMore className="text-yellow-700" />
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
                    <a className="no-underline block">
                      <ListItem
                        {...(isSmallScreen && onClose
                          ? { onClick: onClose }
                          : {})}
                        className={`text-sm rounded-md mb-2 pl-6 flex items-center transition-colors duration-300 ${
                          childActive
                            ? "bg-yellow-300 text-gray-900"
                            : "text-gray-600 hover:bg-yellow-100 hover:text-black"
                        }`}
                      >
                        <ListItemIcon
                          className={`min-w-[36px] ${
                            childActive ? "text-yellow-700" : "text-gray-500"
                          }`}
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
        <a className="no-underline">
          <ListItem
            {...(isSmallScreen && onClose ? { onClick: onClose } : {})}
            className={`cursor-pointer px-4 py-3 rounded-lg mb-1 flex items-center justify-between transition-colors duration-300 ${
              isActive
                ? "bg-yellow-300 text-gray-900"
                : "text-gray-800 hover:bg-yellow-200 hover:text-black"
            }`}
          >
            <ListItemIcon
              className={`${isActive ? "text-yellow-700" : "text-gray-500"}`}
            >
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
        PaperProps={{ className: "bg-pink-300 w-3/4" }}
      >
        {header}
        <List>{menuItems.map(renderMenuItem)}</List>
      </Drawer>
    );
  }

  return (
    <div className="fixed h-full shadow w-64 bg-pink-100">
      {header}
      <List>{menuItems.map(renderMenuItem)}</List>
    </div>
  );
}
