import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiError } from "openapi-typescript-fetch";
import { UseTypedApiClientBY, castRequestBody } from "@/hooks/UseTypedApiClientBY";
import { transformErrorKeys } from "@/utils/util";
import { ErrorDetailsBeYou } from "@/types/api-beyou";

interface UseDeleteProductByIdProps {
  onSuccess?: (data: boolean, variables: number) => void;
  onError?: (error: ErrorDetailsBeYou, variables: number) => void;
  onSettled?: (
    data: boolean | undefined,
    error: ErrorDetailsBeYou | null,
    variables: number
  ) => void;
}

export const UseDeleteProductById = ({
  onSuccess,
  onError,
  onSettled,
}: UseDeleteProductByIdProps = {}) => {
  const path = "/api/Product/{productId}";
  const method = "delete";

  const deleteProduct = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["DeleteProduct"],
    mutationFn: async (productId: number) => {
      const { data } = await deleteProduct(castRequestBody({ productId }, path, method));
      return data;
    },
    onSuccess: async (data: boolean, variables: number) => {
      await queryClient.invalidateQueries({ queryKey: ["GetProducts"] });
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, variables) => {
      onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, variables);
    },
    onSettled: (data, error, variables) => {
      onSettled?.(
        data,
        error ? (transformErrorKeys((error as ApiError).data) as ErrorDetailsBeYou) : null,
        variables
      );
    },
  });
};
