"use client"

import { Box, Typography } from "@mui/material"
import Image from "next/image"

interface NoDataIndicadorProps {
  noDataMessage?: string
  imageSource?: string
}

export const NoDataIndicador = ({
  noDataMessage = "No se encontró información",
  imageSource = "/NoDataFound.svg",
}: NoDataIndicadorProps) => (
  <Box
    display="flex"
    flexDirection="column"
    alignItems="center"
    padding={{ xs: 7 }}
    marginTop={{ xs: 2 }}
  >
    <Image
      src={imageSource}
      alt="No se encontró información"
      width={350}
      height={350}
      style={{ objectFit: "contain" }}
    />

    <Typography variant="body2" marginTop={{ xs: 3 }} color="grey">
      {noDataMessage}
    </Typography>
  </Box>
)
