"use client";

import { useState, useEffect } from "react";
import { Stack } from "@mui/system";
import { Box, TextField, Alert } from "@mui/material";
import { useForm, SubmitHandler, FormProvider } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useIsMobile } from "@/hooks/UseIsMobile";
import { FormButtons } from "@/components/Shared/FormButtons";
import { FormFieldErrorMessage } from "@/components/FormFieldErrorMessage";
import {
  CustomerFormType,
  CustomerSchema,
  initialCustomerValues,
} from "./CustomerSchema";

interface CustomerFormProps {
  onSubmit: SubmitHandler<CustomerFormType>;
  defaultValues?: CustomerFormType;
  isLoading?: boolean;
  isEdit?: boolean;
}

export const CustomerForm = ({
  onSubmit,
  defaultValues = initialCustomerValues,
  isLoading = false,
}: CustomerFormProps) => {
  const isMobile = useIsMobile();

  const formMethods = useForm<CustomerFormType>({
    defaultValues,
    resolver: yupResolver(CustomerSchema),
  });

  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = formMethods;

  useEffect(() => {
    reset(defaultValues);
  }, [defaultValues, reset]);

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
          <Box>
            <TextField
              label="Nombre"
              fullWidth
              {...register("firstName")}
              error={!!errors.firstName}
              helperText={errors.firstName?.message}
            />
            {errors.firstName?.message && (
              <FormFieldErrorMessage message={errors.firstName.message} />
            )}
          </Box>

          <Box>
            <TextField
              label="Apellido"
              fullWidth
              {...register("lastName")}
              error={!!errors.lastName}
              helperText={errors.lastName?.message}
            />
            {errors.lastName?.message && (
              <FormFieldErrorMessage message={errors.lastName.message} />
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

          <FormButtons backPath="/Customer" loadingIndicator={isLoading} />
        </Stack>
      </form>
    </FormProvider>
  );
};
