<template>
  <n-space vertical>
    <n-card title="课程管理" hoverable>
      <n-space vertical>
        <n-space>
          <n-button @click="showModal = true">添加课程</n-button>
          <n-button @click="showModalDel = true">删除课程</n-button>
          <n-button @click="updateItem">保存修改</n-button>
          <n-modal v-model:show="showModal" preset="dialog">
            <template #header>添加课程</template>
            <n-form :label-width="80" :model="newItem">
              <n-form-item label="课程编号" path="id">
                <n-input
                  placeholder="输入课程编号"
                  v-model:value="newItem.id"
                />
              </n-form-item>
              <n-form-item label="课程名" path="name">
                <n-input
                  placeholder="输入课程名"
                  v-model:value="newItem.name"
                />
              </n-form-item>
              <n-form-item label="任课教师" path="teacher">
                <teacher-select
                  v-on:change="newItem.teacherId = $event"
                  :multi="true"
                />
              </n-form-item>
              <n-form-item label="开设学期" path="semester">
                <n-input
                  placeholder="输入开设学期"
                  v-model:value="newItem.semester"
                />
              </n-form-item>
              <n-form-item label="学时" path="hours">
                <n-input-number
                  placeholder="输入学时"
                  v-model:value="newItem.hours"
                />
              </n-form-item>
              <n-form-item label="学分" path="credit">
                <n-input-number
                  placeholder="输入学分"
                  v-model:value="newItem.credit"
                />
              </n-form-item>
              <n-form-item label="考试或考查" path="examMethod">
                <n-input
                  placeholder="输入考试或考查"
                  v-model:value="newItem.examMethod"
                />
              </n-form-item>
            </n-form>
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
import TeacherSelect from "../components/TeacherSelect.vue";
import {
  NDataTable,
  NSpace,
  NButton,
  NCard,
  useMessage,
  NSelect,
  NForm,
  NFormItem,
  NInput,
  NInputNumber,
  NModal,
  NSpin,
} from "naive-ui";

async function getItem() {
  let f = await fetch("https://localhost:5001/api/course:all");
  return await f.json();
}

const emptyItem = {
  id: "",
  name: "",
  teacherId: [],
  teacherName: [],
  semester: "",
  hours: 0,
  credit: 0,
  examMethod: "",
};

export default {
  components: {
    NDataTable,
    NSpace,
    NButton,
    NCard,
    NSelect,
    NModal,
    NForm,
    NFormItem,
    NInput,
    NInputNumber,
    NSpin,
    TeacherSelect,
  },
  data() {
    return {
      showModal: false,
      showModalDel: false,
      inloading: true,
      columns: [
        {
          title: "课程编号",
          key: "id",
        },
        {
          title: "课程名",
          key: "name",
          render(row) {
            return h(
              NInput,
              {
                value: row.name,
                "onUpdate:value": (v) => (row.name = v),
              },
              null
            );
          },
        },
        {
          title: "任课教师",
          key: "teacherId",
          render(row) {
            return h(
              TeacherSelect,
              {
                multi: true,
                value: row.teacherId,
                onChange : $event => row.teacherId = $event
              },
              null
            );
          },
        },
        {
          title: "开设学期",
          key: "semester",
          render(row) {
            return h(
              NInput,
              {
                value: row.semester,
                "onUpdate:value": (v) => (row.semester = v),
              },
              null
            );
          },
        },
        {
          title: "学时",
          key: "hours",
          render(row) {
            return h(
              NInputNumber,
              {
                value: row.hours,
                "onUpdate:value": (v) => (row.hours = v),
              },
              null
            );
          },
        },
        {
          title: "学分",
          key: "credit",
          render(row) {
            return h(
              NInputNumber,
              {
                value: row.credit,
                "onUpdate:value": (v) => (row.credit = v),
              },
              null
            );
          },
        },
        {
          title: "考试或考查",
          key: "examMethod",
          render(row) {
            return h(
              NInput,
              {
                value: row.examMethod,
                "onUpdate:value": (v) => (row.examMethod = v),
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
      newItem: emptyItem,
      inAddItem: false,
      inDelItem: false,
      selDelItem: "",
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
    addItem() {
      this.inAddItme = true;
      fetch(`https://localhost:5001/api/course`, {
        method: "POST",
        body: JSON.stringify(this.newItem),
        headers: new Headers({
          "Content-Type": "application/json",
        }),
      })
        .then(() => {
          this.showModal = false;
          this.inAddItem = false;
          this.newItem.id = "";
          this.newItem.name = "";
          this.newItem.teacherId = [];
          this.newItem.semester = "";
          this.newItem.hours = 0;
          this.newItem.credit = 0;
          this.inloading = true;
          getItem()
            .then((d) => {
              this.data = d;
              this.inloading = false;
            })
            .catch(() => {
              this.inloading = false;
              this.error("获取数据失败");
            });
        })
        .catch(() => {
          this.inAddItem = false;
          this.error("添加失败");
        });
    },
    removeItem() {
      this.inDelItem = true;
      fetch(`https://localhost:5001/api/course:${this.selDelItem}`, {
        method: "DELETE",
      })
        .then(() => {
          this.inDelItem = false;
          this.selDelItem = "";
          this.inloading = true;
          this.showModalDel = false;
          getItem()
            .then((d) => {
              this.data = d;
              this.inloading = false;
            })
            .catch(() => {
              this.inloading = false;
              this.error("获取数据失败");
            });
        })
        .catch(() => {
          this.inDelItem = false;
          this.error("删除失败");
        });
    },
    updateItem() {
      fetch(`https://localhost:5001/api/course`, {
        method: "PUT",
        body: JSON.stringify(this.data),
        headers: new Headers({
          "Content-Type": "application/json",
        }),
      })
        .then(() => {
          this.info("保存完毕");
          this.inloading = true;
          getItem()
            .then((d) => {
              this.data = d;
              this.inloading = false;
            })
            .catch(() => {
              this.error("获取数据失败");
              this.inloading = false;
            });
        })
        .catch(() => {
          this.error("保存失败");
        });
    },
  },
  mounted() {
    getItem()
      .then((d) => {
        this.data = d;
        this.inloading = false;
      })
      .catch(() => {
        this.error("获取数据失败");
        this.inloading = false;
      });
  },
};
</script>