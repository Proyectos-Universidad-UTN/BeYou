import { useSnackbar } from "@/stores/useSnackbar";
import { ErrorDetailsBeYou } from "@/types/api-beyou";
import { useRouter } from "next/router";

export const UseMutationCallbacks = (
  successMessage: string,
  redirectTo: string,
  onSettledCallback?: () => void
) => {
  const router = useRouter();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  return {
    onSuccess: () => {
      setSnackbarMessage(successMessage);
      router.push(redirectTo);
    },
    onError: (data: ErrorDetailsBeYou) => {
      setSnackbarMessage(`${data.message}`, "error");
    },
    onSettled: () => {
      onSettledCallback?.();
    },
  };
};