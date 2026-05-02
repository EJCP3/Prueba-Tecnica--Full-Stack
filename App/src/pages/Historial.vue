<script setup>
import { useVentaStore } from "../stores/venta";
import { onMounted } from "vue";

const ventaStore = useVentaStore();

// Usamos una función para formatear la fecha de forma legible
const formatearFecha = (fechaRaw) => {
  if (!fechaRaw) return "Sin fecha";
  const fecha = new Date(fechaRaw);
  return fecha.toLocaleDateString("es-ES", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  });
};

onMounted(() => {
  ventaStore.fetchVentas();
});
</script>

<template>
  <Navbard />

  <section class="flex flex-col justify-center items-center mt-20 px-4">
    <h1 class="text-3xl md:text-4xl font-bold text-center">
      Historial de Ventas
    </h1>

    <div
      class="overflow-x-auto rounded-xl border border-base-content/10 bg-base-100 mt-10 w-full max-w-5xl shadow"
    >
      <table class="table table-zebra">
        <thead>
          <tr class="text-sm md:text-base">
            <th>ID</th>
            <th>Fecha</th>
            <th>Cliente</th>
            <th class="min-w-[220px]">Lista de productos</th>
            <th>Total</th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="venta in ventaStore.ventas"
            :key="venta.id"
            class="text-sm md:text-base"
          >
            <td>{{ venta.id }}</td>

            <td>{{ formatearFecha(venta.fecha) }}</td>

            <td>
              <div class="font-medium">
                {{ venta.cliente?.nombre || 'Cliente no registrado' }}
              </div>
              <div class="text-xs opacity-50">
                {{ venta.cliente?.email }}
              </div>
            </td>

            <td class="whitespace-normal break-words">
              {{
                (venta.detalles || [])
                  .map(
                    (p) => `${p.nombreProducto} ($${p.precio} x ${p.cantidad})`
                  )
                  .join(", ")
              }}
            </td>

            <td class="font-bold text-success">
              ${{ venta.total.toLocaleString() }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </section>
</template>