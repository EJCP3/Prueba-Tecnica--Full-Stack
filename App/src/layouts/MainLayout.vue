
<script setup>
import { useRouter } from 'vue-router';
import { useAuthStore } from '../stores/auth';
import Footer from '../components/Footer.vue';

const auth = useAuthStore();
const router = useRouter();
const handleLogout = () => {
  auth.logout();          
  router.push("/login");  
};
  

</script>

<template>
  <div class="min-h-screen flex flex-col">
    <header class="navbar bg-base-100  z-10">
      <div class="flex-1">
        <router-link to="/" class="btn btn-ghost text-xl md:text-2xl">Inventario</router-link>
      </div>
      <div class="flex  items-center">
        <h3 class="mr-4">Hola, {{ auth.user?.nombre  }}  </h3>
        <img
          src="/perfil.svg"
          alt="Avatar"
          class="w-10 h-10 rounded-full mr-4"
        />
        <button @click="handleLogout" class="btn btn-soft btn-warning">Cerrar sesión</button>
      </div>
    </header>

    <main class="flex-1 p-4 ">
      <router-view />
    </main>

  <Footer />
  </div>
</template>
