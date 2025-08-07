import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { ApiError } from "openapi-typescript-fetch";
import { UseTypedApiClientBY, castRequestBody } from "@/hooks/UseTypedApiClientBY";
import {  Product } from "@/types/api-beyou"; 

export const UseGetProduct = (): UseQueryResult<Product[], ApiError> => {
  const path = "/api/Product";
  const method = "get";

  const getProducts = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetProducts"],
    queryFn: async () => {
      const { data } = await getProducts(castRequestBody({}, path, method));
      return data;
    },
    staleTime: 1000 * 60 * 5,
  });
};
