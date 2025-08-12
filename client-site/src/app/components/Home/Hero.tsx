"use client";

import { Button } from "@mui/material";
import { useRouter } from "next/navigation";
import { GraduationCap } from "lucide-react";
import { ROUTES } from "@/navigation/Routes";

export default function Hero() {
  const router = useRouter();

  return (
    <section
      className="relative h-[60vh] md:h-[80vh] flex flex-col justify-center items-center text-center px-4 overflow-hidden"
      style={{
        backgroundImage:
          "linear-gradient(to right, rgba(247,168,211,0.6), rgba(123,104,238,0.6)), url('/assets/fondo2.webp')",
        backgroundSize: "cover",
        backgroundPosition: "center",
      }}
    >
      <div className="relative z-10 max-w-3xl">
        <h1 className="text-4xl md:text-6xl font-extrabold mb-4 text-[#F8E8F5] drop-shadow-lg">
          Capacítate en Arte de Uñas
        </h1>
        <p className="text-lg md:text-xl font-light mb-8 text-[#F8E8F5] drop-shadow-md">
          Aprende las técnicas más modernas y conviértete en un experto profesional.
        </p>
        <Button
          variant="contained"
          onClick={() => router.push(ROUTES.RESERVATION)}
          sx={{
            bgcolor: "#F8E8F5",
            color: "#523249",
            fontWeight: "bold",
            borderRadius: 50,
            py: 1.5,
            px: 6,
            transition: "transform 0.3s",
            transform: "scale(1)",
            "&:hover": { bgcolor: "#f0e1f0", transform: "scale(1.05)" },
          }}
          endIcon={<GraduationCap size={20} />}
        >
          ¡Inscríbete Ahora!
        </Button>
      </div>
    </section>
  );
}
