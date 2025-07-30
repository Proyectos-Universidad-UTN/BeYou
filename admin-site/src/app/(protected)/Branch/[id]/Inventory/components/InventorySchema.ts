import * as yup from "yup";
import { InferType } from "yup";
import { Inventory } from "@/types/api-beyou";

export const initialInventoryValues = {
  branchId: 0,
  name: "",
  typeInventory: "",
  active: true,
};

export const InventorySchema = yup.object({
  branchId: yup
    .number()
    .min(1, "Debe seleccionar una sucursal válida")
    .required("Sucursal requerida"),
  name: yup
    .string()
    .required("Nombre de inventario requerido"),
  typeInventory: yup
    .string()
    .required("Tipo de inventario requerido"),
  active: yup
    .boolean()
    .required(),
});

export const ConvertToInventorySchema = (
  inventory: Inventory | null | undefined
): InventoryFormType => {
  if (!inventory) {
    return initialInventoryValues;
  }

  return {
    branchId: Number(inventory.branchId),
    name: String(inventory.name),
    typeInventory: String(inventory.typeInventory),
    active: Boolean(inventory.active),
  };
};

export type InventoryFormType = InferType<typeof InventorySchema>;
