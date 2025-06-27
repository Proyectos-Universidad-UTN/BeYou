"use client";

import dayjs from "dayjs";
import { Box } from "@mui/material";
import { TimePicker } from "@mui/x-date-pickers";
import {
  Control,
  Controller,
  FieldErrors,
  FieldValues,
  Path,
} from "react-hook-form";
import { FormFieldErrorMessage } from "@/components/FormFieldErrorMessage"; 

interface TimePickerFieldProps<T extends FieldValues> {
  name: Path<T>;
  label: string;
  control: Control<T>;
  errors: FieldErrors<T>;
}

export const TimePickerField = <T extends FieldValues>({
  name,
  label,
  control,
  errors,
}: TimePickerFieldProps<T>) => {
  return (
    <Box sx={{ flex: 1 }}>
      <Controller
        name={name}
        control={control}
        render={({ field }) => (
          <TimePicker
            label={label}
            sx={{ width: "100%" }}
            value={field.value ? dayjs(field.value, "HH:mm") : null}
            onChange={(newValue) =>
              field.onChange(newValue ? newValue.format("HH:mm") : "")
            }
            format="HH:mm"
          />
        )}
      />
      {errors[name]?.message && (
        <FormFieldErrorMessage
          message={String(errors[name]?.message)}
        />
      )}
    </Box>
  );
};