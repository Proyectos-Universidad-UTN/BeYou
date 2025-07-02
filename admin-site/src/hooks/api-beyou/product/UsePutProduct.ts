import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Product, ErrorDetailsBeYou } from "@/types/api-beyou";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePutProductProps {
  onSuccess?: (data: Product, variables: Product) => void;
  onError?: (data: ErrorDetailsBeYou, variables: Product) => void;
  onSettled?: (
    data: Product | undefined,
    error: ErrorDetailsBeYou | null,
    variables: Product
  ) => void;
}

export const UsePutProduct = ({
  onSuccess,
  onError,
  onSettled,
}: UsePutProductProps) => {
  const path = "/api/Product/{productId}";
  const method = "put";

  const putProduct = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["PutProduct"],
    mutationFn: async (product: Product) => {
      const { data } = await putProduct(
        castRequestBody({ productId: Number(product.id), ...product }, path, method)
      );
      return data;
    },
    onSuccess: async (data, variables) => {
      await queryClient.invalidateQueries({ queryKey: ["GetProducts"] });
      await queryClient.refetchQueries({ queryKey: ["GetProducts"] }); // <-- Extra refetch
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, variables: Product) => {
      onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, variables);
    },
    onSettled: (data, error, variables) => {
      onSettled?.(data, error, variables);
    },
  });
};
