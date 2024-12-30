<script setup>
import { onMounted, ref } from "vue";
import axios from "axios";
import ConfirmDeletePopUp from "@/components/ConfirmDeletePopUp.vue";

const villaList = ref([]);
const deleteitemid = ref(0);

let deleteModalInstance = null;

onMounted(() => {
    const modalElement = document.getElementById("DeletePopup");
    if (modalElement) {
        deleteModalInstance = new window.bootstrap.Modal(modalElement);
    } else {
        console.error("Modal element not found.");
    }

    axios
        .get("https://localhost:7090/api/VillaAPI")
        .then((response) => {
            villaList.value = response.data;
        })
        .catch((error) => {
            console.error("Error fetching data:", error);
        });
});

const openDeleteModal = (id) => {
    deleteitemid.value = id;
    if (deleteModalInstance) {
        deleteModalInstance.show();
    } else {
        console.error("deleteModalInstance is not initialized.");
    }
};

const confirmDelete = () => {
    axios
        .delete(`https://localhost:7090/api/VillaAPI/${deleteitemid.value}`)
        .then(() => {
            villaList.value = villaList.value.filter(
                (item) => item.id !== deleteitemid.value
            );
            if (deleteModalInstance) {
                deleteModalInstance.hide();
            }
        })
        .catch((error) => {
            console.error("Error deleting item:", error);
        });
};
</script>

<style>
.btn-primary:hover {
    background-color: #0056b3;
    color: white;
}

.btn-danger:hover {
    background-color: #b30000;
    color: white;
}
</style>

<template>
    <div class="container">
        <div class="row pt-2 pb-3">
            <div class="col-6">
                <h2 class="text-primary">
                    <u>Edit or Delete Villa</u>
                </h2>
            </div>
        </div>
        <div class="row row-cols-1 row-cols-md-3 g-4 pt-3 pb-5">
            <div class="col" v-for="item in villaList" :key="item.id">
                <div class="card">
                    <img :src="item.imageurl" class="card-img-top" alt="...">
                    <div class="row justify-content-center my-3">
                        <div class="col-6 text-center">
                            <RouterLink class="btn btn-primary btn-sm w-100" :to="`/edit/${item.id}`">
                                <i class="bi bi-pencil-square me-1"></i> Edit
                            </RouterLink>
                        </div>
                        <div class="col-6 text-center">
                            <button type="button" class="btn btn-danger btn-sm w-100" @click="openDeleteModal(item.id)">
                                <i class="bi bi-trash-fill me-1"></i> Delete
                            </button>
                        </div>
                    </div>
                    <div class="card-body">
                        <h4 class="card-title text-primary">{{ item.name }}</h4>
                        <p class="card-text">
                            <table style="border-collapse: collapse; width: 100%;">
                                <tr>
                                    <th valign="top" style="width:30%">Details :</th>
                                    <td>{{ item.details }}</td>
                                </tr>
                                <tr>
                                    <th style="width:30%">Rate :</th>
                                    <td>{{ item.rate }}</td>
                                </tr>
                                <tr>
                                    <th style="width:40%; text-align:left">Square feet :</th>
                                    <td>{{ item.sqft }}</td>
                                </tr>
                                <tr>
                                    <th style="width:30%">Occupency :</th>
                                    <td>{{ item.occupency }}</td>
                                </tr>
                                <tr>
                                    <th style="width:30%">Amenity :</th>
                                    <td>{{ item.amenity }} (if any)</td>
                                </tr>
                            </table>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <ConfirmDeletePopUp @confirm-delete-click="confirmDelete" />
    </div>
</template>
