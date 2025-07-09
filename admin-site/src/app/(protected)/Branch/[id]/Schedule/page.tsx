// /Branch/[id]/Schedule/page.tsx

"use client";

import { useParams, useRouter } from "next/navigation";
import { Box, Button, Typography } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useEffect } from "react";
import { ErrorProcess } from "@/components/Error/ErrorProcess";
import { useSnackbar } from "@/stores/useSnackbar";
import { getErrorMessage } from "@/utils/util";
import { UseGetBranchById } from "@/hooks/api-beyou/branch/UseGetBranchById";
import Link from "next/link";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { ScheduleTable } from "../../Schedule/components/ScheduleTable";


const BranchSchedulePage = () => {
  const router = useRouter();
  const setSnackbarMessage = useSnackbar((state) => state.setMessage);

  const params = useParams();
  const branchIdParam = params?.id;
  const branchId =
    typeof branchIdParam === "string"
      ? branchIdParam
      : Array.isArray(branchIdParam)
      ? branchIdParam[0]
      : undefined;

  const {
    data: branchData,
    isLoading,
    isError,
    error,
  } = UseGetBranchById(branchId);

  useEffect(() => {
    if (!branchId || isError) {
      setSnackbarMessage(getErrorMessage(error), "error");
      router.push("/Branch");
    }
  }, [branchId, isError, error, router, setSnackbarMessage]);

  if (isLoading) return <CircularLoadingProgress />;
  if (isError || !branchData) return <ErrorProcess />;

  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Box>
          <Typography variant="h5" fontWeight="bold">Horarios de la Sucursal</Typography>
          <Typography variant="subtitle1">{branchData.name}</Typography>
        </Box>
        <Link href={`/Branch/${branchId}/Schedule/Gestion`}>
          <Button variant="contained" startIcon={<AddIcon />}>
            Gestión de horario
          </Button>
        </Link>
      </Box>

      <ScheduleTable
        branchId={Number(branchId)}
        schedules={branchData.branchSchedules ?? []}
      />
    </Box>
  );
};

export default BranchSchedulePage;
