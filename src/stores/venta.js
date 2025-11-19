import { defineStore } from "pinia";
import { reactive } from "vue";
import { useProductoStore } from "./producto";
import { useClienteStore } from "./cliente";
import { date } from "../logic/date";

export const useVentaStore = defineStore("venta", () => {

 const fechaFormateada = date();
console.log(fechaFormateada.value);

  const ventas = reactive([
    {
      id: 1,
      fecha: "12/05/2002 ",
      cliente:"guas",
      listaProductos:[
        {
          nombre:"agua",
          cantidad: 2,
          precio:10
        },
         {
          nombre:"leche",
          cantidad: 1,
          precio:20
        }
      ],
      total: 40
    },
  ]);

  const addVentas = (factura) => {
      ventas.push({
        id: Date.now(),
        fecha:fechaFormateada.value,
        ...factura
      })
  }




  return { ventas, addVentas};
});
