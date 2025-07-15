import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { Schedule } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { isPresent } from "@/utils/util";


export const UseGetScheduleById = (scheduleId: string | undefined): UseQueryResult<Schedule, ApiError> => {
    const path = '/api/Schedule/{scheduleId}';
    const method = 'get';

    const getSchedule = UseTypedApiClientBY({ path, method })

    return useQuery({
        queryKey: ["GetSchedule", scheduleId],
        queryFn: async () => {
            const { data } = await getSchedule(castRequestBody({ scheduleId: Number(scheduleId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(scheduleId),
        staleTime: 0,
    })
}
