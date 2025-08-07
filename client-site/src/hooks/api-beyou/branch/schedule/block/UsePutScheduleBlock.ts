import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { BranchScheduleBlock, BranchScheduleBlockRequest, ErrorDetailsBeYou } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { transformErrorKeys } from "@/utils/util";

interface UsePutScheduleBlockProps {
    blockId: number,
    onSuccess?: (
        data: BranchScheduleBlock,
        variables: BranchScheduleBlockRequest
    ) => void,
    onError?: (
        data: ErrorDetailsBeYou,
        variables: BranchScheduleBlockRequest
    ) => void,
    onSettled?: (
        data: BranchScheduleBlock | undefined,
        error: ErrorDetailsBeYou | null,
        variables: BranchScheduleBlockRequest
    ) => void
}

export const UsePutScheduleBlock = ({
    blockId,
    onSuccess,
    onError,
    onSettled
}: UsePutScheduleBlockProps) => {
    const path = '/api/BranchScheduleBlock/{blockId}';
    const method = 'put';

    const putBranchScheduleBlock = UseTypedApiClientBY({ path, method })
    const queryClient = useQueryClient();

    const updateBranchScheduleBlockMutation = useMutation({
        mutationKey: ['PutBranchScheduleBlock'],
        mutationFn: async (branchScheduleBlock: BranchScheduleBlockRequest) => {
            const { data } = await putBranchScheduleBlock(castRequestBody({ blockId: blockId, ...branchScheduleBlock }, path, method));
            return data;
        },
        onSuccess: async (data: BranchScheduleBlock, variables: BranchScheduleBlockRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetScheduleBlocks', 'GetBranchScheduleBlock', 'GetBranchSchedule']
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

    return updateBranchScheduleBlockMutation;
}