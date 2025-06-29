import { Box, Container, CssBaseline, Stack } from "@mui/material";

export const Page = (props: {
  children: React.ReactNode;
  header?: React.ReactNode;
}) => {
  return (
    <>
      <CssBaseline />

      <Stack direction={"row"}>
        <Box
          sx={{
            flex: 1,
            position: "relative",
            // top: `${APP_BAR_HEIGHT}px`,
            // ml: { xs: 0, sm: `${NAVIGATION_BAR_WIDTH}px` },
            pb: { xs: "21px", sm: "14px" },
            overflowX: "hidden",
            overflowY: "auto",
          }}
        >
          {props.header}
          <Box
            px={{ xs: "16px", sm: "48px" }}
            pt={{ xs: "21px", sm: 4 }}
            pb={{ xs: "21px", sm: "32px" }}
          >
            {props.children}
          </Box>
        </Box>
      </Stack>
    </>
  );
};
