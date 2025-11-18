<script setup>
import { ref } from "vue";
</script>

<template>
  <div
    class="flex flex-col gap-y-20 mx-auto mt-20 justify-center items-center w-4/5 "
  >
    <div class="flex w-100 flex-col text-2xl">
      <router-link
        to="productos"
        class="card bg-info rounded-box grid h-20 place-items-center "
      >
        Productos
      </router-link>
      <div class="divider"></div>
      <router-link
        to="clients"
        class="card bg-info rounded-box grid h-20 place-items-center "
        >Clients</router-link
      >
    </div>

    <div class="flex  flex-col lg:flex-row w-100 text-2xl lg:text-xl">
      <router-link
        to="ventas"
        class="card bg-info rounded-box grid h-20 grow place-items-center"
        >Registrar Ventas</router-link
      >
      <div class="divider lg:divider-horizontal">OR</div>
      <router-link
        to="historial"
        class="card bg-info rounded-box grid h-20 grow place-items-center"
        >Historial Ventas</router-link
      >
    </div>
  </div>
</template>
