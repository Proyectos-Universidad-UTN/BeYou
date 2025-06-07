import { isNil } from "lodash";
import { ApiError } from "openapi-typescript-fetch";
import { ErrorDetailsBeYou, MonthName, WeeklyDay } from "@/types/api-beyou";

export const timeRegex = /^([01]?\d|2[0-3]):([0-5]?\d)$/;
export const telephoneMaskRegex = /^\d{4}-\d{4}$/;

export const weekDays: WeeklyDay[] = [1, 2, 3, 4, 5, 6, 7];

export const weekDaysSpanish: Record<WeeklyDay, string> = {
  1: "Lunes",
  2: "Martes",
  3: "Miércoles",
  4: "Jueves",
  5: "Viernes",
  6: "Sábado",
  7: "Domingo",
};

export const months: Record<MonthName, string> = {
  1: "Enero",
  2: "Febrero",
  3: "Marzo",
  4: "Abril",
  5: "Mayo",
  6: "Junio",
  7: "Julio",
  8: "Agosto",
  9: "Septiembre",
  10: "Octubre",
  11: "Noviembre",
  12: "Diciembre",
};
export const isPresent = <T>(t: T): t is NonNullable<T> => {
  return t !== null && t !== undefined;
};
export const convertToArray = <T>(value: T | readonly T[] | undefined): T[] => {
  if (!value) return [];
  if (Array.isArray(value)) {
    return Array.from(value);
  }
  return [value as T];
};
export const removePhoneMask = (phone: string): number => {
  const unmaskedPhone = phone.replace(/[^\d]/g, "");
  return parseInt(unmaskedPhone, 10);
};
export const applyPhoneMask = (phone: string): string => {
  const numericPhone = phone.replace(/\D/g, "");
  if (numericPhone.length !== 8) {
    return phone;
  }
  return `${numericPhone.slice(0, 4)}-${numericPhone.slice(4)}`;
};
const toCamelCase = (str: string): string => {
  return str
    .replace(/([A-Z])/g, (match) => `_${match.toLowerCase()}`)
    .replace(/^_/, "");
};
export const transformErrorKeys = (
  error: Record<string, any>
): Record<string, any> => {
  const transformedError: Record<string, any> = {};
  for (const key in error) {
    if (Object.prototype.hasOwnProperty.call(error, key)) {
      transformedError[toCamelCase(key)] = error[key];
    }
  }
  return transformedError;
};
export const getErrorMessage = (error: ApiError) => {
  const errorDetail = transformErrorKeys(error.data as ErrorDetailsBeYou);
  return errorDetail.message;
};
export const getDayInSpanish = (day: WeeklyDay | undefined): string => {
  if (isNil(day)) {
    return "";
  }
  return weekDaysSpanish[day] ?? "";
};
export const getMonthInSpanish = (month: MonthName | undefined): string => {
  if (isNil(month)) {
    return "";
  }
  return months[month] ?? "";
};
export const getNestedField = (obj: any, field: string) => {
  return field
    .split(".")
    .reduce(
      (acc, part) => (acc && acc[part] !== undefined ? acc[part] : null),
      obj
    );
};
