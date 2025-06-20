"use client";

import React, { useState } from "react";
import Link from "next/link";
import {
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Collapse,
} from "@mui/material";
import {
  ExpandLess,
  ExpandMore,
} from "@mui/icons-material";
import { usePathname } from "next/navigation";
import { MenuItem } from "./Types";

const iconMap: { [key: string]: React.ReactNode } = {
  Inicio: <span className="material-icons"></span>,
  Sucursal: <span className="material-icons"></span>,
  Sucursales: <span className="material-icons"></span>,
  Gestiones: <span className="material-icons"></span>,
  Reservas: <span className="material-icons"></span>,
  Proforma: <span className="material-icons"></span>,
  Proveedores: <span className="material-icons"></span>,
  Productos: <span className="material-icons"></span>,
  Servicios: <span className="material-icons"></span>,
  Configuración: <span className="material-icons"></span>,
  Perfil: <span className="material-icons"></span>,
  Seguridad: <span className="material-icons"></span>,
};

interface RenderMenuItemProps {
  item: MenuItem;
  isSmallScreen: boolean;
  onClose?: () => void;
}

export function renderMenuItem(
  item: MenuItem,
  isSmallScreen: boolean,
  onClose?: () => void
) {
  const pathname = usePathname();
  const [expandedMenus, setExpandedMenus] = useState<{ [key: string]: boolean }>({});

  const handleToggle = (label: string) => {
    setExpandedMenus((prev) => ({ ...prev, [label]: !prev[label] }));
  };

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
                >
                    <ListItem
                      {...(isSmallScreen && onClose ? { onClick: onClose } : {})}
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
                      />
                    </ListItem>
                </Link>
              );
            })}
          </List>
        </Collapse>
      </React.Fragment>
    );
  }

  return (
    <Link href={item.path!} key={item.label} passHref >
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
    </Link>
  );
}
