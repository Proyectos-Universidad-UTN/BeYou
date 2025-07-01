import * as yup from "yup";
import { InferType } from "yup";

export const initialVendorValues = {
  name: "",
  fiscalNumber: "",
  socialReason: "",
  telephone: "",
  email: "",
  districtId: 0,
  address: "", 
};

export const VendorSchema = yup.object({
  name: yup.string().required("Nombre del proveedor requerido"),
  fiscalNumber: yup.string().required("Número Cédula Jurídica requerido"),
  socialReason: yup.string().required("Razón social requerida"),
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

export type VendorFormType = InferType<typeof VendorSchema>;
