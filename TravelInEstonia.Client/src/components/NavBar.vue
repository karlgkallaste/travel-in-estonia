<template>
  <header class="fixed top-0 left-0 w-full shadow-lg py-4 px-4 sm:px-10 bg-white font-[sans-serif] min-h-[70px] tracking-wide z-[1000]">
  <div class="flex flex-wrap items-center justify-between gap-4 w-full">

      <!-- Mobile Menu Overlay -->
      <div v-if="menuOpen" class="fixed inset-0 bg-black bg-opacity-50 z-50" @click="toggleMenu"></div>

      <!-- Mobile Menu -->
      <div :class="menuOpen ? 'block' : 'hidden'" class="lg:hidden fixed top-0 left-0 w-64 h-full bg-white shadow-md p-6 z-50 ">
        <button class="absolute top-2 right-4 w-9 h-9 flex items-center justify-center border rounded-full bg-white" @click="toggleMenu">
          <span class="pi pi-bars text-2xl text-gray-800"></span>
        </button>
        <ul>
          <li v-for="item in items" :key="item.label" class="border-b py-3 px-3">
            <p @click="item.command" class="text-[#333] block font-semibold text-[15px] cursor-pointer">{{ item.label }}</p>
          </li>
        </ul>
      </div>

      <!-- Desktop Menu -->
      <nav class="hidden lg:flex items-center space-x-6">
        <ul class="flex space-x-6">
          <li v-for="item in items" :key="item.label">
            <p @click="item.command" class="text-[#333] font-semibold text-[15px] cursor-pointer">{{ item.label }}</p>
          </li>
        </ul>
      </nav>

      <!-- Mobile Menu Toggle Button -->
      <button class="lg:hidden" @click="toggleMenu">
        <span class="pi pi-bars text-2xl text-gray-800"></span>
      </button>

    </div>
  </header>
</template>

<script>
import {defineComponent} from 'vue';
import Menubar from 'primevue/menubar';
import {RouterLink} from "vue-router";

export default defineComponent({
  name: 'Navbar',
  components: {
    Menubar,
    RouterLink
  },
  data() {
    return {
      menuOpen: false,
      items: [
        {
          label: 'Home',
          command: () => {
            this.$router.push('/');
          }
        },
        {
          label: 'Reservations',
          command: () => {
            this.$router.push('/reservation/list');
          }
        },
      ]
    };
  },
  methods: {
    toggleMenu() {
      this.menuOpen = !this.menuOpen;
    }
  }
});
</script>

<style scoped>

</style>
