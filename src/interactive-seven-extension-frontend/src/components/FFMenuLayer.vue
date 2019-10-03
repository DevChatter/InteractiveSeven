<template>
  <div class="ff7 menu">
    <div class="layer" :style="topLeftStyle"></div>
    <div class="layer" :style="botLeftStyle"></div>
    <div class="layer" :style="topRightStyle"></div>
    <div class="layer" :style="botRightStyle"></div>
    <div class="layer border"></div>
    <div class="text">
      <slot/>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  computed: {
    ...mapGetters([
      'topLeftStyle',
      'topRightStyle',
      'botLeftStyle',
      'botRightStyle'
    ])
  }
}
</script>

<style scoped>
.ff7 {
  position: relative;
  float: left;
  background: #333;
  -webkit-border-radius: 7px;
  -moz-border-radius: 7px;
  border-radius: 7px;
}

.ff7.menu {
  position: absolute;
  height: auto;
  bottom: 0;
  top: 0;
  left: 0;
  right: 0;
  margin: 10px;
}

.ff7 > .layer {
  position: absolute;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  -webkit-border-radius: 7px;
  -moz-border-radius: 7px;
  border-radius: 7px;
}

.ff7 > .border {
  border: solid 3px #424542;
  box-shadow: 3px 3px #e7dfe7,
    -3px -3px #e7dfe7,
    3px -3px #e7dfe7,
    -3px 3px #e7dfe7,
    0 -6px #9c9a9c,
    -6px 0 #7b757b,
    0 6px #424542,
    6px 0 #424542;
  width: calc(100% - 4px);
  height: calc(100% - 4px);
}

.ff7 .text {
  position: relative;
  padding: 5px 10px;
  width: auto;
}

.ff7 .text * {
  color: #eff1ff;
  text-shadow: 2px 2px #212421, 1px 1px #212021;
  font-family: Verdana, sans-serif;
  font-size: 20px;
  font-weight: normal;
  margin: 5px 0;
}
</style>
