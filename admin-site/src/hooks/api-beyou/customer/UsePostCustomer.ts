import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePostCustomerProps {
  onSuccess?: (data: any, variables: any) => void;
  onError?: (data: any, variables: any) => void;
  onSettled?: (
    data: any | undefined,
    error: any | null,
    variables: any
  ) => void;
}

export const UsePostCustomer = ({
  onSuccess,
  onError,
  onSettled,
}: UsePostCustomerProps) => {
  const path = "/api/Customer";
  const method = "post";

  const postCustomer = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["PostCustomer"],
    mutationFn: async (customer: any) => {
      const { data } = await postCustomer(castRequestBody(customer, path, method));
      return data;
    },
    onSuccess: async (data, variables) => {
      await queryClient.invalidateQueries({ queryKey: ["GetCustomers"] });
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
