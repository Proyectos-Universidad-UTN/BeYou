import { Button, ButtonProps, CircularProgress } from "@mui/material";

interface ConfirmButtonProps extends ButtonProps {
  label?: string;
  isLoading?: boolean;
}

export const ConfirmButton = ({ label = "Confirmar", isLoading = false, disabled, ...props }: ConfirmButtonProps) => {
  return (
    <Button
      variant="contained"
      color="success"
      disabled={isLoading || disabled}
      sx={{
        backgroundColor: "#28a745",
        "&:hover": { backgroundColor: "#218838" },
        textTransform: "none",
        borderRadius: "12px",
        position: "relative",
      }}
      {...props}
    >
      {isLoading && (
        <CircularProgress
          size={24}
          sx={{
            color: "white",
            position: "absolute",
            left: "12px",
            top: "50%",
            marginTop: "-12px",
          }}
        />
      )}
      {label}
    </Button>
  );
};
