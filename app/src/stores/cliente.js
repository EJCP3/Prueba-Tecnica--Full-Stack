import { defineStore } from "pinia";
import { reactive } from "vue";
import { useCrudStore } from "../composable/useCrudStore";

export const useClienteStore = defineStore("cliente", () => {

const clientes = reactive([
   {
      id:1,
      nombre:"pedro",
      email:"pedro@gmail.com",
      telefono:"8054569874"
    
    },
      {
      id:2,
      nombre:"juan",
      email:"juan@gmail.com",
      telefono:"8054569874"
    
    },
      {
      id:1,
      nombre:"perez",
      email:"perez@gmail.com",
      telefono:"8054569874"
    
    }
]);

  const {addItem, deleteItem, editItem} = useCrudStore({clientes})



  return { clientes, addItem, deleteItem,  editItem}

})