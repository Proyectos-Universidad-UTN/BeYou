import Cookies from 'js-cookie';
import { jwtDecode } from 'jwt-decode';
import { createContext, ReactNode, useCallback, useState, useContext, useEffect, useMemo } from 'react';
import { LoginTypeForm } from '@/app/Login/LoginSchema';
import { useSnackbar } from '@/stores/useSnackbar';
import { Authentication, ErrorDetailsBeYou } from '@/types/api-beyou';
import { UsePostAuthentication } from '@/hooks/api-beyou/authentication/UsePostAuthentication';
import { UsePostRefreshAuthentication } from '@/hooks/api-beyou/authentication/UsePostRefreshAuthentication';

interface AuthContextType {
    isAuthenticated: boolean;
    login: (data: LoginTypeForm) => Promise<void>; // Cambia el tipo de retorno a void y agrega el parámetro data: LoginTypeForm) => Promise<void>;
    logout: () => void;
    refreshTokens: () => void;
    authLoaded: boolean;
}

interface DecodedToken {
    exp: number;
    [key: string]: unknown;
    FullName: string;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [authLoaded, setAuthLoaded] = useState(false);
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const getToken = useCallback(() => Cookies.get('access_token'), []);
    const getRefreshToken = useCallback(() => Cookies.get('refresh_token'), []);

    const setUserName = (decodedToken: DecodedToken) => {
        Cookies.set('user_name', decodedToken.FullName)
    }

    const { mutate: postAuthenticationUser } = UsePostAuthentication({
        onSuccess: (data: Authentication) => {
            setSnackbarMessage('Inicio de sesión válido');
            Cookies.set('access_token', String(data.token), { expires: 1 / 24 });
            Cookies.set('refresh_token', String(data.refreshToken), { expires: 30 });
            setUserName(jwtDecode<DecodedToken>(String(data.token)))
            setIsAuthenticated(true);
        },
        onError: (data: ErrorDetailsBeYou) => {
            setSnackbarMessage(`${data.message}`, 'error');
            logout();
        },
    });

    const login = useCallback(async (data: LoginTypeForm) => {
        try {
            postAuthenticationUser(data);
        } catch (error) {
            setSnackbarMessage(`Error al intentar iniciar sesión: ${error}`, 'error');
        }
    }, [postAuthenticationUser, setSnackbarMessage]);

    const logout = useCallback(() => {
        Cookies.remove('access_token');
        Cookies.remove('refresh_token');
        Cookies.remove('user_name');
        setIsAuthenticated(false);
    }, []);

    const { mutate: refreshTokenMutation } = UsePostRefreshAuthentication({
        onSuccess: (data: Authentication) => {
            Cookies.set('access_token', String(data.token), { expires: 1 / 24 });
            Cookies.set('refresh_token', String(data.refreshToken), { expires: 30 });
            setUserName(jwtDecode<DecodedToken>(String(data.token)))
            setIsAuthenticated(true);
            setSnackbarMessage(`Token actualizado correctamente`)
        },
        onError: (data: ErrorDetailsBeYou) => {
            setSnackbarMessage(`Error actualizando tokens: ${data.message}`, 'error');
            logout();
        },
    });

    const refreshTokens = useCallback(() => {
        const token = getToken();
        const refreshToken = getRefreshToken();

        if (!token || !refreshToken) {
            setSnackbarMessage(`Por favor inice sesión`, 'error');
            logout();
            return;
        }

        try {
            const decodedToken: DecodedToken = jwtDecode<DecodedToken>(token);
            const currentTime = Date.now() / 1000;

            if (decodedToken.exp && decodedToken.exp < currentTime) {
                setSnackbarMessage('El token ha expirado. Refrescando...', 'info');
                refreshTokenMutation({ token, refreshToken });
            }
        } catch {
            setSnackbarMessage('Error al intentar verificar el token', 'error');
        }
    }, [getToken, getRefreshToken, setSnackbarMessage, refreshTokenMutation, logout]);

    useEffect(() => {
        if (isAuthenticated) {
            refreshTokens();
        }
    }, [isAuthenticated, refreshTokens]);

    useEffect(() => {
        const token = getToken();
        const refreshToken = getRefreshToken();

        if (token && refreshToken) {
            try {
                const decodedToken: DecodedToken = jwtDecode<DecodedToken>(token);
                const currentTime = Date.now() / 1000;

                if (decodedToken.exp && decodedToken.exp < currentTime) {
                    refreshTokens();
                } else {
                    setIsAuthenticated(true);
                }
            } catch {
                setSnackbarMessage('Error al intentar verificar el token', 'error');
                logout();
            }
        } else {
            setIsAuthenticated(false);
        }

        setAuthLoaded(true);
    }, [getToken, getRefreshToken, logout, refreshTokens, setSnackbarMessage]);

    const contextValue = useMemo(() => ({
        isAuthenticated,
        login,
        logout,
        refreshTokens,
        authLoaded
    }), [isAuthenticated, login, logout, refreshTokens, authLoaded]);

    return (
        <AuthContext.Provider value={contextValue}>
            {children}
        </AuthContext.Provider>
    );
};

const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth debe ser usado dentro de un AuthProvider');
    }
    return context;
};


export { AuthProvider, useAuth };