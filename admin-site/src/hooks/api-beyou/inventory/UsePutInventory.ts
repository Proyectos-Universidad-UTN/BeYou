import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Inventory, InventoryRequest, ErrorDetailsBeYou } from "@/types/api-beyou";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePutInventoryProps {
  onSuccess?: (
    data: Inventory,
    variables: InventoryRequest
  ) => void;
  onError?: (
    data: ErrorDetailsBeYou,
    variables: InventoryRequest
  ) => void;
  onSettled?: (
    data: Inventory | undefined,
    error: ErrorDetailsBeYou | null,
    variables: InventoryRequest
  ) => void;
}

export const UsePutInventory = ({
  onSuccess,
  onError,
  onSettled,
}: UsePutInventoryProps) => {
  const path = "/api/Inventory/{inventoryId}";
  const method = "put";

  const putInventory = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  const updateInventoryMutation = useMutation({
    mutationKey: ["PutInventory"],
    mutationFn: async (inventory: InventoryRequest) => {
      const { data } = await putInventory(
        castRequestBody(
          { inventoryId: Number(inventory.id), ...inventory },
          path,
          method
        )
      );
      return data;
    },
    onSuccess: async (data: Inventory, variables: InventoryRequest) => {
      await queryClient.invalidateQueries({ queryKey: ["GetInventory"] });
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, variables) => {
      onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, variables);
    },
    onSettled: (data, error, variables) => {
      onSettled?.(data, error, variables);
    },
  });

  return updateInventoryMutation;
};
