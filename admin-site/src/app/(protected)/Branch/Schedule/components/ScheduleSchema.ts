import { InferType, number, object } from "yup";

export const ScheduleDefaultValues = {
    scheduleId: 0,
};

export const ScheduleSchema = object().shape({
    scheduleId: number().min(1, 'El horario es requerido').required('El horario es requerido'),
})

export type ScheduleForm = InferType<typeof ScheduleSchema>