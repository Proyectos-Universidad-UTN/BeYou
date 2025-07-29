// src/app/(protected)/Reservation/components/ReservationForm.tsx
"use client";

import { useEffect } from "react";
import { Stack } from "@mui/system";
import {
  Box,
  TextField,
  Alert,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
  Button, // Para los botones de agregar/eliminar items si los incluyes
  Typography
} from "@mui/material";
import { useForm, SubmitHandler, FormProvider, useFieldArray } from "react-hook-form"; // Importa useFieldArray
import { yupResolver } from "@hookform/resolvers/yup";
import { useIsMobile } from "@/hooks/UseIsMobile";
import { FormButtons } from "@/components/Shared/FormButtons";
import { FormFieldErrorMessage } from "@/components/FormFieldErrorMessage";

import {
  ReservationSchema,
  type ReservationFormType,
  initialReservationValues,
} from "./ReservationSchema";
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { TimePicker } from '@mui/x-date-pickers/TimePicker';
import dayjs from "dayjs";
import 'dayjs/locale/es'; // Importa el locale español para Dayjs




interface ReservationFormProps {
  onSubmit: SubmitHandler<ReservationFormType>;
  defaultValues?: ReservationFormType;
  isLoading?: boolean;
  isEdit?: boolean;
  customers?: { id: number; name: string }[];
  branches?: { id: number; name: string }[];
  services?: { id: number; name: string }[];
}

