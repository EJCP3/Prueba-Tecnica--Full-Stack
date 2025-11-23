import { createRouter, createWebHistory } from "vue-router";
import { useAuthStore } from "../stores/auth";

import MainLayout from "../layouts/MainLayout.vue";
import Home from "../pages/Home.vue";
import Productos from "../pages/Products.vue";
import Login from "../pages/Login.vue";

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
});

export default router;
