import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import {
  ErrorDetailsBeYou,
  Schedule,
  ScheduleRequest,
} from "@/types/api-beyou";
import {
  castRequestBody,
  UseTypedApiClientBY,
} from "@/hooks/UseTypedApiClientBY";
import { transformErrorKeys } from "@/utils/util";

interface UsePutScheduleProps {
  onSuccess?: (data: Schedule, variables: ScheduleRequest) => void;
  onError?: (data: ErrorDetailsBeYou, variables: ScheduleRequest) => void;
  onSettled?: (
    data: Schedule | undefined,
    error: ErrorDetailsBeYou | null,
    variables: ScheduleRequest
  ) => void;
}

export const UsePutSchedule = ({
  onSuccess,
  onError,
  onSettled,
}: UsePutScheduleProps) => {
  const path = "/api/Schedule/{scheduleId}";
  const method = "put";

  const putSchedule = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  const updateScheduleMutation = useMutation({
    mutationKey: ["PutSchedule"],
    mutationFn: async (schedule: ScheduleRequest) => {
      const { data } = await putSchedule(
        castRequestBody(
          { scheduleId: Number(schedule.id), ...schedule },
          path,
          method
        )
      );
      return data;
    },
    onSuccess: async (data: Schedule, variables: ScheduleRequest) => {
      await queryClient.invalidateQueries({
        queryKey: ["GetSchedule"],
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

  return updateScheduleMutation;
};
