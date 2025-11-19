import { defineStore } from "pinia";
import { reactive } from "vue";
import { useCrudStore } from "../composable/useCrudStore";


export const useProductoStore = defineStore("producto", () => {
  const productos = reactive([
    {
      id:1,
      nombre:"agua",
      descripcion:"100ml",
      precio:"20",
      stock:"10"
    }
  ]);

  const { addItem, deleteItem, editItem } = useCrudStore({ productos }, "productos");
  

  return { productos ,addItem, deleteItem, editItem };
});
