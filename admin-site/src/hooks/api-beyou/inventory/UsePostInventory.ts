import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePostInventoryProps {
  onSuccess?: (data: any, variables: any) => void;
  onError?: (data: any, variables: any) => void;
  onSettled?: (
    data: any | undefined,
    error: any | null,
    variables: any
  ) => void;
}

export const usePostInventory = ({
  onSuccess,
  onError,
  onSettled,
}: UsePostInventoryProps) => {

  const path = "/api/Inventory"; 
  const method = "post";

  const postInventory = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["PostInventory"], 
    mutationFn: async (inventoryItem: any) => { 
      const { data } = await postInventory(castRequestBody(inventoryItem, path, method));
      return data;
    },
    onSuccess: async (data, variables) => {

      await queryClient.invalidateQueries({ queryKey: ["GetInventoryItems"] }); 
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, variables) => {
      onError?.(transformErrorKeys(error.data), variables);
    },
    onSettled: (data, error, variables) => {
      onSettled?.(data, error, variables);
    },
  });
};