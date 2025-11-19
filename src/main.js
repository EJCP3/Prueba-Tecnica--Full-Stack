import { createApp } from "vue";
import "./style.css";
import App from "./App.vue";
import router from "./router";
import Navbard from "./components/Navbard.vue";
import { plugin, defaultConfig } from "@formkit/vue";
import { createPinia } from "pinia";

const pinia = createPinia();

const app = createApp(App);

app.component("Navbard", Navbard);

app.use(router).use(pinia)
app.use(plugin, defaultConfig({
  config: {
    classes: {
      message: "text-red-500 text-sm font-medium"
    }
  }
}))
app.mount("#app");
