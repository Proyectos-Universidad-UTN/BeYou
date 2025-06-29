import * as yup from "yup";
import { InferType } from "yup";

export const initialUserValues = {
  cardId: "",
  firstName: "",
  lastName: "",
  telephone: "",
  email: "",
  districtId: 0,
  address: "",
  birthday: "",
  password: "",
  genderId: 0,
  profilePictureUrl: null,
  roleId: 0,
};

export const UserSchema = yup.object({
  cardId: yup
    .string()
    .required("Cédula requerida")
    .min(9, "Debe tener al menos 9 caracteres"),
  firstName: yup.string().required("Nombre requerido"),
  lastName: yup.string().required("Apellido requerido"),
  telephone: yup
    .string()
    .required("Teléfono requerido")
    .matches(/^[0-9]{8}$/, "El teléfono debe tener 8 dígitos"),
  email: yup.string().email("Correo inválido").required("Correo requerido"),
  districtId: yup.number().min(1, "Distrito requerido").required(),
  address: yup.string().required("Dirección requerida"),
  birthday: yup
    .string()
    .required("Fecha de nacimiento requerida")
    .matches(/^\d{4}-\d{2}-\d{2}$/, "Formato de fecha inválido (yyyy-mm-dd)"),
  password: yup
    .string()
    .required("Contraseña requerida")
    .min(6, "Debe tener al menos 6 caracteres"),
  genderId: yup.number().min(1, "Género requerido").required(),
  profilePictureUrl: yup.string().nullable(),
  roleId: yup.number().min(1, "Rol requerido").required(),
});

export type UserFormType = InferType<typeof UserSchema>;