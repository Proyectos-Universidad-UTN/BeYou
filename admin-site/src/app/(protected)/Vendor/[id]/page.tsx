"use client";

import { useEffect, useState } from "react";
import { useRouter, usePathname } from "next/navigation";
import { UseGetVendorById } from "@/hooks/api-beyou/vendor/UseGetVendorById";
import { UsePutVendor } from "@/hooks/api-beyou/vendor/UsePutVendor";
import { useSnackbar } from "@/stores/useSnackbar";

import {
  VendorFormType,
  initialVendorValues,
} from "../components/VendorSchema";
import { VendorForm } from "../components/VendorForm";
import { useQueryClient } from "@tanstack/react-query";

export default function EditVendorPage() {
  const router = useRouter();
  const { setMessage } = useSnackbar();

  const pathname = usePathname();
  const idStr = pathname?.split("/").pop();
  const queryClient = useQueryClient();

  const [isLoading, setIsLoading] = useState(false);
  const [defaultValues, setDefaultValues] =
    useState<VendorFormType>(initialVendorValues);

  const {
    data: vendorData,
    isLoading: loadingVendor,
    error,
  } = UseGetVendorById(idStr);

  const putVendor = UsePutVendor({
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["GetVendors"] });
      setMessage("Proveedor actualizado con éxito", "success");
      router.push("/Vendor");
    },

    onError: (error) => {
      setMessage("Error al actualizar proveedor", "error");
      console.error(error);
    },
  });

  useEffect(() => {
    if (vendorData) {
      setDefaultValues({
        name: vendorData.name || "",
        fiscalNumber: vendorData.fiscalNumber || "",
        socialReason: vendorData.socialReason || "",
        telephone: vendorData.telephone ? String(vendorData.telephone) : "",
        email: vendorData.email || "",
        districtId: vendorData.districtId || 0,
        address: vendorData.address || "",
      });
    }
  }, [vendorData]);

  const onSubmit = (data: VendorFormType) => {
    if (!idStr) return;

    setIsLoading(true);

    const payload = {
      id: Number(idStr),
      ...data,
      telephone: Number(data.telephone),
    };

    putVendor.mutate(payload, {
      onSettled: () => setIsLoading(false),
    });
  };

  if (loadingVendor) return <p>Cargando proveedor...</p>;
  if (error) return <p>Error al cargar proveedor.</p>;

  return (
    <div className="p-4 max-w-4xl mx-auto">
      <h1 className="text-2xl font-semibold mb-6">Editar Proveedor</h1>
      <VendorForm
        onSubmit={onSubmit}
        defaultValues={defaultValues}
        isLoading={isLoading}
        isEdit={true}
      />
    </div>
  );
}
