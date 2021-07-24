<template>
  <n-grid x-gap="12" :cols="2">
    <n-gi>
      <class-select v-on:change="changeClass" />
    </n-gi>
    <n-gi>
      <n-select
        v-model:value="value"
        :options="options"
        :loading="inloading"
        placeholder="请选择学生"
      />
    </n-gi>
  </n-grid>
</template>

<script>
import { NSelect, NGi, NGrid } from "naive-ui";
import ClassSelect from "../components/ClassSelect.vue";

async function getStudent(clid) {
  let f = await fetch(`https://localhost:5001/api/class:${clid}/student:all`);
  let j = await f.json();
  let opt = [];
  j.forEach((s) => {
    opt.push({
      label: s.name,
      value: s.studentId,
    });
  });
  return opt;
}

export default {
  components: {
    NSelect,
    ClassSelect,
    NGi,
    NGrid,
  },
  data() {
    return {
      inloading: false,
      value: null,
      options: [],
    };
  },
  props: ["on-change"],
  watch: {
    value(newVal) {
      this.$emit("change", newVal);
    },
  },
  methods: {
    changeClass(cl) {
      this.value = null;
      this.inloading = true;
      getStudent(cl)
        .then((o) => {
          this.options = o;
          this.inloading = false;
        })
        .catch(() => {
          this.inloading = false;
        });
    },
  },
};
</script>