import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { isPresent } from "@/utils/util";
import { BranchSchedule } from "@/types/api-beyou";


export const UseGetBranchScheduleById = (branchScheduleId: string | undefined): UseQueryResult<BranchSchedule, ApiError> => {
    const path = '/api/BranchSchedule/{branchScheduleId}';
    const method = 'get';

    const getBranchSchedule = UseTypedApiClientBY({ path, method })

    return useQuery({
        queryKey: ["GetBranchSchedule", branchScheduleId],
        queryFn: async () => {
            const { data } = await getBranchSchedule(castRequestBody({ branchScheduleId: Number(branchScheduleId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(branchScheduleId),
        staleTime: 0,
    })
}
