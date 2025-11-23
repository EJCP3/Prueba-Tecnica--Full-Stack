
<script setup>
import { useRouter } from 'vue-router';
import { useAuthStore } from '../stores/auth';

const auth = useAuthStore();
const router = useRouter();
const handleLogout = () => {
  auth.logout();          
  router.push("/login");  
};
  

console.log(auth)
</script>

<template>
  <div class="min-h-screen flex flex-col">
    <header class="navbar bg-base-100  z-10">
      <div class="flex-1">
        <router-link to="/" class="btn btn-ghost text-xl md:text-2xl">Inventario</router-link>
      </div>
      <div class="flex  items-center">
        <h3 class="mr-4">Hola, {{ auth.user?.nombre  }}, {{ auth.user?.rol }} </h3>
        <img
          src="/perfil.svg"
          alt="Avatar"
          class="w-10 h-10 rounded-full mr-4"
        />
        <button @click="handleLogout" class="btn btn-soft btn-warning">Cerrar sesión</button>
      </div>
    </header>

    <main class="flex-1 p-4">
      <router-view />
    </main>

    <footer
      class="footer sm:footer-horizontal  text-neutral-content items-center p-4"
    >
      <figure data-tip="Fuera de servicio!!!"
        class=" tooltip  ml-auto flex items-center justify-center space-x-2 flex-row-reverse"
      >
        <img class=" w-14  p-2 table:ml-0" src="/Group.png" />
        <figcaption class="text-white">¿Necesita asistencia?</figcaption>
      </figure>
    </footer>
  </div>
</template>
