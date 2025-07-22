"use client";

import { Box, Alert, Stack, TextField } from "@mui/material";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  BlockForm as BlockFormType,
  BlockSchema,
  BlockDefaultValues,
} from "./BlockSchema";
import { useIsMobile } from "@/hooks/UseIsMobile";
import { FormFieldErrorMessage } from "@/components/FormFieldErrorMessage";
import { FormButtons } from "@/components/Shared/FormButtons";

interface BlockFormProps {
  onSubmit: SubmitHandler<BlockFormType>;
  defaultValues?: BlockFormType;
  isLoading?: boolean;
  backPath?: string;
}

export const BlockFormComponent = ({
  onSubmit,
  defaultValues = BlockDefaultValues,
  isLoading = false,
  backPath = "/Branch/Schedule",
}: BlockFormProps) => {
  const isMobile = useIsMobile();

  const formMethods = useForm<BlockFormType>({
    defaultValues,
    resolver: yupResolver(BlockSchema),
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
          {/* branchScheduleId hidden */}
          <input type="hidden" {...register("branchScheduleId")} />

          {/* Hora inicio */}
          <Box>
            <TextField
              label="Hora de inicio"
              type="time"
              fullWidth
              InputLabelProps={{ shrink: true }}
              inputProps={{ step: 1800 }} // 30 minutos
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
              inputProps={{ step: 1800 }} // 30 minutos
              {...register("endHour")}
              error={!!errors.endHour}
              helperText={errors.endHour?.message}
            />
            {errors.endHour?.message && (
              <FormFieldErrorMessage message={errors.endHour.message} />
            )}
          </Box>

          <FormButtons backPath={backPath} loadingIndicator={isLoading} />
        </Stack>
      </form>
    </FormProvider>
  );
};
