<template>
  <Menubar :model="items">
    <!-- Custom slot content if needed -->
  </Menubar>
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
      items: [
        {
          label: 'Home',
          icon: 'pi pi-home',
          command: () => {
            this.$router.push('/');
          }
        },
        {
          label: 'Reservations',
          icon: 'pi pi-list',
          command: () => {
            this.$router.push('/reservation/list');
          }
        },
      ]
    };
  }
});
</script>

<style scoped>
</style>
