<template>
  <div style="padding: 2rem">
    <n-config-provider :locale="zhCN">
      <n-message-provider>
        <my-header />
        <router-view />
      </n-message-provider>
    </n-config-provider>
  </div>
</template>

<script>
import { NMessageProvider, NConfigProvider,zhCN } from "naive-ui";
import MyHeader from "./components/Header.vue";
export default {
  name: "App",
  components: {
    NMessageProvider,
    MyHeader,
    NConfigProvider
  },
  setup() {
    return {
      zhCN
    }
  }
};
</script>

