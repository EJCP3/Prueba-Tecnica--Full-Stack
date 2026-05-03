<script setup>
import { ref } from "vue";
import { useAuthStore } from "../stores/auth";
import { useRouter } from "vue-router";

const auth = useAuthStore();
const router = useRouter();

const error = ref("");
const loading = ref(false);

// Refs para los campos del formulario (v-model)
const nombre = ref("");
const email = ref("");
const password = ref("");

// Usuarios de acceso rápido
const quickAccessUsers = [
  {
    nombre: "Javier Administrador",
    email: "admin@gmail.com",
    password: "admin123312312312",
    role: "Admin",
    icon: `<svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m5.618-4.016A11.955 11.955 0 0112 2.944a11.955 11.955 0 01-8.618 3.04A12.02 12.02 0 003 9c0 5.591 3.824 10.29 9 11.622 5.176-1.332 9-6.03 9-11.622 0-1.042-.133-2.052-.382-3.016z" />
          </svg>`
  },
  {
    nombre: "Soporte Técnico",
    email: "soporte@gmail.com",
    password: "soporte2026",
    role: "Admin",
    icon: `<svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z" />
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
          </svg>`
  },
  {
    nombre: "Juan Pérez",
    email: "juanperez@gmail.com",
    password: "user123456",
    role: "Usuario",
    icon: `<svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
          </svg>`
  },
  {
    nombre: "María García",
    email: "maria.garci@gmail.com",
    password: "user123456",
    role: "Usuario",
    icon: `<svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5.121 17.804A13.937 13.937 0 0112 16c2.5 0 4.847.655 6.879 1.804M15 10a3 3 0 11-6 0 3 3 0 016 0zm6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>`
  }
];

// ⬇ FormKit manda los valores aquí
const handleLogin = async (data) => {
  loading.value = true;
  error.value = "";

  const ok = await auth.login(data.nombre, data.email, data.password);

  loading.value = false;

  if (!ok) {
    error.value = "Credenciales incorrectas";
    return;
  }

  router.push("/");
};

// Función para login rápido
const loginWithUser = async (user) => {
  nombre.value = user.nombre;
  email.value = user.email;
  password.value = user.password;
  
  // Ejecutar login directamente
  await handleLogin({
    nombre: user.nombre,
    email: user.email,
    password: user.password
  });
};
</script>

<template>
  <div class="bg-login">
    <div class="absolute inset-0 bg-black/30"></div>

    <section
      class="flex flex-col justify-center items-center relative z-10 min-h-screen p-4"
    >
      <fieldset
        class="fieldset bg-base-200/90 backdrop-blur-md border border-base-300 rounded-2xl w-full max-w-md p-6 shadow-2xl transition-all duration-500 hover:shadow-warning/10"
      >
        <legend class="fieldset-legend text-xl font-black text-warning tracking-tight">
          INICIAR SESIÓN
        </legend>

        <FormKit
          type="form"
          :actions="false"
          @submit="handleLogin"
          form-class="flex flex-col gap-4"
          incomplete-message="Por favor completa los campos."
        >
          <!-- Nombre -->
          <FormKit
            type="text"
            name="nombre"
            label="Nombre"
            v-model="nombre"
            validation="required"
            placeholder="Ingresa tu nombre"
            outerClass="flex flex-col gap-1"
            labelClass="font-bold text-xs uppercase tracking-widest text-warning/80"
            inputClass="w-full px-4 py-3 rounded-xl bg-base-100 border border-base-300 focus:border-warning focus:ring-2 focus:ring-warning/20 outline-none transition-all"
          />

          <!-- Email -->
          <FormKit
            type="email"
            name="email"
            label="Email"
            v-model="email"
            validation="required|email"
            placeholder="correo@ejemplo.com"
            outerClass="flex flex-col gap-1"
            labelClass="font-bold text-xs uppercase tracking-widest text-warning/80"
            inputClass="w-full px-4 py-3 rounded-xl bg-base-100 border border-base-300 focus:border-warning focus:ring-2 focus:ring-warning/20 outline-none transition-all"
          />

          <!-- Contraseña -->
          <FormKit
            type="password"
            name="password"
            label="Contraseña"
            v-model="password"
            validation="required"
            placeholder="•••••••••"
            outerClass="flex flex-col gap-1"
            labelClass="font-bold text-xs uppercase tracking-widest text-warning/80"
            inputClass="w-full px-4 py-3 rounded-xl bg-base-100 border border-base-300 focus:border-warning focus:ring-2 focus:ring-warning/20 outline-none transition-all"
          />

          <!-- Botón -->
          <button
            type="submit"
            class="btn btn-warning w-full mt-6 font-bold shadow-lg shadow-warning/20 group"
            :disabled="loading"
          >
            <span v-if="loading" class="loading loading-spinner"></span>
            <span v-else class="flex items-center gap-2">
              Acceder
              <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 group-hover:translate-x-1 transition-transform" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 7l5 5m0 0l-5 5m5-5H6" />
              </svg>
            </span>
          </button>

          <p v-if="error" class="text-error text-center mt-3 text-sm font-medium animate-bounce">{{ error }}</p>
        </FormKit>
      </fieldset>

      <!-- SECCIÓN ACCESO RÁPIDO -->
      <div class="mt-8 w-full max-w-md animate-in fade-in slide-in-from-bottom-4 duration-1000">
        <div class="flex items-center gap-3 mb-6">
          <div class="h-px bg-white/20 flex-1"></div>
          <h3 class="text-white/60 text-[10px] font-black uppercase tracking-[0.2em]">Acceso Rápido</h3>
          <div class="h-px bg-white/20 flex-1"></div>
        </div>
        
        <div class="grid grid-cols-2 sm:grid-cols-4 gap-3">
          <button 
            v-for="user in quickAccessUsers" 
            :key="user.email"
            @click="loginWithUser(user)"
            :disabled="loading"
            class="group flex flex-col items-center p-4 bg-base-200/40 backdrop-blur-md border border-white/5 rounded-2xl hover:bg-warning hover:border-warning transition-all duration-500 shadow-xl"
          >
            <div 
              class="text-2xl mb-2 bg-white/10 p-2 rounded-xl group-hover:bg-black/10 group-hover:scale-110 transition-all text-warning group-hover:text-black"
              v-html="user.icon"
            >
            </div>
            <div class="flex flex-col items-center">
              <span class="text-[10px] font-black text-warning group-hover:text-black text-center leading-tight truncate w-full">
                {{ user.nombre.split(' ')[0] }}
              </span>
              <span class="text-[8px] text-white/40 font-bold group-hover:text-black/60 uppercase tracking-tighter">
                {{ user.role }}
              </span>
            </div>
          </button>
        </div>
      </div>

      <figure
        data-tip="Fuera de servicio!!!"
        class="tooltip mt-12 flex items-center justify-end space-x-2 flex-row-reverse text-white/60 hover:text-warning transition duration-300 cursor-help"
      >
        <img class="w-12 p-2 opacity-80" src="/Group.png" />
        <figcaption class="text-xs font-medium">¿Necesita asistencia?</figcaption>
      </figure> 
    </section>
  </div>
</template>
