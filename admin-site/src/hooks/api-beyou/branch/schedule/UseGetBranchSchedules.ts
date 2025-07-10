import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

import { isPresent } from "@/utils/util";


export const UseGetBranchSchedules = (branchId: string | undefined) => {
  const path = '/api/Branch/{branchId}/Schedule';
  const method = 'get';

  const getBranchSchedules = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetBranchSchedules", branchId], 
    queryFn: async () => {
      const { data } = await getBranchSchedules(
        castRequestBody({ branchId: Number(branchId) }, path, method)
      );
      console.log("Horarios API:", data);
      return data;
    },
    retry: false,
    enabled: isPresent(branchId),
    staleTime: 0,
  });
};
