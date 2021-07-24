<template>
  <n-page-header>
    <template #title>
      <n-text style="font-size: 1.75rem"> 高校成绩管理系统 </n-text>
    </template>
    <n-menu v-model:value="nowkey" mode="horizontal" :options="menuOptions" />
  </n-page-header>
</template>

<script>
import { RouterLink } from "vue-router";
import { NPageHeader, NMenu, NText } from "naive-ui";
import { h } from "vue";

function renderLink(to, label) {
  return () => h(RouterLink, { to: to }, { default: () => label });
}

export default {
  components: {
    NPageHeader,
    NMenu,
    NText,
  },
  data() {
    return {
      menuOptions: [
        {
          label: "数据管理",
          key: "/data-entry",
          children: [
            {
              label: renderLink("/class-entry", "班级管理"),
              key: "/class-entry",
            },
            {
              label: renderLink("/teacher-entry", "教师管理"),
              key: "/teacher-entry",
            },
            {
              label: renderLink("/course-entry", "课程管理"),
              key: "/course-entry",
            },
            {
              label: renderLink("/student-entry", "学生管理"),
              key: "/student-entry",
            },
          ],
        },
        {
          label: renderLink("/course-score-data-entry", "学生成绩管理"),
          key: "/course-score-data-entry",
        },
        {
          label: renderLink("/class-course-query", "班级排课管理"),
          key: "/class-course-query",
        },
        {
          label: renderLink("/teacher-course-query", "教师任课查询"),
          key: "/teacher-course-query",
        },
        {
          label: renderLink("/student-course-query", "学生成绩查询"),
          key: "/student-course-query",
        },
        {
          label: "统计",
          key: "/statistic",
          children: [
            {
              label: renderLink(
                "/student-course-rank-statistic",
                "学生成绩名次排定"
              ),
              key: "/student-course-rank-statistic",
            },
            {
              label: renderLink(
                "/course-avg-score-statistic",
                "课程平均成绩统计"
              ),
              key: "/course-avg-score-statistic",
            },
            {
              label: renderLink("/source-place-count", "学生生源地人数统计"),
              key: "/source-place-count",
            },
          ],
        },
      ],
      nowkey: null,
    };
  },
  watch: {
    $route(to, from) {
      if (this.nowkey === to.path) {
        this.nowkey = from.path;
      } else {
        this.nowkey = to.path;
      }
    },
  },
};
</script>

<style>
a {
  text-decoration: none;
  color: inherit;
}
</style>