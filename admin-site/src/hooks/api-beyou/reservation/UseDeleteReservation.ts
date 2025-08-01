import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { ErrorDetailsBeYou } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { ApiError } from "openapi-typescript-fetch";

interface UseDeleteReservationProps {
  onSuccess?: (data: boolean, variables: number) => void;
  onError?: (data: ErrorDetailsBeYou, variables: number) => void;
  onSettled?: (
    data: boolean | undefined,
    error: ErrorDetailsBeYou | null,
    variables: number
  ) => void;
}

export const UseDeleteReservation = ({
  onSuccess,
  onError,
  onSettled,
}: UseDeleteReservationProps = {}) => {
  const path = "/api/Reservation/{reservationId}";
  const method = "delete";

  const deleteReservation = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation<boolean, ApiError, number>({
    mutationKey: ["DeleteReservation"],
    mutationFn: async (reservationId: number) => {
      const { data } = await deleteReservation(
        castRequestBody({ reservationId }, path, method)
      );
      return data;
    },
    onSuccess: async (data, variables) => {
      await queryClient.invalidateQueries({ queryKey: ["GetReservations"] });
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
