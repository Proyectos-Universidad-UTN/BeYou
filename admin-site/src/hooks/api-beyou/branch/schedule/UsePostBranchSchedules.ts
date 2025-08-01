// UsePostBranchSchedules.ts
import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  BranchScheduleRequest,
  ErrorDetailsBeYou,
} from "@/types/api-beyou";
import {
  castRequestBody,
  UseTypedApiClientBY,
} from "@/hooks/UseTypedApiClientBY";
import { transformErrorKeys } from "@/utils/util";
import { paths } from "@/api/clients/beyou/api";

interface UsePostBranchSchedulesProps {
  branchId: number;
  onSuccess?: (
    data: boolean,
    variables: BranchScheduleRequest[]
  ) => void;
  onError?: (
    data: ErrorDetailsBeYou,
    variables: BranchScheduleRequest[]
  ) => void;
  onSettled?: (
    data: boolean | undefined,
    error: ErrorDetailsBeYou | null,
    variables: BranchScheduleRequest[]
  ) => void;
}

// ✅ Este path debe existir exactamente en tu OpenAPI spec:
const path: keyof paths = "/api/Branch/{branchId}/Schedule";

export const UsePostBranchSchedules = ({
  branchId,
  onSuccess,
  onError,
  onSettled,
}: UsePostBranchSchedulesProps) => {
  const method = "post";
  const postBranchSchedules = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  const createBranchSchedulesMutation = useMutation<
    boolean, // <- El valor devuelto (data)
    ApiError, // <- El tipo de error
    BranchScheduleRequest[] // <- Las variables de entrada
  >({
    mutationKey: ["PostBranchSchedules"],
    mutationFn: async (branchSchedules) => {
      const { data } = await postBranchSchedules(
        castRequestBody({ branchId, body: branchSchedules }, path, method)
      );
      return data as boolean;
    },
    onSuccess: async (data, variables) => {
      await queryClient.invalidateQueries({
        queryKey: ["GetBranchSchedules", branchId.toString()],
      });
      onSuccess?.(data, variables);
    },
    onError: (error, variables) => {
      onError?.(
        transformErrorKeys(error.data) as ErrorDetailsBeYou,
        variables
      );
    },
    onSettled: (data, error, variables) => {
      onSettled?.(
        data,
        error ? (transformErrorKeys(error.data) as ErrorDetailsBeYou) : null,
        variables
      );
    },
  });

  return createBranchSchedulesMutation;
};
