import { MenuItem } from "@/components/Sidebar/Types";
import {
  Calendar,
  FileText,
  Home,
  Package,
  Settings,
  Sparkles,
  Store,
  Truck,
  CalendarDays ,
  Users,
} from "lucide-react";

const menuItems: MenuItem[] = [
  { label: "Inicio", path: "/Dashboard", icon: Home },
  { label: "Sucursal", path: "/Branch", icon: Store },
  {
    label: "Gestiones",
    icon: Calendar,
    children: [
      { label: "Reservas", path: "/Reservation", icon: CalendarDays  },
      { label: "Inventario", path: "/gestiones/proforma", icon: FileText },
    ],
  },
  { label: "Clientes", path: "/Customer", icon: Users },
  { label: "Proveedores", path: "/Vendor", icon: Truck },
  { label: "Productos", path: "/Product", icon: Package },
  { label: "Servicios", path: "/servicios", icon: Sparkles },
  { label: "Usuarios", path: "/User", icon: Users },
  {
    label: "Configuración",
    icon: Settings,
    children: [
      { label: "Perfil", path: "/configuracion/perfil", icon: Users },
      { label: "Seguridad", path: "/configuracion/seguridad", icon: Settings },
      { label: "Horarios", path: "/Schedule", icon: Settings },
    ],
  },
];

export default menuItems;
