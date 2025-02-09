<script lang="ts">
import {defineComponent} from 'vue'
import {api} from "../../apiClients.generated.ts";
import ReservationModal from "./ReservationModal.vue";

export default defineComponent({
  name: "FareList",
  emits: ["reservation-finalized"],
  components: {ReservationModal},
  props: {
    fares: {
      type: Array as () => api.FareModel[],
      required: true
    },
    from: {
      type: String,
      required: true
    },
    to: {
      type: String,
      required: true
    }
  },
  methods:{
    openReservationModal(fareIndex: number){
        return (this.$refs.modal as any).open(fareIndex);
    },
    finalizeReservation(firstName: string, lastName: string, fareIndex: number){
      this.$emit("reservation-finalized", firstName, lastName, this.fares[fareIndex]);
    }
  }
})
</script>

<template>
  <div class="p-4 mt-1 font-sans bg-white">
    <!-- Container for Fare List -->
    <div class="max-w-6xl mx-auto bg-white rounded-lg ">
      <div v-if="fares.length">
        <div class="p-6">
          <h2 class="text-3xl font-extrabold text-indigo-800">
            {{ from }}
            <span class="pi pi-arrow-right text-indigo-800"></span>
            {{ to }}
          </h2>
          <p class="text-gray-600 mt-4 text-sm">Search result</p>
        </div>
        <div
            v-for="(fare, index) in fares"
            :key="index"
            class="w-full max-w-full mb-4 mx-auto bg-white rounded-lg shadow-lg p-6"
        >
          <div class="flex justify-between items-center">
            <div class="text-xl font-semibold text-indigo-600">{{ fare.company?.name }}</div>
            <Tag icon="pi pi-euro" class="text-emerald-500" severity="success" value="Info">{{ fare.totalPrice }}</Tag>
          </div>
          <div class="text-sm text-gray-500 mt-2">
            <p v-if="fare.company">{{ fare.totalDistance }} km</p>
            <p v-if="fare.routes?.length">Transfers: {{ fare.routes.length - 1 }}</p>
          </div>

          <!-- Displaying route details -->
          <div class="flex justify-between items-center">
            <div v-if="fare.routes?.length" class="mt-4">
              <h4 class="font-semibold text-indigo-600">Route Details:</h4>
              <div v-for="(route, index) in fare.routes" :key="index" class="text-sm text-gray-500">
                <p v-if="route.from">From: {{ route.from?.name }}</p>
                <p v-if="route.to">To: {{ route.to?.name }}</p>
                <p v-if="route.departAt">Departure: {{ new Date(route.departAt).toLocaleString() }}</p>
                <p v-if="route.arriveBy">Arrival: {{ new Date(route.arriveBy).toLocaleString() }}</p>
                <p v-if="route.distance">Distance: {{ route.distance }} km</p>
                <p v-if="route.price">Price: {{ route.price }}€</p>
              </div>
            </div>
            <button
                @click="openReservationModal(index)"
                class="bg-indigo-600 text-white py-2 px-4 rounded-lg hover:bg-indigo-700 transition-all duration-300">
              Reserve
            </button>
          </div>
        </div>
      </div>
      <div v-else>
        <p>No fares available.</p>
      </div>
    </div>
  </div>
  <reservation-modal @name-added="(firstName, lastName, fareIndex) => finalizeReservation(firstName, lastName, fareIndex)" ref="modal"></reservation-modal>
</template>


<style scoped>

</style>