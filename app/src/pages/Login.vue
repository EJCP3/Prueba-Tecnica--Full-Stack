<script setup>
import { ref } from "vue";
import { useAuthStore } from "../stores/auth";
import { useRouter } from "vue-router";

const email = ref("");
const nombre = ref("");
const password = ref("");
const error = ref("");

const auth = useAuthStore();
const router = useRouter();

const handleLogin = async () => {
  const ok = await auth.login(nombre.value, email.value, password.value,);
  console.log(nombre.value, email.value, password.value,)
  if (!ok) {
    error.value = "Credenciales incorrectas";
    return;
  }

  router.push("/"); // redirige al home
};
</script>

<template>
  <div class="bg-login">
    <div class="absolute inset-0 bg-black/30"></div>

    <section
      class="flex flex-col justify-center items-center relative z-10 h-full"
    >
      <div class="bg-base-200 rounded-2xl max-w-xs w-full mx-4">
        <h1 class="text-center pt-8 text-2xl font-bold">Datos de acceso</h1>

        <form
          @submit.prevent="handleLogin"
          class="p-8 rounded-lg shadow-lg z-10"
        >
          <div class="mb-4">
            <label for="nombre" class="block font-bold mb-2">nombre:</label>
            <input
              type="text"
              id="nombre"
              v-model="nombre"
              required
              class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
<div class="mb-6">
            <label for="password" class="block font-bold mb-2"
              >Email:</label
            >
             <input
            v-model="email"
            type="email"
            class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          </div>
          
        
          <div class="mb-6">
            <label for="password" class="block font-bold mb-2"
              >Contraseña:</label
            >
            <input
              type="text"
              id="password"
              v-model="password"
              required
              class="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <button
            type="submit"
            class="w-full bg-primary text-primary-content font-bold py-2 px-4 rounded-lg hover:bg-blue-600 transition duration-300"
          >
            Acceder
          </button>
              <p v-if="error" class="text-red-500 mt-3">{{ error }}</p>
        </form>
      </div>
    </section>
  </div>
</template>
