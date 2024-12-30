<script setup>
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import axios from 'axios';

const villaList = ref([]);

const router = useRouter(); 

onMounted(() => {
    axios.get("https://localhost:7090/api/VillaAPI")
        .then((response) => {
            villaList.value = response.data;
        })
});

const goToVillaDetail = (id) => {
    router.push(`/villa/${id}`);
};

</script>

<template>
    <div class="container">
        <div class="row pt-2 pb-3">
            <div class="col-6">
                <h2 class="text-primary">
                    <u>Villa List</u>
                </h2>
            </div>
            <!--<div class="col-6 text-end">
                <RouterLink class="btn btn-primary" to="/add">
                    <i class="bi bi-plus-circle"></i> Add A New Villa
                </RouterLink>
            </div>-->
        </div>
        <div class="row row-cols-1 row-cols-md-3 g-4 pt-3 pb-5">
            <div class="col" v-for="item in villaList" v-bind:key="item.id" @click="goToVillaDetail(item.id)" style="cursor: pointer;">
                <div class="card">
                    <img v-bind:src="item.imageurl" class="card-img-top" alt="...">
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
                        <!--<RouterLink class="btn btn-primary" :to="`/edit/${item.id}`"><i class="bi bi-pencil-square"></i> Edit</RouterLink>-->
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.card {
    cursor: pointer;
}

.card:hover {
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}
</style>