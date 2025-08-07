import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { ApiError } from "openapi-typescript-fetch";
import { UseTypedApiClientBY, castRequestBody } from "@/hooks/UseTypedApiClientBY";
import { VendorList } from "@/types/api-beyou"; 

export const UseGetVendor = (): UseQueryResult<VendorList, ApiError> => {
  const path = "/api/Vendor";
  const method = "get";

  const getVendors = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetVendors"],
    queryFn: async () => {
      const { data } = await getVendors(castRequestBody({}, path, method));
      return data;
    },
    staleTime: 1000 * 60 * 5,
  });
};
