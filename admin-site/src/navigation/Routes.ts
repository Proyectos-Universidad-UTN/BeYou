import { MenuItem } from "@/components/Sidebar/Types";
import { Calendar, FileText, Home, Package, Settings, Sparkles, Store, Truck, Users } from "lucide-react";

const menuItems: MenuItem[] = [
    { label: "Inicio", path: "/Dashboard", icon: Home },
    { label: "Sucursal", path: "/Branch", icon: Store },
    {
        label: "Gestiones",
        icon: Calendar,
        children: [
            { label: "Reservas", path: "/gestiones/reservas", icon: FileText },
            { label: "Proforma", path: "/gestiones/proforma", icon: FileText },
        ],
    },
    { label: "Proveedores", path: "/Vendor", icon: Truck },
    { label: "Productos", path: "/productos", icon: Package },
    { label: "Servicios", path: "/servicios", icon: Sparkles },
    { label: "Usuarios", path: "/User", icon: Users },
    {
        label: "Configuración",
        icon: Settings,
        children: [
            { label: "Perfil", path: "/configuracion/perfil", icon: Users },
            { label: "Seguridad", path: "/configuracion/seguridad", icon: Settings },
        ],
    },
];

export default menuItems;