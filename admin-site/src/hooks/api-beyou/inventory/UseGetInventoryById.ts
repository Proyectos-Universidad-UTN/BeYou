import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { Inventory } from "@/types/api-beyou";
import { isPresent } from "@/utils/util";

export const UseGetInventoryById = (inventoryId: string | undefined): UseQueryResult<Inventory, ApiError> => {
  const path = '/api/Inventory/{inventoryId}';
  const method = 'get';

  const getInventory = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetInventory", inventoryId],
    queryFn: async () => {
      const { data } = await getInventory(
        castRequestBody({ inventoryId: Number(inventoryId) }, path, method)
      );
      return data;
    },
    retry: false,
    enabled: isPresent(inventoryId),
    staleTime: 0,
  });
};
