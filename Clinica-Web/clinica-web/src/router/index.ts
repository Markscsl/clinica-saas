import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/pages/HomePage.vue';
import LoginPage from '@/pages/LoginPage.vue';
import path from 'path';
import RegisterPage from '@/pages/RegisterPage.vue';
import MainLayout from '@/layouts/MainLayout.vue';
import HomePage from '@/pages/HomePage.vue';
import AgendarPage from '@/pages/AgendarPage.vue';
import MinhasConsultasPage from '@/pages/MinhasConsultasPage.vue';
import EsqueciSenhaPage from '@/pages/EsqueciSenhaPage.vue';
import RedefinirSenhaPage from '@/pages/RedefinirSenhaPage.vue';
import EspecialidadesPage from '@/pages/EspecialidadesPage.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/login',
      name: 'login',
      component: LoginPage
    },

    {
      path: '/register',
      name: 'register',
      component: RegisterPage
    },

    {
      path: '/',
      component: MainLayout,

      children: [
        {
          path: '',
          name: 'home',
          component: HomePage
        },

        {
          path: 'agendar',
          name: 'agendar',
          component: AgendarPage
        },

        {
          path: 'minhas-consultas',
          name: 'minhas-consultas',
          component: MinhasConsultasPage
        },

        {
          path: '/esqueci-senha',
          component: EsqueciSenhaPage
        },

        {
          path: '/redefinir-senha',
          component: RedefinirSenhaPage
        },

        {
          path: 'especialidades',
          name: 'especialidades',
          component: EspecialidadesPage,
          meta: { roleRequired: 'Admin' }
        }
      ]
    }
  ],
});

export default router;
