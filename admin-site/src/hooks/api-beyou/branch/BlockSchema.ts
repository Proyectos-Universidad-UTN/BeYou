import { endHourValidation, hourValidation } from "@/components/Shared/SchemaValidations";
import { InferType, number, object } from "yup";


export const BlockDefaultValues = {
    id: 0,
    branchScheduleId: 0,
    startHour: '',
    endHour: ''
};

export const BlockSchema = object().shape({
    id: number(),
    branchScheduleId: number(),
    startHour: hourValidation,
    endHour: endHourValidation
})

export type BlockForm = InferType<typeof BlockSchema>