"use client";

import { useForm, SubmitHandler } from "react-hook-form";
import { useEffect } from "react";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  VendorSchema,
  type VendorFormType,
  initialVendorValues,
} from "./VendorSchema";
import { TextField, Box } from "@mui/material";
import { ConfirmButton } from "@/components/Button/ConfirmButton";
import { CancelButton } from "@/components/Button/CancelButton";
import { useRouter } from "next/navigation";

interface Props {
  onSubmit: SubmitHandler<VendorFormType>;
  defaultValues?: VendorFormType;
  isLoading?: boolean;
  isEdit?: boolean;
}

export const VendorForm = ({
  onSubmit,
  defaultValues = initialVendorValues,
  isLoading = false,
  isEdit = false,
}: Props) => {
  const router = useRouter();

  const {
    handleSubmit,
    register,
    formState: { errors },
    reset, 
  } = useForm<VendorFormType>({
    defaultValues,
    resolver: yupResolver(VendorSchema),
  });

   useEffect(() => {
    reset(defaultValues);
  }, [defaultValues, reset]);

  return (
    <form onSubmit={handleSubmit(onSubmit)} noValidate>
      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <TextField
          label="Nombre Comercial"
          fullWidth
          {...register("name")}
          error={!!errors.name}
          helperText={errors.name?.message}
        />
        <TextField
          label="Razón Social"
          fullWidth
          {...register("socialReason")}
          error={!!errors.socialReason}
          helperText={errors.socialReason?.message}
        />
        <TextField
          label="Cédula Jurídica"
          fullWidth
          {...register("fiscalNumber")}
          error={!!errors.fiscalNumber}
          helperText={errors.fiscalNumber?.message}
        />
        <TextField
          label="Teléfono"
          fullWidth
          {...register("telephone")}
          error={!!errors.telephone}
          helperText={errors.telephone?.message}
        />
        <TextField
          label="Correo Electrónico"
          fullWidth
          {...register("email")}
          error={!!errors.email}
          helperText={errors.email?.message}
        />
        <TextField
          label="Distrito"
          type="number"
          fullWidth
          {...register("districtId")}
          error={!!errors.districtId}
          helperText={errors.districtId?.message}
        />
        <div className="sm:col-span-2">
          <TextField
            label="Dirección"
            fullWidth
            multiline
            rows={2}
            {...register("address")}
            error={!!errors.address}
            helperText={errors.address?.message}
          />
        </div>
      </div>

      <Box display="flex" justifyContent="flex-end" gap={2} mt={4}>
        <ConfirmButton
          type="submit"
          isLoading={isLoading}
          label={isEdit ? "Actualizar" : "Confirmar"}
        />
        <CancelButton onClick={() => router.push("/Vendor")} label="Cancelar" />
      </Box>
    </form>
  );
};
