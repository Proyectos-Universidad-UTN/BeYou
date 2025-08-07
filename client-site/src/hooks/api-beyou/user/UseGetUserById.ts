import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { isPresent } from "@/utils/util";
import { User } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

export const UseGetUserById = (userId: string | undefined): UseQueryResult<User, ApiError> => {
    const path = '/api/User/{userId}';
    const method = 'get';

    const getUser = UseTypedApiClientBY({ path, method })

    return useQuery({
        queryKey: ["GetUser", userId],
        queryFn: async () => {
            const { data } = await getUser(castRequestBody({ scheduleId: Number(userId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(userId),
        staleTime: 0,
    })
}