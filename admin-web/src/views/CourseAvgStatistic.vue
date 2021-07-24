<template>
  <n-space vertical>
    <n-card title="课程平均成绩统计" hoverable>
      <n-space vertical>
        <n-data-table
          :row-key="(c) => c.id"
          :columns="columns"
          :data="data"
          :pagination="pagination"
          :loading="inloading"
        />
      </n-space> </n-card
  ></n-space>
</template>

<script>
import { h } from "vue";

import { NDataTable, NSpace, NCard, NText, useMessage } from "naive-ui";

async function getAverageScore() {
  let f = await fetch(`https://localhost:5001/api/course:all:avg`);
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
          title: "课程编号",
          key: "id",
          sorter: "default",
        },
        {
          title: "课程名",
          key: "name",
        },
        {
          title: "任课教师",
          key: "teacher",
          render(row) {
            const tags = row.teacher.map((tagKey) => {
              return h(
                NText,
                {
                  style: {
                    marginRight: "6px",
                  },
                },
                {
                  default: () => tagKey,
                }
              );
            });
            return tags;
          },
        },
        {
          title: "开设学期",
          key: "semester",
          sorter: "default",
        },
        {
          title: "学时",
          key: "hours",
          sorter: "default",
        },
        {
          title: "学分",
          key: "credit",
          sorter: "default",
        },
        {
          title: "考试或考查",
          key: "examMethod",
        },
        {
          title: "平均成绩",
          key: "averageScore",
          sorter: "default",
        },
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
    getAverageScore()
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