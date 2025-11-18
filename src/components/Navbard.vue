<script setup>
import { ref } from "vue";
</script>

<template class="">
  <div class="navbar bg-base-100 shadow-sm">
    <div class="flex-1">
      <router-link to="/" class="btn btn-ghost text-xl">Panel</router-link>
    </div>
    <div class="flex-none">
      <ul class="menu menu-horizontal px-1">
        <li>
          <details>
            <summary>Optiones</summary>
            <ul class="bg-base-100 rounded-t-none p-2">
              <li><router-link to="productos">Productos</router-link></li>
                 <li>
                <router-link to="ventas"
                  >Registrar Ventas</router-link
                >
              </li>

              <li>
                <router-link to="historial"
                  >Historial ventas</router-link
                >
              </li>
              <li><router-link to="clientes">Clientes</router-link></li>
           
            </ul>
          </details>
        </li>
      </ul>
    </div>
  </div>
</template>
