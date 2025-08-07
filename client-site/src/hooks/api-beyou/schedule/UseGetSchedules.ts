import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { Schedule } from "@/types/api-beyou";
import {
  castRequestBody,
  UseTypedApiClientBY,
} from "@/hooks/UseTypedApiClientBY";

export const UseGetSchedules = (): UseQueryResult<
  Array<Schedule>,
  ApiError
> => {
  const path = "/api/Schedule";
  const method = "get";

  const getSchedules = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetSchedules"],
    queryFn: async () => {
      const { data } = await getSchedules(castRequestBody({}, path, method));
      return data;
    },
    enabled: true,
  });
};
