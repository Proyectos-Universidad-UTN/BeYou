"use client";

import { useForm, SubmitHandler, FormProvider } from "react-hook-form";
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
  Alert,
  Stack,
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

  const formMethods = useForm<ProductFormType>({
    defaultValues,
    resolver: yupResolver(ProductSchema),
  });

  const {
    handleSubmit,
    register,
    formState: { errors },
    reset,
    watch,
    setValue,
  } = formMethods;

  useEffect(() => {
    reset(defaultValues);
  }, [defaultValues, reset]);

  const categoryId = watch("categoryId");
  const unitMeasureId = watch("unitMeasureId");

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

        <Stack spacing={3} maxWidth="600px">
          <Box>
            <TextField
              label="Nombre"
              fullWidth
              {...register("name")}
              error={!!errors.name}
              helperText={errors.name?.message}
            />
          </Box>

          <Box>
            <TextField
              label="Marca"
              fullWidth
              {...register("brand")}
              error={!!errors.brand}
              helperText={errors.brand?.message}
            />
          </Box>

          <Box>
            <TextField
              label="Precio"
              type="number"
              fullWidth
              {...register("price")}
              error={!!errors.price}
              helperText={errors.price?.message}
            />
          </Box>

          <Box>
            <TextField
              label="SKU"
              fullWidth
              {...register("sku")}
              error={!!errors.sku}
              helperText={errors.sku?.message}
            />
          </Box>

          <Box>
            <TextField
              label="Categoría"
              select
              fullWidth
              value={categoryId}
              onChange={(e) => setValue("categoryId", Number(e.target.value))}
              error={!!errors.categoryId}
              helperText={errors.categoryId?.message}
            >
              {CATEGORY_OPTIONS.map((option) => (
                <MenuItem key={option.value} value={option.value}>
                  {option.label}
                </MenuItem>
              ))}
            </TextField>
          </Box>

          <Box>
            <TextField
              label="Unidad de Medida"
              select
              fullWidth
              value={unitMeasureId}
              onChange={(e) => setValue("unitMeasureId", Number(e.target.value))}
              error={!!errors.unitMeasureId}
              helperText={errors.unitMeasureId?.message}
            >
              {UNIT_MEASURE_OPTIONS.map((option) => (
                <MenuItem key={option.value} value={option.value}>
                  {option.label}
                </MenuItem>
              ))}
            </TextField>
          </Box>

          <Box>
            <FormControlLabel
              control={
                <Switch
                  checked={watch("active")}
                  onChange={(e) => setValue("active", e.target.checked)}
                />
              }
              label="Activo"
            />
          </Box>

          <Box>
            <TextField
              label="Descripción"
              fullWidth
              multiline
              rows={3}
              {...register("description")}
              error={!!errors.description}
              helperText={errors.description?.message}
            />
          </Box>

          <Box display="flex" justifyContent="flex-end" gap={2}>
            <ConfirmButton
              type="submit"
              isLoading={isLoading}
              label={isEdit ? "Actualizar" : "Confirmar"}
            />
            <CancelButton
              onClick={() => router.push("/Product")}
              label="Cancelar"
            />
          </Box>
        </Stack>
      </form>
    </FormProvider>
  );
};
