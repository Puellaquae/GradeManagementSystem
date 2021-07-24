<template>
  <n-select clearable v-model:value="tvalue" :multiple="multi" :options="options" :loading="inloading" placeholder="请选择教师"/>
</template>

<script>
import { NSelect } from "naive-ui";

async function getTeachers() {
  let res = await fetch("https://localhost:5001/api/teacher:all");
  let j = await res.json();
  let opt = [];
  j.forEach((t) => {
    opt.push({
      label: t.name,
      value: t.teacherId,
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
      tvalue: this.value,
      options: [],
    };
  },
  async mounted() {
    this.options = await getTeachers();
    this.inloading = false;
  },
  props: ["on-change", "value", "multi"],
  watch: {
    tvalue(newVal) {
      this.$emit("change", newVal);
    },
  },
};
</script>