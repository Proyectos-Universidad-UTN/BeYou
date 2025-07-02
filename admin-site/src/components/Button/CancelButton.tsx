"use client";

import { Button, ButtonProps } from "@mui/material";

interface CancelButtonProps extends ButtonProps {
  label?: string;
}

export const CancelButton = ({ label = "Cancelar", ...props }: CancelButtonProps) => {
  return (
    <Button
      variant="contained"
      color="warning"
      sx={{
        backgroundColor: "#fd7e14",
        "&:hover": { backgroundColor: "#e36a04" },
        textTransform: "none",
        borderRadius: "12px",
      }}
      {...props}
    >
      {label}
    </Button>
  );
};
