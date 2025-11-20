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
    fecha: "12/05/2002",
    cliente: "Juan",
    listaProductos: [
      { nombre: "agua", cantidad: 2, precio: 10 },
      { nombre: "leche", cantidad: 1, precio: 20 }
    ],
    total: 40
  },
  {
    id: 2,
    fecha: "15/05/2002",
    cliente: "Pedro",
    listaProductos: [
      { nombre: "coca cola", cantidad: 3, precio: 15 },
      { nombre: "galletas", cantidad: 1, precio: 12 }
    ],
    total: 57
  },
  {
    id: 3,
    fecha: "18/05/2002",
    cliente: "Maria",
    listaProductos: [
      { nombre: "pan", cantidad: 5, precio: 8 },
      { nombre: "jugo", cantidad: 2, precio: 14 }
    ],
    total: 66
  },
  {
    id: 4,
    fecha: "20/05/2002",
    cliente: "Luis",
    listaProductos: [
      { nombre: "huevos", cantidad: 12, precio: 5 },
      { nombre: "leche", cantidad: 1, precio: 20 }
    ],
    total: 80
  },
  {
    id: 5,
    fecha: "22/05/2002",
    cliente: "Ana",
    listaProductos: [
      { nombre: "arroz", cantidad: 1, precio: 30 },
      { nombre: "aceite", cantidad: 1, precio: 45 }
    ],
    total: 75
  },
  {
    id: 6,
    fecha: "25/05/2002",
    cliente: "Carlos",
    listaProductos: [
      { nombre: "café", cantidad: 2, precio: 18 },
      { nombre: "azúcar", cantidad: 1, precio: 10 }
    ],
    total: 46
  },
  {
    id: 7,
    fecha: "26/05/2002",
    cliente: "Elena",
    listaProductos: [
      { nombre: "jamón", cantidad: 1, precio: 28 },
      { nombre: "pan", cantidad: 2, precio: 8 }
    ],
    total: 44
  },
  {
    id: 8,
    fecha: "28/05/2002",
    cliente: "Roberto",
    listaProductos: [
      { nombre: "yogurt", cantidad: 3, precio: 12 },
      { nombre: "galletas", cantidad: 2, precio: 12 }
    ],
    total: 60
  },
  {
    id: 9,
    fecha: "30/05/2002",
    cliente: "Sara",
    listaProductos: [
      { nombre: "pollo", cantidad: 1, precio: 90 },
      { nombre: "arroz", cantidad: 2, precio: 30 }
    ],
    total: 150
  },
  {
    id: 10,
    fecha: "02/06/2002",
    cliente: "Miguel",
    listaProductos: [
      { nombre: "queso", cantidad: 1, precio: 50 },
      { nombre: "pan", cantidad: 3, precio: 8 }
    ],
    total: 74
  }
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
