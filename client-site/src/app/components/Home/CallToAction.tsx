"use client";

import { Button, Container } from "@mui/material";
import { useRouter } from "next/navigation";
import { ROUTES } from "@/navigation/Routes";

export default function CourseCallToAction() {
  const router = useRouter();

  return (
    <section
      className="py-20 text-center text-white"
      style={{
        backgroundImage:
          "linear-gradient(rgba(247,168,211,0.6), rgba(123,104,238,0.6)), url('/assets/fondo.png')",
        backgroundSize: "cover",
        backgroundPosition: "center",
      }}
    >
      <Container maxWidth="lg">
        <h2 className="text-3xl md:text-4xl font-bold mb-4">
          ¡Capacítate y domina el arte de las uñas!
        </h2>
        <p className="text-lg md:text-xl font-light mb-8 max-w-3xl mx-auto">
          Inscríbete en nuestro curso de uñas y comienza a construir tu futuro
          en el mundo de la belleza.
        </p>
        <Button
          variant="contained"
          onClick={() => router.push(ROUTES.RESERVATION)}
          sx={{
            bgcolor: "white",
            color: "#7B68EE",
            fontWeight: "bold",
            borderRadius: 50,
            py: 2,
            px: 8,
            fontSize: "1.125rem",
            transition: "transform 0.3s",
            transform: "scale(1)",
            "&:hover": { bgcolor: "#f0f0f0", transform: "scale(1.05)" },
          }}
        >
          ¡Inscríbete Ahora!
        </Button>
      </Container>
    </section>
  );
}
