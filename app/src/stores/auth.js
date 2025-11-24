import { defineStore } from "pinia";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { useRouter } from "vue-router";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    token: localStorage.getItem("token") || null, // Token JWT almacenado localmente
    user: JSON.parse(localStorage.getItem("user")) || null, // Datos del usuario logueado
  }),

  actions: {
    async login(nombreUsuario, emailUsuario, passwordUsuario) {
      // Realiza login y guarda token y datos del usuario
      try {
        const respuesta = await axios.post(
          "https://localhost:7249/api/Acceso/Login",
          {
            nombre: nombreUsuario,
            email: emailUsuario,
            password: passwordUsuario,
          }
        );

        // Guarda el token recibido
        this.token = respuesta.data.token;
        localStorage.setItem("token", this.token);

        // Decodifica el token para obtener datos del usuario
        const decodedToken = jwtDecode(this.token);
        this.user = {
          id: decodedToken[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
          ],
          email: decodedToken[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
          ],
          nombre: decodedToken[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
          ],
          rol: decodedToken[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ],
        };
        localStorage.setItem("user", JSON.stringify(this.user));

        return true;
      } catch (error) {
        console.error("Error login:", error);
        return false;
      }
    },

    logout() {
      // Cierra sesión y limpia token y datos del usuario
      this.token = null;
      this.user = null;
      localStorage.removeItem("token");
      localStorage.removeItem("user");
    },
  },
});
