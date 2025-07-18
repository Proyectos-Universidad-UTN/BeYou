import * as yup from "yup";
import { InferType } from "yup";
import { Product } from "@/types/api-beyou";

export const initialProductValues = {
  name: "",
  description: "",
  brand: "",
  price: 0,
  sku: "",
  categoryId: 0,
  unitMeasureId: 0,
  active: true,
};

export const ProductSchema = yup.object({
  name: yup.string().required("Nombre requerido"),
  description: yup.string().required("Descripción requerida"),
  brand: yup.string().required("Marca requerida"),
  price: yup
    .number()
    .typeError("Precio requerido")
    .positive("Debe ser un número positivo")
    .required("Precio requerido"),
  sku: yup.string().required("SKU requerido"),
  categoryId: yup
    .number()
    .typeError("Categoría requerida")
    .min(1, "Categoría requerida")
    .required(),
  unitMeasureId: yup
    .number()
    .typeError("Unidad requerida")
    .min(1, "Unidad requerida")
    .required(),
  active: yup.boolean().required(),
});

export const ConvertToProductSchema = (
  product: Product | null | undefined
): ProductFormType => {
  if (!product) {
    return initialProductValues;
  }
  return {
    name: product.name ?? "",
    description: product.description ?? "",
    brand: product.brand ?? "",
    price: product.price ?? 0,
    sku: product.sku ?? "",
    categoryId: product.categoryId ?? 0,
    unitMeasureId: product.unitMeasureId ?? 0,
    active: product.active ?? true,
  };
};

export type ProductFormType = InferType<typeof ProductSchema>;
