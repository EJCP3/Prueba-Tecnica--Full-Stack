export function useCrudStore(store) {
  // Agrega un nuevo elemento al array del store
  const addItem = (item, key) => {
    store[key].value.push({ ...item });
  };

  // Elimina un elemento del array por su ID
  const deleteItem = (id, key) => {
    const index = store[key].value.findIndex((item) => item.id === id);
    if (index !== -1) {
      store[key].value.splice(index, 1);
    }
  };

  // Edita un elemento existente por su ID
  const editItem = (id, data, key) => {
    const index = store[key].value.findIndex((item) => item.id === id);
    if (index !== -1) {
      store[key].value[index] = {
        ...store[key].value[index],
        ...data,
      };
    }
  };

  return { addItem, deleteItem, editItem };
}
