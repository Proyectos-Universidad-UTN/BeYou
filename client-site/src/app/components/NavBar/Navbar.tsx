"use client";

import Link from "next/link";
import { Container } from "@mui/material";
import MobileDrawer from "./MobileDrawer";
import { ROUTES } from "@/navigation/Routes";
import { Instagram, Facebook, Phone, Mail } from "lucide-react"; // Importa los nuevos iconos

// Array de enlaces para iconos de redes sociales, teléfono y correo
const socialLinks = [
  { Icon: Instagram, href: "https://www.instagram.com/suplidorastylecr/?hl=es-la" },
  { Icon: Facebook, href: "https://www.facebook.com/profile.php?id=100068502045044&ref=bookmarks#" },
];

export default function Navbar() {
  return (
    <header className="bg-white shadow-lg py-3 fixed top-0 left-0 right-0 z-50 transition-all duration-300">
      <Container
        maxWidth="lg"
        className="flex justify-between items-center px-4 sm:px-6 lg:px-8"
      >
        {/* Sección del Logo */}
        <Link href="/" className="flex-shrink-0">
          <img
            src="/assets/logo.webp"
            alt="BeYou Spa Logo"
            className="h-24 w-56 object-contain"
          />
        </Link>

        {/* Navegación para escritorio */}
        <nav className="hidden md:flex gap-8 text-[#523249] font-semibold text-lg items-center">
          <a
            href="#servicios"
            className="relative group hover:text-[#C55D96] transition-colors duration-300"
          >
            Servicios
            <span className="absolute bottom-0 left-0 w-full h-0.5 bg-[#C55D96] scale-x-0 group-hover:scale-x-100 transition-transform duration-300 ease-out"></span>
          </a>
          <a
            href="#productos"
            className="relative group hover:text-[#C55D96] transition-colors duration-300"
          >
            Productos
            <span className="absolute bottom-0 left-0 w-full h-0.5 bg-[#C55D96] scale-x-0 group-hover:scale-x-100 transition-transform duration-300 ease-out"></span>
          </a>
          <Link
            href={ROUTES.RESERVATION}
            className="py-2 px-6 rounded-full bg-[#523249] text-white hover:bg-[#C55D96] transition-colors duration-300 shadow-md"
          >
            Reservar
          </Link>
        </nav>

        {/* Iconos de Redes Sociales, Teléfono y Correo */}
        <div className="hidden md:flex items-center gap-4">
          {socialLinks.map(({ Icon, href }, idx) => (
            <a
              key={idx}
              href={href}
              target="_blank"
              rel="noopener noreferrer"
              className="text-[#523249] hover:text-[#C55D96] transition-colors duration-300"
            >
              <Icon size={24} strokeWidth={1.5} />
            </a>
          ))}
          {/* Nuevo: Icono de Teléfono */}
          <a
            href="tel:+50660744198" // Enlace para llamar
            className="text-[#523249] hover:text-[#C55D96] transition-colors duration-300"
          >
            <Phone size={24} strokeWidth={1.5} />
          </a>
          {/* Nuevo: Icono de Correo Electrónico */}
          <a
            href="mailto:info@beyouspa.com" // Enlace para enviar un correo
            className="text-[#523249] hover:text-[#C55D96] transition-colors duration-300"
          >
            <Mail size={24} strokeWidth={1.5} />
          </a>
        </div>

        {/* Cajón de menú móvil */}
        <div className="md:hidden">
          <MobileDrawer />
        </div>
      </Container>
    </header>
  );
}
