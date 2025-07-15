"use client";

import { Box, Alert, Stack, MenuItem, TextField } from "@mui/material";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  ScheduleForm,
  ScheduleSchema,
  ScheduleDefaultValues,
} from "./ScheduleSchema";
import { weekDaysSpanish } from "@/utils/util";
import { useIsMobile } from "@/hooks/UseIsMobile";
import { FormFieldErrorMessage } from "@/components/FormFieldErrorMessage";
import { FormButtons } from "@/components/Shared/FormButtons";

interface ScheduleFormProps {
  onSubmit: SubmitHandler<ScheduleForm>;
  defaultValues?: ScheduleForm;
  isLoading?: boolean;
}

export const ScheduleFormComponent = ({
  onSubmit,
  defaultValues = ScheduleDefaultValues,
  isLoading = false,
}: ScheduleFormProps) => {
  const isMobile = useIsMobile();

  const formMethods = useForm<ScheduleForm>({
    defaultValues,
    resolver: yupResolver(ScheduleSchema),
  });

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = formMethods;

  return (
    <FormProvider {...formMethods}>
      <form onSubmit={handleSubmit(onSubmit)} noValidate>
        <Box pb={2}>
          {Object.keys(errors).length > 0 && (
            <Alert severity="error">
              Por favor corrija los errores para continuar
            </Alert>
          )}
        </Box>

        <Stack spacing={3} maxWidth={isMobile ? "90vw" : "600px"}>
          {/* Día */}
          <Box>
            <TextField
              select
              fullWidth
              label="Día de la semana"
              {...register("day")}
              error={!!errors.day}
              helperText={errors.day?.message}
            >
              {Object.entries(weekDaysSpanish).map(([value, label]) => (
                <MenuItem key={value} value={value}>
                  {label}
                </MenuItem>
              ))}
            </TextField>
            {errors.day?.message && (
              <FormFieldErrorMessage message={errors.day.message} />
            )}
          </Box>

          {/* Hora inicio */}
          <Box>
            <TextField
              label="Hora de inicio"
              type="time"
              fullWidth
              InputLabelProps={{ shrink: true }}
              inputProps={{ step: 300 }}
              {...register("startHour")}
              error={!!errors.startHour}
              helperText={errors.startHour?.message}
            />
            {errors.startHour?.message && (
              <FormFieldErrorMessage message={errors.startHour.message} />
            )}
          </Box>

          {/* Hora fin */}
          <Box>
            <TextField
              label="Hora de finalización"
              type="time"
              fullWidth
              InputLabelProps={{ shrink: true }}
              inputProps={{ step: 300 }}
              {...register("endHour")}
              error={!!errors.endHour}
              helperText={errors.endHour?.message}
            />
            {errors.endHour?.message && (
              <FormFieldErrorMessage message={errors.endHour.message} />
            )}
          </Box>

          <FormButtons
            backPath="/Branch/Schedule"
            loadingIndicator={isLoading}
          />
        </Stack>
      </form>
    </FormProvider>
  );
};
