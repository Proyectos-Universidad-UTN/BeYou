// Inside UseGetScheduleBlockById.ts
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { isPresent } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { BranchScheduleBlock } from "@/types/api-beyou";

export const UseGetScheduleBlockById = (branchScheduleBlockId: number | undefined): UseQueryResult<BranchScheduleBlock, ApiError> => {
    const path = '/api/BranchScheduleBlock/{branchScheduleßlockId}';
    const method = 'get';

    const getBlock = UseTypedApiClientBY({ path, method })

    return useQuery({
        queryKey: ["GetBranchScheduleBlock", branchScheduleBlockId],
        queryFn: async () => {
            const { data } = await getBlock(castRequestBody({ branchScheduleßlockId: branchScheduleBlockId! }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(branchScheduleBlockId),
        staleTime: 0,
    })
}