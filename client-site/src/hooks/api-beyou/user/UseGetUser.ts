import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { ApiError } from "openapi-typescript-fetch";
import { UseTypedApiClientBY, castRequestBody } from "@/hooks/UseTypedApiClientBY";
import { UserList } from "@/types/api-beyou"; 

export const UseGetUser = (): UseQueryResult<UserList, ApiError> => {
  const path = "/api/User";
  const method = "get";

  const getUsers = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetUsers"],
    queryFn: async () => {
      const { data } = await getUsers(castRequestBody({}, path, method));
      return data;
    },
    staleTime: 1000 * 60 * 5,
  });
};
