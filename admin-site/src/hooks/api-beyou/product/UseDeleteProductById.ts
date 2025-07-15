import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiError } from "openapi-typescript-fetch";
import { UseTypedApiClientBY, castRequestBody } from "@/hooks/UseTypedApiClientBY";

export const UseDeleteProductById = ({
  onSuccess,
  onError,
  onSettled,
}: {
  onSuccess?: () => void;
  onError?: (error: ApiError) => void;
  onSettled?: () => void;
} = {}) => {
  const path = "/api/Product/{productId}";
  const method = "delete";

  const deleteProduct = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["DeleteProduct"],
    mutationFn: async (productId: number) => {
      const { data } = await deleteProduct(
        castRequestBody({ productId }, path, method)
      );
      return data;
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ["GetProducts"] });
      onSuccess?.();
    },
    onError: (error: ApiError) => {
      onError?.(error);
    },
    onSettled: () => {
      onSettled?.();
    },
  });
};
