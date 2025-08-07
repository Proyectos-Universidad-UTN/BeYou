import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { isPresent } from "@/utils/util";
import { Product } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

export const UseGetProductById = (
  productId: string | undefined
): UseQueryResult<Product, ApiError> => {
  const path = "/api/Product/{productId}";
  const method = "get";

  const getProduct = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetProduct", productId],
    queryFn: async () => {
      const { data } = await getProduct(
        castRequestBody({ productId: Number(productId) }, path, method)
      );
      return data;
    },
    retry: false,
    enabled: isPresent(productId),
    staleTime: 0,
  });
};
