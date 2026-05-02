<script setup>
import { FormKitSchema } from "@formkit/vue";

import { useUsuarioStore } from "../stores/usuarios";
import { userSchema } from "../schema/nuevoUsuarioSchema";
import { ref } from "vue";

const formRef = ref(null);

const usuarioStore = useUsuarioStore();

const handleSubmit = async (data) => {
  try {
    await usuarioStore.crearUsuario(data);

    alert("Usuario creado correctamente");
  } catch (error) {
    alert(usuarioStore.error);
  }
  formRef.value.node.reset();
};
</script>


<template>
  <div class="max-w-xl mx-auto mt-10 p-8 bg-base-200 shadow-xl rounded-xl">
    <h2 class="text-2xl font-bold mb-6 text-primary">Crear nuevo usuario</h2>

    <FormKit
       ref="formRef"
      type="form"
      :config="{ validationVisibility: 'submit' }"
      @submit="handleSubmit"
        :actions="false"
      
    >
      <FormKitSchema :schema="userSchema" />

      
        <FormKit
          inputClass="btn btn-primary w-full mt-6"
          type="submit"
          :label="'Guardar'"
        />
    </FormKit>
  </div>
</template>