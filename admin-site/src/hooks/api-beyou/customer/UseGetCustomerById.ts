import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { isPresent } from "@/utils/util";
import { Customer } from "@/types/api-beyou"; 
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

export const UseGetCustomerById = (
  customerId: string | undefined
): UseQueryResult<Customer, ApiError> => {
  const path = "/api/Customer/{customerId}";
  const method = "get";

  const getCustomer = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetCustomer", customerId],
    queryFn: async () => {
      const { data } = await getCustomer(
        castRequestBody({ customerId: Number(customerId) }, path, method)
      );
      return data;
    },
    retry: false,
    enabled: isPresent(customerId),
    staleTime: 0,
  });
};
