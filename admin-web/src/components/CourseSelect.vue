<template>
  <n-select v-model:value="value" :options="options" :loading="inloading" placeholder="请选择课程"/>
</template>

<script>
import { NSelect } from "naive-ui";

async function getCourses() {
  let res = await fetch("https://localhost:5001/api/course:all");
  let j = await res.json();
  let opt = [];
  j.forEach((t) => {
    opt.push({
      label: t.name,
      value: t.id,
    });
  });
  return opt;
}

export default {
  components: {
    NSelect,
  },
  data() {
    return {
      inloading: true,
      value: null,
      options: [],
    };
  },
  async mounted() {
    this.options = await getCourses();
    this.inloading = false;
  },
  props: ["on-change"],
  watch: {
    value(newVal) {
      this.$emit("change", newVal);
    },
  },
};
</script>