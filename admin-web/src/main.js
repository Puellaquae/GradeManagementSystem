import { createApp } from 'vue'
import App from './App.vue'

import Home from './views/Home.vue'

import CourseScoreDataEntry from './views/CourseScoreDataEntry.vue'
import StudentEntry from './views/StudentEntry.vue'
import TeacherEntry from './views/TeacherEntry.vue'
import CourseEntry from './views/CourseEntry.vue'
import ClassEntry from './views/ClassEntry.vue'

import ClassCourseQuery from './views/ClassCourseQuery.vue'
import TeacherCourseQuery from "./views/TeacherCourseQuery.vue"
import StudentCourseQuery from "./views/StudentCourseQuery.vue"

import CourseAvgStatistic from "./views/CourseAvgStatistic.vue"
import CourseRankStatistic from "./views/CourseRankStatistic.vue"
import SourcePlaceCount from "./views/SourcePlaceCount.vue"

import NotFound from "./views/NotFound.vue"

import { createRouter, createWebHistory } from 'vue-router'

const app = createApp(App)

const router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: '/', component: Home },
        { path: '/class-course-query', component: ClassCourseQuery},
        { path: '/course-score-data-entry', component: CourseScoreDataEntry},
        { path: '/student-entry', component: StudentEntry},
        { path: '/teacher-entry', component: TeacherEntry},
        { path: '/course-entry', component: CourseEntry},
        { path: '/class-entry', component: ClassEntry},
        { path: '/teacher-course-query', component: TeacherCourseQuery},
        { path: '/student-course-query', component: StudentCourseQuery},
        { path: '/student-course-rank-statistic', component: CourseRankStatistic},
        { path: '/course-avg-score-statistic', component: CourseAvgStatistic},
        { path: '/source-place-count', component: SourcePlaceCount},
        { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFound },
    ],
})

app.use(router)

app.mount('#app')
