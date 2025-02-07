<script lang="ts">
import {defineComponent} from 'vue'
import {api} from "../../apiClients.generated";

export default defineComponent({
  name: "Test",
  data() {
    return {
      locations: [] as api.LocationModel[],
    }
  },
  mounted() {
    new api.ScheduleClient().destinations().then(x => {
      this.locations = x;
    });
  }

})
</script>

<template>
{{locations}}
</template>

<style scoped>

</style>