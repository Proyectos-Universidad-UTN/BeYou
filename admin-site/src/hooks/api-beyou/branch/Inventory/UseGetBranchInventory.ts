import { useQuery } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { isPresent } from "@/utils/util";

export const UseGetBranchInventories = (branchId: string | undefined) => {
  const path = "/api/Inventory";
  const method = "get";

  const getBranchInventories = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetBranchInventories", branchId],
    queryFn: async () => {
      const { data } = await getBranchInventories(
        castRequestBody({ branchId: Number(branchId) }, path, method)
      );
      return data;
    },
    retry: false,
    enabled: isPresent(branchId),
    staleTime: 0,
  });
};
