import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePostVendorProps {
  onSuccess?: (data: any, variables: any) => void;
  onError?: (data: any, variables: any) => void;
  onSettled?: (
    data: any | undefined,
    error: any | null,
    variables: any
  ) => void;
}

export const UsePostVendor = ({
  onSuccess,
  onError,
  onSettled,
}: UsePostVendorProps) => {
  const path = "/api/Vendor";
  const method = "post";

  const postVendor = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["PostVendor"],
    mutationFn: async (vendor: any) => {
      const { data } = await postVendor(castRequestBody(vendor, path, method));
      return data;
    },
    onSuccess: async (data, variables) => {
      await queryClient.invalidateQueries({ queryKey: ["GetVendors"] });
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
