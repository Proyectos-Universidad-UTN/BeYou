import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { isPresent } from "@/utils/util";
import {
  castRequestBody,
  UseTypedApiClientBY,
} from "@/hooks/UseTypedApiClientBY";
import { ReservationDetail } from "@/types/api-beyou";

export const UseGetDetailReservationById = (
  reservationDetailId: string | number | undefined
): UseQueryResult<ReservationDetail, ApiError> => {
  const path = "/api/ReservationDetail/{reservationDetailId}";
  const method = "get";

  const getReservationDetail = UseTypedApiClientBY({ path, method });

  return useQuery<ReservationDetail, ApiError>({
    queryKey: ["GetDetailReservationById", reservationDetailId],

    queryFn: async () => {
      const { data } = await getReservationDetail(
        castRequestBody(
          { reservationDetailId: Number(reservationDetailId) },
          path,
          method
        )
      );
      return data;
    },

    retry: false,

    enabled: isPresent(reservationDetailId),

    staleTime: 0,
  });
};
