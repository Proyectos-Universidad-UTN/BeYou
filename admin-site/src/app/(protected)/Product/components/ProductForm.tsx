"use client";

import { useForm, SubmitHandler } from "react-hook-form";
import { useEffect } from "react";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  ProductSchema,
  type ProductFormType,
  initialProductValues,
} from "./ProductSchema";
import {
  TextField,
  Box,
  MenuItem,
  FormControlLabel,
  Switch,
} from "@mui/material";
import { ConfirmButton } from "@/components/Button/ConfirmButton";
import { CancelButton } from "@/components/Button/CancelButton";
import { useRouter } from "next/navigation";

interface Props {
  onSubmit: SubmitHandler<ProductFormType>;
  defaultValues?: ProductFormType;
  isLoading?: boolean;
  isEdit?: boolean;
}

const CATEGORY_OPTIONS = [
  { value: 1, label: "Ropa Deportiva" },
  { value: 2, label: "Ropa Casual" },
];

const UNIT_MEASURE_OPTIONS = [
  { value: 1, label: "un" },
  { value: 2, label: "kg" },
];

export const ProductForm = ({
  onSubmit,
  defaultValues = initialProductValues,
  isLoading = false,
  isEdit = false,
}: Props) => {
  const router = useRouter();

  const {
    handleSubmit,
    register,
    formState: { errors },
    reset,
    watch,
    setValue,
  } = useForm<ProductFormType>({
    defaultValues,
    resolver: yupResolver(ProductSchema),
  });

  useEffect(() => {
    reset(defaultValues);
  }, [defaultValues, reset]);

  return (
    <form onSubmit={handleSubmit(onSubmit)} noValidate>
      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <TextField
          label="Nombre"
          fullWidth
          {...register("name")}
          error={!!errors.name}
          helperText={errors.name?.message}
        />

        <TextField
          label="Marca"
          fullWidth
          {...register("brand")}
          error={!!errors.brand}
          helperText={errors.brand?.message}
        />

        <TextField
          label="Precio"
          type="number"
          fullWidth
          {...register("price")}
          error={!!errors.price}
          helperText={errors.price?.message}
        />

        <TextField
          label="SKU"
          fullWidth
          {...register("sku")}
          error={!!errors.sku}
          helperText={errors.sku?.message}
        />

        <TextField
          label="Categoría"
          select
          fullWidth
          {...register("categoryId")}
          error={!!errors.categoryId}
          helperText={errors.categoryId?.message}
        >
          {CATEGORY_OPTIONS.map((option) => (
            <MenuItem key={option.value} value={option.value}>
              {option.label}
            </MenuItem>
          ))}
        </TextField>

        <TextField
          label="Unidad de Medida"
          select
          fullWidth
          {...register("unitMeasureId")}
          error={!!errors.unitMeasureId}
          helperText={errors.unitMeasureId?.message}
        >
          {UNIT_MEASURE_OPTIONS.map((option) => (
            <MenuItem key={option.value} value={option.value}>
              {option.label}
            </MenuItem>
          ))}
        </TextField>

        <FormControlLabel
          control={
            <Switch
              checked={watch("active")}
              onChange={(e) => setValue("active", e.target.checked)}
            />
          }
          label="Activo"
        />

        <div className="sm:col-span-2">
          <TextField
            label="Descripción"
            fullWidth
            multiline
            rows={3}
            {...register("description")}
            error={!!errors.description}
            helperText={errors.description?.message}
          />
        </div>
      </div>

      <Box display="flex" justifyContent="flex-end" gap={2} mt={4}>
        <ConfirmButton
          type="submit"
          isLoading={isLoading}
          label={isEdit ? "Actualizar" : "Confirmar"}
        />
        <CancelButton onClick={() => router.push("/Product")} label="Cancelar" />
      </Box>
    </form>
  );
};
