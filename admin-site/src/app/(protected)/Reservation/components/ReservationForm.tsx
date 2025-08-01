"use client";

import { useEffect } from "react";
import { Box, MenuItem, Stack, TextField } from "@mui/material";
import {
  useForm,
  SubmitHandler,
  FormProvider,
  Controller,
} from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import {
  DatePicker,
  TimePicker,
  LocalizationProvider,
} from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import { parseISO, format } from "date-fns";

import {
  ReservationSchema,
  ReservationFormType,
  initialReservationValues,
} from "./ReservationSchema";

import { FormButtons } from "@/components/Shared/FormButtons";
import { UseGetCustomer } from "@/hooks/api-beyou/customer/UseGetCustomer";

interface ReservationFormProps {
  onSubmit: SubmitHandler<ReservationFormType>;
  defaultValues?: ReservationFormType;
  isLoading?: boolean;
  onCancel?: () => void;
}

export const ReservationForm = ({
  onSubmit,
  defaultValues = initialReservationValues,
  isLoading = false,
  onCancel,
}: ReservationFormProps) => {
  const formMethods = useForm<ReservationFormType>({
    defaultValues,
    resolver: yupResolver(ReservationSchema),
  });

  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
    reset,
  } = formMethods;

  const { data: customers = [], isLoading: loadingCustomers } =
    UseGetCustomer();

  useEffect(() => {
    const isCustomersReady = customers.length > 0;
    const isDefaultCustomerValid = customers.some(
      (c) => c.id === defaultValues.customerId
    );

    if (isCustomersReady && defaultValues) {
      reset({
        ...initialReservationValues,
        ...defaultValues,
        customerId: isDefaultCustomerValid ? defaultValues.customerId : 0,
        status: defaultValues.status || "P",
        customerName: defaultValues.customerName || "",
      } as ReservationFormType);
    }
  }, [defaultValues, customers, reset]);

  return (
    <FormProvider {...formMethods}>
      <LocalizationProvider dateAdapter={AdapterDateFns}>
        <form
          onSubmit={handleSubmit((data: ReservationFormType) => {
            if (!data.customerName) {
              data.customerName = "";
            }
            onSubmit(data);
          })}
          noValidate
        >
          <Box pb={2}>
            {Object.keys(errors).length > 0 && (
              <>
                <Box className="mb-4 text-red-600 font-semibold">
                  Por favor corrija los errores para continuar
                </Box>
              </>
            )}
          </Box>

          <Stack spacing={4} maxWidth="600px" className="mx-auto">
            {/* Cliente */}
            <Box>
              <Controller
                name="customerId"
                control={control}
                render={({ field }) => (
                  <TextField
                    select
                    label="Seleccione un cliente"
                    fullWidth
                    error={!!errors.customerId}
                    helperText={errors.customerId?.message}
                    disabled={loadingCustomers}
                    value={field.value ?? 0}
                    onChange={(event) => {
                      field.onChange(event);
                    }}
                  >
                    <MenuItem value={0} disabled>
                      Seleccione un cliente
                    </MenuItem>
                    {customers.map((customer) => (
                      <MenuItem key={customer.id} value={customer.id}>
                        {customer.firstName} {customer.lastName}
                      </MenuItem>
                    ))}
                  </TextField>
                )}
              />
            </Box>

            {/* Fecha */}
            <Box>
              <Controller
                name="date"
                control={control}
                render={({ field }) => {
                  const dateValue = field.value ? parseISO(field.value) : null;

                  return (
                    <DatePicker
                      label="Fecha"
                      value={dateValue}
                      onChange={(date) => {
                        if (date instanceof Date && !isNaN(date.getTime())) {
                          const isoDate = format(date, "yyyy-MM-dd");
                          field.onChange(isoDate);
                        } else {
                          field.onChange("");
                        }
                      }}
                      slotProps={{
                        textField: {
                          fullWidth: true,
                          error: !!errors.date,
                          helperText: errors.date?.message,
                        },
                      }}
                    />
                  );
                }}
              />
            </Box>

            {/* Hora */}
            <Box>
              <Controller
                name="hour"
                control={control}
                render={({ field }) => {
                  const hourValue = (() => {
                    if (!field.value) return null;
                    const [hours, minutes] = field.value.split(":").map(Number);
                    if (
                      Number.isInteger(hours) &&
                      Number.isInteger(minutes) &&
                      hours >= 0 &&
                      hours < 24 &&
                      minutes >= 0 &&
                      minutes < 60
                    ) {
                      const date = new Date();
                      date.setHours(hours, minutes, 0, 0);
                      return date;
                    }
                    return null;
                  })();

                  return (
                    <TimePicker
                      label="Hora"
                      value={hourValue}
                      onChange={(time) => {
                        if (time instanceof Date && !isNaN(time.getTime())) {
                          const hours = time
                            .getHours()
                            .toString()
                            .padStart(2, "0");
                          const minutes = time
                            .getMinutes()
                            .toString()
                            .padStart(2, "0");
                          field.onChange(`${hours}:${minutes}`);
                        } else {
                          field.onChange("");
                        }
                      }}
                      slotProps={{
                        textField: {
                          fullWidth: true,
                          error: !!errors.hour,
                          helperText: errors.hour?.message,
                        },
                      }}
                    />
                  );
                }}
              />
            </Box>

            {/* Estado */}
            <Box>
              <TextField
                select
                label="Estado"
                fullWidth
                defaultValue="P"
                error={!!errors.status}
                helperText={errors.status?.message}
                {...register("status")}
              >
                {[
                  { label: "Pendiente", value: "P" },
                  { label: "Confirmada", value: "C" },
                  { label: "Cancelada", value: "X" },
                ].map((option) => (
                  <MenuItem key={option.value} value={option.value}>
                    {option.label}
                  </MenuItem>
                ))}
              </TextField>
            </Box>

            <FormButtons backPath="/Reservation" loadingIndicator={isLoading} />
          </Stack>
        </form>
      </LocalizationProvider>
    </FormProvider>
  );
};
