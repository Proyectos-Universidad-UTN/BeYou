"use client"

import { useState } from "react";
import { removePhoneMask } from "@/utils/util";
import { Page } from "@/components/Shared/Page";
import { BranchForm } from "../components/BranchForm";
import { PageHeader } from "@/components/Shared/PageHeader";
import { initialBranchValues, BranchFormType } from "../components/BranchSchema";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UsePostBranch } from "@/hooks/api-beyou/branch/UsePostBranch";

const NewBranchPage = () => {
    const [loading, setLoading] = useState(false);

    const closeLoading = () => setLoading(false);

    const { mutate: postBranch } = UsePostBranch(UseMutationCallbacks('Sucursal creada exitosamente', '/Branch', closeLoading))

    const handleSubmit = (data: BranchFormType) => {
        setLoading(true)
        postBranch({
            ...data,
            telephone: removePhoneMask(data.telephone)
        })
    }

    return (
        <Page
            header={
                <PageHeader
                    title="Crear Sucursal"
                    subtitle="Agrega una nueva sucursal"
                />
            }
        >
            <BranchForm defaultValues={initialBranchValues} onSubmit={handleSubmit} isLoading={loading} />
        </Page>
    )
};

export default NewBranchPage