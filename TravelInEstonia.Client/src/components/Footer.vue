<script lang="ts">
import {defineComponent} from 'vue'

export default defineComponent({
  name: "Footer"
})
</script>

<template>
  <footer class="font-sans tracking-wide bg-indigo-800 pt-12 pb-4 px-6 sm:px-10">
    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8 max-w-screen-xl mx-auto">
      <!-- Quick Links -->
      <div>
        <h4 class="text-white font-semibold text-lg mb-6">Lorem Ipsum</h4>
        <ul class="space-y-5">
          <li><a href="javascript:void(0)" class="hover:text-white text-gray-300 text-[15px] transition-all">Lorem ipsum dolor sit amet</a></li>
          <li><a href="javascript:void(0)" class="hover:text-white text-gray-300 text-[15px] transition-all">Praesent commodo</a></li>
          <li><a href="javascript:void(0)" class="hover:text-white text-gray-300 text-[15px] transition-all">Maecenas vel mollis tellus</a></li>
          <li><a href="javascript:void(0)" class="hover:text-white text-gray-300 text-[15px] transition-all">Curabitur faucibus</a></li>
        </ul>
      </div>

      <!-- Services -->
      <div>
        <h4 class="text-white font-semibold text-lg mb-6">Lorem Ipsum</h4>
        <ul class="space-y-5">
          <li><a href="javascript:void(0)" class="hover:text-white text-gray-300 text-[15px] transition-all">Nullam id </a></li>
          <li><a href="javascript:void(0)" class="hover:text-white text-gray-300 text-[15px] transition-all">Praesent commodo</a></li>
          <li><a href="javascript:void(0)" class="hover:text-white text-gray-300 text-[15px] transition-all">Lorem ipsum dolor sit amet</a></li>
          <li><a href="javascript:void(0)" class="hover:text-white text-gray-300 text-[15px] transition-all">Curabitur faucibus</a></li>
        </ul>
      </div>
    </div>

    <!-- Footer Bottom -->
    <div class="text-center border-gray-600 pt-4 mt-8">
      <p class="text-gray-400 text-[15px]">
       Travel In Estonia
      </p>
    </div>
  </footer>

</template>

<style scoped>

</style>