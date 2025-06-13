'use client';
import { useRouter } from "next/navigation";
import { yupResolver } from "@hookform/resolvers/yup";
import { useCallback, useEffect, useState } from "react";
import {
  Box,
  Button,
  TextField,
  Typography,
  Paper,
  Avatar,
  FormControlLabel,
  Checkbox,
  Link as MuiLink,
} from "@mui/material";
import { Controller, FormProvider, useForm } from "react-hook-form";
import { LoginDefaultValues, LoginSchema, LoginTypeForm } from "./LoginSchema";
import { useAuth } from "@/context/AuthContext";
import { Lock } from "@mui/icons-material";

export const LoginForm = () => {
  const formMethods = useForm({
    resolver: yupResolver(LoginSchema),
    defaultValues: { ...LoginDefaultValues, remember: false },
  });

  const {
    handleSubmit,
    control,
    formState: { errors },
  } = formMethods;

  const { login, isAuthenticated } = useAuth();
  const router = useRouter();
  const [loading, setLoading] = useState(false);

  const createLoginWrapper = useCallback(
  async (data: LoginTypeForm & { remember?: boolean }) => {
    setLoading(true);
    try {
      await login(data);
      router.push("/Dashboard");
    } catch (error) {
      console.error("Error de login:", error);
    } finally {
      setLoading(false);
    }
  },
  [login, router]
);
  return (
    <Box className="min-h-screen flex items-center justify-center bg-[#ffe4ec] px-4">
      <Paper
        elevation={6}
        sx={{
          padding: 4,
          width: "100%",
          maxWidth: 420,
          borderRadius: 4,
          textAlign: "center",
        }}
      >
        <Avatar
          sx={{
            bgcolor: "#b8860b",
            width: 56,
            height: 56,
            margin: "0 auto",
            mb: 2,
          }}
        >
          <Lock />
        </Avatar>

        <Typography
          variant="h5"
          component="h1"
          fontWeight="bold"
          color="#b8860b"
          gutterBottom
        >
          Beauty and Style
        </Typography>

        <Typography variant="subtitle1" gutterBottom>
          Inicia sesión para continuar
        </Typography>

        <FormProvider {...formMethods}>
          <Box
            component="form"
            onSubmit={handleSubmit(createLoginWrapper)}
            noValidate
          >
            <Controller
              name="email"
              control={control}
              render={({ field }) => (
                <TextField
                  {...field}
                  label="Correo electrónico"
                  margin="normal"
                  fullWidth
                  required
                  variant="outlined"
                  autoComplete="email"
                  autoFocus
                  error={!!errors.email}
                  helperText={errors.email?.message}
                />
              )}
            />
            <Controller
              name="password"
              control={control}
              render={({ field }) => (
                <TextField
                  {...field}
                  label="Contraseña"
                  type="password"
                  margin="normal"
                  fullWidth
                  required
                  variant="outlined"
                  autoComplete="current-password"
                  error={!!errors.password}
                  helperText={errors.password?.message}
                />
              )}
            />

            {/* Recordarme + Olvidaste tu contraseña (alineados horizontalmente) */}
            <Box
              display="flex"
              justifyContent="space-between"
              alignItems="center"
              mb={2}
            >
              <Controller
                name="remember"
                control={control}
                render={({ field }) => (
                  <FormControlLabel
                    control={
                      <Checkbox
                        {...field}
                        checked={field.value}
                        sx={{
                          color: "#b8860b",
                          "&.Mui-checked": { color: "#b8860b" },
                          p: 0.5,
                        }}
                      />
                    }
                    label="Recordarme"
                  />
                )}
              />

              <MuiLink
                href="/recuperar-password"
                underline="hover"
                sx={{ fontSize: "0.9rem", color: "#b8860b" }}
              >
                ¿Olvidaste tu contraseña?
              </MuiLink>
            </Box>

            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{
                mt: 1,
                backgroundColor: "#b8860b",
                "&:hover": { backgroundColor: "#a87407" },
              }}
              disabled={loading}
            >
              {loading ? "Cargando..." : "Ingresar"}
            </Button>
          </Box>
        </FormProvider>
      </Paper>
    </Box>
  );
};
