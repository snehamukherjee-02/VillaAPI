<script setup>
import { onMounted, reactive, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import axios from 'axios';

let editVilla = reactive({
    id: "",
    name: "",
    details: "",
    rate: "",
    sqft: null,
    occupency: null,
    imageurl: "",
    amenity: ""
});

const route = useRoute(); // to read the data from the route

const router = useRouter(); // for navigation from edit page to home page

const file = ref(null);
const handleFileChange = (event) => {
    file.value = event.target.files[0];
};

onMounted(() => {
    axios.get(`https://localhost:7090/api/VillaAPI/${route.params.id}`)
        .then((response) => {
            editVilla.id = response.data.id;
            editVilla.name = response.data.name;
            editVilla.details = response.data.details;
            editVilla.rate = response.data.rate;
            editVilla.sqft = response.data.sqft;
            editVilla.occupency = response.data.occupency;
            editVilla.imageurl = response.data.imageurl;
            editVilla.amenity = response.data.amenity;
        });
});

const updateVilla = async () => {
    try {
        // Check if a file is selected for upload
        if (file.value) {
            const formData = new FormData();
            formData.append("file", file.value);

            // Upload the file to get the image URL
            const uploadResponse = await axios.put("https://localhost:7090/api/VillaAPI/upload-image", formData, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            });

            // Update imageURL in the villa object
            editVilla.imageurl = uploadResponse.data.url;
        }

        // Update the villa details
        await axios.put(`https://localhost:7090/api/VillaAPI/${editVilla.id}`, editVilla);
        alert("Villa updated successfully!");
        router.push("/edit-delete");
    } catch (error) {
        console.error("Error:", error.response || error);
        alert("An error occurred. Please try again.");
    }
};
</script>

<template>
    <div class="container mt-4">
        <form @submit.prevent="updateVilla">
            <div class="border p-3 m-4">
                <div class="row pb-2">
                    <h2>Edit Villa</h2>
                    <hr />
                </div>
                <div class="mb-3">
                    <label for="lblvillaname" class="form-label text-primary-emphasis">Villa Name</label>
                    <input type="text" v-model="editVilla.name" class="form-control" id="lblvillaname"
                        aria-describedby="villaname" required>
                </div>

                <div class="mb-3">
                    <label for="lbldetails" class="form-label text-primary-emphasis">Details</label>
                    <input type="text" v-model="editVilla.details" class="form-control" id="lbldetails"
                        aria-describedby="details">
                </div>

                <div class="mb-3">
                    <label for="lblrate" class="form-label text-primary-emphasis">Rate</label>
                    <input type="text" v-model="editVilla.rate" class="form-control" id="lblrate"
                        aria-describedby="rate" required>
                </div>

                <div class="mb-3">
                    <label for="lblsquarefeet" class="form-label text-primary-emphasis">Square Feet</label>
                    <input type="text" v-model="editVilla.sqft" class="form-control" id="lblsquarefeet"
                        aria-describedby="squarefeet" required>
                </div>

                <div class="mb-3">
                    <label for="lbloccupency" class="form-label text-primary-emphasis">Occupency</label>
                    <input type="text" v-model="editVilla.occupency" class="form-control" id="lbloccupency"
                        aria-describedby="occupency" required>
                </div>

                <div class="mb-3">
                    <label for="lblcurrentimgurl" class="form-label text-primary-emphasis">Current Image URL</label>
                    <input type="text" v-model="editVilla.imageurl" class="form-control" id="lblcurrentimgurl" readonly>
                </div>

                <div class="mb-3">
                    <label for="lblimgurl" class="form-label text-primary-emphasis">Upload New Image</label>
                    <input type="file" @change="handleFileChange" class="form-control" id="lblimgurl"
                        aria-describedby="imgurl" accept="image/png, image/gif, image/jpeg">
                </div>

                <div class="mb-3">
                    <label for="lblamenity" class="form-label text-primary-emphasis">Amenity</label>
                    <input type="text" v-model="editVilla.amenity" class="form-control" id="lblamenity"
                        aria-describedby="amenity">
                </div>

                <div class="row">
                    <div class="col-6 col-md-3">
                        <button type="submit" class="btn btn-primary form-control">Update</button>
                    </div>
                    <div class="col-6 col-md-3">
                        <RouterLink class="btn btn-primary form-control" to="/">Back To Home</RouterLink>
                    </div>
                </div>
            </div>
        </form>
    </div>
</template>