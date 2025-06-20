"use client";

import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { UserProfile } from "@/types/api-beyou";
import { useTokenStore } from "@/stores/UseTokenStore";
import { castRequestBody, UseTypedApiClientBY } from "../UseTypedApiClientBY";
import { ApiError } from "openapi-typescript-fetch";

// Ajusta el path y método exacto según tu API
const PROFILE_PATH = "/api/Me" as const;
const PROFILE_METHOD = "get" as const;

export const UseGetProfileLogged = (): UseQueryResult<UserProfile | null, ApiError> => {
  const {accessToken}= useTokenStore() //destructurar
  // Crear función fetch para el endpoint perfil
  const fetchProfile = UseTypedApiClientBY({
    path: PROFILE_PATH,
    method: PROFILE_METHOD,
  });

  return useQuery({
    queryKey: ["profile-logged"],
    queryFn: async () => {
      const {data} = await fetchProfile(castRequestBody({},PROFILE_PATH,PROFILE_METHOD));
      return data;
    },
    staleTime: 0,
    retry: false,
    enabled: !!accessToken
  });
};