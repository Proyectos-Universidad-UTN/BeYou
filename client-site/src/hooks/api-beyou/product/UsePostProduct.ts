import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePostProductProps {
  onSuccess?: (data: any, variables: any) => void;
  onError?: (data: any, variables: any) => void;
  onSettled?: (
    data: any | undefined,
    error: any | null,
    variables: any
  ) => void;
}

export const UsePostProduct = ({
  onSuccess,
  onError,
  onSettled,
}: UsePostProductProps) => {
  const path = "/api/Product";
  const method = "post";

  const postProduct = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["PostProduct"],
    mutationFn: async (product: any) => {
      const { data } = await postProduct(castRequestBody(product, path, method));
      return data;
    },
    onSuccess: async (data, variables) => {
      await queryClient.invalidateQueries({ queryKey: ["GetProducts"] }); // actualiza lista
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
