<script setup>
import { RouterLink, RouterView } from 'vue-router'

</script>

<style>
.active-tab {
  color: #ff5722;
  /* Highlight color for the active tab */
  font-weight: bold;
  /* Optional: Makes the active tab bold */
  border-bottom: 2px solid #ff5722;
  /* Optional: Adds a bottom border */
}
</style>

<template>

  <nav class="navbar bg-dark border-bottom border-body" data-bs-theme="dark">
    <div class="container-fluid d-flex justify-content-between align-items-center">
      <a class="navbar-brand text-white" href="#">Rent a Villa</a>
      <ul class="navbar-nav d-flex flex-row">
        <li class="nav-item">
          <RouterLink class="nav-link text-white mx-2" to="/" active-class="active-tab">
            Home
          </RouterLink>
        </li>
        <li class="nav-item">
          <RouterLink class="nav-link text-white mx-2" to="/add" active-class="active-tab">
            Create
          </RouterLink>
        </li>
        <li class="nav-item">
          <RouterLink class="nav-link text-white mx-2" to="/edit-delete" active-class="active-tab">
            Edit / Delete
          </RouterLink>
        </li>
      </ul>
    </div>
  </nav>

  <div class="container">
    <RouterView />
  </div>

  <footer class="border-top footer bg-primary text-muted" style="height: 80px;">
    <div class="text-center text-light pt-4">
      Made with <i class="bi bi-heart-fill"></i> by Sneha
    </div>
  </footer>

</template>
