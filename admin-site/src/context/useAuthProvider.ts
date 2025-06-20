import { useState, useEffect, useCallback } from "react";
import Cookies from "js-cookie";
import { useTokenStore } from "@/stores/UseTokenStore";
import { useSnackbar } from "@/stores/useSnackbar";
import { UsePostAuthentication } from "@/hooks/api-beyou/authentication/UsePostAuthentication";
import { UsePostRefreshAuthentication } from "@/hooks/api-beyou/authentication/UsePostRefreshAuthentication";
import {
  LoginUserRequest,
  Authentication,
  ErrorDetailsBeYou,
  UserProfile,
} from "@/types/api-beyou";
import { UseGetProfileLogged } from "@/hooks/api-beyou/UseGetProfileLogged";

export const useAuthProvider = () => {
  const [userProfile, setUserProfile] = useState<UserProfile | null>(null);
  const [authLoaded, setAuthLoaded] = useState(false);
  const { accessToken, setTokens, clearTokens } = useTokenStore();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const isAuthenticated = !!accessToken;

  const { data, isLoading, isError } = UseGetProfileLogged();

  const { mutate: postAuthenticationUser } = UsePostAuthentication({
    onSuccess: (data: Authentication) => {
      setTokens(data.token!, data.refreshToken!);
    },
    onError: (error: ErrorDetailsBeYou) => {
      setSnackbarMessage(error.message || "Error en inicio de sesión", "error");
      logout();
    },
  });

  const { mutate: postRefreshAuthentication } = UsePostRefreshAuthentication({
    onSuccess: (data: Authentication) => {
      setTokens(data.token!, data.refreshToken!);
    },
    onError: (error: ErrorDetailsBeYou) => {
      setSnackbarMessage(
        error.message || "Error al actualizar sesión",
        "error"
      );
      logout();
    },
  });

  const login = useCallback(
    (data: LoginUserRequest) => {
      postAuthenticationUser(data);
    },
    [postAuthenticationUser]
  );

  const logout = useCallback(() => {
    clearTokens();
    setUserProfile(null);
  }, [clearTokens]);

  const refreshToken = useCallback(() => {
    try {
      const { refreshToken } = useTokenStore();
      if (!refreshToken) throw new Error("No hay token de refresco");

      postRefreshAuthentication({ refreshToken: refreshToken });
      return true;
    } catch (error) {
      setSnackbarMessage(
        "Error al actualizar sesión, por favor inicia sesión nuevamente",
        "error"
      );
      return false;
    }
  }, [postRefreshAuthentication, setSnackbarMessage]);

  // Verificar expiración token y refrescar
  useEffect(() => {
    if (!isAuthenticated) return;

    const checkTokenExpiration = () => {
      if (!accessToken) return;
      try {
        const payload = JSON.parse(atob(accessToken.split(".")[1]));
        const expiresIn = payload.exp * 1000 - Date.now();

        if (expiresIn < 5 * 60 * 1000) {
          refreshToken();
        }
      } catch (error) {
        console.error("Error verificando expiración token:", error);
      }
    };

    checkTokenExpiration();
    const interval = setInterval(checkTokenExpiration, 60 * 1000);
    return () => clearInterval(interval);
  }, [isAuthenticated, accessToken, refreshToken]);

  useEffect(() => {
    if (!isAuthenticated) {
      setUserProfile(null);
      setAuthLoaded(true);
      return;
    }

    if (!isLoading && !isError && data) {
      setUserProfile(data);
      setAuthLoaded(true);
    } else if (isError) {
      setAuthLoaded(true);
    }
  }, [data, isLoading, isError, isAuthenticated]);

  return {
    isAuthenticated,
    login,
    logout,
    authLoaded,
    userProfile,
    refreshToken,
  };
};
