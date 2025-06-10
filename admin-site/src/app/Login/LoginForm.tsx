'use client'; 
import { useRouter } from "next/navigation"; 
import { yupResolver } from "@hookform/resolvers/yup";
import { useCallback, useEffect, useState } from "react";
import { Box, Button, TextField, Typography } from "@mui/material";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { LoginDefaultValues, LoginSchema, LoginTypeForm } from "./LoginSchema";
import { isPresent } from "@/utils/util";
import { FormFieldErrorMessage } from "@/components/FormFieldErrorMessage";
import { useAuth } from "@/context/AuthContext";

export const LoginForm = () => {
  const formMethods = useForm({
    resolver: yupResolver(LoginSchema),
    defaultValues: LoginDefaultValues,
  });

  const router = useRouter();
  const [loading, setLoading] = useState(false);
  const {
    handleSubmit,
    control,
    formState: { errors },
  } = formMethods;

  const { login, isAuthenticated } = useAuth();

  const createLoginWrapper = useCallback(
    async (data: LoginTypeForm) => {
      setLoading(true);
      try {
        await login(data); // asumiendo que login es async y devuelve una promesa
      } finally {
        setLoading(false);
      }
    },
    [login]
  );

  useEffect(() => {
    if (isAuthenticated) {
      setLoading(false);
      router.push('/Dashboard');
    }
  }, [isAuthenticated, router]);

  return (
    <Box
      sx={{
        padding: { xs: "2rem", md: "3rem" },
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        backgroundColor: "#e6e6e6",
        borderRadius: "10px",
        xs: { width: "calc(100vh - 80px - 2rem)" },
        md: { width: "calc(100vh - 80px - 10rem)" },
        minHeight: { xs: "calc(40vh)", md: "calc(40vh)" },
      }}
    >
      <Typography component="h1" variant="h2" sx={{ textAlign: "center" }}>
        Inicio de sesión
      </Typography>
      <FormProvider {...formMethods}>
        <Box
          component="form"
          onSubmit={handleSubmit(createLoginWrapper)}
          noValidate
          sx={{ mt: 1 }}
          width="100%"
        >
          <Box>
            <Controller
              name="email"
              control={control}
              render={({ field }) => (
                <TextField
                  {...field}
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  label="Correo electrónico"
                  autoFocus
                  error={isPresent(errors.email)}
                />
              )}
            />
            {errors.email?.message && (
              <FormFieldErrorMessage message={errors.email.message} />
            )}
          </Box>
          <Box>
            <Controller
              name="password"
              control={control}
              render={({ field }) => (
                <TextField
                  {...field}
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  label="Contraseña"
                  type="password"
                  error={isPresent(errors.password)}
                />
              )}
            />
            {errors.password?.message && (
              <FormFieldErrorMessage message={errors.password.message} />
            )}
          </Box>
          <Button
            disabled={loading}
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            sx={{ mt: 3, mb: 2 }}
          >
            {loading ? "Cargando..." : "Ingresar"}
          </Button>
        </Box>
      </FormProvider>
    </Box>
  );
};
