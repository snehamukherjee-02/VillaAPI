import { createRouter, createWebHistory } from 'vue-router'
import VillaList from '../views/MagicVilla/VillaList.vue'
import AddVilla from '../views/MagicVilla/AddVilla.vue'
import EditVilla from '@/views/MagicVilla/EditVilla.vue'
import EditorDelete from '@/views/MagicVilla/EditorDelete.vue'
import VillaDetail from '@/views/MagicVilla/VillaDetail.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: VillaList,
    },
    {
      path: '/add',
      name: 'addvilla',
      component: AddVilla,
    },
    {
      path: '/edit/:id', // Dynamic route for villa details
      name: 'editvilla',
      component: EditVilla,
    },
    {
      path: '/edit-delete',
      name: 'edit_del',
      component: EditorDelete,
    },
    {
      path: "/villa/:id", // Dynamic route for villa details
      name: "VillaDetail",
      component: VillaDetail,
  },
  ],
})

export default router
