// ReservationSchema.ts
import * as yup from "yup";

export interface ReservationFormType {
  customerId: number;
  customerName: string | null;
  date: string;
  hour: string;
  status: "P" | "C" | "X";
  branchId?: number;
  reservationQuestion?: any[];
  reservationDetails?: any[];
  id?: number;
}

export const initialReservationValues: ReservationFormType = {
  customerId: 8,
  customerName: "Contacto",
  date: "",
  hour: "",
  status: "P",
  branchId: 1,
  reservationQuestion: [],
  reservationDetails: [],
  id: 0,
};

export const ReservationSchema = yup.object().shape({
  customerId: yup
    .number()
    .required("Debe seleccionar un cliente")
    .min(1, "Seleccione un cliente válido"),
  customerName: yup.string().nullable().default(""),
  date: yup
    .string()
    .required("La fecha es obligatoria")
    .matches(/^\d{4}-\d{2}-\d{2}$/, "Formato: YYYY-MM-DD"),
  hour: yup
    .string()
    .required("La hora es obligatoria")
    .matches(/^([0-1]\d|2[0-3]):([0-5]\d):([0-5]\d)$/, "Formato: HH:mm:ss"),
  status: yup
    .string()
    .oneOf(["P", "C", "X"], "Estado inválido")
    .required("El estado es obligatorio"),
});
