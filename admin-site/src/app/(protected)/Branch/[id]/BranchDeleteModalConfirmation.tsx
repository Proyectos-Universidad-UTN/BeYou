import { useState } from "react";
import { Button, Stack } from "@mui/material";
import { Modal } from "@/components/Modal/Modal";
import { ModalFooter } from "@/components/Modal/ModalFooter";
import { ModalHeader } from "@/components/Modal/ModalHeader";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UseDeleteBranch } from "@/hooks/api-beyou/branch/UseDeleteBranchById";

interface BranchDeleteModalConfirmationProps {
    isModalOpen: boolean
    toggleIsOpen: () => void
    branchId: number
    title: string
}
export const BranchDeleteModalConfirmation = ({
    isModalOpen,
    toggleIsOpen,
    branchId,
    title
}: BranchDeleteModalConfirmationProps) => {
    const [loading, setLoading] = useState(false);

    const { mutate: deleteBranch } = UseDeleteBranch(UseMutationCallbacks('Sucursal eliminada exitosamente', '/Branch', toggleIsOpen))

    const handleConfirm = () => {
        setLoading(true);
        deleteBranch(branchId)
    }

    return (
        <Modal
            isOpen={isModalOpen}
            toggleIsOpen={toggleIsOpen}
            sx={{
                width: { xs: '90vw', sm: '50%' },
                height: 'auto'
            }}
        >
            <ModalHeader
                toggleIsOpen={toggleIsOpen}
                title="¿Esta seguro que desear eliminar la sucursal?"
                subTitle={`Nombre: ${title}`}
            />
            <ModalFooter>
                <Stack direction='row' spacing={2}>
                    <Button variant="outlined" onClick={toggleIsOpen}>Cancelar</Button>
                    <Button
                        loading={loading}
                        loadingPosition="start"
                        variant="contained"
                        onClick={handleConfirm}
                    >
                        Confirmar
                    </Button>
                </Stack>
            </ModalFooter>

        </Modal >
    )
}