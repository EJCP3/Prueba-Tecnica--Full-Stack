import { createWebHistory } from "vue-router";
import { createRouter } from "vue-router";
import Home from "../pages/Home.vue";
import Clients from "../pages/Clients.vue";
import Products from "../pages/Products.vue";
import Ventas from "../pages/Ventas.vue";
import Historial from "../pages/Historial.vue";

const router = createRouter({
    history: createWebHistory(),
    routes:[
        {
        path: "/",
        name: "home",
        component: Home
    },
  {
        path: "/clientes",
        name: "clientes",
        component: Clients
    },
  {
        path: "/productos",
        name: "productos",
        component: Products
    },
  {
        path: "/ventas",
        name: "ventas",
        component : Ventas
    },
  {
        path: "/historial",
        name: "historial",
        component: Historial
    }]
})

export default router;