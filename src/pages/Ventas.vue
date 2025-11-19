<script setup>
  import {ventaSchema} from "../schema/ventaSchema";
  import { useVentaStore } from "../stores/venta";
  import { ref } from "vue";
  import { useCrudForm } from "../composable/useCrudForm";

  const ventaStore = useVentaStore();

  const formRef = ref(null);

  const venta = {
    cliente: "",
    productos: [],
    total: 0,
  };

  const { formData, editId, handleEdit, saveEdit, handleSubmit, handleRemove } =
    useCrudForm(ventaStore, venta, formRef, "ventas");

  const onSubmit = (data) => {
    console.log(data);
    if (editId.value) {
      saveEdit(data);
    } else {
      handleSubmit(data);
    }
    formRef.value.node.reset();
  };
</script>


<template>
  <Navbard/>
 <section class="flex flex-col justify-center items-center mt-20 ">
     <h1 class="text-4xl">Registrar Ventas</h1>
<fieldset
      class="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4"
    >
      <legend class="fieldset-legend">Ventas</legend>

      <FormKit
        ref="formRef"
        id="myForm"
        type="form"
        @submit="onSubmit"
        :actions="false"
        
      >
        <FormKitSchema   :schema="ventaSchema" />
        <FormKit
          inputClass="btn btn-secondary mt-10"
          type="submit"
          :label="editId ? 'Editar' : 'Guardar'"
        />
      </FormKit>
    </fieldset>

 </section>
</template>
