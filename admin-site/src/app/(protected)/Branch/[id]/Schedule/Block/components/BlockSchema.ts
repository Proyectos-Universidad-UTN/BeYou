import { hourValidation, endHourValidation } from "@/components/Shared/SchemaValidations";
import { InferType, number, object } from "yup";

export const BlockDefaultValues = {
    id: 0,
    branchScheduleId: 0,
    startHour: '',
    endHour: ''
};

export const BlockSchema = object().shape({
    id: number().defined().default(0), // Ensure 'id' is defined and has a default
    branchScheduleId: number().defined().default(0), // Ensure 'branchScheduleId' is defined and has a default
    startHour: hourValidation,
    endHour: endHourValidation
});

export type BlockForm = InferType<typeof BlockSchema>;