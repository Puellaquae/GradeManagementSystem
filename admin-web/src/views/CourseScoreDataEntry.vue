<template>
  <n-space vertical>
    <n-card title="学生成绩管理" hoverable>
      <n-space vertical>
        <n-grid x-gap="12" :cols="3">
          <n-gi>
            <course-select v-on:change="selectCourse = $event" />
          </n-gi>
          <n-gi>
            <n-space>
              <n-button @click="query">选择</n-button>
              <n-button @click="save">保存数据</n-button>
            </n-space>
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
  NSpace,
  NButton,
  NGrid,
  NGi,
  NCard,
  useMessage,
  NInputNumber,
} from "naive-ui";
import CourseSelect from "../components/CourseSelect.vue";

async function getStudentScore(cid) {
  let f = await fetch(`https://localhost:5001/api/course:${cid}/student:all`);
  let j = await f.json();
  return j;
}

export default {
  components: {
    NDataTable,
    CourseSelect,
    NSpace,
    NButton,
    NGrid,
    NGi,
    NCard,
  },
  data() {
    return {
      selectCourse: null,
      inloading: false,
      columns: [
        {
          title: "学号",
          key: "id",
        },
        {
          title: "姓名",
          key: "name",
        },
        {
          title: "成绩",
          key: "score",
          render(row) {
            return h(
              NInputNumber,
              {
                value: row.score,
                "onUpdate:value": (v) => (row.score = v),
                max: 100,
                min: 0,
              },
              null
            );
          },
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
  methods: {
    query() {
      this.inloading = true;
      getStudentScore(this.selectCourse)
        .then((d) => {
          this.data = d;
          this.inloading = false;
        })
        .catch(() => {
          this.inloading = false;
          this.error("获取数据失败")
        });
    },
    save() {
      fetch(
        `https://localhost:5001/api/course:${this.selectCourse}/student:all`,
        {
          method: "PUT",
          body: JSON.stringify(this.data),
          headers: new Headers({
            "Content-Type": "application/json",
          }),
        }
      )
        .then(() => {
          this.info("保存完毕");
        })
        .catch(() => {
          this.error("保存失败");
        });
    },
  },
};
</script>