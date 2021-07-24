<template>
  <n-card hoverable>
    <n-result
      status="404"
      title="你怎么到的这里"
      description="网址不要乱写"
    />
  </n-card>
</template>

<script>
import { NResult, NCard } from "naive-ui";

export default {
  components: {
    NResult,
    NCard,
  },
};
</script>