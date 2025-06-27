import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { ErrorDetailsBeYou, ScheduleRequest, User } from "@/types/api-beyou";

interface UsePostUserProps {
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

export const UsePostSchedule = ({
    onSuccess,
    onError,
    onSettled
}: UsePostUserProps) => {
    const path = '/api/User';
    const method = 'post';

    const postUser = UseTypedApiClientBY({ path, method })
    const queryClient = useQueryClient();

    const createUserMutation = useMutation({
        mutationKey: ['PostUser'],
        mutationFn: async (user: ScheduleRequest) => {
            const { data } = await postUser(castRequestBody(user, path, method))
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

    return createUserMutation;
}