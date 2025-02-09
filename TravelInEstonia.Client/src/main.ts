import { createApp } from 'vue'
import App from './App.vue'
import PrimeVue from 'primevue/config';
import Aura from '@primevue/themes/aura';
import router from "../router/router.ts";
import Select from "primevue/select";
import './tailwind.css';
import 'primeicons/primeicons.css';
import DatePicker  from "primevue/datepicker";
import Button from "primevue/button";
import Tag from "primevue/tag";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
import FloatLabel from "primevue/floatlabel";

createApp(App).use(router).use(PrimeVue, {
    theme: {
        preset: Aura,
        options: {
            cssLayer: {
                name: 'primevue',
                order: 'tailwind-base, primevue, tailwind-utilities'
            }
        }
    }
})
    .component('Calendar', DatePicker)
    .component('Select', Select)
    .component('Button', Button)
    .component('Tag', Tag)
    .component('Dialog', Dialog)
    .component('InputText', InputText)
    .component('FloatLabel', FloatLabel)
    .mount('#app')
