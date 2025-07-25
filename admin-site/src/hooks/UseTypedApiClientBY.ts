'use client'

import { paths } from '@/api/clients/beyou/api';
import { useTokenStore } from '@/stores/UseTokenStore';
import { Fetcher, Middleware, type TypedFetch } from "openapi-typescript-fetch";
import { createAuthMiddleware } from './middleware/authMiddleware';

const getHeaders = (disableAuth: boolean, token: string): Record<string, string> => {
    if (disableAuth) {
        return { "x-api-version": "1" };
    }

    return {
        "x-api-version": "1",
        Authorization: `Bearer ${token}`,
    };
};

const arrayWrapperMiddleware: Middleware = async (url, init, next) => {
    if (init?.body) {
        const body = JSON.parse(init.body as string);
        const keys = Object.keys(body);
        if (keys.length === 1 && Array.isArray(body[String(keys[0])])) {
            init.body = JSON.stringify(body[String(keys[0])]);
        }
    }
    return next(url, init);
};

export const UseTypedApiClientBY = <
    PathT extends keyof paths,
    MethodT extends keyof paths[PathT],
>({
    path,
    method,
    disableAuth = false,
}: {
    path: PathT;
    method: MethodT;
    disableAuth?: boolean;
}): TypedFetch<paths[PathT][MethodT]> => {
    const {getAccessToken} = useTokenStore();
    const fetcher = Fetcher.for<paths>();
    fetcher.configure({
        baseUrl: process.env.NEXT_PUBLIC_API_BEYOU_BASE_URL,
        init: {
            headers: getHeaders(disableAuth, getAccessToken() ?? ''),
        },
        use: [arrayWrapperMiddleware, createAuthMiddleware(process.env.NEXT_PUBLIC_API_BEYOU_BASE_URL!)],
        
    });

    return fetcher.path(path).method(method).create({}) as TypedFetch<paths[PathT][MethodT]>;
};

export const castRequestBody = <
    PathT extends keyof paths,
    MethodT extends keyof paths[PathT]
>(
    data: unknown,
    path: PathT,
    method: MethodT
): paths[PathT][MethodT] extends { requestBody: { content: { 'application/json': infer R } } } ? R | undefined : never => {
    if (method === 'post' || method === 'put' || method === 'patch') {
        return data as paths[PathT][MethodT] extends { requestBody: { content: { 'application/json': infer R } } } ? R : never;
    }

    if (method === 'get' || method === 'delete') {
        if (data && typeof data === 'object') {
            return data as never;
        }

        const pathParams = path.match(/{([^{}]+)}/g);
        if (pathParams) {
            const pathObj = pathParams.reduce((acc, param) => {
                const paramName = param.replace(/[{}]/g, '');
                if (typeof data === 'object' && data !== null && paramName in (data as Record<string, unknown>)) {
                    acc[paramName] = (data as Record<string, unknown>)[paramName];
                }
                return acc;
            }, {} as { [key: string]: unknown });

            return pathObj as never;
        }
    }

    return undefined as never;
};