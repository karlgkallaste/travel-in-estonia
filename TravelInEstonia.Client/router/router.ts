import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Home from "../src/views/Home.vue";

const routes: RouteRecordRaw[] = [
    {
        path: "/",
        name: "Home",
        component: Home,
    },
    {
        path: "/:pathMatch(.*)*",
        redirect: { name: "Home" },
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
