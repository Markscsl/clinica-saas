<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { Plus, Search, Stethoscope, UserRoundMinus } from 'lucide-vue-next';
import type { Medico, Especialidade } from '@/shared/types/Domain';
import { EspecialidadeService } from '@/shared/services/config/api/especialidade.service';
import { MedicoService } from '@/shared/services/config/api/medico.service';

const medicos = ref<Medico[]>([]);
const especialidades = ref<Especialidade[]>([]);
const loading = ref(false);
const showForm = ref(false);
const filtro = ref('');

const form = ref({
    nome: '',
    crm: '',
    especialidadeId: ''
});

async function fetchData() {
    try {
        loading.value = true;

        const [medicosData, espData] = await Promise.all([
            MedicoService.getAll(),
            EspecialidadeService.getAll()
        ]);

        medicos.value = medicosData;
        especialidades.value = espData;

    } catch (e) {
        alert('Erro ao carregar dados.');
    } finally {
        loading.value = false;
    }
}

async function handleSalvar() {
    if (!form.value.nome || !form.value.crm || !form.value.especialidadeId)
        return;

    try {
        loading.value = true;

        await MedicoService.create(form.value);

        form.value = {
            nome: '',
            crm: '',
            especialidadeId: ''
        };

        showForm.value = false;

        medicos.value = await MedicoService.getAll();
        alert('Médico cadastrado com sucesso.');

    } catch (e: any) {
        alert('Erro ao criar médico: ' + (e.response?.data || 'Erro desconhecido'));
    } finally {
        loading.value = false;
    }
}

// Adicione 'computed' se não estiver importado no topo
// import { ref, onMounted, computed } from 'vue';

const listaFiltrada = computed(() => { // <-- USE COMPUTED
    return medicos.value.filter(m => 
        m.nome.toLowerCase().includes(filtro.value.toLowerCase()) ||
        m.crm.includes(filtro.value)
    );
});

onMounted(() => {
    fetchData();
});
</script>

<template>
    <div>
        <div class="flex justify-between items-center gap-6">
            <div>
                <h2 class="text-2xl font-bold text-gray-800">Corpo Clínico</h2>
                <p class="text-gray-500">Gerencie o médico e suas especialidades</p>
            </div>

            <button @click="showForm = !showForm"
                class="flex items-center gap-2 bg-blue-600 px-4 py-2 text-white rounded-lg hover:bg-blue-700 transition shadow-sm">
                <Plus v-if="!showForm" class="w-5 h-5" />
                <span v-if="!showForm">Novo Médico</span>
                <span v-else>Cancelar</span>
            </button>
        </div>

        <div v-if="showForm" class="bg-white p-6 rounded-xl shadow-sm border border-blue-100 mb-6 animate-fade-in-down">
            <h3 class="font-bold text-gray-700 mb-4">Adicionar Novo Médico</h3>

            <form @submit.prevent="handleSalvar" class="grid grid-cols-1 md:grid-cols-4 gap-4 items-end">

                <div class="md:col-span-1">
                    <label class="block text-sm font-medium text-gray-700 mb-1">Nome Completo</label>
                    <input v-model="form.nome" placeholder="Dr. Fulano" required type="text"
                        class="w-full p-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none">
                </div>

                <div class="md:col-span-1">
                    <label class="block text-sm font-medium text-gray-700 mb-1">CRM</label>
                    <input v-model="form.crm" placeholder="12345/SP" required type="text"
                        class="w-full p-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none">
                </div>

                <div class="md:col-span-1">
                    <label class="block text-sm font-medium text-gray-700 mb-1">Especialidade</label>
                    <select v-model="form.especialidadeId" required
                        class="w-full p-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none bg-white">
                        <option value="" disabled>Selecione...</option>
                        <option v-for="esp in especialidades" :key="esp.id" :value="esp.id">
                            {{ esp.nome }}
                        </option>
                    </select>
                </div>

                <div class="md:col-span-1">
                    <button type="submit" :disabled="loading"
                        class="w-full bg-gray-600 text-white px-4 py-2 rounded-lg hover:bg-green-700 transition disabled:opacity-50 h-[42px]">
                        {{ loading ? 'Salvando' : 'Salvar Médico' }}
                    </button>
                </div>
            </form>
        </div>

        <div class="mb-4 relative">
            <Search class="w-5 h-5 absolute left-3 top-3 text-gray-400" />
            <input v-model="filtro" type="text" placeholder="Buscar por nome ou CRM..."
                class="w-full pl-10 p-2.5 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none">
        </div>

        <div class="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
            <table class="min-w-full divide-y divide-gray-200">
                <thead class="bg-gray-50">
                    <tr>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Nome
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">CRM
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Especialidade</th>
                    </tr>
                </thead>
                <tbody class="bg-white divide-y divide-gray-200">
                    <tr v-for="medico in listaFiltrada" :key="medico.id" class="hover:bg-gray-50 transition">
                        <td class="px-6 py-4 whitespace-nowrap flex items-center gap-3">
                            <div class="bg-blue-50 p-2 rounded-full text-blue-600">
                                <UserRoundMinus class="w-4 h-4" />
                            </div>
                            <span class="text-sm font-medium text-gray-900">
                                {{ medico.nome }}
                            </span>
                        </td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{{ medico.crm }}</td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                            <span class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-blue-100 text-blue-800">
                                {{ medico.nomeEspecialidade || 'N/A' }}
                            </span>
                        </td>
                    </tr>
                    <tr v-if="listaFiltrada.length === 0">
                        <td colspan="3" class="px-6 py-8 text-center text-gray-500">Nenhum médico encontrado.</td>
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
    from { opacity: 0; transform: translateY(-10px); }
    to { opacity: 1; transform: translateY(0); }
}
</style>