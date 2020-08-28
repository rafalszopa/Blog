<template>
    <div>
        <h1>Posts</h1>
        <pre v-if="this.post">{{ post.id }}</pre>
        <pre v-if="this.post">{{ post.title }}</pre>
        <pre v-if="this.post">{{ post.content }}</pre>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue, Model } from 'vue-property-decorator';
    import PostsService from '../services/posts.service';
    import { Post } from '../models/Post';

    @Component
    export default class PostsPage extends Vue {

        private post: Post | null;

        constructor() {
            super();
            this.post = null;
        }

        async created() {
            let postsService = new PostsService();
            this.post = await postsService.get(897);
        }
    }
</script>