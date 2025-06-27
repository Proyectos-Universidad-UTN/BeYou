"use client";

import { useIsMobile } from "@/hooks/UseIsMobile";
import { Box, Button, Stack } from "@mui/material";
import Link from "next/link";

interface FormButtonsProps {
  backPath: string;
  loadingIndicator: boolean;
}

export const FormButtons = ({
  backPath,
  loadingIndicator,
}: FormButtonsProps) => {
  const isMobile = useIsMobile();

  if (isMobile) {
    return (
      <Box
        display="flex"
        justifyContent="space-between"
        maxWidth="100vw"
        gap="8px"
      >
        <Box flex={1} px={1} pr={2} sx={{ pl: 0 }}>
          <Link href={backPath} style={{ textDecoration: "none" }}>
            <Button variant="outlined" fullWidth>
              Cancelar
            </Button>
          </Link>
        </Box>
        <Box flex={1} px={1} pl={2} sx={{ pr: 0 }}>
          <Button
            disabled={loadingIndicator}
            type="submit"
            variant="contained"
            fullWidth
          >
            {loadingIndicator ? "Cargando..." : "Guardar"}
          </Button>
        </Box>
      </Box>
    );
  }

  return (
    <Stack direction="row" spacing={2} justifyContent="flex-end">
      <Link href={backPath} style={{ textDecoration: "none" }}>
        <Button variant="outlined">Cancelar</Button>
      </Link>
      <Button disabled={loadingIndicator} type="submit" variant="contained">
        {loadingIndicator ? "Cargando..." : "Guardar"}
      </Button>
    </Stack>
  );
};