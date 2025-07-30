import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { ErrorDetailsBeYou } from "@/types/api-beyou";

interface UseDeleteInventoryProps {
  onSuccess?: (
    data: boolean,
    variables: number
  ) => void;
  onError?: (
    data: ErrorDetailsBeYou,
    variables: number
  ) => void;
  onSettled?: (
    data: boolean | undefined,
    error: ErrorDetailsBeYou | null,
    variables: number
  ) => void;
}

export const UseDeleteInventory = ({
  onSuccess,
  onError,
  onSettled,
}: UseDeleteInventoryProps) => {
  const path = '/api/Inventory/{inventoryId}';
  const method = 'delete';

  const deleteInventory = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  const deleteInventoryMutation = useMutation({
    mutationKey: ['DeleteInventory'],
    mutationFn: async (inventoryId: number) => {
      const { data } = await deleteInventory(
        castRequestBody({ inventoryId }, path, method)
      );
      return data;
    },
    onSuccess: async (data: boolean, variables: number) => {
      await queryClient.invalidateQueries({
        queryKey: ['GetInventory']
      });
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, variables) => {
      onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, variables);
    },
    onSettled: (data, error, variables) => {
      onSettled?.(data, error, variables);
    }
  });

  return deleteInventoryMutation;
};
