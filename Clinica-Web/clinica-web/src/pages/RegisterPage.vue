<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { api } from '@/shared/services/config/api.config';

const router = useRouter()

const form = ref({
    nome: '',
    cpf: '',
    telefone: '',
    email: '',
    senha: ''
})

const errorMessage = ref('')
const loading = ref(false)

async function handleRegister() {
    loading.value = true
    errorMessage.value = ''

    try {
        await api.post('api/pacientes', form.value)

        alert('Cadastro realizado com sucesso! Faça login.')
        router.push('/login')
    }

    catch (error: any) {
        errorMessage.value = error.response?.data || 'Erro ao cadastrar.'
    }

    finally {
        loading.value = false
    }

}

</script>

<template>
    <div class="flex justify-center items-center min-h-screen bg-blue-200 py-10">
        <div class="bg-white shadow-xl flex rounded-2xl space-y-8 flex-col p-10 px-16 w-full max-w-lg">
            <div class="flex flex-col justify-center items-center">
                <div class="mb-4">
                    <img src="../assets/img/hospital.png">
                </div>
                <h2 class="font-bold text-3xl text-gray-800">Criar Conta</h2>
                <p class="font-normal text-gray-500">Preencha seus dados de paciente</p>
            </div>

            <form @submit.prevent="handleRegister" class="space-y-4">
                <div>
                    <label class="block font-medium text-gray-700">Nome Completo</label>
                    <input v-model="form.nome" type="text" required class="input-padrao" placeholder="Seu nome" />
                </div>

                <div class="grid grid-cols-2 gap-4">
                    <div>
                        <label class="block font-medium text-gray-700">CPF</label>
                        <input v-model="form.cpf" type="text" required class="input-padrao"
                            placeholder="000.000.000-00" />
                    </div>
                    <div>
                        <label class="block font-medium text-gray-700">Telefone</label>
                        <input v-model="form.telefone" type="text" required class="input-padrao"
                            placeholder="(00) 00000-0000" />
                    </div>
                </div>

                <div>
                    <label class="block font-medium text-gray-700">E-mail</label>
                    <input v-model="form.email" type="email" required class="input-padrao"
                        placeholder="seu@email.com" />
                </div>

                <div>
                    <label class="block font-medium text-gray-700">Senha</label>
                    <input v-model="form.senha" type="password" required class="input-padrao" placeholder="******" />
                </div>

                <div v-if="errorMessage" class="text-red-500 text-sm text-center bg-red-50 p-2 rounded">
                    {{ errorMessage }}
                </div>

                <button type="submit" :disabled="loading"
                    class="mt-1 bg-blue-500 text-white p-2 rounded-xl w-full hover:scale-105 transition-all">
                    {{ loading ? 'Cadastrando...' : 'Finalizar Cadastro' }}
                </button>
            </form>

            <div class="text-center">
                <p class="font-medium text-gray-600">Já tem conta?
                    <RouterLink to="/login"
                        class="hover:[text-shadow:_0_0_1px_currentColor] text-blue-600 cursor-pointer">
                        Faça Login.
                    </RouterLink>
                </p>
            </div>
        </div>
    </div>
</template>

<style scoped>
/* Classe auxiliar para não repetir CSS */
.input-padrao {
    @apply border mt-1 border-gray-200 p-3 outline-none rounded-xl w-full focus:border-blue-500 focus:ring focus:ring-blue-200 transition;
}
</style>