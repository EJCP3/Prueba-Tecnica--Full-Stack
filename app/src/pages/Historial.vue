<script setup>
import { useVentaStore } from "../stores/venta";
const ventaStore = useVentaStore();
const ventas = ventaStore.ventas;
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
            v-for="venta in ventas"
            :key="venta.id"
            class="text-sm md:text-base"
          >
            <td>{{ venta.id }}</td>
            <td>{{ venta.fecha }}</td>
            <td>{{ venta.cliente }}</td>

            <td class="whitespace-normal break-words">
              {{
                (venta.listaProductos || [])
                  .map(
                    (p) => `${p.nombre} ($${p.precio} x ${p.cantidad})`
                  )
                  .join(", ")
              }}
            </td>

            <td class="font-semibold text-primary">
              ${{ venta.total }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </section>
</template>
