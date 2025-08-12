"use client";

import { useState } from "react";
import { Container, Typography, Button } from "@mui/material";
import { useRouter } from "next/navigation";
import { ReservationForm } from "./components/ReservationForm";
import { ReservationCalendar } from "./components/ReservationCalendar";
import { format } from "date-fns";
import { es } from "date-fns/locale";

export default function ReservationPage() {
  const [selectedDateISO, setSelectedDateISO] = useState<string>("");
  const [selectedDateDisplay, setSelectedDateDisplay] = useState<string>("");
  const router = useRouter();

  const handleDateSelect = (date: string) => {
    setSelectedDateISO(date); // para backend
    const selected = new Date(date + "T00:00:00");
    const formattedDate = format(selected, "d 'de' MMMM", { locale: es });
    setSelectedDateDisplay(formattedDate); // para UI mostrar
  };

  return (
    <div className="relative min-h-screen bg-[#F8E8F5] py-12 px-4 overflow-hidden">
      <Container maxWidth="lg" className="relative z-10">
        <div className="bg-[#C55D96] text-white px-6 py-6 rounded-2xl mb-8 shadow-lg text-center">
          <Typography
            variant="h4"
            component="h1"
            className="font-extrabold mb-2"
          >
            ¡Agenda tu Cita de Belleza! 💅
          </Typography>
          <Typography variant="body1" className="opacity-90">
            Selecciona una fecha en el calendario y completa el formulario para
            reservar tu lugar.
          </Typography>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
          <ReservationCalendar onDateSelect={handleDateSelect} />
          <ReservationForm
            selectedDateISO={selectedDateISO}
            selectedDateDisplay={selectedDateDisplay}
          />
        </div>
        <Button
          variant="contained"
          onClick={() => router.back()}
          sx={{
            mb: 4,
            bgcolor: "#F7A8D3", 
            color: "#523249",
            fontWeight: "bold",
            "&:hover": {
              bgcolor: "#e18cbe", 
            },
          }}
        >
          Regresar
        </Button>
      </Container>
    </div>
  );
}
