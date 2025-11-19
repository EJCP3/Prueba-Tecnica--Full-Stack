export function useCrud(store, key) {
  console.log(store, key);

  const addItem = (item) => {
    store[key].push({ ...item });
  };

  const deleteItem = (id) => {
    store[key] = store[key].filter((x) => x.id !== id);
    console.log(key);
  };

  const editItem = (id, data) => {
    console.log("first");

    const index = store[key].findIndex((x) => x.id === id);
    if (index !== -1) {
      store[key][index] = {
        ...store[key][index],
        ...data,
      };
    }
  };

  return { addItem, deleteItem, editItem };
}
