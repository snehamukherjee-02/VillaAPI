<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import { useRoute } from "vue-router";

// Access the route parameters to get the villa ID
const route = useRoute();
const villa = ref(null);
const comments = ref([]);
const newComment = ref({ title: '', content: '' });
const editComment = ref({ id: null, title: '', content: '' });

// Fetch villa details and associated comments
onMounted(() => {
    const villaId = route.params.id;

    // Fetch villa details
    axios.get(`https://localhost:7090/api/VillaAPI/${villaId}`)
        .then((response) => {
            villa.value = response.data;
        })
        .catch((error) => {
            console.error("Error fetching villa details:", error);
        });

    // Fetch comments associated with the villa
    axios.get(`https://localhost:7090/api/CommentAPI/${villaId}`)
        .then((response) => {
            comments.value = response.data;
        })
        .catch((error) => {
            console.error("Error fetching comments:", error);
        });
});

// POST new comment
const addComment = () => {
    const villaId = route.params.id;
    const commentData = {
        title: newComment.value.title,
        content: newComment.value.content
    };

    axios.post(`https://localhost:7090/api/CommentAPI/${villaId}`, commentData)
        .then((response) => {
            comments.value.push(response.data);
            newComment.value.title = ''; // Clear form
            newComment.value.content = '';
        })
        .catch((error) => {
            console.error("Error posting comment:", error);
        });
};

// Edit comment
const startEditComment = (comment) => {
    editComment.value = { ...comment }; // Fill the edit form with current comment data
};

// Submit edited comment
const updateComment = () => {
    console.log("Updating comment:", editComment.value); // Debugging: Inspect data being sent

    axios.put(`https://localhost:7090/api/CommentAPI/${editComment.value.id}`, {
        id: editComment.value.id,
        title: editComment.value.title,
        content: editComment.value.content,
        villaId: route.params.id, // Include VillaId if required by the API
    })
        .then((response) => {
            // Find the comment in the array and update it
            const index = comments.value.findIndex(c => c.id === editComment.value.id);
            if (index !== -1) {
                comments.value[index] = { ...comments.value[index], ...editComment.value };
            }
            // Reset the edit form
            editComment.value = { id: null, title: '', content: '' };
        })
        .catch((error) => {
            console.error("Error updating comment:", error);
        });
};

// Delete comment
const deleteComment = (commentId) => {
    axios.delete(`https://localhost:7090/api/CommentAPI/${commentId}`)
        .then(() => {
            comments.value = comments.value.filter(c => c.id !== commentId); // Remove from list
        })
        .catch((error) => {
            console.error("Error deleting comment:", error);
        });
};
</script>

<template>
    <div class="container">
        <div class="row pt-2 pb-3">
            <div class="col-12">
                <h2 class="text-primary">
                    <u>{{ villa?.title }} Details</u>
                </h2>
            </div>
        </div>

        <!-- Villa Details Section -->
        <div v-if="villa" class="row">
            <div class="col-md-6">
                <img :src="villa.imageurl" class="img-fluid" alt="Villa Image">
            </div>
            <div class="col-md-6">
                <h4 class="card-title">{{ villa.name }}</h4><br>
                <p><strong>Details:</strong> {{ villa.details }}</p>
                <p><strong>Rate:</strong> {{ villa.rate }}</p>
                <p><strong>Square Feet:</strong> {{ villa.sqft }}</p>
                <p><strong>Occupancy:</strong> {{ villa.occupency }}</p>
                <p><strong>Amenities:</strong> {{ villa.amenity }}</p>
            </div>
        </div>

        <!-- Comment Section -->
        <div class="row pt-4">
            <div class="col-12">
                <h4 class="text-secondary">Comments</h4>

                <!-- Comment Form (Add New Comment) -->
                <div class="card my-3">
                    <div class="card-body">
                        <h5>Add a Comment</h5>
                        <form @submit.prevent="addComment">
                            <div class="mb-3">
                                <label for="commentTitle" class="form-label">Title</label>
                                <input v-model="newComment.title" type="text" class="form-control" id="commentTitle"
                                    required />
                            </div>
                            <div class="mb-3">
                                <label for="commentContent" class="form-label">Content</label>
                                <textarea v-model="newComment.content" class="form-control" id="commentContent" rows="3"
                                    required></textarea>
                            </div>
                            <button type="submit" class="btn btn-primary">Add Comment</button>
                        </form>
                    </div>
                </div>

                <!-- Display Comments -->
                <div v-for="comment in comments" :key="comment.id" class="card my-2">
                    <div class="card-body">
                        <p><strong>{{ comment.title }}:</strong> {{ comment.content }}</p>

                        <!-- Edit and Delete buttons -->
                        <button @click="startEditComment(comment)" class="btn btn-warning btn-sm"
                            style="margin-right: 5px;">Edit</button>
                        <button @click="deleteComment(comment.id)" class="btn btn-danger btn-sm"
                            style="margin-left: 5px;">Delete</button>
                    </div>
                </div>

                <!-- Edit Comment Modal -->
                <div v-if="editComment.id" class="modal" tabindex="-1" style="display: block;" aria-modal="true"
                    role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Edit Comment</h5>
                                <button type="button" class="btn-close" @click="editComment.id = null"
                                    aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <form @submit.prevent="updateComment">
                                    <div class="mb-3">
                                        <label for="editCommentTitle" class="form-label">Title</label>
                                        <input v-model="editComment.title" type="text" class="form-control"
                                            id="editCommentTitle" required />
                                    </div>
                                    <div class="mb-3">
                                        <label for="editCommentContent" class="form-label">Content</label>
                                        <textarea v-model="editComment.content" class="form-control"
                                            id="editCommentContent" rows="3" required></textarea>
                                    </div>
                                    <button type="submit" class="btn btn-success">Update Comment</button>
                                    <button type="button" class="btn btn-secondary ml-2"
                                        @click="editComment.id = null">Cancel</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.card {
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}
.modal {
    position: fixed;
    top: 0;
    left: 0;
    z-index: 1050;
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: rgba(0, 0, 0, 0.5);
}

.modal-dialog {
    max-width: 500px;
    width: 100%;
}

.modal-content {
    background: white;
    border-radius: 0.3rem;
    overflow: hidden;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
}

.modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 1rem;
    border-bottom: 1px solid #dee2e6;
}

.modal-title {
    margin: 0;
}

.modal-body {
    padding: 1rem;
}
</style>
