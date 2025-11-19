import { ref } from "vue";
import { useProductoStore } from "../stores/producto";

export function useProductos(formRef) {
  const productoStore = useProductoStore();

  const editando = ref(null);

  const producto = ref({
    nombre: "",
    descripcion: "",
    precio: "",
    stock: "",
  });

  const handleEdit = (productoEdit) => {
    editando.value = productoEdit.id;
    producto.value = { ...productoEdit };

    formRef.value.node.input({
      nombre: productoEdit.nombre,
      descripcion: productoEdit.descripcion,
      precio: productoEdit.precio,
      stock: productoEdit.stock,
    });
  };

  const saveEdit = (data) => {
    console.log("first");
    productoStore.editProducto(editando.value, data);
    editando.value = null;

  };

  const handleSubmit = (data) => {
    const newProducto = {
      id: Date.now(),
      ...data,
    };

    productoStore.addProducto(newProducto);
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
