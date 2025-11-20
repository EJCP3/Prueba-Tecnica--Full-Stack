<script setup>
import { useProductoStore } from "../stores/producto";
import { productoSchema } from "../schema/productoSchema";
import { ref } from "vue";
import { useCrudForm } from "../composable/useCrudForm";

const productoStore = useProductoStore();
const formRef = ref(null);

const producto = {
  nombre: "",
  descripcion: "",
  precio: "",
  stock: "",
};

const { formData, editId, handleEdit, saveEdit, handleSubmit, handleRemove } =
  useCrudForm(productoStore, producto, formRef, "productos");




const onSubmit = (data) => {
  console.log(data);
  if (editId.value) {
   
    saveEdit(data);
  } else {
    handleSubmit(data);
  }
  formRef.value.node.reset();
};
</script>

<template>
  <Navbard />

  <section class="flex flex-col items-center mt-20 gap-10 px-4">
    <h1 class="text-4xl text-center">Crear Productos</h1>

    <!-- Contenedor responsivo -->
    <div class="flex flex-col w-full max-w-6xl gap-10 md:flex-row md:items-start">

      <!-- FORMULARIO -->
      <fieldset
        class="fieldset bg-base-200 border-base-300 rounded-box w-full md:w-1/3 border p-4"
      >
        <legend class="fieldset-legend">Productos</legend>

        <FormKit
          ref="formRef"
          id="myForm"
          type="form"
          @submit="onSubmit"
          :actions="false"
        >
          <FormKitSchema :schema="productoSchema" />

          <FormKit
            inputClass="btn btn-secondary mt-10 w-full"
            type="submit"
            :label="editId ? 'Editar' : 'Guardar'"
          />
        </FormKit>
      </fieldset>

      <!-- TABLA RESPONSIVA -->
      <div
        class="w-full overflow-x-auto rounded-box border border-base-content/5 bg-base-100"
      >
        <table class="table w-full min-w-[600px] md:min-w-full">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Descripción</th>
              <th>Precio</th>
              <th>Stock</th>
              <th>Acciones</th>
            </tr>
          </thead>

          <tbody>
            <tr v-for="producto in productoStore.productos" :key="producto.id">
              <th>{{ producto.id }}</th>
              <td>{{ producto.nombre }}</td>
              <td>{{ producto.descripcion }}</td>
              <td>${{ producto.precio }}</td>
              <td>{{ producto.stock }}</td>

              <td class="flex flex-col gap-2 md:flex-row">
                <button
                  class="btn btn-sm btn-info w-full md:w-auto"
                  @click="handleEdit(producto)"
                >
                  Editar
                </button>

                <button
                  class="btn btn-sm btn-error w-full md:w-auto"
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