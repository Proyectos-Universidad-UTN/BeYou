"use client";

import { Button } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import Link from "next/link";

interface CreateButtonProps {
  href: string;
  label?: string;
}

export const CreateButton = ({ href, label = "Crear" }: CreateButtonProps) => {
  return (
    <Link href={href}>
      <Button
        variant="contained"
        startIcon={<AddIcon />}
        sx={{
          backgroundColor: "#D63384",
          "&:hover": { backgroundColor: "#c02976" },
          textTransform: "none",
          borderRadius: "12px",
        }}
      >
        {label}
      </Button>
    </Link>
  );
};
