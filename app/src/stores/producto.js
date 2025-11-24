import { defineStore } from "pinia";
import { ref } from "vue";
import { useCrudApi } from "../composable/useCrudAPI";

export const useProductoStore = defineStore("producto", () => {
  const productos = ref([]); // Lista reactiva de productos

  // Composable para operaciones CRUD y estados de carga/error
  const {
    fetchItems,
    createItemApi,
    updateItemApi,
    deleteItemApi,
    loading,
    error,
  } = useCrudApi({ productos });

  // Funciones listas para usar con la API y el store
  const fetchProductos = () => fetchItems("Productos", "productos"); // Carga todos los productos
  const addItem = (nuevoProducto) => createItemApi("Productos", "productos", nuevoProducto); // Agrega producto
  const editItem = (productoId, datosActualizados) => updateItemApi("Productos", "productos", productoId, datosActualizados); // Edita producto
  const deleteItem = (productoId) => deleteItemApi("Productos", "productos", productoId); // Elimina producto

  return {
    productos,
    fetchProductos,
    addItem,
    editItem,
    deleteItem,
    loading,
    error,
  };
});
