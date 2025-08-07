import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ErrorDetailsBeYou, ScheduleRequest, User } from "@/types/api-beyou";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePutUserProps {
    onSuccess?: (
        data: User,
        variables: ScheduleRequest
    ) => void,
    onError?: (
        data: ErrorDetailsBeYou,
        variables: ScheduleRequest
    ) => void,
    onSettled?: (
        data: User | undefined,
        error: ErrorDetailsBeYou | null,
        variables: ScheduleRequest
    ) => void
}

export const UsePutSchedule = ({
    onSuccess,
    onError,
    onSettled
}: UsePutUserProps) => {
    const path = '/api/User/{userId}';
    const method = 'put';

    const putUser = UseTypedApiClientBY({ path, method })
    const queryClient = useQueryClient();

    const updateUserMutation = useMutation({
        mutationKey: ['PutUser'],
        mutationFn: async (user: ScheduleRequest) => {
            const { data } = await putUser(castRequestBody({ scheduleId: Number(user.id), ...user }, path, method));
            return data;
        },
        onSuccess: async (data: User, variables: ScheduleRequest) => {
            await queryClient.invalidateQueries({
                queryKey: ['GetUser']
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

    return updateUserMutation;
}