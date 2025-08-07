import { useMutation } from "@tanstack/react-query";
import { ApiError } from "openapi-typescript-fetch";
import { transformErrorKeys } from "@/utils/util";
import {
  Authentication,
  ErrorDetailsBeYou,
  LoginUserRequest,
} from "@/types/api-beyou";
import {
  castRequestBody,
  UseTypedApiClientBY,
} from "@/hooks/UseTypedApiClientBY";

interface UsePostAuthenticationProps {
  onSuccess?: (data: Authentication, variables: LoginUserRequest) => void;
  onError?: (data: ErrorDetailsBeYou, variables: LoginUserRequest) => void;
}

export const UsePostAuthentication = ({
  onSuccess,
  onError,
}: UsePostAuthenticationProps) => {
  const path = "/api/Authentication";
  const method = "post";

  const postAuthentication = UseTypedApiClientBY({ path, method });

  return useMutation({
    mutationFn: async (loginUserInformation: LoginUserRequest) => {
      const { data } = await postAuthentication(
        castRequestBody(loginUserInformation, path, method)
      );
      return data;
    },
    onSuccess,
    onError: (error: ApiError, _) => {
      onError?.(transformErrorKeys(error.data) as ErrorDetailsBeYou, _);
    },
  });
};
