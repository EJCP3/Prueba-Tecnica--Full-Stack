import { defineStore } from "pinia";
import { ref } from "vue";
import { useCrudApi } from "../composable/useCrudAPI";

export const useClienteStore = defineStore("clientes", () => {
  const clientes = ref([]); // Lista reactiva de clientes

  // Composable para manejar operaciones CRUD y estados de carga/error
  const {
    fetchItems,
    createItemApi,
    updateItemApi,
    deleteItemApi,
    loading,
    error,
  } = useCrudApi({ clientes });

  // Funciones preparadas para usar con la API y store
  const fetchClientes = () => fetchItems("Clientes", "clientes"); // Carga todos los clientes
  const addItem = (nuevoCliente) => createItemApi("Clientes", "clientes", nuevoCliente); // Agrega cliente
  const editItem = (clienteId, datosActualizados) => updateItemApi("Clientes", "clientes", clienteId, datosActualizados); // Edita cliente
  const deleteItem = (clienteId) => deleteItemApi("Clientes", "clientes", clienteId); // Elimina cliente

  return {
    clientes,
    fetchClientes,
    addItem,
    editItem,
    deleteItem,
    loading,
    error,
  };
});
