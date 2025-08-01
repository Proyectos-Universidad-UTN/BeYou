import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ErrorDetailsBeYou, Customer } from "@/types/api-beyou";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePutCustomerProps {
  onSuccess?: (
    data: Customer,
    variables: Customer
  ) => void;
  onError?: (
    data: ErrorDetailsBeYou,
    variables: Customer
  ) => void;
  onSettled?: (
    data: Customer | undefined,
    error: ErrorDetailsBeYou | null,
    variables: Customer
  ) => void;
}

export const UsePutCustomer = ({
  onSuccess,
  onError,
  onSettled,
}: UsePutCustomerProps) => {
  const path = "/api/Customer/{customerId}";
  const method = "put";

  const putCustomer = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["PutCustomer"],
    mutationFn: async (customer: Customer) => {
      const { data } = await putCustomer(
        castRequestBody({ customerId: Number(customer.id), ...customer }, path, method)
      );
      return data;
    },
    onSuccess: async (data: Customer, variables: Customer) => {
      await queryClient.invalidateQueries({ queryKey: ["GetCustomers"] });
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, variables: Customer) => {
      onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, variables);
    },
    onSettled: (data, error, variables) => {
      onSettled?.(data, error, variables);
    },
  });
};
