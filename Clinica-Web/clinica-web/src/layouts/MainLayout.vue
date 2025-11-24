<script setup lang="ts">
import { computed } from 'vue';
import { useAuthStore } from '@/shared/stores/auth.store';
import { useRouter, useRoute, RouterLink } from 'vue-router';
import { 
  Home, CalendarPlus, Stethoscope, Activity, Users, Calendar, FileText, LogOut, UserPlus,
  CalendarPlus2
} from 'lucide-vue-next';

const auth = useAuthStore();
const router = useRouter();
const route = useRoute();

const userRole = computed(() => auth.user?.role || '');

function handleLogout() {
  auth.logout();
  router.push('/login');
}

const isActive = (path: string) => route.path === path;
</script>

<template>
  <div class="flex h-screen bg-gray-50">
    <aside class="w-64 bg-white shadow-lg flex flex-col z-10">
      <div class="p-6 border-b border-gray-100 flex items-center gap-3">
        <div class="bg-blue-600 p-2 rounded-full">
          <activity class="text-white w-6 h-6"/>
        </div>
        <div>
          <h1 class="text-lg font-bold text-gray-800 leading-tight">Clínica Médica</h1>
          <p class="text-xs text-gray-500">Sistema de Gestão</p>
        </div>
      </div>

      <nav class="flex-1 p-4 space-y-1 overflow-y-auto">
        
        <RouterLink to="/" class="flex items-center gap-3 px-4 py-3 rounded-lg transition-colors" :class="isActive('/')? 'bg-blue-600 text-white shadow-md' : 'text-gray-600 hover:text-blue-500 hover:bg-blue-100'">
          <Home class="w-5 h-5"/>
          <span class="font-medium">Início</span>
        </RouterLink>

        <RouterLink v-if="['Admin', 'Secretaria'].includes(userRole)" to="/agendar" class="flex items-center gap-3 px-4 py-3 rounded-lg transition-colors" :class="isActive('/agendar')? 'bg-blue-600 text-white shadow-md' : 'text-gray-600 hover:text-blue-500 hover:bg-blue-100'">
          <CalendarPlus class="w-5 h-5"/>
          <span class="font-medium">Agendar Consulta</span>
        </RouterLink>

        <RouterLink to="/medicos" class="flex items-center gap-3 px-4 py-3 rounded-lg transition-colors" :class="isActive('/medicos')? 'bg-blue-600 text-white shadow-md' : 'text-gray-600 hover:text-blue-500 hover:bg-blue-100'">
          <Stethoscope class="w-5 h-5"/>
          <span class="font-medium">Médicos</span>
        </RouterLink>

        <RouterLink v-if="['Admin', 'Secretaria'].includes(userRole)" to="/especialidades" class="flex items-center gap-3 px-4 py-3 rounded-lg transition-colors" :class="isActive('/especialidades')? 'bg-blue-600 text-white shadow-md' : 'text-gray-600 hover:text-blue-500 hover:bg-blue-100'">
          <UserPlus class="w-5 h-5"/>
          <span class="font-medium">Especialidades</span>
        </RouterLink>

        <RouterLink v-if="['Admin', 'Secretaria'].includes(userRole)" to="/pacientes" class="flex items-center gap-3 px-4 py-3 rounded-lg transition-colors" :class="isActive('/pacientes')? 'bg-blue-600 text-white shadow-md' : 'text-gray-600 hover:text-blue-500 hover:bg-blue-100'"> 
          <Users class="w-5 h-5"/>
          <span class="font-medium">Pacientes</span>
        </RouterLink>

        <RouterLink v-if="userRole === 'Medico'" to="/minha-agenda" class="flex items-center gap-3 px-4 py-3 rounded-lg transition-colors" :class="isActive('/minha-agenda')? 'bg-blue-600 text-white shadow-md' : 'text-gray-600 hover:text-blue-500 hover:bg-blue-100'">
          <FileText class="w-5 h-5"/>
          <span class="font-medium">Minha Agenda</span>
        </RouterLink>

        <RouterLink v-if="userRole === 'Paciente'" to="/consultas" class="flex items-center gap-3 px-4 py-3 rounded-lg transition-colors" :class="isActive('/consultas')? 'bg-blue-600 text-white shadow-md' : 'text-gray-600 hover:text-blue-500 hover:bg-blue-100'">
          <FileText class="w-5 h-5"/>
          <span class="font-medium">Minhas Consultas</span>
        </RouterLink>
      </nav>

      <div>
        <button @click="handleLogout" class="flex items-center gap-3 px-4 py-3 w-full text-red-600 hover:bg-red-50 rounded-lg transition-colors">
            <LogOut class="w-5 h-5"/>
            <span class="font-medium">Sair</span>
        </button>
      </div>
    </aside>

    <main class="flex-1 overflow-auto p-8">
        <header class="mb-8 flex justify-between items-center">
            <div>
                <h2 class="text-2xl font-bold text-gray-800">Sistema de Gestão de Clínica Médica</h2>
                <p class="text-gray-500">Gerencie consultas, médicos e pacientes</p>
            </div>

            <div class="flex items-center gap-3">
                <div class="text-right">
                    <p class="text-sm font-bold text-gray-700">{{ auth.user?.email }}</p>
                    <p class="text-xs text-gray-500 uppercase">{{ userRole }}</p>
                </div>
                <div class="w-10 h-10 bg-blue-100 rounded-full flex items-center justify-center text-blue-700">
                    {{ auth.user?.email[0].toUpperCase() }}
                </div>
            </div>
        </header>

        <RouterView/>
    </main>
  </div>
</template>