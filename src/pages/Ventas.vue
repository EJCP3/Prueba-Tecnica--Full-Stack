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

const carrito = ref([]);

const ventaStore = useVentaStore();


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
  const ventaData = {
    cliente: data.cliente,
    productos: carrito.value.map((p) => ({
      id: p.id,
      nombre: p.nombre,
      precio: p.precio,
      cantidad: p.cantidad,
    })),
    total: total.value,
  };

  ventaStore.addVentas(ventaData);

  console.log(ventaStore.ventas)
};
</script>

<template>
  <Navbard />
  <section class="flex flex-col justify-center items-center mt-20">
    <h1 class="text-4xl">Registrar Ventas</h1>

    <fieldset
      class="fieldset bg-base-200 border border-base-300 rounded-xl shadow-md w-full max-w-md p-6 mt-6"
    >
      <legend class="fieldset-legend text-xl font-bold px-2 text-primary">
        🧾 Ventas
      </legend>

      <FormKit
        ref="formRef"
        id="myForm"
        type="form"
        @submit="handleSubmit"
        :actions="false"
      >
        <div class="mb-4">
          <FormKit
            type="select"
            label="Cliente"
            name="cliente"
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

        <h2 class="text-lg font-bold mt-4 mb-2">Productos</h2>

        <div
          v-for="p in productos"
          :key="p.id"
          class="flex justify-between items-center p-3 border rounded-lg mb-2 bg-base-300"
        >
          <span>{{ p.nombre }} - ${{ p.precio }}</span>

          <button
            type="button"
            class="btn btn-sm btn-primary"
            @click="addToCart(p)"
          >
            Añadir
          </button>
        </div>

        <!-- CARRITO -->
        <h2 class="text-lg font-bold mt-6">Lista</h2>

        <div v-if="carrito.length" class="space-y-3 mt-2">
          <div
            v-for="item in carrito"
            :key="item.id"
            class="flex justify-between items-center p-3 bg-base-200 rounded-lg"
          >
            <div>
              <p class="font-semibold">{{ item.nombre }}</p>
              <p class="text-sm text-gray-500">$ {{ item.precio }} c/u</p>
            </div>

            <div class="flex items-center gap-3">
              <input
                type="number"
                min="1"
                v-model.number="item.cantidad"
                class="input input-bordered w-20"
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

        <p v-if="carrito.length === 0" class="text-gray-500">
          No has agregado productos.
        </p>
        <!-- TOTAL -->
        <div
          class="p-3 mt-4 bg-base-300 rounded-lg text-right font-semibold text-lg"
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
</template>
