import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router'
import Navbard from './components/Navbard.vue'
import { createPinia} from 'pinia'

const pinia = createPinia();

const app = createApp(App)

app.component('Navbard', Navbard)


app.use(router).use(pinia).mount('#app')