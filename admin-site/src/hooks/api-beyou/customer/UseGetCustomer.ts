import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { ApiError } from "openapi-typescript-fetch";
import { UseTypedApiClientBY, castRequestBody } from "@/hooks/UseTypedApiClientBY";
import { Customer } from "@/types/api-beyou";

export const UseGetCustomer = (): UseQueryResult<Customer[], ApiError> => {
  const path = "/api/Customer";
  const method = "get";

  const getCustomers = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetCustomers"],
    queryFn: async () => {
      const { data } = await getCustomers(castRequestBody({}, path, method));
      return data;
    },
    staleTime: 1000 * 60 * 5,
  });
};
