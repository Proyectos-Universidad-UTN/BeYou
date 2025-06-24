"use client";

import {
  Home,
  Store,
  Calendar,
  FileText,
  Truck,
  Package,
  Sparkles,
  Users,
  Settings,
} from "lucide-react";
import { useIsMobile } from "@/hooks/UseIsMobile";
import MobileSidebar from "./MobileSidebar";
import DesktopSidebar from "./DesktopSidebar";
import { MenuItem } from "./Types";

const menuItems: MenuItem[] = [
  { label: "Inicio", path: "/", icon: Home },
  { label: "Sucursal", path: "/sucursales", icon: Store },
  {
    label: "Gestiones",
    icon: Calendar,
    children: [
      { label: "Reservas", path: "/gestiones/reservas", icon: FileText },
      { label: "Proforma", path: "/gestiones/proforma", icon: FileText },
    ],
  },
  { label: "Proveedores", path: "/proveedores", icon: Truck },
  { label: "Productos", path: "/productos", icon: Package },
  { label: "Servicios", path: "/servicios", icon: Sparkles },
  { label: "Usuarios", path: "/usuarios", icon: Users },
  {
    label: "Configuración",
    icon: Settings,
    children: [
      { label: "Perfil", path: "/configuracion/perfil", icon: Users },
      { label: "Seguridad", path: "/configuracion/seguridad", icon: Settings },
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
