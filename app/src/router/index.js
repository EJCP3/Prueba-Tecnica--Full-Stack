import { createRouter, createWebHistory } from "vue-router";
import { useAuthStore } from "../stores/auth";

import MainLayout from "../layouts/MainLayout.vue";
import Home from "../pages/Home.vue";
import Productos from "../pages/Products.vue";
import Login from "../pages/Login.vue";
import Cliente from "../pages/Clients.vue"
import Historial from "../pages/Historial.vue"
import Ventas from "../pages/Ventas.vue"
import NewUsuario from "../pages/NewUsuario.vue";


const routes = [
  {
    path: "/login",
    component: Login,
  },
  {
    path: "/",
    component: MainLayout,
    meta: { requiresAuth: true },
    children: [
      { path: "", component: Home },
      { path: "productos", component: Productos },
      { path: "cliente", component: Cliente },
      { path: "historial", component: Historial },
      { path: "ventas", component: Ventas },
       { path: "usuario", component: NewUsuario,  meta: { requiresAdmin: true }  },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Middleware: protege rutas
router.beforeEach((to) => {
  const auth = useAuthStore();

  if (to.meta.requiresAuth && !auth.token) {
    return "/login";
  }

   if (to.meta.requiresAdmin && auth.user.rol !== "admin") {
    return "/";  // o "/productos"
  }
});

export default router;