export const ReservationForm = ({
  onSubmit,
  defaultValues = initialReservationValues,
  isLoading = false,
  isEdit = false,
  customers = [],
  branches = [],
  services = [],
}: ReservationFormProps) => {
  const isMobile = useIsMobile();

  const formMethods = useForm<ReservationFormType>({
    // Asegura que defaultValues sea del tipo correcto.
    // El tipo de `defaultValues` debe coincidir exactamente con `ReservationFormType`
    defaultValues: defaultReservationValues(defaultValues),
    resolver: yupResolver(ReservationSchema),
  });

  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
    setValue,
    watch,
    control, // Necesario para useFieldArray
  } = formMethods;

  // Manejo de arrays anidados (ReservationDetails)
  const { fields: detailFields, append: appendDetail, remove: removeDetail } = useFieldArray({
    control,
    name: "reservationDetails", // Debe coincidir con el nombre en ReservationFormType
  });

  // Manejo de arrays anidados (ReservationQuestions) - si los necesitas
  const { fields: questionFields, append: appendQuestion, remove: removeQuestion } = useFieldArray({
    control,
    name: "reservationQuestion", // Debe coincidir con el nombre en ReservationFormType
  });

  useEffect(() => {
    // Cuando defaultValues cambien (ej. carga de datos para edición)
    // Se necesitan ajustes especiales para 'date' y 'hour' porque son strings
    // pero los DatePickers/TimePickers manejan objetos Dayjs.
    // La función `defaultReservationValues` ya hace esto.
    reset(defaultReservationValues(defaultValues));
  }, [defaultValues, reset]);

  // Watchers para los campos principales
  const watchedDate = watch("date");
  const watchedHour = watch("hour");
  const watchedCustomerId = watch("customerId");
  const watchedBranchId = watch("branchId");
  const watchedStatus = watch("status");


  return (
    <FormProvider {...formMethods}>
      <form onSubmit={handleSubmit(onSubmit)} noValidate>
        <Box pb={2}>
          {Object.keys(errors).length > 0 && (
            <Alert severity="error">
              Por favor corrija los errores para continuar
            </Alert>
          )}
        </Box>

        <Stack spacing={3} maxWidth={isMobile ? "90vw" : "600px"}>
          {isEdit && defaultValues.id && ( // Solo mostrar si estamos editando y hay un ID
            <TextField
              label="ID de Reserva"
              fullWidth
              value={defaultValues.id} // El ID para mostrar
              InputProps={{ readOnly: true }}
              disabled
            />
          )}

          {/* Nombre del Cliente - solo para visualización si customerId se usa para el PUT/POST */}
          <TextField
            label="Nombre del Cliente"
            fullWidth
            {...register("customerName")}
            error={!!errors.customerName}
            helperText={errors.customerName?.message}
            // Puedes hacerlo de solo lectura si el nombre se deriva de la selección de customerId
            // InputProps={{ readOnly: true }}
          />
          {errors.customerName?.message && (
            <FormFieldErrorMessage message={errors.customerName.message} />
          )}

          {/* Selector de Cliente */}
          <FormControl fullWidth error={!!errors.customerId}>
            <InputLabel id="customer-label">Cliente</InputLabel>
            <Select
              labelId="customer-label"
              id="customerId"
              label="Cliente"
              {...register("customerId", { valueAsNumber: true })} // Asegura que el valor sea un número
              value={watchedCustomerId || ""} // Asegura que el valor sea controlado
            >
              <MenuItem value={0}>Seleccione un cliente</MenuItem>
              {customers.map((customer) => (
                <MenuItem key={customer.id} value={customer.id}>
                  {customer.name}
                </MenuItem>
              ))}
            </Select>
            {errors.customerId?.message && (
              <FormFieldErrorMessage message={errors.customerId.message} />
            )}
          </FormControl>


          <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="es">
            {/* DatePicker para la fecha */}
            <DatePicker
              label="Fecha de la Reserva"
              format="YYYY-MM-DD"
              // Usar un `dayjs` object para el `value` del DatePicker
              value={watchedDate ? dayjs(watchedDate) : null}
              onChange={(newValue) => {
                // Convertir el Dayjs object de vuelta a string para el formulario
                setValue("date", newValue ? dayjs(newValue).format("YYYY-MM-DD") : "", { shouldValidate: true });
              }}
              slotProps={{
                textField: {
                  fullWidth: true,
                  error: !!errors.date,
                  helperText: errors.date?.message,
                },
              }}
            />
             {errors.date?.message && (
                <FormFieldErrorMessage message={errors.date.message} />
            )}

            {/* TimePicker para la hora */}
            <TimePicker
              label="Hora de la Reserva"
              format="HH:mm"
              // Usar un `dayjs` object para el `value` del TimePicker
              value={watchedHour ? dayjs(`2000-01-01T${watchedHour}`) : null} // Fecha ficticia para parsear solo la hora
              onChange={(newValue) => {
                // Convertir el Dayjs object de vuelta a string para el formulario
                setValue("hour", newValue ? dayjs(newValue).format("HH:mm") : "", { shouldValidate: true });
              }}
              slotProps={{
                textField: {
                  fullWidth: true,
                  error: !!errors.hour,
                  helperText: errors.hour?.message,
                },
              }}
            />
            {errors.hour?.message && (
                <FormFieldErrorMessage message={errors.hour.message} />
            )}
          </LocalizationProvider>

          {/* Selector de Sucursal */}
          <FormControl fullWidth error={!!errors.branchId}>
            <InputLabel id="branch-label">Sucursal</InputLabel>
            <Select
              labelId="branch-label"
              id="branchId"
              label="Sucursal"
              {...register("branchId", { valueAsNumber: true })}
              value={watchedBranchId || ""}
            >
              <MenuItem value={0}>Seleccione una sucursal</MenuItem>
              {branches.map((branch) => (
                <MenuItem key={branch.id} value={branch.id}>
                  {branch.name}
                </MenuItem>
              ))}
            </Select>
            {errors.branchId?.message && (
              <FormFieldErrorMessage message={errors.branchId.message} />
            )}
          </FormControl>

          {/* Selector de Estado */}
          <FormControl fullWidth error={!!errors.status}>
            <InputLabel id="status-label">Estado</InputLabel>
            <Select
              labelId="status-label"
              id="status"
              label="Estado"
              {...register("status")}
              value={watchedStatus || ""}
            >
              <MenuItem value="P">Pendiente</MenuItem>
              <MenuItem value="C">Confirmada</MenuItem>
              <MenuItem value="X">Cancelada</MenuItem>
            </Select>
            {errors.status?.message && (
              <FormFieldErrorMessage message={errors.status.message} />
            )}
          </FormControl>

          <Box>
            
            <Typography variant="h6">Servicios/Productos de la Reserva</Typography>
            {detailFields.map((field, index) => (
              <Box key={field.id} sx={{ border: '1px solid #ccc', p: 2, mb: 1, borderRadius: '4px' }}>
                <FormControl fullWidth error={!!errors.reservationDetails?.[index]?.serviceId}>
                  <InputLabel id={`service-label-${index}`}>Servicio</InputLabel>
                  <Select
                    labelId={`service-label-${index}`}
                    id={`serviceId-${index}`}
                    label="Servicio"
                    {...register(`reservationDetails.${index}.serviceId`, { valueAsNumber: true })}
                    value={watch(`reservationDetails.${index}.serviceId`) || ""}
                  >
                    <MenuItem value={0}>Seleccione un servicio</MenuItem>
                    {services.map((service) => (
                      <MenuItem key={service.id} value={service.id}>
                        {service.name}
                      </MenuItem>
                    ))}
                  </Select>
                  {errors.reservationDetails?.[index]?.serviceId?.message && (
                    <FormFieldErrorMessage message={errors.reservationDetails[index].serviceId.message} />
                  )}
                </FormControl>

                

                <Button
                  onClick={() => removeDetail(index)}
                  color="error"
                  variant="outlined"
                  sx={{ mt: 1 }}
                >
                  Eliminar Servicio
                </Button>
              </Box>
            ))}
            <Button
              onClick={() => appendDetail({ id: undefined, reservationId: undefined, serviceId: 0, productId: undefined })}
              variant="contained"
              sx={{ mt: 2 }}
            >
              Agregar Servicio
            </Button>
            {errors.reservationDetails?.message && (
                <FormFieldErrorMessage message={errors.reservationDetails.message} />
            )}
          </Box>


         


          <FormButtons backPath="/Reservation" loadingIndicator={isLoading} />
        </Stack>
      </form>
    </FormProvider>
  );
};

// Helper para asegurar que los defaultValues se ajusten a ReservationFormType
const defaultReservationValues = (values: ReservationFormType): ReservationFormType => {
  return {
    id: values.id ?? undefined,
    customerName: values.customerName ?? "",
    customerId: values.customerId ?? 0,
    date: values.date ?? "",
    hour: values.hour ?? "",
    branchId: values.branchId ?? 0,
    status: values.status ?? "P",
    reservationQuestion: values.reservationQuestion ?? [],
    reservationDetails: values.reservationDetails ?? [],
  };
};