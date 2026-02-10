import { ref, computed } from "vue";

export function useCarrito() {
  const carrito = ref([]); // Estado reactivo que almacena los productos del carrito

  const addToCart = (producto) => {
    // Agrega un producto o incrementa su cantidad si ya existe
    const existe = carrito.value.find(p => p.id === producto.id);
    if (existe) {
      existe.cantidad++;
    } else {
      carrito.value.push({ ...producto, cantidad: 1 });
    }
  };

  const removeFromCart = (id) => {
    // Elimina un producto según su ID
    carrito.value = carrito.value.filter(p => p.id !== id);
  };

  const clearCarrito = () => {
    // Limpia todo el carrito
    carrito.value = [];
  };

  const total = computed(() =>
    // Calcula el total del carrito en base a precio × cantidad
    carrito.value.reduce((sum, p) => sum + p.precio * p.cantidad, 0)
  );

  return {
    carrito,
    addToCart,
    removeFromCart,
    clearCarrito,
    total
  };
}
