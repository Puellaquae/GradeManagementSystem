<template>
  <n-space vertical>
    <n-card title="学生生源地人数统计" hoverable>
      <n-space vertical>
        <n-data-table
          :row-key="(c) => c.sourcePlace"
          :columns="columns"
          :data="data"
          :pagination="pagination"
          :loading="inloading"
        />
      </n-space> </n-card
  ></n-space>
</template>

<script>

import { NDataTable, NSpace, NCard, useMessage } from "naive-ui";

async function getItem() {
  let f = await fetch(`https://localhost:5001/api/student:all/source-place:count`);
  let j = await f.json();
  return j;
}

export default {
  components: {
    NDataTable,
    NSpace,
    NCard,
  },
  data() {
    return {
      selectCourse: null,
      inloading: true,
      columns: [
        {
          title: "生源地",
          key: "sourcePlace",
        },
        {
          title: "人数",
          key: "count",
          sorter: "default",
        }
      ],
      data: [],
      pagination: {
        pageSize: 10,
      },
    };
  },
  setup() {
    const msg = useMessage();
    return {
      info(m) {
        msg.info(m);
      },
      warn(m) {
        msg.warning(m);
      },
      error(m) {
        msg.error(m);
      },
    };
  },
  mounted() {
    getItem()
      .then((d) => {
        this.data = d;
        this.inloading = false;
      })
      .catch(() => {
        this.inloading = false;
        this.error("获取数据失败");
      });
  },
};
</script>