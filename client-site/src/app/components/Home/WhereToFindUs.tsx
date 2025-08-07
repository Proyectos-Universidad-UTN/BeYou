"use client";

import { Box, Typography, Link } from "@mui/material";
import { MapPin } from "lucide-react";

export default function WhereToFindUs() {
  return (
    <Box
      component="section"
      sx={{
        py: 8,
        px: 4,
        // Eliminamos backgroundColor, borderRadius y boxShadow para quitar el cuadro
        maxWidth: 900,
        margin: "0 auto",
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        gap: 4,
        flexWrap: "wrap", // para responsividad
      }}
    >
      <Box sx={{ flex: "1 1 300px", minWidth: 280, textAlign: "left" }}>
        <Typography
          variant="h3"
          sx={{ mb: 2, color: "#523249", fontWeight: "bold" }}
        >
          ¿Dónde encontrarnos?
        </Typography>
        <Link
          href="https://maps.app.goo.gl/ormaPwibk1pY4wwT8"
          target="_blank"
          rel="noopener noreferrer"
          underline="hover"
          sx={{
            display: "inline-flex",
            alignItems: "center",
            color: "#7B68EE",
            fontWeight: "bold",
            fontSize: "1.5rem",
            gap: 1,
            mb: 1,
            "&:hover": {
              color: "#6a5ad6",
            },
          }}
        >
          <MapPin size={28} />
          Alajuela Centro
        </Link>
        <Typography sx={{ color: "#555" }}>
          Visítanos en el corazón de Alajuela para disfrutar de nuestros servicios.
        </Typography>
      </Box>

      <Box
        sx={{
          flex: "1 1 300px",
          minWidth: 280,
          height: 250,
          borderRadius: 2,
          overflow: "hidden",
          // Puedes mantener la sombra ligera si quieres o eliminarla también
          // boxShadow: "0 0 8px rgba(0,0,0,0.1)",
        }}
      >
        <iframe
          src="https://maps.google.com/maps?q=Alajuela%20Centro&t=&z=15&ie=UTF8&iwloc=&output=embed"
          width="100%"
          height="100%"
          style={{ border: 0 }}
          loading="lazy"
          allowFullScreen
          referrerPolicy="no-referrer-when-downgrade"
          title="Mapa Alajuela Centro"
        />
      </Box>
    </Box>
  );
}
