import { InferType, number, object, string } from "yup";

export const ScheduleDefaultValues = {
  day: 1,
  startHour: "08:00",
  endHour: "17:00",
};

export const ScheduleSchema = object().shape({
  day: number().min(1).max(7).required("El día es requerido"),
  startHour: string().required("La hora de inicio es requerida"),
  endHour: string().required("La hora de finalización es requerida"),
});

export type ScheduleForm = InferType<typeof ScheduleSchema>;
