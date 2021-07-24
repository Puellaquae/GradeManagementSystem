<template>
  <n-space vertical>
    <n-card title="班级管理" hoverable>
      <n-space vertical>
        <n-grid x-gap="12" :cols="3">
          <n-gi>
            <n-select v-model:value="selectMajor" :options="majorOpt" />
          </n-gi>
          <n-gi :span="2">
            <n-space>
              <n-button @click="query">选择</n-button>
              <n-button @click="showModal = true">添加班级</n-button>
              <n-button @click="showModalDel = true">删除班级</n-button>
              <n-button @click="updateClass">保存修改</n-button>
              <n-modal v-model:show="showModal" preset="dialog">
                <template #header>添加班级</template>
                <n-form :label-width="80" :model="newClass">
                  <n-form-item label="班级编号" path="id">
                    <n-input
                      placeholder="输入班级编号"
                      v-model:value="newClass.id"
                    />
                  </n-form-item>
                  <n-form-item label="班级名称" path="name">
                    <n-input
                      placeholder="输入班级名称"
                      v-model:value="newClass.name"
                    />
                  </n-form-item>
                </n-form>
                <template #action>
                  <n-spin :show="inAddClass">
                    <n-button @click="addClass">添加</n-button>
                  </n-spin>
                </template>
              </n-modal>
              <n-modal v-model:show="showModalDel" preset="dialog">
                <template #header>删除班级</template>
                <n-select v-model:value="selDelClass" :options="classOpt" />
                <template #action>
                  <n-spin :show="inDelClass">
                    <n-button @click="removeClass">删除</n-button>
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
} from "naive-ui";

async function getClass(mid) {
  let f = await fetch(`https://localhost:5001/api/major:${mid}/class:all`);
  let j = await f.json();
  return j;
}

async function getMajor() {
  let f = await fetch(`https://localhost:5001/api/major:all`);
  let j = await f.json();
  let o = [];
  j.forEach((d) => {
    o.push({
      label: d.name,
      value: d.id,
    });
  });
  return o;
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
    NSpin,
  },
  data() {
    return {
      selectMajor: null,
      showModal: false,
      showModalDel: false,
      inloading: false,
      columns: [
        {
          title: "班级编号",
          key: "id",
        },
        {
          title: "班级名称",
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
      ],
      data: [],
      majorOpt: [],
      pagination: {
        pageSize: 10,
      },
      newClass: {
        name: "",
        id: "",
      },
      inAddClass: false,
      inDelClass: false,
      selDelClass: "",
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
    classOpt() {
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
    async query() {
      this.inloading = true;
      this.data = await getClass(this.selectMajor);
      this.inloading = false;
    },
    addClass() {
      this.inAddClass = true;
      fetch(
        `https://localhost:5001/api/class:${this.newClass.id}?name=${this.newClass.name}&majorId=${this.selectMajor}`,
        { method: "POST" }
      )
        .then(() => {
          this.showModal = false;
          this.inAddClass = false;
          this.newClass.name = "";
          this.newClass.id = "";
          this.inloading = true;
          getClass(this.selectMajor).then((d) => {
            this.data = d;
            this.inloading = false;
          });
        })
        .catch(() => {
          this.error("添加失败");
        });
    },
    removeClass() {
      this.inDelClass = true;
      fetch(`https://localhost:5001/api/class:${this.selDelClass}`, {
        method: "DELETE",
      }).then(() => {
        this.inDelClass = false;
        this.selDelClass = "";
        this.inloading = true;
        this.showModalDel = false;
        getClass(this.selectMajor).then((d) => {
          this.data = d;
          this.inloading = false;
        });
      });
    },
    updateClass() {
      fetch(`https://localhost:5001/api/class`, {
        method: "PUT",
        body: JSON.stringify(this.data),
        headers: new Headers({
          "Content-Type": "application/json",
        }),
      })
        .then(() => {
          this.info("保存完毕");
          this.inloading = true;
          getClass(this.selectMajor).then((d) => {
            this.data = d;
            this.inloading = false;
          });
        })
        .catch(() => {
          this.error("保存失败");
        });
    },
  },
  async mounted() {
    this.majorOpt = await getMajor();
  },
};
</script>