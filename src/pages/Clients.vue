<script setup>
import { clienteSchema } from "../schema/clienteSchema";
import { useClienteStore } from "../stores/cliente";
import { ref } from "vue";
import { useCrudForm } from "../composable/useCrudForm";
const clienteStore = useClienteStore();



const formRef = ref(null);

const cliente = {
  nombre: "",
  email: "",
  telefono: "",
};

const { formData, editId, handleEdit, saveEdit, handleSubmit, handleRemove } =
  useCrudForm(clienteStore, cliente, formRef, "clientes");

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

  <section
    class="flex flex-col justify-center items-center mt-20 px-4 gap-y-10"
  >
    <h1 class="text-3xl md:text-4xl font-bold text-center">Crear Clientes</h1>

    <!-- FORMULARIO -->
    <fieldset
      class="fieldset bg-base-200 border border-base-300 rounded-xl w-full max-w-md p-6 shadow"
    >
      <legend class="fieldset-legend text-lg font-bold text-primary">
        Clientes
      </legend>

      <FormKit
        ref="formRef"
        id="myForm"
        type="form"
       
        @submit="onSubmit"
        :actions="false"
      >
        <FormKitSchema :schema="clienteSchema" />

        <FormKit
          inputClass="btn btn-primary w-full mt-6"
          type="submit"
          :label="editId ? 'Editar' : 'Guardar'"
        />
      </FormKit>
    </fieldset>

  

    <!-- TABLA -->
    <div
      class="overflow-x-auto rounded-xl border border-base-content/10 bg-base-100 w-full max-w-4xl shadow"
    >
      <table class="table table-zebra">
        <thead>
          <tr class="text-sm md:text-base">
            <th>ID</th>
            <th>Nombre</th>
            <th>Email</th>
            <th>Telefono</th>
            <th>Acciones</th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="cliente in clienteStore.clientes"
            :key="cliente.id"
            class="text-sm md:text-base"
          >
            <td>{{ cliente.id }}</td>
            <td>{{ cliente.nombre }}</td>
            <td>{{ cliente.email }}</td>
            <td>{{ cliente.telefono }}</td>

            <td class="flex gap-2">
              <button class="btn btn-sm btn-info" @click="handleEdit(cliente)">
                Editar
              </button>

              <button
                class="btn btn-sm btn-error"
                @click="handleRemove(cliente.id)"
              >
                Eliminar
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </section>
</template>
