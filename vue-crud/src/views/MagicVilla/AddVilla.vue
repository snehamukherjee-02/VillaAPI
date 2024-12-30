<script setup>
import axios from 'axios';
import { reactive, ref } from "vue";
import { useRouter } from 'vue-router';

let createVilla = reactive({
    name: "",
    details: "",
    rate: "",
    sqft: null,
    occupency: null,
    imageurl: "",
    amenity: ""
});

const file = ref(null);
const router = useRouter();

const handleFileChange = (event) => {
    file.value = event.target.files[0];
};

const addvilla = async () => {
    try {

        if (file.value) {
            const formData = new FormData();
            formData.append("file", file.value);

            const uploadResponse = await axios.post("https://localhost:7090/api/VillaAPI/upload-image", formData, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            });

            createVilla.imageurl = uploadResponse.data.url;
        }

        await axios.post("https://localhost:7090/api/VillaAPI", createVilla);
        alert("Villa created successfully!");
        router.push("/");
    } catch (error) {
        console.error("Error:", error.response || error);
        alert("An error occurred. Please try again.");
    }
};
</script>

<template>
    <div class="container mt-4">
        <form @submit.prevent="addvilla">
            <div class="border p-3 m-4">
                <div class="row pb-2">
                    <h2>Create Villa</h2>
                    <hr />
                </div>
                <div class="mb-3">
                    <label for="lblvillaname" class="form-label text-primary-emphasis">Villa Name</label>
                    <input type="text" v-model="createVilla.name" class="form-control" id="lblvillaname"
                        aria-describedby="villaname" required>
                </div>

                <div class="mb-3">
                    <label for="lbldetails" class="form-label text-primary-emphasis">Details</label>
                    <input type="text" v-model="createVilla.details" class="form-control" id="lbldetails"
                        aria-describedby="details">
                </div>

                <div class="mb-3">
                    <label for="lblrate" class="form-label text-primary-emphasis">Rate</label>
                    <input type="text" v-model="createVilla.rate" class="form-control" id="lblrate"
                        aria-describedby="rate" required>
                </div>

                <div class="mb-3">
                    <label for="lblsquarefeet" class="form-label text-primary-emphasis">Square Feet</label>
                    <input type="text" v-model="createVilla.sqft" class="form-control" id="lblsquarefeet"
                        aria-describedby="squarefeet">
                </div>

                <div class="mb-3">
                    <label for="lbloccupency" class="form-label text-primary-emphasis">Occupency</label>
                    <input type="text" v-model="createVilla.occupency" class="form-control" id="lbloccupency"
                        aria-describedby="occupency">
                </div>

                <div class="mb-3">
                    <label for="lblimgurl" class="form-label text-primary-emphasis">Image URL</label>
                    <input type="file" @change="handleFileChange" class="form-control" id="lblimgurl"
                        aria-describedby="imgurl" accept="image/png, image/jpg, image/jpeg">
                </div>

                <div class="mb-3">
                    <label for="lblamenity" class="form-label text-primary-emphasis">Amenity</label>
                    <input type="text" v-model="createVilla.amenity" class="form-control" id="lblamenity"
                        aria-describedby="amenity">
                </div>

                <div class="row">
                    <div class="col-6 col-md-3">
                        <button type="submit" class="btn btn-primary form-control">Create</button>
                    </div>
                    <div class="col-6 col-md-3">
                        <RouterLink class="btn btn-primary form-control" to="/">Back To Home</RouterLink>
                    </div>
                </div>
            </div>
        </form>
    </div>
</template>