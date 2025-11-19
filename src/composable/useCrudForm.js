import { ref } from "vue";
import { useCrudStore } from "./useCrudStore";

export function useCrudForm(store, initialData, formRef, key) {


  const editId = ref(null);
  const formData = ref({ ...initialData });

  const handleEdit = (item) => {
    editId.value = item.id;
    formData.value = { ...item };

    if (formRef?.value?.node?.input) {
      formRef.value.node.input({ ...item });
    }
  };

  const saveEdit = (data) => {
    store.editItem(editId.value, data, key);  // ← CORRECTO
    editId.value = null;
  };

  const handleSubmit = (data) => {
    const newItem = {
      id: Date.now(),
      ...data,
    };

    store.addItem({ ...newItem }, key);       // ← CORRECTO
  };

  const handleRemove = (id) => {

    store.deleteItem(id, key );                // ← CORRECTO
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
