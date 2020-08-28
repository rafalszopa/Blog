<template>
    <div id="wrapper">
        <Sidebar />
        <!-- Content Wrapper -->
        <div id="content-wrapper" class="d-flex flex-column">
            <!-- Main Content -->
            <div id="content">
                <Topbar />
                <!-- Begin Page Content -->
                <div class="container-fluid">
                    <slot />
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from "vue-property-decorator";
    import Topbar from "../components/Topbar.vue";
    import Sidebar from "../components/Sidebar.vue";

    @Component({
        components: {
            Topbar,
            Sidebar
        },
    })
    export default class DefaultLayout extends Vue {
    }
</script>