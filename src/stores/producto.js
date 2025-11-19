import { defineStore } from "pinia";
import { reactive } from "vue";
import { useCrud } from "../composable/useCrud";


export const useProductoStore = defineStore("producto", () => {
  const productos = reactive([]);

  const { addItem, deleteItem, editItem } = useCrud({ productos }, "productos");
  

  return { productos , addProducto:addItem, deleteProducto:deleteItem, editProducto:editItem };
});
