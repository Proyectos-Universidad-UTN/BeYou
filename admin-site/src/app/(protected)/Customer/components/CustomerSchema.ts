import * as yup from "yup";
import { InferType } from "yup";

export const initialCustomerValues = {
  firstName: "",
  lastName: "",
  telephone: "",
  email: "",
  districtId: 0,
  address: "",
};

export const CustomerSchema = yup.object({
  firstName: yup.string().required("Nombre requerido"),
  lastName: yup.string().required("Apellido requerido"),
  telephone: yup
    .string()
    .required("Teléfono requerido")
    .matches(/^[0-9]{8}$/, "El teléfono debe tener 8 dígitos"),
  email: yup.string().email("Correo inválido").required("Correo requerido"),
  districtId: yup.number().min(1, "Distrito requerido").required(),
  address: yup
    .string()
    .nullable()
    .notRequired()
    .default(null),
});

export type CustomerFormType = InferType<typeof CustomerSchema>;
