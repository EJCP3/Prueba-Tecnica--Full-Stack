import { defineStore } from "pinia";
import { reactive } from "vue";
import { useCrudStore } from "../composable/useCrudStore";

export const useVentaStore = defineStore("venta", () => {
  const ventas = reactive([
    {
      id: 1,
      fecha: "12/05/2002",
      cliente:"guas",
      
    },
  ]);

  const { addItem } = useCrudStore({ ventas }, "productos");

  return { ventas, addVenta: addItem };
});
