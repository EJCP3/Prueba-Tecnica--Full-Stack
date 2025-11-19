import { defineStore } from "pinia";
import { reactive } from "vue";
import { useCrud } from "../composable/useCrud";

export const useClienteStore = defineStore("cliente", () => {

const clientes = reactive([]);

  const {addItem, deleteItem, editItem} = useCrud({clientes})



  return { clientes, addCliente: addItem, deleteCliente:deleteItem, editCliente: editItem}

})