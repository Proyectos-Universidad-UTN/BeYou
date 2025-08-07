import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ErrorDetailsBeYou, Vendor } from "@/types/api-beyou";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePutVendorProps {
  onSuccess?: (
    data: Vendor,
    variables: Vendor
  ) => void;
  onError?: (
    data: ErrorDetailsBeYou,
    variables: Vendor
  ) => void;
  onSettled?: (
    data: Vendor | undefined,
    error: ErrorDetailsBeYou | null,
    variables: Vendor
  ) => void;
}

export const UsePutVendor = ({
  onSuccess,
  onError,
  onSettled,
}: UsePutVendorProps) => {
  const path = "/api/Vendor/{vendorId}";
  const method = "put";

  const putVendor = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["PutVendor"],
    mutationFn: async (vendor: Vendor) => {

      const { data } = await putVendor(
        castRequestBody({ vendorId: Number(vendor.id), ...vendor }, path, method)
      );
      return data;
    },
    onSuccess: async (data: Vendor, variables: Vendor) => {
      await queryClient.invalidateQueries({ queryKey: ["GetVendors"] });
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, variables: Vendor) => {
      onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, variables);
    },
    onSettled: (data, error, variables) => {
      onSettled?.(data, error, variables);
    },
  });
};
