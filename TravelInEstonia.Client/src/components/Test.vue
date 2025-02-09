<script lang="ts">
import {defineComponent, PropType} from 'vue'
import {api} from "../../apiClients.generated";

export default defineComponent({
  name: "FarePicker",
  emits: ["values-ready"],
  props: {
    locations: {
      type: Array as PropType<api.LocationModel[]>,
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
              from: (newFrom as api.LocationModel).name,
              to: (newTo as api.LocationModel).name,
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
  <div class="relative w-full sm:w-[320px]">
    <Select
        v-model="from"
        :options="locations"
        optionLabel="name"
        class="w-full bg-gradient-to-r from-indigo-600 to-indigo-700 text-white font-semibold rounded-lg px-6 py-4 shadow-lg hover:from-indigo-500 hover:to-indigo-600 focus:outline-none focus:ring-4 focus:ring-indigo-400 transition-all duration-300 ease-in-out"
    >
      <!-- Custom Label Slot -->
      <template #value="slotProps">
        <span v-if="!slotProps.value" class="text-indigo-200">Where from?</span>
        <span v-else class="font-bold text-white">{{ slotProps.value.name }}</span>
      </template>
    </Select>
  </div>
</template>

<style scoped>

</style>