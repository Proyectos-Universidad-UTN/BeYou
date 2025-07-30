import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { Inventory } from "@/types/api-beyou";

export const UseGetInventory = (): UseQueryResult<Array<Inventory>, ApiError> => {
  const path = "/api/Inventory";
  const method = "get";

  const getInventory = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetInventory"],
    queryFn: async () => {
      const { data } = await getInventory(castRequestBody({}, path, method));
      return data;
    },
    enabled: true,
    staleTime: 0,
  });
};
