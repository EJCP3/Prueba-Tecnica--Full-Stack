<script setup>
import { useProductoStore } from "../stores/producto";
import { useProductos } from "../composable/useProductos";
import { productoSchema } from "../schema/productoSchema";

const productoStore = useProductoStore();

const { producto, editando, handleEdit, saveEdit, handleSubmit, handleRemove } =
  useProductos();

  const onSubmit = (data) => {
  if (editando.value) {
    saveEdit(data);   // ← le pasas los datos editados
  } else {
    handleSubmit(data);   // ← le pasas los datos nuevos
  }
};

</script>
<template>
  <Navbard />
  <section class="flex flex-col justify-center items-center mt-20 gap-y-10">
    <h1 class="text-4xl">Crear Productos</h1>
    <div class="flex flex-col md:flex-row gap-x-20">
      <!-- <fieldset
        class="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4"
      >
        <legend class="fieldset-legend">Productos</legend>
        <form @submit.prevent="editando ? saveEdit() : handleSubmit()">
          <label class="label">Nombre</label>
          <input
            v-model="producto.nombre"
            type="text"
            class="input"
            placeholder="Nombre"
          />

          <label class="label">Descripcion</label>
          <input
            v-model="producto.descripcion"
            type="text"
            class="input"
            placeholder="Descripcion"
          />

          <label class="label">Precio</label>
          <input
            v-model="producto.precio"
            type="text"
            class="input"
            placeholder="Precio"
          />

          <label class="label">Stock</label>
          <input
            v-model="producto.stock"
            type="text"
            class="input"
            placeholder="Stock"
          />

          <button class="btn btn-neutral mt-4">
            {{ editando ? "Guardar Cambios" : "Agregar" }}
          </button>
        </form>
      </fieldset> -->

       <fieldset class="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4">
      <legend class="fieldset-legend">Productos</legend>

      <FormKit type="form"   @submit="onSubmit" :actions="false"   :value="producto">
        <FormKitSchema :schema="productoSchema" />
      </FormKit>
    </fieldset>

      <div
        class="overflow-x-auto rounded-box border border-base-content/5 bg-base-100"
      >
        <table class="table">
          <!-- head -->
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Descripcion</th>
              <th>Precio</th>
              <th>Stock</th>
            </tr>
          </thead>
          <tbody>
            <!-- row 1 -->
            <tr v-for="producto in productoStore.productos" :key="producto.id">
              <th>{{ producto.id }}</th>
              <td>{{ producto.nombre }}</td>
              <td>{{ producto.descripcion }}</td>
              <td>${{ producto.precio }}</td>
              <td>{{ producto.stock }}</td>
              <td class="flex gap-x-2">
                <button
                  class="btn btn-sm btn-info"
                  @click="handleEdit(producto)"
                >
                  Editar
                </button>

                <button
                  class="btn btn-sm btn-error"
                  @click="handleRemove(producto.id)"
                >
                  Eliminar
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </section>
</template>
