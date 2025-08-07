"use client";

import { Container } from "@mui/material";
import { Instagram, Facebook, Phone } from "lucide-react";

const socialLinks = [
  { Icon: Instagram, href: "https://www.instagram.com/suplidorastylecr/?hl=es-la" },
  { Icon: Facebook, href: "https://www.facebook.com/profile.php?id=100068502045044&ref=bookmarks#" },
];

export default function Footer() {
  return (
    <footer className="bg-[#523249] text-[#F8E8F5] py-10">
      <Container maxWidth="lg" className="text-center">
        <p className="mb-6 text-sm tracking-wide font-light">
          © 2024 BeYou Spa. Todos los derechos reservados.
        </p>

        <div className="flex justify-center space-x-8">
          {socialLinks.map(({ Icon, href }, idx) => (
            <a
              key={idx}
              href={href}
              target="_blank"
              rel="noopener noreferrer"
              aria-label={`Visita nuestro ${href.split(".")[1]} oficial`}
              className="text-[#F8E8F5] hover:text-pink-400 transition-colors duration-300"
            >
              <Icon size={28} strokeWidth={1.5} />
            </a>
          ))}
        </div>
      </Container>
    </footer>
  );
}
