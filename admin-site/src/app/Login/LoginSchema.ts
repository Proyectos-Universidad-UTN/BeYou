import { InferType, object, string, boolean } from "yup";

export const LoginDefaultValues = {
  email: "",
  password: "",
  remember: false,
};

export const LoginSchema = object({
  email: string()
    .required("El correo electrónico es requerido")
    .email("Ingrese un correo electrónico válido"),
  password: string().required("La contraseña es requerida"),
  remember: boolean(),
});

export type LoginTypeForm = InferType<typeof LoginSchema>;
