<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { api } from '@/shared/services/config/api.config'; // Lembre-se das chaves {}
import { Calendar, Clock, User } from 'lucide-vue-next';

interface Consulta {
  id: string;
  data: string;
  nomeMedico: string;
  nomeEspecialidade: string;
  nomePaciente: string;
  status: string;
}

const consultas = ref<Consulta[]>([]);
const loading = ref(true);

// Função para formatar data bonita (ex: 25/11/2025 às 14:30)
const formatDate = (dateString: string) => {
  return new Date(dateString).toLocaleString('pt-BR', {
    day: '2-digit', month: '2-digit', year: 'numeric',
    hour: '2-digit', minute: '2-digit'
  });
}

const getStatusColor = (status: string) => {
  switch(status) {
    case 'Agendada': return 'bg-blue-100 text-blue-800';
    case 'Realizada': return 'bg-green-100 text-green-800';
    case 'Cancelada': return 'bg-red-100 text-red-800';
    default: return 'bg-gray-100 text-gray-800';
  }
}

onMounted(async () => {
  try {
    const response = await api.get('/api/consultas/minhas');
    consultas.value = response.data;
  } catch (e) {
    console.error(e);
  } finally {
    loading.value = false;
  }
});
</script>

<template>
  <div>
    <h2 class="text-2xl font-bold text-gray-800 mb-6">Meus Agendamentos</h2>

    <div v-if="loading" class="text-gray-500">Carregando...</div>

    <div v-else-if="consultas.length === 0" class="text-center py-10 bg-white rounded-lg shadow-sm">
      <p class="text-gray-500 mb-4">Você não tem consultas agendadas.</p>
      <RouterLink to="/agendar" class="text-blue-600 hover:underline font-medium">
        Agendar agora
      </RouterLink>
    </div>

    <div v-else class="grid gap-4">
      <div v-for="consulta in consultas" :key="consulta.id" 
           class="bg-white p-6 rounded-xl shadow-sm border border-gray-100 flex justify-between items-center">
        
        <div class="flex gap-4">
          <div class="bg-blue-50 w-12 h-12 rounded-lg flex items-center justify-center text-blue-600">
            <Calendar class="w-6 h-6" />
          </div>

          <div>
            <h4 class="font-bold text-gray-800 text-lg">{{ consulta.nomeEspecialidade }}</h4>
            <p class="text-gray-500 flex items-center gap-2 text-sm mt-1">
              <User class="w-4 h-4" /> Dr(a). {{ consulta.nomeMedico }}
            </p>
            <p class="text-gray-500 flex items-center gap-2 text-sm mt-1">
              <Clock class="w-4 h-4" /> {{ formatDate(consulta.data) }}
            </p>
          </div>
        </div>

        <div>
          <span :class="['px-3 py-1 rounded-full text-xs font-bold uppercase tracking-wider', getStatusColor(consulta.status)]">
            {{ consulta.status }}
          </span>
        </div>

      </div>
    </div>
  </div>
</template>