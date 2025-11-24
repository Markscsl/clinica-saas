<script setup lang="ts">
import { useAuthStore } from '@/shared/stores/auth.store';
import { CalendarPlus, Stethoscope, Users, Activity } from 'lucide-vue-next';

const auth = useAuthStore();
</script>

<template>
  <div>

    <section class="bg-white rounded-xl shadow-sm border border-gray-100 p-8">
      <h3 class="text-2xl font-bold text-gray-800 mb-2">Bem-vindo ao Sistema de Gestão</h3>
      <p class="text-gray-500 mb-6">Gerencie consultas, médicos, especialidades e pacientes de forma eficiente.</p>
      
      <div class="bg-blue-50 border border-blue-100 rounded-lg p-6 flex items-start gap-4">
        <div class="bg-blue-600 p-2 rounded-full mt-1">
          <Activity class="text-white w-5 h-5" />
        </div>
        <div>
          <div class="mb-6">
            <h4 class="font-bold text-xl text-gray-800">Sistema de Gestão de Clínica Médica</h4>
            <p class="text-gray-600 text-sm">Plataforma completa para gerenciamento</p>
          </div>
          <p class="text-gray-600 text-sm mt-2">Use a navegação lateral para acessar as diferentes seções do sistema e gerenciar todas as operações da clínica.</p>
        </div>
      </div>
    </section>

    <section>
      <h3 class="text-lg font-bold text-gray-800 mb-4 mt-4">Ações Rápidas</h3>

      <div class="grid grid-cols-1 md:grid-cols-3 gap-6">

        <RouterLink v-if="auth.isAdmin || auth.user?.role === 'Secretaria'" to="/agendar" 
          class="bg-white p-6 rounded-xl shadow-sm border border-gray-100 hover:shadow-md transition-shadow group">
          <div class="bg-blue-50 w-12 h-12 rounded-lg flex items-center justify-center mb-4 group-hover:bg-blue-600 transition-colors">
            <CalendarPlus class="text-blue-600 w-6 h-6 group-hover:text-white transition-colors" />
          </div>
          <h4 class="font-bold text-gray-800 mb-2">Agendar Consulta</h4>
          <p class="text-sm text-gray-500">Agende uma nova consulta para um paciente</p>
        </RouterLink>

        <RouterLink v-if="auth.isAdmin" to="/medicos" class="bg-white p-6 rounded-xl shadow-sm border border-gray-100 hover:shadow-md transition-shadow group">
          <div class="bg-blue-50 w-12 h-12 rounded-lg flex items-center justify-center mb-4 group-hover:bg-blue-600 transition-colors">
            <Stethoscope class="text-blue-600 w-6 h-6 group-hover:text-white transition-colors" />
          </div>
          <h4 class="font-bold text-gray-800 mb-2">Gerenciar Médicos</h4>
          <p class="text-sm text-gray-500">Adicione o visualize médicos.</p>
        </RouterLink>

        <RouterLink to="/pacientes" v-if="auth.isAdmin || auth.user?.role === 'Secretaria'" 
          class="bg-white p-6 rounded-xl shadow-sm border border-gray-100 hover:shadow-md transition-shadow group">
          <div class="bg-blue-50 w-12 h-12 rounded-lg flex items-center justify-center mb-4 group-hover:bg-blue-600 transition-colors"> 
            <Users class="text-blue-600 w-6 h-6 group-hover:text-white transition-colors"/>
          </div>
          <h4 class="font-bold text-gray-800 mb-2">Gerenciar Pacientes</h4>
          <p class="text-sm text-gray-500">Visualize todos os pacientes cadastrados.</p>
        </RouterLink>
      </div>
    </section>
  </div>
</template>