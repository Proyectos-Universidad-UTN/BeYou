/* eslint-disable @typescript-eslint/no-explicit-any */
/**
 * Utility functions for data formatting, validation, error handling, and date manipulation.
 * 
 * This module provides reusable helpers such as:
 * - Regex patterns for time and telephone formats
 * - Date formatting and translation to Spanish
 * - Currency formatting
 * - Error transformation for API responses
 * - Utility helpers for arrays, strings, and nested fields
 * 
 * Intended to be used throughout the BeYou application for consistent data handling.
 */

import { isNil } from "lodash";
import { ApiError } from "openapi-typescript-fetch";
import { format } from "date-fns";
import { es } from "date-fns/locale";
import { ErrorDetailsBeYou, MonthName, WeeklyDay } from "@/types/api-beyou";
import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

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

export const getErrorMessage = (error: unknown): string => {
  if (
    typeof error === "object" &&
    error !== null &&
    "data" in error &&
    typeof (error as any).data === "object"
  ) {
    const data = (error as ApiError).data as ErrorDetailsBeYou;
    const transformed = transformErrorKeys(data);
    return transformed.message || "Error desconocido";
  }
  return "Error desconocido";
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

export const getInitials = (fullName?: string): string => {
  if (!fullName) return "US";
  const names = fullName.trim().split(" ");
  const initials = names
    .slice(0, 2)
    .map((n) => n.charAt(0).toUpperCase())
    .join("");
  return initials || "US";
};

export const formatErrorMessage = (error: ErrorDetailsBeYou): string => {
  const { detail, message } = error;

  if (Array.isArray(detail) && detail.length > 0) {
    return detail
      .map(
        (d: any) =>
          d?.ctx?.reason || d?.msg || "Error de validación desconocido"
      )
      .join("\n");
  }

  if (typeof detail === "string") {
    return detail;
  }

  return message ?? "Error desconocido";
};

export const formatCurrency = (
  value: number,
  options: {
    thousandSeparator?: string;
    decimalSeparator?: string;
    prefix?: string;
  } = {}
): string => {
  const {
    thousandSeparator = ",",
    decimalSeparator = ".",
    prefix = "$",
  } = options;

  const parts = Math.abs(value).toFixed(2).split(".");
  parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, thousandSeparator);

  const formattedValue = `${prefix}${parts.join(decimalSeparator)}`;
  return value < 0 ? `-${formattedValue}` : formattedValue;
};

export const formatDate = (date: string | Date | null): string => {
  if (!date) return "-";
  try {
    return format(new Date(date), "dd/MM/yyyy", { locale: es });
  } catch {
    return "-";
  }
};

interface SelectOption {
  value: string;
  label: string;
}

export const createSelectOptions = <T extends Record<string, any>>(
  items: T[] | undefined,
  config: {
    valueField: keyof T;
    labelField: keyof T;
  }
): SelectOption[] => {
  return (
    items?.map((item) => ({
      value: String(item[config.valueField]),
      label: String(item[config.labelField]),
    })) ?? []
  );
};
