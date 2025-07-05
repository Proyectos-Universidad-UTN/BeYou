import * as yup from "yup";
import { InferType } from "yup";

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

export type ProductFormType = InferType<typeof ProductSchema>;
