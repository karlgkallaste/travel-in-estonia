<script setup lang="ts">
import Navbar from "./components/NavBar.vue";
import Footer from "./components/Footer.vue";
</script>

<template>
  <div class="bg-gray-100" id="app">
    <Navbar/>
      <Toast/>
      <router-view/>
    <Footer/>
  </div>
</template>

<style scoped>
#app {
  display: flex;
  flex-direction: column;
  min-height: 100vh; /* Full height of the screen */
}

body {
  margin: 0; /* Remove any default margin */
  font-family: Avenir, Helvetica, Arial, sans-serif;
}
</style>
