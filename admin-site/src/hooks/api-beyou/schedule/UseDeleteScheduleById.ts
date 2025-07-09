import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ErrorDetailsBeYou } from "@/types/api-beyou";
import {
  castRequestBody,
  UseTypedApiClientBY,
} from "@/hooks/UseTypedApiClientBY";
import { transformErrorKeys } from "@/utils/util";

interface UseDeleteScheduleProps {
  onSuccess?: (data: boolean, variables: number) => void;
  onError?: (data: ErrorDetailsBeYou, variables: number) => void;
  onSettled?: (
    data: boolean | undefined,
    error: ErrorDetailsBeYou | null,
    variables: number
  ) => void;
}

export const UseDeleteSchedule = ({
  onSuccess,
  onError,
  onSettled,
}: UseDeleteScheduleProps) => {
  const path = "/api/Schedule/{scheduleId}";
  const method = "delete";

  const deleteSchedule = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  const deleteScheduleMutation = useMutation({
    mutationKey: ["DeleteSchedule"],
    mutationFn: async (scheduleId: number) => {
      const { data } = await deleteSchedule(
        castRequestBody({ scheduleId }, path, method)
      );
      return data;
    },
    onSuccess: async (data: boolean, variables: number) => {
      await queryClient.invalidateQueries({
        queryKey: ["Schedules"],
      });
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, _) => {
      onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, _);
    },
    onSettled: (data, error, variables) => {
      onSettled?.(data, error, variables);
    },
  });

  return deleteScheduleMutation;
};
