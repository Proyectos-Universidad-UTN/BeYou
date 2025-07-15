/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-hooks/rules-of-hooks */
"use client";

import { Fetcher } from "openapi-typescript-fetch";
import { useSnackbar } from "@/stores/useSnackbar";
import { ApiError } from "openapi-typescript-fetch";
import { paths } from "@api/clients/beyou/api";
import { Middleware } from "openapi-typescript-fetch";
import { useTokenStore } from '@/stores/UseTokenStore';

export const createAuthMiddleware = (baseUrl: string): Middleware => {
    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    return async (url, init, next) => {
        const tokenStore = useTokenStore.getState();
        const originalRequest = () => next(url, init);

        try {
            return await originalRequest();
        } catch (error) {
            if (error instanceof ApiError && error.status === 401) {
                const refreshToken = tokenStore.getRefreshToken();
                const token = tokenStore.getAccessToken();

                if (!refreshToken) {
                    setSnackbarMessage('Sesión expirada. Por favor inicie sesión nuevamente', 'error');
                    tokenStore.clearTokens();
                    throw new Error('SESSION_EXPIRED');
                }

                try {
                    const refreshFetcher = Fetcher.for<paths>();
                    refreshFetcher.configure({
                        baseUrl,
                        init: {
                            headers: {
                                'Content-Type': 'application/json',
                            },
                        },
                    });

                    const refreshEndpoint = refreshFetcher.path('/api/Authentication/refreshToken').method('post').create();
                    const {data} = await refreshEndpoint({ token: token, refreshToken: refreshToken } as never);

                    tokenStore.setTokens(data.token!, data.refreshToken!);

                    const newInit = {
                        ...init,
                        headers: {
                            ...init?.headers,
                            Authorization: `Bearer ${data.token!}`
                        },
                    };

                    return next(url, newInit);
                } catch (refreshError) {
                    setSnackbarMessage('No se pudo renovar la sesión. Por favor inicie sesión nuevamente', 'error');
                    tokenStore.clearTokens();
                    throw new Error('REFRESH_FAILED');
                }
            }

            throw error;
        }
    };
};