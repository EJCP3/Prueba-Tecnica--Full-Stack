import { defineStore } from "pinia";
import { reactive } from "vue";
import { useCrudStore } from "../composable/useCrudStore";


export const useProductoStore = defineStore("producto", () => {
  const productos = reactive([
    {
      id:1,
      nombre:"agua",
      descripcion:"100ml",
      precio:"20",
      stock:"10"
    },
     {
      id:2,
      nombre:"cocoa",
      descripcion:"lata",
      precio:"30",
      stock:"2"
    },
     {
      id:3,
      nombre:"leche",
      descripcion:"semi",
      precio:"50",
      stock:"2"
    },
     {
      id:4,
      nombre:"galleta",
      descripcion:"grande",
      precio:"32",
      stock:"4"
    },
    {
      id:5,
      nombre:"pan",
      descripcion:"funda",
      precio:"32",
      stock:"4"
    },
  ]);

  const { addItem, deleteItem, editItem } = useCrudStore({ productos }, "productos");
  

  return { productos ,addItem, deleteItem, editItem };
});
