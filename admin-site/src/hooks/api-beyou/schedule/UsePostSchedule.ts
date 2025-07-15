import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { ErrorDetailsBeYou, Schedule, ScheduleRequest } from "@/types/api-beyou";
import { transformErrorKeys } from "@/utils/util";

interface UsePostScheduleProps {
    onSuccess?: (
        data: Schedule,
        variables: ScheduleRequest
    ) => void,
    onError?: (
        data: ErrorDetailsBeYou,
        variables: ScheduleRequest
    ) => void,
    onSettled?: (
        data: Schedule | undefined,
        error: ErrorDetailsBeYou | null,
        variables: ScheduleRequest
    ) => void
}

export const UsePostSchedule = ({
    onSuccess,
    onError,
    onSettled
}: UsePostScheduleProps) => {
    const path = '/api/Schedule';
    const method = 'post';

    const postSchedule = UseTypedApiClientBY({ path, method })
    const queryClient = useQueryClient();

    const createScheduleMutation = useMutation({
        mutationKey: ['PostSchedule'],
        mutationFn: async (schedule: ScheduleRequest) => {
            const { data } = await postSchedule(castRequestBody(schedule, path, method))
            return data;
        },
        onSuccess: async (data: Schedule, variables: ScheduleRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetSchedule']
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

    return createScheduleMutation;
}