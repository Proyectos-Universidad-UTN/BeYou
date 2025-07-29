import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { Reservation } from "@/types/api-beyou";

export const UseGetReservations = (): UseQueryResult<Array<Reservation>, ApiError> => {
  const path = "/api/Reservation";
  const method = "get";

  const getReservations = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetReservations"],
    queryFn: async () => {
      const { data } = await getReservations(castRequestBody({}, path, method));
      return data;
    },
    enabled: true,
    staleTime: 0,
  });
};
