<template>
  <n-space vertical>
    <n-card title="班级排课管理" hoverable>
      <n-space vertical>
        <n-grid x-gap="12" :cols="3">
          <n-gi>
            <class-select v-on:change="selectClass = $event" />
          </n-gi>
          <n-gi>
            <n-space>
              <n-button @click="query">查询</n-button>
              <n-button @click="showModal = true">添加课程</n-button>
              <n-button @click="showModalDel = true">删除课程</n-button>
              <n-modal v-model:show="showModal" preset="dialog">
                <template #header>添加课程</template>
                <course-select v-on:change="selAddItem = $event" />
                <template #action>
                  <n-spin :show="inAddItem">
                    <n-button @click="addItem">添加</n-button>
                  </n-spin>
                </template>
              </n-modal>
              <n-modal v-model:show="showModalDel" preset="dialog">
                <template #header>删除课程</template>
                <n-select v-model:value="selDelItem" :options="itemOpt" />
                <template #action>
                  <n-spin :show="inDelItem">
                    <n-button @click="removeItem">删除</n-button>
                  </n-spin>
                </template>
              </n-modal>
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
import CourseSelect from "../components/CourseSelect.vue";
import {
  NDataTable,
  NText,
  NSpace,
  NButton,
  NGrid,
  NModal,
  NGi,
  NCard,
  NSelect,
  NSpin,
  useMessage,
} from "naive-ui";
import ClassSelect from "../components/ClassSelect.vue";

async function getItem(cl) {
  let res = await fetch(`https://localhost:5001/api/class:${cl}/course:all`);
  let j = await res.json();
  return j;
}

export default {
  components: {
    NDataTable,
    ClassSelect,
    NSpace,
    NButton,
    NGrid,
    NGi,
    NCard,
    CourseSelect,
    NModal,
    NSelect,
    NSpin,
  },
  data() {
    return {
      selectClass: null,
      inloading: false,
      columns: [
        {
          title: "课程编号",
          key: "id",
        },
        {
          title: "课程名",
          key: "name",
        },
        {
          title: "任课教师",
          key: "teacherName",
          render(row) {
            const tags = row.teacherName.map((tagKey) => {
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
        },
        {
          title: "学时",
          key: "hours",
        },
        {
          title: "学分",
          key: "credit",
        },
        {
          title: "考试或考查",
          key: "examMethod",
        },
      ],
      data: [],
      pagination: {
        pageSize: 10,
      },
      showModal: false,
      showModalDel: false,
      inAddItem: false,
      inDelItem: false,
      selAddItem: "",
      selDelItem: "",
    };
  },
  setup() {
    const msg = useMessage();
    return {
      warn(m) {
        msg.warning(m);
      },
      error(m) {
        msg.error(m);
      },
    };
  },
  computed: {
    itemOpt() {
      let o = [];
      this.data.forEach((d) => {
        o.push({
          label: d.name,
          value: d.id,
        });
      });
      return o;
    },
  },
  methods: {
    query() {
      if (this.selectClass === null) {
        this.warn("请先选择班级");
      } else {
        this.inloading = true;
        getItem(this.selectClass)
          .then((d) => {
            this.data = d;
            this.inloading = false;
          })
          .catch(() => {
            this.error("获取数据失败");
            this.inloading = false;
          });
      }
    },
    addItem() {
      this.inAddClass = true;
      fetch(
        `https://localhost:5001/api/class:${this.selectClass}/course:${this.selAddItem}`,
        { method: "POST" }
      )
        .then(() => {
          this.showModal = false;
          this.inAddItem = false;
          this.inloading = true;
          this.selAddItem = "";
          getItem(this.selectClass)
            .then((d) => {
              this.data = d;
              this.inloading = false;
            })
            .catch(() => {
              this.inloading = false;
              this.error("数据加载失败");
            });
        })
        .catch(() => {
          this.error("添加失败");
        });
    },
    removeItem() {
      this.inDelItem = true;
      fetch(
        `https://localhost:5001/api/class:${this.selectClass}/course:${this.selDelItem}`,
        {
          method: "DELETE",
        }
      )
        .then(() => {
          this.inDelItem = false;
          this.selDelItem = "";
          this.inloading = true;
          this.showModalDel = false;
          getItem(this.selectClass)
            .then((d) => {
              this.data = d;
              this.inloading = false;
            })
            .catch(() => {
              this.inloading = false;
              this.error("数据加载失败");
            });
        })
        .catch(() => {
          this.error("删除失败");
        });
    },
  },
};
</script>