// CustomerDeleteModalConfirmation.tsx
"use client";

import { useState } from "react";
import { Button, Stack } from "@mui/material";
import { Modal } from "@/components/Modal/Modal";
import { ModalFooter } from "@/components/Modal/ModalFooter";
import { ModalHeader } from "@/components/Modal/ModalHeader";
import { LoadingButton } from "@mui/lab";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UseDeleteCustomer } from "@/hooks/api-beyou/customer/UseDeleteCustomer";

interface CustomerDeleteModalConfirmationProps {
    isModalOpen: boolean;
    toggleIsOpen: () => void;
    customerId: number;
    title: string;
}

export const CustomerDeleteModalConfirmation = ({
    isModalOpen,
    toggleIsOpen,
    customerId,
    title,
}: CustomerDeleteModalConfirmationProps) => {
    const [loading, setLoading] = useState(false);

    // Obtén el objeto de callbacks una sola vez
    const callbacks = UseMutationCallbacks(
        "Cliente eliminado exitosamente",
        "/Customer",
        toggleIsOpen
    );

    // ✅ La forma correcta de llamar al hook
    const { mutate: deleteCustomer } = UseDeleteCustomer({
        onSuccess: (data, variables) => {
            setLoading(false);
            callbacks.onSuccess();
        },
        onError: (error, variables) => {
            setLoading(false);
            callbacks.onError(error);
        },
        onSettled: (data, error, variables) => {
            setLoading(false);
        }
    });

    const handleConfirm = () => {
        console.log("Deleting customer:", customerId);
        setLoading(true);
        deleteCustomer(customerId);
    };

    return (
        <Modal
            isOpen={isModalOpen}
            toggleIsOpen={toggleIsOpen}
            sx={{
                width: { xs: "90vw", sm: "50%" },
                height: "auto",
            }}
        >
            <ModalHeader
                toggleIsOpen={toggleIsOpen}
                title="¿Está seguro que desea eliminar el cliente?"
                subTitle={`Nombre: ${title}`}
            />
            <ModalFooter>
                <Stack direction="row" spacing={2}>
                    <Button variant="outlined" onClick={toggleIsOpen}>
                        Cancelar
                    </Button>
                    <LoadingButton
                        loading={loading}
                        loadingPosition="start"
                        variant="contained"
                        onClick={handleConfirm}
                    >
                        Confirmar
                    </LoadingButton>
                </Stack>
            </ModalFooter>
        </Modal>
    );
};