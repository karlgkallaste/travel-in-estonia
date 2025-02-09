<script lang="ts">
import {defineComponent} from 'vue'
import FarePicker from "../components/FarePicker.vue";
// @ts-ignore
import {api} from "../../apiClients.generated.ts";
import FareList from "../components/FareList.vue";
import FaqSection from "../components/FaqSection.vue";

export default defineComponent({
  name: "Home",
  components: {FaqSection, FareList, FarePicker},
  data() {
    return {
      options: [] as api.LocationModel[],
      fares: [] as api.FareModel[],
      filters: new SearchFiltersModel(),
    }
  },
  created() {
    // Watch the 'filters' object for changes, and update query parameters in the URL
    this.$watch(
        'filters',
        (newValue) => {
          setTimeout(() => {
            this.filters = newValue;
            this.fetchFares();
            this.updateQueryParams();
          }, 500);
        },
        {deep: true}
    );
  },
  mounted() {
    new api.ScheduleClient().locations().then(locations => {
      this.options = locations;
    });

    this.readQueryParams();
  },
  methods: {
    fetchFares() {
      new api.ScheduleClient().fares(this.filters.from, this.filters.to, this.filters.date).then(fares => {
        this.fares = fares;
      })
    },

    updateQueryParams() {
      this.$router.push({
        query: {
          from: this.filters.from,
          to: this.filters.to,
          departDate: this.filters.date ? this.filters.date.toISOString() : '',
        },
      });
    },

    // Method to read the query parameters from the URL and update filters
    readQueryParams() {
      const query = this.$route.query;
      if (query.from) {
        this.filters.from = query.from as string;
      }
      if (query.to) {
        this.filters.to = query.to as string;
      }
      if (query.departDate) {
        this.filters.date = new Date(query.departDate as string);
      }

      // Fetch fares based on the query parameters
      this.fetchFares();
    },
    handleValuesReady(payload: { from: string, to: string, departDate: Date }) {
      this.filters.from = payload.from;
      this.filters.to = payload.to;
      this.filters.date = payload.departDate;
    },
    createReservation(firstName: string, lastName: string, fare: api.FareModel) {
      const request = new api.CreateReservationModel();
      request.firstName = firstName;
      request.lastName = lastName;
      request.fare = fare;
      new api.ReservationClient().create(request).then(response => {
        console.log("Success")
      })
    }
  }
})

class SearchFiltersModel {
  from: string = "";
  to: string = "";
  date: Date | undefined = undefined;
}
</script>

<template>

  <div
      class="relative font-sans before:absolute before:w-full before:h-full before:inset-0 before:bg-black before:opacity-50 before:z-10 mt-10">
    <img src="../../public/bus-interior.jpg" alt="Banner Image"
         class="absolute inset-0 w-full h-full object-cover blur-sm"/>

    <div
        class="min-h-[350px] relative z-50 h-full max-w-6xl mx-auto flex flex-col justify-center items-center text-center text-white p-6">
      <h2 class="sm:text-4xl text-2xl font-bold mb-6">Travel In Estonia</h2>
      <p class="sm:text-lg text-base text-center text-gray-200">Visit Estonia. It's about time.</p>

      <fare-picker
          :locations="options"
          @values-ready="handleValuesReady"
          class="w-full max-w-3xl rounded-lg"
          style="overflow: hidden;"
      />
    </div>
  </div>

  <faq-section></faq-section>

  <fare-list @reservation-finalized="(firstName, lastName, fare) => createReservation(firstName, lastName, fare)"
             :fares="fares" :from="filters.from" :to="filters.to"></fare-list>

</template>

<style scoped>

</style>