<script setup>
import { clienteSchema } from "../schema/clienteSchema";
import { useClienteStore } from "../stores/cliente";
import { useClientes } from "../composable/useClientes";
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
  <section class="flex flex-col justify-center items-center mt-20 gap-y-10">
    <h1 class="text-4xl">Crear Clientes</h1>

    <fieldset
      class="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4"
    >
      <legend class="fieldset-legend">Clientes</legend>

      <FormKit
        ref="formRef"
        id="myForm"
        type="form"
        @submit="onSubmit"
        :actions="false"
      >
        <FormKitSchema :schema="clienteSchema" />
        <FormKit
          inputClass="btn btn-secondary mt-10"
          type="submit"
          :label="editId ? 'Editar' : 'Guardar'"
        />
      </FormKit>
    </fieldset>

    <!-- <fieldset
      class="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4"
    >
      <legend class="fieldset-legend">Clientes</legend>

      <label class="label">Nombre</label>
      <input type="email" class="input" placeholder="Email" />

      <label class="label">Email</label>
      <input type="password" class="input" placeholder="Password" />

      <label class="label">telefono</label>
      <input type="password" class="input" placeholder="Password" />

      <button class="btn btn-neutral mt-4">Agregar</button>
    </fieldset> -->

    <div
      class="overflow-x-auto rounded-box border border-base-content/5 bg-base-100"
    >
      <table class="table">
        <!-- head -->
        <thead>
          <tr>
            <th></th>
            <th>Nombre</th>
            <th>Email</th>
            <th>Telefono</th>
          </tr>
        </thead>
        <tbody>
          <!-- row 1 -->
          <tr v-for="cliente in clienteStore.clientes" :key="cliente.id">
            <th>{{ cliente.id }}</th>
            <td>{{ cliente.nombre }}</td>
            <td>{{ cliente.email }}</td>
            <td>{{ cliente.telefono }}</td>
            <td class="flex gap-x-2">
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
