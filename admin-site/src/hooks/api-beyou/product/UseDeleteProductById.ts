import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { ErrorDetailsBeYou } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UseDeleteProductProps {
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

export const UseDeleteProductById = ({
  onSuccess,
  onError,
  onSettled
}: UseDeleteProductProps = {}) => {
  const path = "/api/Product/{productId}";
  const method = "delete";

  const deleteProduct = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  const deleteProductMutation = useMutation({
    mutationKey: ["DeleteProduct"],
    mutationFn: async (productId: number) => {
      const { data } = await deleteProduct(
        castRequestBody({ productId }, path, method)
      );
      return data;
    },
    onSuccess: async (data: boolean, variables: number) => {
      await queryClient.invalidateQueries({
        queryKey: ["Products"]
      });
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, variables: number) => {
      onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, variables);
    },
    onSettled: (data, error, variables) => {
      onSettled?.(data, error, variables);
    }
  });

  return deleteProductMutation;
};
