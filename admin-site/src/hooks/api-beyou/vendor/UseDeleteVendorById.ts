import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { ErrorDetailsBeYou } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { ApiError } from "openapi-typescript-fetch";

interface UseDeleteVendorProps {
  onSuccess?: (data: boolean, variables: number) => void;
  onError?: (data: ErrorDetailsBeYou, variables: number) => void;
  onSettled?: (
    data: boolean | undefined,
    error: ErrorDetailsBeYou | null,
    variables: number
  ) => void;
}

export const UseDeleteVendor = ({
  onSuccess,
  onError,
  onSettled,
}: UseDeleteVendorProps = {}) => {
  const path = "/api/Vendor/{vendorId}";
  const method = "delete";

  const deleteVendor = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation<boolean, ApiError, number>({
    mutationKey: ["DeleteVendor"],
    mutationFn: async (vendorId: number) => {
      const { data } = await deleteVendor(
        castRequestBody({ vendorId }, path, method)
      );
      return data;
    },
    onSuccess: async (data, variables) => {
      await queryClient.invalidateQueries({ queryKey: ["GetVendors"] });
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
