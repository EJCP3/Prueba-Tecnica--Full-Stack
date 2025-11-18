<template>
  <Navbard/>
  <section class="flex flex-col justify-center items-center mt-20 ">
     <h1 class="text-4xl">Historial de Ventas</h1>
    <div
      class="overflow-x-auto rounded-box border border-base-content/5 bg-base-100"
    >
      <table class="table">
        <!-- head -->
        <thead>
          <tr>
            <th></th>
            <th>Fecha</th>
            <th>Cliente</th>
            <th>Lista de productos</th>
              <th>Total</th>
          </tr>
        </thead>
        <tbody>
          <!-- row 1 -->
          <tr>
            <th>01/01/2024</th>
            <td>Pedro</td>
            <td>Agua, leche, cocoa</td>
            <td>$10.00</td>
          </tr>
        </tbody>
      </table>
    </div>
  </section>
</template>
