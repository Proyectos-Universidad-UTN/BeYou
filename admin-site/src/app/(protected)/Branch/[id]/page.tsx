"use client";

import { Button } from "@mui/material";
import { useEffect, useState } from "react";
import { Page } from "@/components/Shared/Page";
import { useSnackbar } from "@/stores/useSnackbar";
import { BranchForm } from "../components/BranchForm";
import { useParams, useRouter } from "next/navigation";
import { PageHeader } from "@/components/Shared/PageHeader";
import { getErrorMessage, removePhoneMask } from "@/utils/util";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UsePutBranch } from "@/hooks/api-beyou/branch/UsePutBranch";
import { UseGetBranchById } from "@/hooks/api-beyou/branch/UseGetBranchById";
import { BranchFormType, ConvertToBranchSchema } from "../components/BranchSchema";
import { CircularLoadingProgress } from "@/components/LoadingProgress/CircularLoadingProcess";
import { BranchDeleteModalConfirmation } from "./BranchDeleteModalConfirmation";

const EditBranchPage = () => {
    const router = useRouter()
    const params = useParams()
    const [loading, setLoading] = useState(false)
    const [openModalConfirmation, setOpenModalConfirmation] = useState(false);

    const setSnackbarMessage = useSnackbar((state) => state.setMessage);

    const branchIdRaw = params?.id

    const branchId = branchIdRaw && !isNaN(Number(branchIdRaw)) ? String(branchIdRaw) : undefined

    const closeLoading = () => setLoading(false)

    const { data, isLoading, isError, error } = UseGetBranchById(branchId)

    const { mutate: putBranch } = UsePutBranch(UseMutationCallbacks('Sucursal actualizada exitosamente', '/Branch', closeLoading))

    useEffect(() => {

        if (isError) {
            setSnackbarMessage(getErrorMessage(error), 'error')
            router.replace('/Branch')
        }
    }, [isError, router, setSnackbarMessage, error])

    const handleSubmit = (data: BranchFormType) => {
        setLoading(true)
        putBranch({
            ...data,
            telephone: removePhoneMask(data.telephone)
        })
    }

    if (isLoading) return <CircularLoadingProgress />

    return (
        <Page
            header={
                <PageHeader
                    title={`Editar Sucursal Nº ${data?.id}`}
                    subtitle="Actualiza los datos de la sucursal"
                    backPath="/Branch"
                    backText="Sucursales"
                    actionButton={
                        <Button className="!bg-red-500 hover:bg-red-600" variant="contained" size="large" fullWidth onClick={() => setOpenModalConfirmation(true)}>
                            Eliminar
                        </Button>
                    }
                />}
        >
            <BranchForm defaultValues={ConvertToBranchSchema(data)} onSubmit={handleSubmit} isLoading={loading} />

            <BranchDeleteModalConfirmation
                isModalOpen={openModalConfirmation}
                toggleIsOpen={() => setOpenModalConfirmation(!openModalConfirmation)}
                branchId={data?.id ?? 0}
                title={data?.name ?? ""}
            />
        </Page>
    )
};

export default EditBranchPage
