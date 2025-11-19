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

  const saveEdit = (data) => {
    productoStore.editProducto(editando.value, data);
    editando.value = null;
    resetForm();
  };

  const handleSubmit = (data) => {
    const newProducto = {
      id: Date.now(),
      ...data,
    };

    productoStore.addProducto(newProducto);
    resetForm();
  };

  const handleRemove = (id) => {
    productoStore.deleteProducto(id);
    console.log("first")
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
