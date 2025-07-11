import * as yup from "yup";
import { InferType } from "yup";
import { Branch } from "@/types/api-beyou";
import { applyPhoneMask, telephoneMaskRegex } from "@/utils/util";

export const initialBranchValues = {
  id: 0,
  name: "",
  description: "",
  telephone: "",
  email: "",
  districtId: 0,
  address: "",
};

export const BranchSchema = yup.object({
  id: yup.number().default(0),
  name: yup.string().required("Nombre de la sucursal requerido"),
  description: yup.string().required("Descripción requerida"),
  telephone: yup.string().matches(telephoneMaskRegex, { message: 'Ingrese un número de teléfono válido' }).required('Ingrese el número de teléfono').max(9),
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

export const ConvertToBranchSchema = (branch: Branch | null | undefined): BranchFormType => {
  if (!branch) {
    return initialBranchValues;
  }
  return {
    id: Number(branch.id),
    name: String(branch.name),
    description: String(branch.description),
    telephone: applyPhoneMask(String(branch.telephone)),
    email: String(branch.email),
    districtId: Number(branch.districtId),
    address: branch.address !== undefined && branch.address !== null ? String(branch.address) : null,
  };
};

export type BranchFormType = InferType<typeof BranchSchema>;
