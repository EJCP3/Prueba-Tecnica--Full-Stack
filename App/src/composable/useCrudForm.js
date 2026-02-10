import { ref } from "vue";

export function useCrudForm(store, initialData, formRef) {

  console.log(store, initialData, formRef) // Muestra los parámetros al iniciar

  const editId = ref(null); // Almacena el ID del elemento que se está editando
  const formData = ref({ ...initialData }); // Datos reactivos del formulario

  const handleEdit = (item) => {
    // Prepara el formulario para editar un elemento
    editId.value = item.id;
    formData.value = { ...item };

    if (formRef?.value?.node?.input) {
      formRef.value.node.input({ ...item }); // Actualiza el form externo si existe
    }
  };

  const saveEdit = async (data) => {
    // Guarda los cambios de edición en el store
    await store.editItem(editId.value, data);
    editId.value = null; // Resetea el ID de edición
  };

  const handleSubmit = async (data) => {
    // Agrega un nuevo elemento al store
    await store.addItem(data);
  };

  const handleRemove = async (id) => {
    // Elimina un elemento del store
    await store.deleteItem(id);
  };

  return {
    formData,
    editId,
    handleEdit,
    saveEdit,
    handleSubmit,
    handleRemove,
  };
}
