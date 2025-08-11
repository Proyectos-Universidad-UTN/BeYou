import { ApiError } from "openapi-typescript-fetch";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { transformErrorKeys } from "@/utils/util";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { useTokenStore } from "@/stores/UseTokenStore";

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

  const token = useTokenStore(state => state.getAccessToken());
  const queryClient = useQueryClient();

  const postReservation = token 
    ? UseTypedApiClientBY({ path, method, token }) 
    : null;

  return useMutation({
    mutationKey: ["PostReservation"],
    mutationFn: async (reservation: any) => {
      if (!postReservation) {
        throw new Error("Token no disponible para autenticación.");
      }
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

