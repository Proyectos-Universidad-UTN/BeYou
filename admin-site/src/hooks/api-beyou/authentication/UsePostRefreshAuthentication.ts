import { useMutation } from "@tanstack/react-query"
import { ApiError } from "openapi-typescript-fetch"
import { transformErrorKeys } from "@/utils/util"
import { castRequestBody, UseTypedApiClientBS } from "@/hooks/UseTypedApiClientBS"
import { Authentication, ErrorDetailsBeYou, UserTokenRefreshRequest } from "@/types/api-beyou"

interface UsePostRefreshAuthenticationProps {
    onSuccess?: (
        data: Authentication,
        variables: UserTokenRefreshRequest
    ) => void,
    onError?: (
        data: ErrorDetailsBeYou,
        variables: UserTokenRefreshRequest
    ) => void
}

export const UsePostRefreshAuthentication = ({
    onSuccess,
    onError
}: UsePostRefreshAuthenticationProps) => {
    const path = '/api/Authentication/refreshToken';
    const method = 'post';

    const postRefreshToken = UseTypedApiClientBS({ path, method })

    return useMutation({
        mutationFn: async (
            tokenRefreshModel: UserTokenRefreshRequest
        ) => {
            const { data } = await postRefreshToken(castRequestBody(tokenRefreshModel, path, method));
            return data;
        },
        onSuccess,
        onError: (error: ApiError, _) => {
            onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, _)
        }
    })
}