<script setup lang="ts">
import { api } from '@/shared/services/config/api.config';
import { ref, onMounted, computed, watch } from 'vue';
import { useRouter } from 'vue-router';
import { ChevronRight, ChevronLeft, Check, CalendarPlus } from 'lucide-vue-next';

const router = useRouter()

const currentStep = ref(1)
const loading = ref(false)
const errorMsg = ref('')

const selectedEspecialidade = ref('')
const selectedMedico = ref('')
const selectedPaciente = ref('')
const dataHora = ref('')

const especialidades = ref<{ id: string, nome: string }[]>([])
const medicos = ref<{ id: string, nome: string }[]>([])
const pacientes = ref<{ id: string, nome: string, cpf: string }[]>([])

onMounted(async () => {
    try {
        loading.value = true

        const responseEsp = await api.get('/api/especialidades')
        especialidades.value = responseEsp.data

        const responsePac = await api.get('/api/pacientes')
        pacientes.value = responsePac.data

    } catch (error) {

        errorMsg.value = 'Erro ao carregar dados iniciais.'

    } finally {

        loading.value = false

    }

})

watch(selectedEspecialidade, async (newId) => {
    if (!newId) return

    selectedMedico.value = ''

    try {

        loading.value = true

        const response = await api.get(`/api/medicos?especialidadeId=${newId}`)
        medicos.value = response.data

    } catch (error) {

        console.error(error)

    } finally {

        loading.value = false

    }
})

function nextStep() {
    if (currentStep.value === 1 && !selectedEspecialidade.value) return alert('Selecione uma especialidade')
    if (currentStep.value === 2 && !selectedMedico.value) return alert('Selecione um médico')
    if (currentStep.value === 3 && !selectedPaciente.value) return alert('Selecione um paciente')

    if (currentStep.value < 4) {
        currentStep.value++
    }
}

function prevStep() {
    if (currentStep.value > 1) {
        currentStep.value--
    }
}

async function finalizarAgendamento() {
    if (!dataHora.value) return alert('Selecione a data e hora')

    try {
        loading.value = true

        const payload = {
            pacienteId: selectedPaciente.value,
            medicoId: selectedMedico.value,
            dataHoraInicio: dataHora.value
        }

        await api.post('/api/consultas/agendar', payload)

        alert('Consulta agendada com sucesso!')
        router.push('/')

    } catch (e: any) {

        alert('Erro ao agendar: ' + (e.response?.data || 'Tente novamente.'))

    } finally {

        loading.value = false

    }
}

const stepTitle = computed(() => {
    switch (currentStep.value) {
        case 1: return 'Selecione a especialidade médica'
        case 2: return 'Escolha o médico disponível'
        case 3: return 'Identifique o paciente'
        case 4: return 'Defina a data e hora'
        default: return ''
    }
})
</script>

<template>
    <div class="max-w-4xl mx-auto">
        <div class="mb-8">
            <h2 class="text-2xl font-bold text-gray-800">Agendar Consulta</h2>
            <p class="text-gray-500">Siga os passos para agendar uma nova consulta</p>
        </div>

        <div class="bg-white rounded-xl shadow-sm border border-gray-100 p-8 min-h-[400px] flex flex-col">

            <div class="flex items-center justify-center mb-8">
                <div v-for="step in 4" :key="step" class="flex items-center">
                    <div class="w-10 h-10 rounded-full flex items-center justify-center font-bold text-sm transition-colors duration-300"
                        :class="step <= currentStep ? 'bg-blue-600 text-white' : 'bg-gray-200 text-gray-500'">
                        {{ step }}
                    </div>

                    <div v-if="step < 4" class="w-16 h-1 bg-gray-200 mx-2"
                        :class="step < currentStep ? 'bg-blue-600' : 'bg-gray-200'">
                    </div>
                </div>
            </div>

            <div class="mb-6">
                <div class="flex items-center gap-2 mb-1">
                    <CalendarPlus class="w-5 h-5 text-blue-600" />
                    <span class="font-bold text-gray-700">Passo {{ currentStep }} de 4</span>
                </div>
                <p class="text-gray-500">{{ stepTitle }}</p>
            </div>

            <div class="flex-1">
                <div v-if="currentStep === 1">
                    <label class="block text-sm font-medium text-gray-700 mb-2">Especialidade Médica</label>
                    <select v-model="selectedEspecialidade"
                        class="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none">
                        <option value="" disabled>Selecione uma especialidade</option>
                        <option v-for="esp in especialidades" :key="esp.id" :value="esp.id">{{ esp.nome }}</option>
                    </select>
                </div>

                <div v-if="currentStep === 2">
                    <label class="block text-sm font-medium text-gray-700 mb-2">Médico</label>
                    <select v-model="selectedMedico"
                        class="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none">
                        <option value="" disabled>Selecione um médico</option>
                        <option v-for="med in medicos" :key="med.id" :value="med.id">
                            {{ med.nome }} - CRM: {{ med.crm }}
                        </option>
                    </select>
                    <p v-if="medicos.length === 0 && !loading" class="text-sm text-red-500 mt-2">Nenhum médico
                        encontrado para esta especialidade.</p>
                </div>

                <div v-if="currentStep === 3">
                    <label class="block text-sm font-medium text-gray-700 mb-2">Paciente</label>
                    <select v-model="selectedPaciente"
                        class="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none">
                        <option value="" disabled>Selecione um paciente</option>
                        <option v-for="pac in pacientes" :key="pac.id" :value="pac.id">{{ pac.nome }} (CPF: {{ pac.cpf
                        }})</option>
                    </select>
                </div>

                <div v-if="currentStep === 4">
                    <label class="block text-sm font-medium text-gray-700 mb-2">Data e Hora da Consulta</label>
                    <input type="datetime-local" v-model="dataHora"
                        class="w-full p-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none mb-6">

                    <div class="bg-gray-50 p-4 rounded-lg border border-gray-200">
                        <h4 class="font-bold text-gray-700 m-2">Resumo do Agendamento</h4>
                        <ul class="text-sm text-gray-600 space-y-1">
                            <li><strong>Médico:</strong> {{medicos.find(m => m.id === selectedMedico)?.nome}} </li>
                            <li><strong>Especialidade:</strong> {{especialidades.find(e => e.id ===
                                selectedEspecialidade)?.nome}}</li>
                            <li><strong>Paciente:</strong> {{pacientes.find(p => selectedPaciente)?.nome}}</li>
                            <li><strong>Data:</strong>{{ dataHora ? new Date(dataHora).toLocaleString('pt-BR') : '...'
                            }}
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <div class="flex justify-between mt-8 pt-6 border-t border-gray-100">
                <button v-if="currentStep > 1" @click="prevStep"
                    class="flex items-center gap-2 px-6 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 transition">
                    <ChevronLeft class="w-4 h-4" /> Voltar
                </button>

                <div v-else></div>
                <button v-if="currentStep < 4" @click="nextStep"
                    class="flex items-center gap-2 px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition">
                    Próximo
                    <ChevronRight class="w-4 h-4" />
                </button>

                <button v-else @click="finalizarAgendamento" :disabled="loading"
                    class="flex items-center gap-2 px-6 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition">
                    <Check class="w-4 h-4" /> {{ loading ? 'Agendando...' : 'Confirmar Agendamento' }}
                </button>

            </div>
        </div>
    </div>
</template>