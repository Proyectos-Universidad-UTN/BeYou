// src/app/(protected)/Reservation/components/ReservationSchema.ts
import * as yup from "yup";
import { InferType } from "yup";


export const ReservationSchema = yup.object({
  // `id` es opcional en la estructura del objeto y puede ser número o null.
  // `.optional()` aquí hace que la propiedad `id` pueda no existir en el objeto
  // `.nullable()` permite que su valor sea `null` si existe.
  id: yup.number().nullable().optional(), // `id?: number | null | undefined`

  customerName: yup.string().required("Nombre del cliente es requerido"),
  customerId: yup.number().required("Cliente es requerido").min(1, "Cliente inválido"),

  date: yup.string().required("Fecha es requerida")
    .matches(/^\d{4}-\d{2}-\d{2}$/, "Formato de fecha inválido (YYYY-MM-DD)"),
  hour: yup.string().required("Hora es requerida")
    .matches(/^\d{2}:\d{2}$/, "Formato de hora inválido (HH:mm)"),

  branchId: yup.number().required("Sede es requerida").min(1, "Sede inválida"),
  status: yup.string()
    .oneOf(["P", "C", "X"], "Estado inválido")
    .required("Estado es requerido"),

  reservationQuestion: yup.array(ReservationQuestionSchema).nullable().optional(),
  reservationDetails: yup.array(ReservationDetailSchema).min(1, "Debe agregar al menos un servicio o producto").required("Detalles de la reserva requeridos"),
});

// La inferencia aquí debería ser correcta:
export type ReservationFormType = InferType<typeof ReservationSchema>;

// No es necesario cambiar initialReservationValues, ya es compatible con el tipo inferido
export const initialReservationValues: ReservationFormType = {
  id: undefined,
  customerName: "",
  customerId: 0,
  date: "",
  hour: "",
  branchId: 0,
  status: "P",
  reservationQuestion: [],
  reservationDetails: [],
};