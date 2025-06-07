import { InferType, object, string } from "yup";

export const LoginDefaultValues = {
  email: "",
  password: "",
};

export const LoginSchema = object({
  email: string()
    .required("El correo electrónico es requerido")
    .email("Ingrese un correo electrónico válido"),
  password: string().required("La contraseña es requerida"),
});

export type LoginTypeForm = InferType<typeof LoginSchema>;
