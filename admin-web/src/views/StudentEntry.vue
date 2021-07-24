<template>
  <n-space vertical>
    <n-card title="学生管理" hoverable>
      <n-space vertical>
        <n-grid x-gap="12" :cols="3">
          <n-gi>
            <class-select v-on:change="selectClass = $event" />
          </n-gi>
          <n-gi :span="2">
            <n-space>
              <n-button @click="query">选择</n-button>
              <n-button @click="showModal = true">添加学生</n-button>
              <n-button @click="showModalDel = true">删除学生</n-button>
              <n-button @click="updateItem">保存修改</n-button>
              <n-modal v-model:show="showModal" preset="dialog">
                <template #header>添加学生</template>
                <n-form :label-width="80" :model="newItem">
                  <n-form-item label="学号" path="studentId">
                    <n-input
                      placeholder="输入学号"
                      v-model:value="newItem.studentId"
                    />
                  </n-form-item>
                  <n-form-item label="姓名" path="name">
                    <n-input
                      placeholder="输入姓名"
                      v-model:value="newItem.name"
                    />
                  </n-form-item> </n-form
                ><n-form-item label="年龄" path="age">
                  <n-input-number
                    placeholder="输入年龄"
                    v-model:value="newItem.age"
                  />
                </n-form-item>
                <n-form-item label="性别" path="gender">
                  <n-input
                    placeholder="输入性别"
                    v-model:value="newItem.gender"
                  />
                </n-form-item>
                <n-form-item label="生源所在地" path="title">
                  <n-input
                    placeholder="输入生源所在地"
                    v-model:value="newItem.sourcePlace"
                  />
                </n-form-item>
                <template #action>
                  <n-spin :show="inAddItem">
                    <n-button @click="addItem">添加</n-button>
                  </n-spin>
                </template>
              </n-modal>
              <n-modal v-model:show="showModalDel" preset="dialog">
                <template #header>删除学生</template>
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
          :row-key="(c) => c.studentId"
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
import ClassSelect from "../components/ClassSelect.vue";
import {
  NDataTable,
  NSpace,
  NButton,
  NGrid,
  NGi,
  NCard,
  useMessage,
  NSelect,
  NForm,
  NFormItem,
  NInput,
  NModal,
  NSpin,
  NInputNumber,
} from "naive-ui";

async function getItem(cid) {
  let f = await fetch(`https://localhost:5001/api/class:${cid}/student:all`);
  let j = await f.json();
  return j;
}

export default {
  components: {
    NDataTable,
    NSpace,
    NButton,
    NGrid,
    NGi,
    NCard,
    NSelect,
    NModal,
    NForm,
    NFormItem,
    NInput,
    NInputNumber,
    NSpin,
    ClassSelect,
  },
  data() {
    return {
      selectClass: null,
      showModal: false,
      showModalDel: false,
      inloading: false,
      columns: [
        {
          title: "学号",
          key: "studentId",
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
          title: "生源所在地",
          key: "sourcePlace",
          render(row) {
            return h(
              NInput,
              {
                value: row.sourcePlace,
                "onUpdate:value": (v) => (row.sourcePlace = v),
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
      newItem: {
        name: "",
        studentId: "",
        age: 0,
        gender: "",
        sourcePlace: "",
      },
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
          value: d.studentId,
        });
      });
      return o;
    },
  },
  methods: {
    query() {
      this.inloading = true;
      getItem(this.selectClass)
        .then((d) => {
          this.data = d;
          this.inloading = false;
        })
        .catch(() => {
          this.inloading = false;
          this.error("数据加载失败");
        });
    },
    addItem() {
      this.inAddClass = true;
      fetch(
        `https://localhost:5001/api/student:${this.newItem.studentId}?name=${this.newItem.name}&age-${this.newItem.age}&gender=${this.newItem.gender}&classId=${this.selectClass}&sourcePlace=${this.newItem.sourcePlace}`,
        { method: "POST" }
      )
        .then(() => {
          this.showModal = false;
          this.inAddItem = false;
          this.newItem.name = "";
          this.newItem.studentId = "";
          this.newItem.age = 0;
          this.newItem.gender = "";
          this.newItem.sourcePlace = "";
          this.inloading = true;
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
      fetch(`https://localhost:5001/api/student:${this.selDelItem}`, {
        method: "DELETE",
      }).then(() => {
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
      });
    },
    updateItem() {
      fetch(`https://localhost:5001/api/student`, {
        method: "PUT",
        body: JSON.stringify(this.data),
        headers: new Headers({
          "Content-Type": "application/json",
        }),
      })
        .then(() => {
          this.info("保存完毕");
          this.inloading = true;
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
          this.error("保存失败");
        });
    },
  },
};
</script>