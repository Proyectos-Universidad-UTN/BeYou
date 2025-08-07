
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { Branch } from "@/types/api-beyou";
import { isPresent } from "@/utils/util";


export const UseGetBranchById = (branchId: string | undefined): UseQueryResult<Branch, ApiError> => {
    const path = '/api/Branch/{branchId}';
    const method = 'get';

    const getBranch = UseTypedApiClientBY({ path, method })

    return useQuery({
        queryKey: ["GetBranch", branchId],
        queryFn: async () => {
            const { data } = await getBranch(castRequestBody({ branchId: Number(branchId) }, path, method));
            return data
        },
        retry: false,
        enabled: isPresent(branchId),
        staleTime: 0,
    })
}
