import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ErrorDetailsBeYou, Reservation } from "@/types/api-beyou";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

interface UsePutReservationProps {
  onSuccess?: (
    data: Reservation,
    variables: Reservation
  ) => void;
  onError?: (
    data: ErrorDetailsBeYou,
    variables: Reservation
  ) => void;
  onSettled?: (
    data: Reservation | undefined,
    error: ErrorDetailsBeYou | null,
    variables: Reservation
  ) => void;
}

export const UsePutReservation = ({
  onSuccess,
  onError,
  onSettled,
}: UsePutReservationProps) => {
  const path = "/api/Reservation/{reservationId}";
  const method = "put";

  const putReservation = UseTypedApiClientBY({ path, method });
  const queryClient = useQueryClient();

  return useMutation({
    mutationKey: ["PutReservation"],
    mutationFn: async (reservation: Reservation) => {
      const { data } = await putReservation(
        castRequestBody(
          { reservationId: Number(reservation.id), ...reservation },
          path,
          method
        )
      );
      return data;
    },
    onSuccess: async (data: Reservation, variables: Reservation) => {
      await queryClient.invalidateQueries({ queryKey: ["GetReservations"] });
      onSuccess?.(data, variables);
    },
    onError: (error: ApiError, variables: Reservation) => {
      onError?.(
        transformErrorKeys(error.data) as ErrorDetailsBeYou,
        variables
      );
    },
    onSettled: (data, error, variables) => {
      onSettled?.(data, error, variables);
    },
  });
};
