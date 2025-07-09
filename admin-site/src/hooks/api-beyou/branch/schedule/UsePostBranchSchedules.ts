import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { BranchScheduleRequest, ErrorDetailsBeYou } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { transformErrorKeys } from "@/utils/util";


interface UsePostBranchSchedulesProps {
    branchId: number,
    onSuccess?: (
        data: boolean,
        variables: BranchScheduleRequest[]
    ) => void,
    onError?: (
        data: ErrorDetailsBeYou,
        variables: BranchScheduleRequest[]
    ) => void,
    onSettled?: (
        data: boolean | undefined,
        error: ErrorDetailsBeYou | null,
        variables: BranchScheduleRequest[]
    ) => void
}

export const UsePostBranchSchedules = ({
    branchId,
    onSuccess,
    onError,
    onSettled
}: UsePostBranchSchedulesProps) => {
    const path = `/api/Branch/{branchId}/Schedule`;
    const method = 'post';

    const postBranchSchedules = UseTypedApiClientBY({ path, method })
    const queryClient = useQueryClient();

    const createBranchSchedulesMutation = useMutation({
        mutationKey: ['PostBranchSchedules'],
        mutationFn: async (branchSchedules: Array<BranchScheduleRequest>) => {
            const { data } = await postBranchSchedules(castRequestBody({ branchId, branchSchedule: branchSchedules }, path, method))
            return data;
        },
        onSuccess: async (data: boolean, variables: BranchScheduleRequest[]) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetBranchSchedule']
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

    return createBranchSchedulesMutation;
}