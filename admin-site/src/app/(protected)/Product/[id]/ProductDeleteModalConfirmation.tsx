import { useState } from "react";
import { Button, Stack } from "@mui/material";
import { Modal } from "@/components/Modal/Modal";
import { ModalFooter } from "@/components/Modal/ModalFooter";
import { ModalHeader } from "@/components/Modal/ModalHeader";
import { UseMutationCallbacks } from "@/hooks/UseMutationCallbacks";
import { UseDeleteProductById } from "@/hooks/api-beyou/product/UseDeleteProductById";

interface ProductDeleteModalConfirmationProps {
    isModalOpen: boolean
    toggleIsOpen: () => void
    productId: number
    title: string
}
export const ProductDeleteModalConfirmation = ({
    isModalOpen,
    toggleIsOpen,
    productId,
    title
}: ProductDeleteModalConfirmationProps) => {
    const [loading, setLoading] = useState(false);

    const { mutate: deleteProduct } = UseDeleteProductById(UseMutationCallbacks('Producto eliminado exitosamente', '/Product', toggleIsOpen))

    const handleConfirm = () => {
        setLoading(true);
        deleteProduct(productId)
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
                title="¿Esta seguro que desear eliminar el producto?"
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