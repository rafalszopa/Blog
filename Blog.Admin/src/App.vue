<template>
    <div id="app">
        <component :is="layout">
            <router-view></router-view>
        </component>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import DefaultLayout from './layouts/Default.vue';
    import BlankLayout from './layouts/Blank.vue';
    import { Getter } from 'vuex-class';

    const defaultLayout = 'default';


@Component({
  components: {
        DefaultLayout,
        BlankLayout
  },
})
export default class App extends Vue {
    get layout() {
        return (this.$route.meta.layout || defaultLayout) + '-layout';
    }

    @Getter('isAuthenticated') private isAuthenticated!: boolean
    created() {
    }

}
</script>

<style>
</style>
