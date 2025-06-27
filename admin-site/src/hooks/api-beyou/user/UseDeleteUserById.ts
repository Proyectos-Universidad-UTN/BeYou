import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { ErrorDetailsBeYou } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UseDeleteUserProps {
    onSuccess?: (
        data: boolean,
        variables: number
    ) => void,
    onError?: (
        data: ErrorDetailsBeYou,
        variables: number
    ) => void,
    onSettled?: (
        data: boolean | undefined,
        error: ErrorDetailsBeYou | null,
        variables: number
    ) => void
}

export const UseDeleteUser = ({
    onSuccess,
    onError,
    onSettled
}: UseDeleteUserProps) => {
    const path = '/api/User/{userId}';
    const method = 'delete';

    const deleteUser = UseTypedApiClientBY({ path, method })
    const queryClient = useQueryClient();

    const deleteUserMutation = useMutation({
        mutationKey: ['DeleteUser'],
        mutationFn: async (userId: number) => {
            const { data } = await deleteUser(castRequestBody({ userId }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: number) => {
            await queryClient.invalidateQueries({
                queryKey: ['Users']
            })
            onSuccess?.(data, variables)
        },
        onError: (error: ApiError, _) => {
            onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, _)
        },
        onSettled: (data, error, variables) => {
            onSettled?.(data, error, variables)
        }
    })

    return deleteUserMutation;
}