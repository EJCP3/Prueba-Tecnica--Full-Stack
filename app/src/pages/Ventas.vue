<script setup>
import { date } from "../logic/date";
import { useClienteStore } from "../stores/cliente";
const clienteStore = useClienteStore();
const clientes = clienteStore.clientes;
import { useProductoStore } from "../stores/producto";
const productoStore = useProductoStore();
const productos = productoStore.productos;
import { ref, computed } from "vue";
import { useVentaStore } from "../stores/venta";
import ModalError from "../components/ModalError.vue";

const carrito = ref([]);
const formRef = ref(null);

const ventaStore = useVentaStore();
const showErrorModal = ref(false);

const onInvalid = () => {
  showErrorModal.value = true;
};

const addToCart = (producto) => {
  const existe = carrito.value.find((p) => p.id === producto.id);

  if (existe) {
    existe.cantidad++;
  } else {
    carrito.value.push({
      ...producto,
      cantidad: 1,
    });
  }
};

const removeFromCart = (id) => {
  carrito.value = carrito.value.filter((p) => p.id !== id);
};

const total = computed(() =>
  carrito.value.reduce((sum, p) => sum + p.precio * p.cantidad, 0)
);

const handleSubmit = (data) => {
   if (carrito.value.length === 0) {
    onInvalid();
    return;
  }
  const ventaData = {
    cliente: data.cliente,
    listaProductos: carrito.value.map((p) => ({
      id: p.id,
      nombre: p.nombre,
      precio: p.precio,
      cantidad: p.cantidad,
    })),
    total: total.value,
  };

  ventaStore.addVentas(ventaData);
  carrito.value = [];
  formRef.value.node.reset();

};
</script>

<template>
  <Navbard />

  <section class="flex flex-col justify-center items-center mt-20 px-4">
    <h1 class="text-3xl md:text-4xl font-bold text-center">
      Registrar Ventas
    </h1>

    <fieldset
      class="fieldset bg-base-200 border border-base-300 rounded-xl shadow-md w-full max-w-xl p-6 mt-6"
    >
      <legend class="fieldset-legend text-xl font-bold px-2 text-primary">
        🧾 Ventas
      </legend>

      <FormKit
        ref="formRef"
        id="myForm"
        type="form"
        @submit="handleSubmit"
        @invalid="onInvalid"
        :actions="false"
      >
        <!-- CLIENTE -->
        <div class="mb-4">
          <FormKit
            type="select"
            name="cliente"
            label="Cliente"
            validation="required"
            validation-label="cliente"
            placeholder="Seleccionar cliente"
            labelClass="font-semibold"
            inputClass="select select-bordered w-full"
            :options="
              clientes.map((c) => ({
                label: `${c.nombre} - ${c.email}`,
                value: c.nombre,
              }))
            "
          />
        </div>

        <!-- PRODUCTOS -->
        <h2 class="text-lg font-bold mt-4 mb-2">Productos</h2>

        <div class="grid grid-cols-1 sm:grid-cols-2 gap-3">
          <div
            v-for="p in productos"
            :key="p.id"
            class="flex justify-between items-center p-3 border rounded-lg bg-base-300"
          >
            <span class="font-medium text-sm sm:text-base">
              {{ p.nombre }} - ${{ p.precio }}
            </span>

            <button
              type="button"
              class="btn btn-sm btn-primary"
              @click="addToCart(p)"
            >
              Añadir
            </button>
          </div>
        </div>

        <!-- LISTA (CARRITO) -->
        <h2 class="text-lg font-bold mt-6">Lista</h2>

        <!-- VALIDACIÓN: carrito vacío -->
        <p v-if="carrito.length === 0" class="text-error text-sm mb-2">
          Debes agregar al menos un producto.
        </p>

        <div v-if="carrito.length" class="space-y-3 mt-2">
          <div
            v-for="item in carrito"
            :key="item.id"
            class="flex flex-col sm:flex-row justify-between items-start sm:items-center p-3 bg-base-200 rounded-lg gap-3"
          >
            <div>
              <p class="font-semibold">{{ item.nombre }}</p>
              <p class="text-sm text-gray-500">$ {{ item.precio }} c/u</p>
            </div>

            <div class="flex items-center gap-3 w-full sm:w-auto">
              <FormKit
                type="number"
                name="cantidad"
                v-model.number="item.cantidad"
                min="1"
                validation="required|number|min:1"
                inputClass="input input-bordered w-full sm:w-20"
              />

              <button
                type="button"
                class="btn btn-sm btn-error"
                @click="removeFromCart(item.id)"
              >
                X
              </button>
            </div>
          </div>
        </div>

        <!-- TOTAL -->
        <div
          class="p-3 mt-4 bg-base-300 rounded-lg text-right font-semibold text-xl"
        >
          <span class="text-neutral">Total:</span>
          <span class="text-primary">${{ total }}</span>
        </div>

        <!-- BOTÓN -->
        <FormKit
          inputClass="btn btn-primary w-full mt-6"
          type="submit"
          label="Guardar"
        />
      </FormKit>
    </fieldset>
  </section>
    <ModalError
    v-model="showErrorModal"
    title="Formulario incompleto"
    message="Por favor completa todos los campos obligatorios."
  />
</template>

