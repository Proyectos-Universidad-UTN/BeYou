
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { ErrorDetailsBeYou } from "@/types/api-beyou";

interface UseDeleteBranchProps {
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

export const UseDeleteBranch = ({
    onSuccess,
    onError,
    onSettled
}: UseDeleteBranchProps) => {
    const path = '/api/Branch/{branchId}';
    const method = 'delete';

    const deleteBranch = UseTypedApiClientBY({ path, method })
    const queryClient = useQueryClient();

    const deleteBranchMutation = useMutation({
        mutationKey: ['DeleteBranch'],
        mutationFn: async (branchId: number) => {
            const { data } = await deleteBranch(castRequestBody({ branchId }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: number) => {
            await queryClient.invalidateQueries({
                queryKey: ['Branches']
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

    return deleteBranchMutation;
}