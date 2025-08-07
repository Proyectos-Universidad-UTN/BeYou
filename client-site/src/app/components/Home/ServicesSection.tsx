"use client";

import { Container } from "@mui/material";
import { useRouter } from "next/navigation";
import { ROUTES } from "@/navigation/Routes";
import {
  Palette,
  Droplets,
  Sparkles,
  HeartHandshake,
  Crown,
  BookOpen, // Icono para capacitaciones/cursos
  ShoppingBag, // Icono para productos
  Package, // Icono para envíos
  Pin, // Icono para la ubicación
} from "lucide-react";

// Servicios de Spa, ajustados a la información
const spaServices = [
  {
    name: "Productos para Uñas",
    description:
      "Encuentra todo lo que necesitas para crear diseños únicos y profesionales en uñas.",
    icon: Palette,
  },
  {
    name: "Cuidado Facial y Corporal",
    description:
      "Prodcutos para el cuido corporal.",
    icon: Sparkles,
  },
  {
    name: "Productos para Emprendedoras",
    description:
      "Insumos y herramientas ideales para iniciar o potenciar tu negocio de belleza.",
    icon: ShoppingBag,
  },
];

// Otros servicios y detalles importantes
const otherServices = [
  {
    name: "Productos de Belleza",
    description: "Esmaltes, cuidado personal y más para tu rutina diaria.",
    icon: ShoppingBag,
    route: "#productos", // Enlace a la sección de productos
  },
  {
    name: "Capacitaciones",
    description:
      "Cursos profesionales para pintar y diseñar uñas. ¡Inscríbete!",
    icon: BookOpen,
    route: "#capacitaciones", // Enlace a la sección de capacitaciones
  },
];

export default function ServicesSection() {
  const router = useRouter();

  const handleServiceClick = (route: string) => {
    if (route.startsWith("#")) {
      // Si es un enlace de ancla, se desplaza en la misma página
      document
        .getElementById(route.substring(1))
        ?.scrollIntoView({ behavior: "smooth" });
    } else {
      // Si es una ruta de Next.js, navega a otra página
      router.push(route);
    }
  };

  return (
    <section id="servicios" className="py-20 bg-[#F8E8F5]">
      <Container maxWidth="lg" className="text-center">
        <h2 className="text-3xl md:text-4xl font-extrabold mb-4 text-[#523249]">
          Nuestros Servicios & Academia
        </h2>
        <p className="text-gray-600 mb-12 max-w-2xl mx-auto font-light">
          En Sytle and Beauty, te ofrecemos una combinación única de servicios
          de belleza, productos de alta calidad y capacitaciones profesionales.
        </p>

        {/* Sección de Servicios de Spa */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8 mb-16">
          {spaServices.map((service, index) => (
            <div
              key={index}
              className="flex flex-col items-center p-8 bg-white rounded-xl shadow-lg hover:shadow-2xl transition-all duration-300 transform hover:-translate-y-2 cursor-pointer border border-gray-100"
              onClick={() => router.push(ROUTES.RESERVATION)}
            >
              <div className="p-4 rounded-full bg-[#523249] mb-4 text-white hover:bg-[#C55D96] transition-colors duration-300">
                <service.icon size={36} />
              </div>
              <h3 className="text-2xl font-bold mb-2 text-[#523249]">
                {service.name}
              </h3>
              <p className="text-gray-500 text-base">{service.description}</p>
            </div>
          ))}
        </div>

        {/* Sección de Productos, Cursos y Envíos - con un diseño diferente */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          {otherServices.map((item, index) => (
            <div
              key={index}
              className="flex flex-col items-center p-8 bg-white rounded-xl shadow-lg hover:shadow-2xl transition-all duration-300 transform hover:-translate-y-2 cursor-pointer border border-gray-100"
              onClick={() => handleServiceClick(item.route)}
            >
              <div className="p-4 rounded-full bg-[#C55D96] mb-4 text-white hover:bg-[#523249] transition-colors duration-300">
                <item.icon size={36} />
              </div>
              <h3 className="text-2xl font-bold mb-2 text-[#523249]">
                {item.name}
              </h3>
              <p className="text-gray-500 text-base">{item.description}</p>
            </div>
          ))}
          {/* Tarjeta de Información de Ubicación y Envíos */}
          <div className="flex flex-col items-center p-8 bg-white rounded-xl shadow-lg border border-gray-100">
            <div className="p-4 rounded-full bg-gray-200 mb-4 text-[#523249]">
              <Pin size={36} />
            </div>
            <h3 className="text-2xl font-bold mb-2 text-[#523249]">
              Estamos en Alajuela Centro
            </h3>
            <p className="text-gray-500 text-base mb-4">
              Hacemos envíos por Correos de Costa Rica 🏍
            </p>
            <div className="p-4 rounded-full bg-gray-200 mb-4 text-[#523249]">
              <Package size={36} />
            </div>
          </div>
        </div>
      </Container>
    </section>
  );
}
