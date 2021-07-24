<template>
  <n-cascader
    v-model:value="value"
    placeholder="请筛选班级"
    :options="options"
    :leaf-only="true"
    :show-path="true"
    multiple
    clearable
    remote
    :on-load="handleLoad"
    :loading="inloading"
  />
</template>

<script>
import { NCascader } from "naive-ui";

async function getClass(option) {
  const children = [];
  let res = await fetch(
    `https://localhost:5001/api/major:${option.value}/class:all`
  );
  let j = await res.json();
  j.forEach((x) => {
    children.push({
      label: x.name,
      value: x.id,
      depth: option.depth,
      isLeaf: true,
    });
  });
  return children;
}

async function getMajor() {
  const major = [];
  let res = await fetch("https://localhost:5001/api/major:all");
  let j = await res.json();
  j.forEach((x) => {
    major.push({
      label: x.name,
      value: x.id,
      depth: 1,
      isLeaf: false,
    });
  });
  return major;
}

export default {
  components: {
    NCascader,
  },
  data() {
    return {
      options: [],
      value: null,
      inloading:true
    };
  },
  async mounted() {
    this.options = await getMajor();
    this.inloading = false
  },
  props: ["on-change"],
  watch:{
    value(newVal) {
      this.$emit('change', newVal)
    }
  },
  methods: {
    handleLoad(option) {
      return new Promise((resolve) => {
        getClass(option).then((c) => {
          option.children = c;
          resolve();
        });
      });
    }
  },
};
</script>