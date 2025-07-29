import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePostReservationProps {
  onSuccess?: (data: any, variables: any) => void;
  onError?: (data: any, variables: any) => void;
  onSettled?: (
    data: any | undefined,
    error: any | null,
    variables: any
  ) => void;
}

export const UsePostReservation = ({
  onSuccess,
  onError,
  onSettled,
}: UsePostReservationProps) => {
  const path = "/api/Reservation";
  const method = "post";

  const postReservation = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["PostReservation"],
    mutationFn: async (reservation: any) => {
      const { data } = await postReservation(castRequestBody(reservation, path, method));
      return data;
    },
    onSuccess: async (data, variables) => {
      await queryClient.invalidateQueries({ queryKey: ["GetReservations"] });
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
