import * as yup from "yup";
import { InferType, number, object, string } from "yup";

export const hourValidation = string()
  .required("La hora es requerida")
  .matches(/^([01]\d|2[0-3]):([0-5]\d)$/, "Formato de hora inválido. Use HH:mm");

export const endHourValidation = string()
  .required("La hora es requerida")
  .matches(/^([01]\d|2[0-3]):([0-5]\d)$/, "Formato de hora inválido. Use HH:mm")
  .test(
    "endHour-after-startHour",
    "La hora de fin debe ser mayor a la hora de inicio",
    function (value) {
      const { startHour } = this.parent;
      if (!startHour || !value) return true;
      const toMinutes = (time: string) => {
        const [h, m] = time.split(":").map(Number);
        return h * 60 + m;
      };
      return toMinutes(value) > toMinutes(startHour);
    }
  );

export const BlockDefaultValues = {
  id: 0,
  branchScheduleId: 0,
  startHour: "",
  endHour: "",
};

export const BlockSchema = object({
  id: number().default(0),
  branchScheduleId: number().default(0).required("ID de programación obligatorio"),
  startHour: hourValidation,
  endHour: endHourValidation,
});

export type BlockForm = InferType<typeof BlockSchema>;
