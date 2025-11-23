import { defineStore } from "pinia";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { useRouter } from "vue-router";



export const useAuthStore = defineStore("auth", {
  state: () => ({
    token: localStorage.getItem("token") || null,
    user: JSON.parse(localStorage.getItem("user")) || null,
  }),

  actions: {
    async login(nombre, email, password) {
      try {
        const resp = await axios.post(
          "https://localhost:7249/api/Acceso/Login",
          {
            nombre,
            email,
            password,
          }
        );

        // El token enviado por tu API
        this.token = resp.data.token;
        localStorage.setItem("token", this.token);
        const decoded = jwtDecode(this.token);
        this.user = {
          id: decoded[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
          ],
          email:
            decoded[
              "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
            ],
          nombre:
            decoded[
              "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
            ],
          rol: decoded["role"] || "user", // si existe rol en token
        };
        localStorage.setItem("user", JSON.stringify(this.user));

        return true;
      } catch (error) {
        console.error("Error login:", error);
        return false;
      }
    },

     logout() {
      this.token = null;
  this.user = null;
  localStorage.removeItem("token");
  localStorage.removeItem("user");
    },
  },
});
