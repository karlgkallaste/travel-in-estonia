<script lang="ts">
import {defineComponent} from 'vue'

export default defineComponent({
  name: "FaqSection"
})
</script>

<template>
  <div class="p-4 font-sans bg-white">
    <div class="max-w-6xl mx-auto bg-white rounded-lg shadow-blue-100 shadow-md">
      <div class="p-6 border-b">
        <h2 class="text-3xl font-extrabold text-indigo-800">Frequently Asked Questions</h2>
        <p class="text-gray-600 mt-4 text-sm">Explore our comprehensive FAQ to find answers to common queries.</p>
      </div>
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 p-6">
        <div class="bg-indigo-50 p-6 rounded-lg border border-indigo-200">
          <h3 class="text-lg font-semibold text-indigo-700 mb-2">Lorem Ipsum?</h3>
          <p class="text-gray-600 text-sm">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas vel mollis tellus. Praesent lacus lacus, facilisis ut lacinia sit amet, venenatis vitae odio.</p>
        </div>
        <div class="bg-indigo-50 p-6 rounded-lg border border-indigo-200">
          <h3 class="text-lg font-semibold text-indigo-700 mb-2">Lorem Ipsum?</h3>
          <p class="text-gray-600 text-sm">Praesent commodo hendrerit est, commodo posuere sapien porta quis. Curabitur faucibus in nisl at accumsan. Sed rutrum sit amet elit vitae ultrices.</p>
        </div>
        <div class="bg-indigo-50 p-6 rounded-lg border border-indigo-200">
          <h3 class="text-lg font-semibold text-indigo-700 mb-2">Lorem Ipsum?</h3>
          <p class="text-gray-600 text-sm">In bibendum viverra dolor vitae lobortis. Nam sollicitudin porttitor molestie. Aliquam a justo ac mi interdum lobortis.</p>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>