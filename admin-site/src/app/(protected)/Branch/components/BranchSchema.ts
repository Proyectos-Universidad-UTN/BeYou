import * as yup from "yup";
import { InferType } from "yup";

export const initialBranchValues = {
  name: "",
  description: "",
  telephone: "",
  email: "",
  districtId: 0,
  address: "",
};

export const BranchSchema = yup.object({
  name: yup.string().required("Nombre de la sucursal requerido"),
  description: yup.string().required("Descripción requerida"),
  telephone: yup
    .string()
    .required("Teléfono requerido")
    .matches(/^[0-9]{8}$/, "El teléfono debe tener 8 dígitos"),
  email: yup
    .string()
    .email("Correo electrónico inválido")
    .required("Correo electrónico requerido"),
  districtId: yup
    .number()
    .min(1, "Debe seleccionar un distrito válido")
    .required("Distrito requerido"),
  address: yup
    .string()
    .nullable()
    .notRequired()
    .default(null),
});

export type BranchFormType = InferType<typeof BranchSchema>;
