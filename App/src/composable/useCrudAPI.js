import { ref } from "vue";
import { useCrudStore } from "./useCrudStore";
import API from "../axios";

export function useCrudApi(store) {
  const { addItem, editItem, deleteItem } = useCrudStore(store); // Funciones del store para manipular datos

  const loading = ref(false); // Indica si la petición está en curso
  const error = ref(null); // Almacena errores de la API

  const fetchItems = async (resource, key) => {
    // Obtiene los datos de la API y actualiza el store
    loading.value = true;
    error.value = null;
    try {
      const { data } = await API.get(`/${resource}`);
      store[key].value.splice(0, store[key].value.length, ...data);
      return data;
    } catch (err) {
      console.error(`Error al cargar ${resource}`, err);
      error.value = err;
      throw err;
    } finally {
      loading.value = false;
    }
  };

  const createItemApi = async (resource, key, nuevo) => {
    // Crea un nuevo elemento en la API y lo agrega al store
    error.value = null;
    try {
      const { data } = await API.post(`/${resource}`, nuevo);
      addItem(data, key);
      return data;
    } catch (err) {
      console.error(`Error al crear ${resource}`, err);
      error.value = err;
      throw err;
    }
  };

  const updateItemApi = async (resource, key, id, datosActualizados) => {
    // Actualiza un elemento existente en la API y en el store
    error.value = null;
    try {
      await API.put(`/${resource}/${id}`, datosActualizados);
      editItem(id, datosActualizados, key);
    } catch (err) {
      console.error(`Error al actualizar ${resource}`, err);
      error.value = err;
      throw err;
    }
  };

  const deleteItemApi = async (resource, key, id) => {
    // Elimina un elemento de la API y del store
    error.value = null;
    try {
      await API.delete(`/${resource}/${id}`);
      deleteItem(id, key);
    } catch (err) {
      console.error(`Error al eliminar ${resource}`, err);
      error.value = err;
      throw err;
    }
  };

  return {
    loading,
    error,
    fetchItems,
    createItemApi,
    updateItemApi,
    deleteItemApi,
  };
}
