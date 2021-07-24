<template>
  <n-space vertical>
    <n-card title="教师管理" hoverable>
      <n-space vertical>
        <n-space>
          <n-button @click="showModal = true">添加教师</n-button>
          <n-button @click="showModalDel = true">删除教师</n-button>
          <n-button @click="updateTeacher">保存修改</n-button>
          <n-modal v-model:show="showModal" preset="dialog">
            <template #header>添加教师</template>
            <n-form :label-width="80" :model="newTeacher">
              <n-form-item label="工号" path="teacherId">
                <n-input
                  placeholder="输入工号"
                  v-model:value="newTeacher.teacherId"
                />
              </n-form-item>
              <n-form-item label="姓名" path="name">
                <n-input
                  placeholder="输入姓名"
                  v-model:value="newTeacher.name"
                />
              </n-form-item>
              <n-form-item label="年龄" path="age">
                <n-input-number
                  placeholder="输入年龄"
                  v-model:value="newTeacher.age"
                />
              </n-form-item>
              <n-form-item label="性别" path="gender">
                <n-input
                  placeholder="输入性别"
                  v-model:value="newTeacher.gender"
                />
              </n-form-item>
              <n-form-item label="职称" path="title">
                <n-input
                  placeholder="输入职称"
                  v-model:value="newTeacher.title"
                />
              </n-form-item>
              <n-form-item label="电话" path="telephone">
                <n-input
                  placeholder="输入电话"
                  v-model:value="newTeacher.telephone"
                />
              </n-form-item>
            </n-form>
            <template #action>
              <n-spin :show="inAddTeacher">
                <n-button @click="addTeacher">添加</n-button>
              </n-spin>
            </template>
          </n-modal>
          <n-modal v-model:show="showModalDel" preset="dialog">
            <template #header>删除教师</template>
            <n-select v-model:value="selDelTeacher" :options="teacherOpt" />
            <template #action>
              <n-spin :show="inDelTeacher">
                <n-button @click="removeTeacher">删除</n-button>
              </n-spin>
            </template>
          </n-modal>
        </n-space>
        <n-data-table
          :row-key="(c) => c.teacherId"
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

async function getTeacher() {
  let f = await fetch("https://localhost:5001/api/teacher:all");
  return await f.json();
}

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
  },
  data() {
    return {
      showModal: false,
      showModalDel: false,
      inloading: true,
      columns: [
        {
          title: "工号",
          key: "teacherId",
        },
        {
          title: "姓名",
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
          title: "年龄",
          key: "age",
          render(row) {
            return h(
              NInputNumber,
              {
                value: row.age,
                "onUpdate:value": (v) => (row.age = v),
              },
              null
            );
          },
        },
        {
          title: "性别",
          key: "gender",
          render(row) {
            return h(
              NInput,
              {
                value: row.gender,
                "onUpdate:value": (v) => (row.gender = v),
              },
              null
            );
          },
        },
        {
          title: "职称",
          key: "title",
          render(row) {
            return h(
              NInput,
              {
                value: row.title,
                "onUpdate:value": (v) => (row.title = v),
              },
              null
            );
          },
        },
        {
          title: "电话",
          key: "telephone",
          render(row) {
            return h(
              NInput,
              {
                value: row.telephone,
                "onUpdate:value": (v) => (row.telephone = v),
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
      newTeacher: {
        name: "",
        id: "",
        age: 0,
        gender: "",
        title: "",
        telephone: "",
      },
      inAddTeacher: false,
      inDelTeacher: false,
      selDelTeacher: "",
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
    teacherOpt() {
      let o = [];
      this.data.forEach((d) => {
        o.push({
          label: d.name,
          value: d.teacherId,
        });
      });
      return o;
    },
  },
  methods: {
    addTeacher() {
      this.inAddTeacher = true;
      fetch(`https://localhost:5001/api/teacher`, {
        method: "POST",
        body: JSON.stringify(this.newTeacher),
        headers: new Headers({
          "Content-Type": "application/json",
        }),
      })
        .then(() => {
          this.showModal = false;
          this.inAddTeacher = false;
          this.newTeacher.name = "";
          this.newTeacher.id = "";
          this.newTeacher.age = 0;
          this.newTeacher.gender = "";
          this.newTeacher.title = "";
          this.newTeacher.telephone = "";
          this.inloading = true;
          getTeacher()
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
          this.inAddTeacher = false;
          this.error("添加失败");
        });
    },
    removeTeacher() {
      this.inDelTeacher = true;
      fetch(`https://localhost:5001/api/teacher:${this.selDelTeacher}`, {
        method: "DELETE",
      })
        .then(() => {
          this.inDelTeacher = false;
          this.selDelTeacher = "";
          this.inloading = true;
          this.showModalDel = false;
          getTeacher()
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
          this.inDelTeacher = false;
          this.error("删除失败");
        });
    },
    updateTeacher() {
      fetch(`https://localhost:5001/api/teacher`, {
        method: "PUT",
        body: JSON.stringify(this.data),
        headers: new Headers({
          "Content-Type": "application/json",
        }),
      })
        .then(() => {
          this.info("保存完毕");
          this.inloading = true;
          getTeacher()
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
    getTeacher()
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