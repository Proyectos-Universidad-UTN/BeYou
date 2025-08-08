// components/ReservationForm.tsx
"use client";

import { Button, Stack, TextField, Typography } from "@mui/material";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  ReservationSchema,
  ReservationFormType,
  initialReservationValues,
} from "./ReservationSchema";
import { createTheme, ThemeProvider } from "@mui/material/styles";

type Props = {
  selectedDate: string;
};

const formTheme = createTheme({
  palette: {
    primary: { main: "#C55D96" },
  },
});

export const ReservationForm = ({ selectedDate }: Props) => {
  const { handleSubmit, control, formState: { errors } } = useForm<ReservationFormType>({
    resolver: yupResolver(ReservationSchema),
    defaultValues: {
      ...initialReservationValues,
      date: selectedDate,
    },
  });

  const onSubmit = (data: ReservationFormType) => {
    console.log("Formulario enviado", data);
  };

  return (
    <ThemeProvider theme={formTheme}>
      <form
        onSubmit={handleSubmit(onSubmit)}
        className="p-8 bg-white shadow-md rounded-xl border border-gray-100"
      >
        <Stack spacing={3}>
          <Typography variant="h5" className="font-bold text-[#523249]">
            Formulario de Reservación
          </Typography>

          {selectedDate && (
            <Typography variant="body1" sx={{ fontWeight: "bold", color: "#C55D96" }}>
              📅 La fecha seleccionada es el {selectedDate}
            </Typography>
          )}

          <Controller
            name="hour"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Hora de la Cita"
                placeholder="Ej: 14:30"
                fullWidth
                error={!!errors.hour}
                helperText={errors.hour?.message}
              />
            )}
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
