<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { api } from '@/shared/services/config/api.config';
import { Plus, Search, Trash2, Stethoscope } from 'lucide-vue-next';
import type { Especialidade } from '@/shared/types/Domain';
import { EspecialidadeService } from '@/shared/services/config/api/especialidade.service';

const especialidades = ref<Especialidade[]>([]);
const loading = ref(false);
const showForm = ref(false);
const novaEspecialidade = ref('');
const filtro = ref('');

async function fetchEspecialidade() {
    try {
        loading.value = true
        especialidades.value = await EspecialidadeService.getAll();

    }
    catch (e) {
        alert('Erro ao carregar especialidades');
    }
    finally {
        loading.value = false;
    }
}

async function handleSalvar() {
    try {
        loading.value = true;

        await EspecialidadeService.create(novaEspecialidade.value);
        showForm.value = false;

        await fetchEspecialidade();
    }
    catch (e) {
        alert('Erro ao criar especialidade');
    }
    finally {
        loading.value = false;
    }
}

// O SEU CÓDIGO ATUAL:
const listaFiltrada = computed(() => {
    return especialidades.value.filter(e => 
        e.nome.toLowerCase().includes(filtro.value.toLowerCase())
    );
});

onMounted(() => {
    fetchEspecialidade();
});
</script>


<template>
    <div>
        <div class="flex justify-between items-center mb-6">
            <div>
                <h2 class="text-2xl font-bold text-gray-800">Especialidades</h2>
                <p class="text-gray-500">Gerencie as áreas de atuação da clínica</p>
            </div>

            <button @click="showForm = !showForm"
                class="flex items-center gap-2 bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition shadow-sm">
                <Plus v-if="!showForm" class="w-5 h-5" />
                <span v-if="!showForm">Nova Especialidade</span>
                <span v-else>Cancelar</span>
            </button>
        </div>

        <div v-if="showForm" class="bg-white p-6 rounded-xl shadow-sm border border-blue-100 mb-6 animate-fade-in-down">
            <h3 class="font-bold text-gray-700 mb-4">Adicionar Nova Especialidade</h3>
            <form @submit.prevent="handleSalvar" class="flex gap-4 items-end">
                <div class="flex-1">
                    <label class="block text-sm font-medium text-gray-700 mb-1"> Nome da Especialidade</label>
                    <input v-model="novaEspecialidade" type="text" placeholder="Ex: Cardiologia, Pediatria..."
                        class="w-full p-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none"
                        required>
                </div>
                <button type="submit" :disabled="loading"
                    class="bg-green-600 text-white px-6 py-2 rounded-lg hover:bg-green-700 transition disabled:opacity-50">
                    {{ loading ? 'Salvando...' : 'Salvar' }}
                </button>
            </form>
        </div>

        <div class="mb-4 relative">
            <Search class="w-5 h-5 absolute left-3 top-3" />
            <input v-model="filtro" type="text" placeholder="Buscar especialidade..."
                class="w-full pl-10 p-2.5 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none" />
        </div>

        <div class="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
            <table class="min-w-full divide-y divide-gray-200">
                <thead class="bg-gray-50">
                    <tr>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Nome
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ações
                        </th>
                    </tr>
                </thead>
                <tbody class="bg-white divide-y divide-gray-200">
                    <tr v-for="item in listaFiltrada" :key="item.id" class="hover:bg-gray-50 transition">
                        <td class="px-6 py-4 whitespace-nowrap flex items-center gap-3">
                            <div class="bg-blue-50 p-2 rounded-full text-blue-600">
                                <Stethoscope class="w-4 h-4" />
                            </div>
                            <span class="text-sm font-medium text-gray-900">{{ item.nome }}</span>
                        </td>
                        <td class="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                            <button class="text-red-400 hover:text-red-600 transition" title="Excluit (Em breve)">
                                <Trash2 class="w-4 h-4" />
                            </button>
                        </td>
                    </tr>
                    <tr v-if="listaFiltrada.length === 0">
                        <td colspan="2" class="px-6 py-8 text-gray-500">
                            Nenhuma especialidade encontrada.
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<style scoped>
.animate-fade-in-down {
    animation: fadeInDown 0.3s ease-out;
}

@keyframes fadeInDown {
    from {
        opacity: 0;
        transform: translateY(-10px);
    }

    to {
        opacity: 1;
        transform: translateY(0);
    }
}
</style>