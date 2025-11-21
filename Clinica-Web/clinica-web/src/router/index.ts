import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/pages/HomePage.vue';
import LoginPage from '@/pages/LoginPage.vue';
import path from 'path';
import RegisterPage from '@/pages/RegisterPage.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
    },

    {
      path: '/login',
      name: 'login',
      component: LoginPage
    },
    
    {
      path: '/register',
      name: 'register',
      component: RegisterPage
    }
  ],
});

export default router;
