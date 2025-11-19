export function useCrudStore(store) {

  const addItem = (item, key) => {
    store[key].push({ ...item });
  };

 const deleteItem = (id, key) => {
  const index = store[key].findIndex(x => x.id === id);
  if (index !== -1) {
    store[key].splice(index, 1);
  }
};

  const editItem = (id, data, key) => {
    console.log(id);

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
