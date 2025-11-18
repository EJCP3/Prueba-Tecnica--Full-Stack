<template>
  <Navbard/>
 <section class="flex flex-col justify-center items-center mt-20 gap-y-10">
     <h1 class="text-4xl">Crear Clientes</h1>
  <fieldset
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
  </fieldset>

  <div class="overflow-x-auto rounded-box border border-base-content/5 bg-base-100">
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
        <tr>
            <th>1</th>
            <td>pedro</td>
            <td>pedro@example.com</td>
            <td>123-456-7890</td>
        </tr>
     
    </tbody>
  </table>
</div>
 </section>
</template>
