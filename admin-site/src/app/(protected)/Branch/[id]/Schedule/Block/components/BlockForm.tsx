"use client";

import { Stack } from "@mui/system";
import { useIsMobile } from "@/hooks/UseIsMobile";
import { TextField, Box, Alert } from "@mui/material";
import { yupResolver } from "@hookform/resolvers/yup";
import { BlockSchema, BlockForm as BlockFormType, BlockDefaultValues } from "./BlockSchema"; // Import BlockDefaultValues
import { FormButtons } from "@/components/Shared/FormButtons";
import { useForm, SubmitHandler, FormProvider } from "react-hook-form";
import { FormFieldErrorMessage } from "@/components/FormFieldErrorMessage";

interface BlockFormProps {
  onSubmit: SubmitHandler<BlockFormType>;
  defaultValues?: BlockFormType;
  isLoading?: boolean;
}

export const BlockForm = ({
  onSubmit,
  defaultValues,
  isLoading = false,
}: BlockFormProps) => {
  const isMobile = useIsMobile();
  const formMethods = useForm<BlockFormType>({
    defaultValues: defaultValues || BlockDefaultValues, 
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
            <Alert severity="error">Por favor corrija los errores para continuar</Alert>
          )}
        </Box>
        <Stack spacing={3} maxWidth={isMobile ? '90vw' : '600px'}>
          <Box sx={{ display: 'none' }}> 
            <TextField
              label="Branch Schedule ID"
              fullWidth
              {...register("branchScheduleId")}
              error={!!errors.branchScheduleId}
              helperText={errors.branchScheduleId?.message}
            />
            {errors.branchScheduleId?.message && (
              <FormFieldErrorMessage message={errors.branchScheduleId.message} />
            )}
          </Box>

          <Box>
            <TextField
              label="Hora de Inicio"
              fullWidth
              {...register("startHour")}
              error={!!errors.startHour}
              helperText={errors.startHour?.message}
            />
            {errors.startHour?.message && (
              <FormFieldErrorMessage message={errors.startHour.message} />
            )}
          </Box>

          <Box>
            <TextField
              label="Hora de Fin"
              fullWidth
              {...register("endHour")}
              error={!!errors.endHour}
              helperText={errors.endHour?.message}
            />
            {errors.endHour?.message && (
              <FormFieldErrorMessage message={errors.endHour.message} />
            )}
          </Box>

          <FormButtons backPath="/Branch" loadingIndicator={isLoading} /> 
        </Stack>
      </form>
    </FormProvider>
  );
};