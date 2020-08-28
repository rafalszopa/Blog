import { Post } from '../models/Post';
import axios from 'axios';

export default class PostsService {
    async get(id: number): Promise<Post> {
        return await axios.get<Post>('api/posts?id=' + id)
            .then(response => response.data);
            //.catch(() => { console.log('Error happened') });
    }
}