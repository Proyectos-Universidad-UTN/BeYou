import { isNil } from "lodash";
import { Button } from "@mui/material";
import { useEffect, useState } from "react";
import { getDayInSpanish, getErrorMessage } from "utils/util";
import { Page } from "components/Shared/Page";
import AddIcon from '@mui/icons-material/Add';
import { useSnackbar } from "stores/useSnackbar";
import { PageHeader } from "components/Shared/PageHeader";
import { ErrorProcess } from "components/Error/ErrorProcess";
import { Link, useNavigate, useParams } from "react-router-dom";
import { CircularLoadingProgress } from "components/LoadingProgress/CircularLoadingProcess";
import { UseGetBranchScheduleById } from "hooks/api-basereservation/branch/schedule/UseGetBranchScheduleById";
import { BlockTable } from "./BlockTable";

export const Block = () => {
    const { branchId, scheduleId } = useParams<{ branchId?: string, scheduleId?: string }>();
    const navigate = useNavigate();

    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const { data, isLoading, isError, error } = UseGetBranchScheduleById(scheduleId);
    const [loading, setLoading] = useState<boolean>(true);
    const isValidBranchId = isNil(branchId) || !isNil(branchId) && !isNaN(Number(branchId));
    const isValidScheduleId = isNil(scheduleId) || !isNil(scheduleId) && !isNaN(Number(scheduleId));

    if (isValidBranchId && isValidScheduleId && !loading && !isLoading && !isError && branchId != data?.branchId) {
        setSnackbarMessage("Horario no corresponde a sucursal", "error");
        navigate(`/Sucursal/${branchId}/Horario`);
    }

    useEffect(() => {
        if (!isValidBranchId || !isValidScheduleId) {
            navigate(`/Sucursal/${branchId}/Horario`);
            return;
        }
        if (isError) {
            navigate(`/Sucursal/${branchId}/Horario`);
            setSnackbarMessage(`${getErrorMessage(error)}`, 'error')
            return;
        }
        setLoading(false)
    }, [isError, navigate, setSnackbarMessage, isValidBranchId, isValidScheduleId, error, branchId]);

    if (isLoading || loading) {
        return <CircularLoadingProgress />
    }

    if (isError) {
        return <ErrorProcess />
    }

    return (
        <Page
            header={
                <PageHeader
                    title={`Bloqueos del horario - ${getDayInSpanish(data?.schedule?.day)}`}
                    subtitle={`${data?.branch?.name ?? ''}`}
                    backPath={`/Sucursal/${branchId}/Horario`}
                    backText="Horarios"
                    actionButton={
                        <Link to={`/Sucursal/${Number(branchId)}/Horario/${scheduleId}/Bloqueo/Nuevo`}>
                            <Button variant="contained" size="large" fullWidth startIcon={<AddIcon />}>Agregar bloqueo</Button>
                        </Link>
                    } />
            }
        >
            <BlockTable branchId={Number(branchId)} scheduleId={Number(scheduleId)} blocks={data?.branchScheduleBlocks ?? []} />
        </Page>
    )
}