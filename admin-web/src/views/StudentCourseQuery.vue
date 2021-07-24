<template>
  <n-space vertical>
    <n-card title="学生成绩查询" hoverable>
      <n-space vertical>
        <n-grid x-gap="12" :cols="4">
          <n-gi :span="2">
            <student-select v-on:change="selectStudent = $event" />
          </n-gi>
          <n-gi>
            <n-button @click="query">查询</n-button>
          </n-gi>
          <n-gi>
            <n-statistic label="已修学分" :value="credit"></n-statistic>
          </n-gi>
        </n-grid>
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
import {
  NDataTable,
  NText,
  NSpace,
  NButton,
  NGrid,
  NGi,
  NCard,
  NStatistic,
  useMessage,
} from "naive-ui";
import StudentSelect from "../components/StudentSelect.vue";

async function getStudentCourse(sid) {
  let res = await fetch(`https://localhost:5001/api/student:${sid}/course:all`);
  let j = await res.json();
  return j;
}

async function getStudentCredit(sid) {
  let res = await fetch(`https://localhost:5001/api/student:${sid}/credit`);
  let j = await res.text();
  return j;
}

export default {
  components: {
    NDataTable,
    StudentSelect,
    NSpace,
    NButton,
    NGrid,
    NGi,
    NCard,
    NStatistic,
  },
  data() {
    return {
      selectStudent: null,
      inloading: false,
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
          filterOptions: [
            {
              label: "2020",
              value: "2020",
            },
            {
              label: "2021",
              value: "2021",
            },
          ],
          filter(value, row) {
            return ~row.semester.indexOf(value);
          },
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
          title: "成绩",
          key: "score",
          sorter: "default",
        },
      ],
      data: [],
      pagination: {
        pageSize: 10,
      },
      credit: 0,
    };
  },
  setup() {
    const msg = useMessage();
    return {
      warn(m) {
        msg.warning(m);
      },
      errot(m) {
        msg.error(m);
      },
    };
  },
  methods: {
    async query() {
      if (this.selectStudent === null) {
        this.warn("请先选择学生");
      } else {
        this.inloading = true;
        this.credit = await getStudentCredit(this.selectStudent);
        getStudentCourse(this.selectStudent)
          .then((d) => {
            this.data = d;
            this.inloading = false;
          })
          .catch(() => {
            this.errot("获取数据失败");
            this.inloading = false;
          });
      }
    },
  },
};
</script>