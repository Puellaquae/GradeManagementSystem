<template>
  <n-space vertical>
    <n-card title="学生成绩名次排定" hoverable>
      <n-space vertical>
        <class-select :multi="true" v-on:change="selectClass = $event" />
        <n-data-table
          :row-key="(c) => c.id"
          :columns="columns"
          :data="showData"
          :pagination="pagination"
          :loading="inloading"
        />
      </n-space> </n-card
  ></n-space>
</template>

<script>
import { h } from "vue";
import ClassSelect from "../components/ClassSelect.vue";
import { NDataTable, NSpace, NCard, NText, useMessage } from "naive-ui";

async function getAverageScore() {
  let f = await fetch(`https://localhost:5001/api/student:all/grade:rank`);
  let j = await f.json();
  return j;
}

export default {
  components: {
    NDataTable,
    NSpace,
    NCard,
    ClassSelect,
  },
  data() {
    return {
      selectClass: null,
      inloading: true,
      columns: [
        {
          title: "序号",
          key: "rank",
          render(row, index) {
            return h(NText, null, {
              default: () => `${index + 1}`,
            });
          },
        },
        {
          title: "学号",
          key: "studentId",
        },
        {
          title: "姓名",
          key: "name",
        },
        {
          title: "专业",
          key: "major",
        },
        {
          title: "班级",
          key: "class",
        },
        {
          title: "课程平均分数",
          key: "grade",
        },
      ],
      data: [],
      pagination: {
        pageSize: 10,
      },
    };
  },
  computed: {
    showData() {
      if (this.selectClass == null || this.selectClass.length == 0) {
        return this.data;
      }
      return this.data.filter((d) => ~this.selectClass.indexOf(d.classId));
    },
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
  async mounted() {
    this.data = await getAverageScore();
    this.inloading = false;
  },
};
</script>