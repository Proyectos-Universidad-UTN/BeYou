"use client";

import { useState } from "react";
import {
  Button,
  Stack,
  TextField,
  Typography,
  ThemeProvider,
  createTheme,
} from "@mui/material";
import emailjs from "@emailjs/browser";
import { useSnackbar } from "notistack";
import { useRouter } from "next/navigation";

const formTheme = createTheme({
  palette: { primary: { main: "#C55D96" } },
});

type Props = {
  selectedDateISO: string;
  selectedDateDisplay: string;
};

export const ReservationForm = ({ selectedDateISO, selectedDateDisplay }: Props) => {
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    address: "",
    hour: "",
  });

  const { enqueueSnackbar } = useSnackbar();
  const router = useRouter();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!selectedDateISO) {
      enqueueSnackbar("❗ Por favor selecciona una fecha en el calendario.", { variant: "warning" });
      return;
    }

    const templateParams = {
      firstName: formData.firstName,
      lastName: formData.lastName,
      email: formData.email,
      phone: formData.phone,
      address: formData.address,
      date: selectedDateISO,
      hour: formData.hour,
    };

    try {
      await emailjs.send(
        "service_zr46oqa",
        "template_wqfuo9x",
        templateParams,
        "7WgZwh3I0LN2kRXly"
      );
      enqueueSnackbar("✅ Reserva enviada con éxito", { variant: "success" });
      setFormData({
        firstName: "",
        lastName: "",
        email: "",
        phone: "",
        address: "",
        hour: "",
      });

      // Redirigir a la página principal después de 2 segundos
      setTimeout(() => {
        router.push("/");
      }, 2000);
    } catch (error) {
      enqueueSnackbar("❌ Hubo un problema al enviar la reserva", { variant: "error" });
    }
  };

  return (
    <ThemeProvider theme={formTheme}>
      <form
        onSubmit={handleSubmit}
        className="p-8 bg-white shadow-md rounded-xl border border-gray-100"
      >
        <Stack spacing={3}>
          <Typography variant="h5" className="font-bold text-[#523249]">
            Formulario de Reservación
          </Typography>

          {selectedDateDisplay && (
            <Typography variant="body1" sx={{ fontWeight: "bold", color: "#C55D96" }}>
              📅 La fecha seleccionada es el {selectedDateDisplay}
            </Typography>
          )}

          <TextField
            name="firstName"
            value={formData.firstName}
            onChange={handleChange}
            label="Nombre"
            fullWidth
            required
          />
          <TextField
            name="lastName"
            value={formData.lastName}
            onChange={handleChange}
            label="Apellidos"
            fullWidth
            required
          />
          <TextField
            name="email"
            type="email"
            value={formData.email}
            onChange={handleChange}
            label="Correo electrónico"
            fullWidth
            required
          />
          <TextField
            name="phone"
            value={formData.phone}
            onChange={handleChange}
            label="Teléfono"
            fullWidth
            required
          />
          <TextField
            name="address"
            value={formData.address}
            onChange={handleChange}
            label="Dirección"
            fullWidth
            required
          />
          <TextField
            name="hour"
            value={formData.hour}
            onChange={handleChange}
            label="Hora de la Cita"
            type="time"
            inputProps={{ step: 1 }}
            fullWidth
            required
          />

          <Button
            type="submit"
            variant="contained"
            color="primary"
            sx={{
              bgcolor: "#C55D96",
              color: "#F8E8F5",
              fontWeight: "bold",
              py: 1.5,
              mt: 2,
              "&:hover": { bgcolor: "#523249" },
            }}
          >
            Reservar Ahora ✨
          </Button>
        </Stack>
      </form>
    </ThemeProvider>
  );
};
