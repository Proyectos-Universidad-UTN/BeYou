"use client";

import { useForm, SubmitHandler } from "react-hook-form";
import { useEffect } from "react";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  BranchSchema,
  type BranchFormType,
  initialBranchValues,
} from "./BranchSchema";
import { TextField, Box } from "@mui/material";
import { ConfirmButton } from "@/components/Button/ConfirmButton";
import { CancelButton } from "@/components/Button/CancelButton";
import { useRouter } from "next/navigation";

interface Props {
  onSubmit: SubmitHandler<BranchFormType>;
  defaultValues?: BranchFormType;
  isLoading?: boolean;
  isEdit?: boolean;
}

export const BranchForm = ({
  onSubmit,
  defaultValues = initialBranchValues,
  isLoading = false,
  isEdit = false,
}: Props) => {
  const router = useRouter();

  const {
    handleSubmit,
    register,
    formState: { errors },
    reset,
  } = useForm<BranchFormType>({
    defaultValues,
    resolver: yupResolver(BranchSchema),
  });

  useEffect(() => {
    reset(defaultValues);
  }, [defaultValues, reset]);

  return (
    <form onSubmit={handleSubmit(onSubmit)} noValidate>
      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <TextField
          label="Nombre de Sucursal"
          fullWidth
          {...register("name")}
          error={!!errors.name}
          helperText={errors.name?.message}
        />
        <TextField
          label="Descripción"
          fullWidth
          {...register("description")}
          error={!!errors.description}
          helperText={errors.description?.message}
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
        <CancelButton onClick={() => router.push("/Branch")} label="Cancelar" />
      </Box>
    </form>
  );
};
