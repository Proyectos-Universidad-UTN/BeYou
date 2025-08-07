
import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { Branch } from "@/types/api-beyou";


export const UseGetBranches = (): UseQueryResult<Array<Branch>, ApiError> => {
    const path = '/api/Branch';
    const method = 'get';

    const getBranches = UseTypedApiClientBY({ path, method })

    return useQuery({
        queryKey: ["GetBranches"],
        queryFn: async () => {
            const { data } = await getBranches(castRequestBody({}, path, method));
            return data
        },
        enabled: true,
        staleTime: 0,
    })
}
