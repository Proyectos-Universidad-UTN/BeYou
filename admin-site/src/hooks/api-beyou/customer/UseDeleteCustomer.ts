// src/hooks/api-beyou/customer/UseDeleteCustomer.ts

import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { ErrorDetailsBeYou } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { ApiError } from "openapi-typescript-fetch";

interface UseDeleteCustomerProps {
  onSuccess?: (data: boolean, variables: number) => void;
  onError?: (data: ErrorDetailsBeYou, variables: number) => void;
  onSettled?: (
    data: boolean | undefined,
    error: ErrorDetailsBeYou | null,
    variables: number
  ) => void;
}

export const UseDeleteCustomer = ({
  onSuccess,
  onError,
  onSettled,
}: UseDeleteCustomerProps = {}) => {
  const path = "/api/Customer/{customerId}";
  const method = "delete";

  const deleteCustomer = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation<boolean, ApiError, number>({
    mutationKey: ["DeleteCustomer"],
    mutationFn: async (customerId: number) => {
      await deleteCustomer(castRequestBody({ customerId }, path, method));
      return true;
    },
    onSuccess: async (data, variables) => {
      await queryClient.invalidateQueries({ queryKey: ["GetCustomers"] });
      onSuccess?.(data, variables);
    },
    onError: (error, variables) => {
      const parsed = transformErrorKeys(error.data) as ErrorDetailsBeYou;
      onError?.(parsed, variables);
    },
    onSettled: (data, error, variables) => {
      const parsed = error ? (transformErrorKeys(error.data) as ErrorDetailsBeYou) : null;
      onSettled?.(data, parsed, variables);
    },
  });
};
