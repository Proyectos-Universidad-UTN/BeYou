import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { isPresent } from "@/utils/util";
import { Vendor } from "@/types/api-beyou"; 
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

export const UseGetVendorById = (
  vendorId: string | undefined
): UseQueryResult<Vendor, ApiError> => {
  const path = "/api/Vendor/{vendorId}";
  const method = "get";

  const getVendor = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetVendor", vendorId],
    queryFn: async () => {
      const { data } = await getVendor(
        castRequestBody({ vendorId: Number(vendorId) }, path, method)
      );
      return data;
    },
    retry: false,
    enabled: isPresent(vendorId),
    staleTime: 0,
  });
};
