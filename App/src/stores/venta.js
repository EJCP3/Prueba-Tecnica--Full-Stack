import { defineStore } from "pinia";
import { ref } from "vue";
import { useCrudApi } from "../composable/useCrudAPI";

export const useVentaStore = defineStore("venta", () => {
  const ventas = ref([]); // Lista reactiva de ventas

  // Composable para operaciones CRUD y estados de carga/error
  const {
    fetchItems,
    createItemApi,
    updateItemApi,
    deleteItemApi,
    loading,
    error,
  } = useCrudApi({ ventas });

  // Funciones listas para usar con la API y el store
  const fetchVentas = () => fetchItems("Ventas", "ventas"); // Carga todas las ventas
  const addItem = (nuevaVenta) => createItemApi("Ventas", "ventas", nuevaVenta); // Agrega venta
  const editItem = (ventaId, datosActualizados) => updateItemApi("Ventas", "ventas", ventaId, datosActualizados); // Edita venta
  const deleteItem = (ventaId) => deleteItemApi("Ventas", "ventas", ventaId); // Elimina venta

  return {
    ventas,
    fetchVentas,
    addItem,
    editItem,
    deleteItem,
    loading,
    error,
  };
});
