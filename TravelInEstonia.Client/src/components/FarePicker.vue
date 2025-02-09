<script lang="ts">
import {defineComponent, type PropType} from 'vue'
// @ts-ignore
import {api} from "../../apiClients.generated.ts";
import LocationModel = api.LocationModel;

export default defineComponent({
  name: "FarePicker",
  emits: ["values-ready"],
  props: {
    locations: {
      type: Array as PropType<LocationModel[]>,
      required: false,
    }
  },
  data() {
    return {
      from: null,
      to: null,
      departDate: null,
    }
  },
  mounted() {
    this.$watch(
        () => [this.from, this.to, this.departDate],
        ([newFrom, newTo, newDepartDate]) => {
          if (newFrom && newTo && newDepartDate) {
            this.$emit('values-ready', {
              from: (newFrom as LocationModel).name,
              to: (newTo as LocationModel).name,
              departDate: (newDepartDate as Date)
            });
          }
        },
        {immediate: false}
    );
  }
})
</script>

<template>
  <div class="flex flex-col sm:flex-row items-center justify-center mt-6 space-y-4 sm:space-y-0 sm:space-x-4">
    <!-- "From" Select Component -->
    <div class="relative w-full sm:w-[280px]">
      <Select
          v-model="from"
          :options="locations"
          optionLabel="name"
          class="px-5 py-2.5 rounded-lg text-sm tracking-wider font-medium border border-current outline-none bg-blue-700 hover:bg-transparent text-white hover:text-blue-700 transition-all duration-300"
      >
        <!-- Custom Label Slot -->
        <template #value="slotProps">
          <span v-if="!slotProps.value" class="text-indigo-200">Where from?</span>
          <span v-else class="font-bold text-white">{{ slotProps.value.name }}</span>
        </template>
      </Select>
    </div>

    <!-- "To" Select Component -->
    <div class="relative w-full sm:w-[280px]">
      <Select
          v-model="to"
          :options="locations"
          optionLabel="name"
          class="px-5 py-2.5 rounded-lg text-sm tracking-wider font-medium border border-current outline-none bg-blue-700 hover:bg-transparent text-white hover:text-blue-700 transition-all duration-300"
      >
        <!-- Custom Label Slot -->
        <template #value="slotProps">
          <span v-if="!slotProps.value" class="text-indigo-200">Where to?</span>
          <span v-else class="font-bold text-white">{{ slotProps.value.name }}</span>
        </template>
      </Select>
    </div>
  </div>

  <!-- Calendar Component -->
  <div class="relative w-full sm:w-[280px] mt-6">
    <Calendar
        input-class="px-5 py-2.5 rounded-lg text-sm tracking-wider font-medium border border-current outline-none bg-blue-700 hover:bg-transparent text-white hover:text-blue-700 transition-all duration-300"
        v-model="departDate"
        placeholder="Select Date and Time"
    />
  </div>

</template>

<style scoped>

</style>