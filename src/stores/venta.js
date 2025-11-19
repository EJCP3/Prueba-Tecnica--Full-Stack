import { defineStore } from "pinia";
import { reactive } from "vue";
import { useCrud } from "../composable/useCrud";


export const useVentaStore = defineStore("venta", () => {

    const ventas = reactive([])

    const {addItem} = useCrud({ventas})


    return {ventas, addVenta: addItem}


})