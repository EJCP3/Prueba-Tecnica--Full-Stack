import { ref, computed, onMounted } from "vue";
import { useClienteStore } from "../stores/cliente";
import { useProductoStore } from "../stores/producto";
import { useVentaStore } from "../stores/venta";
import { useCarrito } from "./useCarrito";

export function useVentaForm() {
  const clienteStore = useClienteStore(); 
  const productoStore = useProductoStore();
  const ventaStore = useVentaStore(); 

  const clientes = clienteStore.clientes; 
  const productos = productoStore.productos; 

  const formRef = ref(null); // Referencia al formulario
  const showErrorModal = ref(false); // Controla la visibilidad del modal de error

  // Carrito reutilizable
  const { carrito, addToCart, removeFromCart, total, clearCarrito } =
    useCarrito();

  onMounted(() => {
    // Carga inicial de datos al montar el componente
    ventaStore.fetchVentas();
    clienteStore.fetchClientes();
    productoStore.fetchProductos();
  });

  const onInvalid = () => {
    // Muestra el modal de error si el formulario es inválido
    showErrorModal.value = true;
  };

  const handleSubmit = (formData) => {
    // Encuentra el cliente seleccionado
    const clienteSeleccionado = clientes.find(
      (cliente) => cliente.nombre === formData.cliente
    );

    // Valida que haya un cliente y productos en el carrito
    if (!clienteSeleccionado || carrito.value.length === 0) {
      onInvalid();
      return;
    }

    // Prepara los datos de la venta
    const ventaData = {
      ClienteID: clienteSeleccionado.id,
      Detalles: carrito.value.map((productoCarrito) => ({
        ProductoID: productoCarrito.id,
        NombreProducto: productoCarrito.nombre,
        Cantidad: productoCarrito.cantidad,
        Precio: productoCarrito.precio,
      })),
    };

    // Agrega la venta al store
    ventaStore.addItem(ventaData);

    // Limpia el carrito y resetea el formulario
    clearCarrito();
    formRef.value.node.reset();
  };

  return {
    clientes,
    productos,
    carrito,
    total,
    formRef,
    showErrorModal,
    addToCart,
    removeFromCart,
    onInvalid,
    handleSubmit,
  };
}
