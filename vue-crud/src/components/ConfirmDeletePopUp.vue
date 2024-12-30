<script setup>
</script>

<template>
    <div class="container">
        <div class="modal fade" id="DeletePopup" tabindex="-1" aria-labelledby="DeletePopupLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="DeletePopupLabel">Delete Confirmation</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        Are you sure you want to delete this item?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" @click="$emit('confirm-delete-click')" class="btn btn-danger">Confirm Delete</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
