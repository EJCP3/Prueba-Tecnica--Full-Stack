import { ref } from "vue";
import { useProductoStore } from "../stores/producto";

export function useProductos() {
  const productoStore = useProductoStore();

  const editando = ref(null);

  const producto = ref({
    nombre: "",
    descripcion: "",
    precio: "",
    stock: "",
  });

  const resetForm = () => {
    producto.value = {
      nombre: "",
      descripcion: "",
      precio: "",
      stock: "",
    };
  };

  const handleEdit = (productoEdit) => {
    editando.value = productoEdit.id;
    producto.value = { ...productoEdit };
  };

  const saveEdit = () => {
    productoStore.editProducto(editando.value, producto.value);
    editando.value = null;
    resetForm();
  };

  const handleSubmit = () => {
    const newProducto = {
      id: Date.now(),
      ...producto.value,
    };

    productoStore.addProducto(newProducto);
    resetForm();
  };

  const handleRemove = (id) => {
    productoStore.deleteProducto(id);
  };

  return {
    producto,
    editando,
    handleEdit,
    saveEdit,
    handleSubmit,
    handleRemove,
  };
}
