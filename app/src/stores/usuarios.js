import { defineStore } from "pinia";
import API from "../axios";

export const useUsuarioStore = defineStore("usuario", {
  state: () => ({
    cargando: false, // Indica si la petición está en curso
    error: null, // Almacena errores de la API
    mensaje: null, // Mensajes de éxito o información
  }),

  actions: {
    async crearUsuario(nuevoUsuario) {
      // Crea un usuario a través de la API
      this.cargando = true;
      this.error = null;
      this.mensaje = null;

      try {
        const { data } = await API.post("/Acceso/Registrarse", nuevoUsuario);

        // Guarda mensaje de éxito
        this.mensaje = data?.mensaje || "Usuario creado correctamente";

        return data;
      } catch (err) {
        // Guarda mensaje de error si falla la petición
        this.error =
          err.response?.data?.message ||
          err.response?.data ||
          "Ocurrió un error al registrar el usuario";

        console.error("Error al crear usuario:", this.error);

        throw err;
      } finally {
        this.cargando = false; // Finaliza el estado de carga
      }
    },
  },
});
