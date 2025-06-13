import { useQuery, UseQueryResult } from '@tanstack/react-query';
import { paths } from '@/api/clients/beyou/api';
import { UseTypedApiClientBS } from '@/hooks/api-beyou/UseTypedApiClientBS';

// Ajusta el path y método exacto según tu API
const PROFILE_PATH = '/auth/profile' as const;
const PROFILE_METHOD = 'get' as const;

// Tipo de la respuesta (ajustar según tu spec)
type UserProfile = paths[typeof PROFILE_PATH][typeof PROFILE_METHOD]['responses'][200]['content']['application/json'];

export const UseGetProfileLogged = (): UseQueryResult<UserProfile, Error> => {
  // Crear función fetch para el endpoint perfil
  const fetchProfile = UseTypedApiClientBS({
    path: PROFILE_PATH,
    method: PROFILE_METHOD,
  });

  return useQuery<UserProfile, Error>(
    ['profile-logged'],
    async () => {
      const response = await fetchProfile();
      return response;
    },
    {
      staleTime: 5 * 60 * 1000, 
      retry: false,
    }
  );
};
