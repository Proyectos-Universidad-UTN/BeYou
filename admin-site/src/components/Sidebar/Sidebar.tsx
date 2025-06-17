"use client";

import { useIsMobile } from "@/hooks/UseIsMobile";
import MobileSidebar from "./MobileSidebar";
import DesktopSidebar from "./DesktopSidebar";
import { MenuItem } from "./types";

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

interface SidebarProps {
  isOpen?: boolean;
  onClose?: () => void;
}

export default function Sidebar({ isOpen, onClose }: SidebarProps) {
  const isMobile = useIsMobile();

  return isMobile ? (
    <MobileSidebar isOpen={isOpen} onClose={onClose} menuItems={menuItems} />
  ) : (
    <DesktopSidebar menuItems={menuItems} />
  );
}
