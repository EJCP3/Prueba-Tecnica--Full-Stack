import { defineStore } from "pinia";
import { ref } from "vue";

export const useProductoStore = defineStore("producto", () => {
  const productos = ref([

  ]);

  const addProducto = (producto) => {
    productos.value.push({ ...producto });
  };

  const deleteProducto = (id) => {
    productos.value = productos.value.filter((item) => item.id !== id);
  };

  const editProducto = (id, data) => {
    const index = productos.value.findIndex((p) => p.id === id);
    if (index !== -1) {
      productos.value[index] = { ...productos.value[index], ...data };
    }
  };

  return { productos, addProducto, deleteProducto, editProducto };
});
