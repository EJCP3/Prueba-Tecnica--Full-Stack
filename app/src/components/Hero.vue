<script setup>

const buttones = [
  {
    to: "productos",
    label: "Productos",
    icon:"/btn1.svg"
  },
  {
    to: "clients",
    label: "Clients",
    icon:"/btn2.svg"
  },
  {
    to: "ventas",
    label: "Registrar Ventas",
    icon:"/btn3.svg"
  },
  {
    to: "historial",
    label: "Historial Ventas",
    icon:"/btn4.svg"
  },
];



</script>

<template>
<section
  class="flex flex-col md:flex-row md:items-center md:justify-between md:w-[90%] md:mx-auto  lg:justify-center lg:items-center md:gap-x-20 md:mt-20 "
>
  <!-- IZQUIERDA → Logo + Título -->
  <figure class="flex flex-col items-center  mt-10 md:mt-0">
    <div class="flex justify-center items-center">
      <img class="md:size-60 size-40" src="/Finances.svg" />
      <figcaption
        class="font-dm-sans font-extralight text-3xl md:text-4xl pl-6 pt-6 text-gray-400"
      >
        Formulario <br class="table:hidden" />
        de verificación<br />
        y Aseguramiento<br />
        de unidad de ventas
      </figcaption>
    </div>
  </figure>

  <!-- DERECHA → Botones -->
  <article class="w-full md:w-[50%] lg:w-[70%] lg:max-w-[600px]  mt-6 md:mt-0">
    <div
      class="grid grid-cols-1 gap-4 md:grid-cols-1 md:gap-6 md:place-items-center lg:grid-cols-2 "
    >
      <router-link
        v-for="button in buttones"
        :key="button.to"
        :to="button.to"
        class="w-full md:w-[280px] bg-neutral text-300-context rounded-xl py-6 shadow-lg hover:opacity-90 transition flex justify-center items-center hover:border-primary hover:border-2  duration-300 delay-75 lg:gap-x-4"
      >
        <div class="flex justify-between items-center gap-x-6 w-50 text-center">
          <img :src="button.icon" class="w-10 h-10 mb-2" />
          <span class="text-xl font-semibold  w-50">{{ button.label }}</span>
        </div>
      </router-link>
    </div>
  </article>
</section>

<!-- <section class="  flex flex-col  md:flex-row md:mx-auto md:w-[90%]   md:mt-10  md:justify-between ">

  <figure class="flex justify-center mt-10 ">
        <img class="size-40" src="/Finances.svg"/>
        <figcaption class="font-dm-sans font-extralight text-3xl pl-6 pt-6 text-gray-400 table:mx-3 ">
        Formulario <br class="table:hidden"/>de verificación<br/> y Aseguramiento<br/> de unidad de ventas   
        </figcaption>
    </figure>


    <article>
      <div
        class="flex flex-col mt-6 gap-4 md:flex-wrap md:flex-row  md:mx-auto md:w-[50%]  "
      >
        <router-link
          v-for="button in buttones"
          :key="button.to"
          :to="button.to"
          class="md:w-[300px] card bg-neutral text-neutral-content rounded-box grid h-22 place-items-center "
        >
          <div class="flex  justify-between items-center gap-x-6">
            <img :src="button.icon" class="w-10 h-10 mb-2" />
            <span class="text-2xl">{{ button.label }}</span>
          </div>
        </router-link>
      </div>
    </article>


</section> -->

  <!-- <div
    class="flex flex-col gap-y-20 mx-auto mt-20 justify-center items-center w-4/5 text-black font-medium"
  >
    <div class="flex w-100 flex-col text-2xl ">
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
      <div class="divider lg:divider-horizontal text-yellow-500">OR</div>
      <router-link
        to="historial"
        class="card bg-info rounded-box grid h-20 grow place-items-center"
        >Historial Ventas</router-link
      >
    </div>
  </div> -->
</template>
