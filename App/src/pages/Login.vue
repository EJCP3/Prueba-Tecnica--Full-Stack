<script setup>
import { ref } from "vue";
import { useAuthStore } from "../stores/auth";
import { useRouter } from "vue-router";

const auth = useAuthStore();
const router = useRouter();

const error = ref("");
const loading = ref(false);

// ⬇ FormKit manda los valores aquí
const handleLogin = async (data) => {
  loading.value = true;
  error.value = "";

  const ok = await auth.login(data.nombre, data.email, data.password);

  loading.value = false;

  if (!ok) {
    error.value = "Credenciales incorrectas";
    return;
  }

  router.push("/");
};
</script>

<template>
  <div class="bg-login">
    <div class="absolute inset-0 bg-black/30"></div>

    <section
      class="flex flex-col justify-center items-center relative z-10 h-full"
    >
     
   <fieldset
    class="fieldset bg-base-200 border border-base-300 rounded-xl w-full max-w-md p-6 shadow"
  >
    <legend class="fieldset-legend text-lg font-bold text-warning">
      Iniciar Sesión
    </legend>

    <FormKit
      type="form"
      :actions="false"
      @submit="handleLogin"
      form-class="flex flex-col gap-4"
  incomplete-message="Por favor completa los campos."   
   >
      <!-- Nombre -->
      <FormKit
        type="text"
        name="nombre"
        label="Nombre"
        v-model="nombre"
        validation="required"
        placeholder="Ingresa tu nombre"
        outerClass="flex flex-col gap-1"
        labelClass="font-semibold text-warning"
        inputClass="w-full px-3 py-2 rounded-md bg-base-100 shadow-sm focus:outline-none focus:ring-2 focus:ring-warning transition-all"
      />

      <!-- Email -->
      <FormKit
        type="email"
        name="email"
        label="Email"
        v-model="email"
        validation="required|email"
        placeholder="correo@ejemplo.com"
        outerClass="flex flex-col gap-1"
        labelClass="font-semibold text-warning"
        inputClass="w-full px-3 py-2 rounded-md bg-base-100 shadow-sm focus:outline-none focus:ring-2 focus:ring-warning transition-all"
      />

      <!-- Contraseña -->
      <FormKit
        type="password"
        name="password"
        label="Contraseña"
        v-model="password"
        validation="required"
        placeholder="•••••••••"
        outerClass="flex flex-col gap-1"
        labelClass="font-semibold text-warning"
        inputClass="w-full px-3 py-2 rounded-md bg-base-100 shadow-sm focus:outline-none focus:ring-2 focus:ring-warning transition-all"
      />

      <!-- Botón -->
      <button
        type="submit"
        class="btn btn-primary w-full mt-4 flex justify-center"
        :disabled="loading"
      >
        <span v-if="loading" class="loading loading-spinner"></span>
        <span v-else>Acceder</span>
      </button>

      <p v-if="error" class="text-error text-center mt-2">{{ error }}</p>
    </FormKit>
  </fieldset>
      <figure
        data-tip="Fuera de servicio!!!"
        class="tooltip mt-10 flex items-center justify-end space-x-2 flex-row-reverse hover:text-base-context transition duration-300"
      >
        <img class="w-14 p-2 table:ml-0" src="/Group.png" />
        <figcaption class="text-white">¿Necesita asistencia?</figcaption>
      </figure>
    </section>
  </div>
</template>
