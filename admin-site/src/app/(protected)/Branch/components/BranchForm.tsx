"use client";


import { Stack } from "@mui/system";
import { useIsMobile } from "@/hooks/UseIsMobile";
import { TextField, Box, Alert } from "@mui/material";
import { yupResolver } from "@hookform/resolvers/yup";
import { BranchSchema, BranchFormType } from "./BranchSchema";
import { FormButtons } from "@/components/Shared/FormButtons";
import { useForm, SubmitHandler, FormProvider } from "react-hook-form";
import { FormFieldErrorMessage } from "@/components/FormFieldErrorMessage";

interface BranchFormProps {
  onSubmit: SubmitHandler<BranchFormType>;
  defaultValues?: BranchFormType;
  isLoading?: boolean;
}

export const BranchForm = ({
  onSubmit,
  defaultValues,
  isLoading = false,
}: BranchFormProps) => {
  const isMobile = useIsMobile();
  const formMethods = useForm<BranchFormType>({
    defaultValues,
    resolver: yupResolver(BranchSchema),
  })

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
          <Box>
            <TextField
              label="Nombre de Sucursal"
              fullWidth
              {...register("name")}
              error={!!errors.name}
              helperText={errors.name?.message}
            />
            {errors.name?.message && (
              <FormFieldErrorMessage message={errors.name.message} />
            )}
          </Box>

          <Box>
            <TextField
              label="Descripción"
              fullWidth
              {...register("description")}
              error={!!errors.description}
              helperText={errors.description?.message}
            />
            {errors.description?.message && (
              <FormFieldErrorMessage message={errors.description.message} />
            )}

          </Box>

          <Box>
            <TextField
              label="Teléfono"
              fullWidth
              {...register("telephone")}
              error={!!errors.telephone}
              helperText={errors.telephone?.message}
            />
            {errors.telephone?.message && (
              <FormFieldErrorMessage message={errors.telephone.message} />
            )}
          </Box>

          <Box>
            <TextField
              label="Correo Electrónico"
              fullWidth
              {...register("email")}
              error={!!errors.email}
              helperText={errors.email?.message}
            />
            {errors.email?.message && (
              <FormFieldErrorMessage message={errors.email.message} />
            )}
          </Box>

          <Box>
            <TextField
              label="Distrito"
              type="number"
              fullWidth
              {...register("districtId")}
              error={!!errors.districtId}
              helperText={errors.districtId?.message}
            />
            {errors.districtId?.message && (
              <FormFieldErrorMessage message={errors.districtId.message} />
            )}
          </Box>

          <Box>
            <TextField
              label="Dirección"
              fullWidth
              multiline
              rows={2}
              {...register("address")}
              error={!!errors.address}
              helperText={errors.address?.message}
            />
            {errors.address?.message && (
              <FormFieldErrorMessage message={errors.address.message} />
            )}
          </Box>

          <FormButtons backPath="/Branch" loadingIndicator={isLoading} />
        </Stack>
      </form>
    </FormProvider>
  );
};
