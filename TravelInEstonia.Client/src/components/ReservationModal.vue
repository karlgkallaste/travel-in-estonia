<script lang="ts">
import {defineComponent} from 'vue'

export default defineComponent({
  name: "ReservationModal",
  emits: ["name-added"],
  data() {
    return {
      visible: false,
      firstName: "",
      lastName: "",
      fareIndex: null as number | null,
      error: "",
    }
  },
  methods: {
    open(fareIndex: number) {
      this.visible = true;
      this.fareIndex = fareIndex;
    },
    reserve() {
      this.error = "";
      if (this.firstName == "" || this.lastName == "") {
        this.error = "Some properties are not filled.";
      }
      this.$emit("name-added", this.firstName, this.lastName, this.fareIndex);
      this.visible = false;
      this.firstName = "";
      this.lastName = "";
      this.fareIndex = null;
    }
  }
})
</script>

<template>
  <Dialog v-model:visible="visible" modal header="Reservation" :style="{ width: '50rem' }"
          :breakpoints="{ '1199px': '75vw', '575px': '90vw' }">
    <div class="p-6 sm:p-8 lg:p-10 space-y-8 max-w-full sm:max-w-lg lg:max-w-xl mx-auto">
      <h1>{{ error }}</h1>
      <!-- Grid for Side-by-Side Inputs -->
      <div class="grid grid-cols-1 sm:grid-cols-2 gap-6">
        <!-- Floating Label for First Name -->
        <FloatLabel>
          <InputText id="firstname" v-model="firstName" class="w-full"/>
          <label for="firstname">First name</label>
        </FloatLabel>

        <!-- Floating Label for Last Name -->
        <FloatLabel>
          <InputText id="lastname" v-model="lastName" class="w-full"/>
          <label for="lastname">Last name</label>
        </FloatLabel>
      </div>

      <!-- Reserve Button -->
      <Button
          @click="reserve"
          label="Reserve"
          class="w-full bg-indigo-600 text-white py-3 rounded-md hover:bg-indigo-700 transition-all duration-200"
      />
    </div>
  </Dialog>
</template>

<style scoped>

</style>