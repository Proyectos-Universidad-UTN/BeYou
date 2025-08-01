import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { castRequestBody, UseTypedApiClientBY } from "@/hooks/UseTypedApiClientBY";
import { components } from "@/api/clients/beyou/api";

type ResponseReservationCalendarAgendaDto = components["schemas"]["ResponseReservationCalendarAgendaDto"];

export const UseGetReservationsCalendar = (
  branchId: number,
  startDate?: string,
  endDate?: string
): UseQueryResult<ResponseReservationCalendarAgendaDto[], ApiError> => {
  const path = "/api/Branch/{branchId}/reservations"; // clave exacta del tipo
  const method = "get";

  const getReservations = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetReservationsCalendar", branchId, startDate, endDate],
    queryFn: async () => {
      const { data } = await getReservations(
        castRequestBody(
          {
            path: { branchId },  // Aquí va branchId en la propiedad path
            query: {
              startDate,
              endDate,
            },
          },
          path,
          method
        )
      );
      return data.flat(); // El endpoint devuelve array de arrays
    },
    enabled: !!branchId,
    staleTime: 0,
  });
};
