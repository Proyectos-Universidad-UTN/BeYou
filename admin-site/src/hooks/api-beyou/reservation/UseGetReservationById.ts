import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { isPresent } from "@/utils/util";
import { Reservation } from "@/types/api-beyou";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";

export const UseGetReservationById = (
  reservationId: string | undefined
): UseQueryResult<Reservation, ApiError> => {
  const path = "/api/Reservation/{reservationId}";
  const method = "get";

  const getReservation = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetReservation", reservationId],
    queryFn: async () => {
      const { data } = await getReservation(
        castRequestBody({ reservationId: Number(reservationId) }, path, method)
      );
      return data;
    },
    retry: false,
    enabled: isPresent(reservationId),
    staleTime: 0,
  });
};
