"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { VendorForm } from "../components/VendorForm";
import { UsePostVendor } from "@/hooks/api-beyou/vendor/UsePostVendor";
import { VendorFormType, initialVendorValues } from "../components/VendorSchema";
import { useSnackbar } from "@/stores/useSnackbar";

export default function NewVendorPage() {
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const { setMessage } = useSnackbar();

  const postVendor = UsePostVendor({
    onSuccess: () => {
      setMessage("Proveedor creado exitosamente", "success");
      router.push("/Vendor");
    },
    onError: (error) => {
      setMessage("Error al crear proveedor", "error");
      console.error(error);
    },
  });

  const onSubmit = (data: VendorFormType) => {
    setIsLoading(true);
    postVendor.mutate(data, {
      onSettled: () => setIsLoading(false),
    });
  };

  return (
    <div className="p-4 max-w-4xl mx-auto">
      <h1 className="text-2xl font-semibold mb-6">Crear nuevo proveedor</h1>
      <VendorForm
        onSubmit={onSubmit}
        defaultValues={initialVendorValues}
        isLoading={isLoading}
      />
    </div>
  );
}
