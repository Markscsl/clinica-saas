<script setup lang="ts">
import { computed, ref } from 'vue';
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

const passwordRules = ref({
    minLength: false,
    hasNumber: false,
    hasSpecial: false,
    hasCapital: false
})

function validarSenha(event: Event) {
    const input = event.target as HTMLInputElement
    const val = input.value

    form.value.senha = val

    passwordRules.value.minLength = val.length >= 6
    passwordRules.value.hasNumber = /\d/.test(val)
    passwordRules.value.hasSpecial = /[!@#$%^&(),.?":{}|<>]/.test(val)
    passwordRules.value.hasCapital = /[A-Z]/.test(val)
}

const errorMessage = ref('')
const loading = ref(false)

async function handleRegister() {
    loading.value = true
    errorMessage.value = ''

    try {

        const payload = {
            ...form.value,
            cpf: form.value.cpf.replace(/\D/g, '')
        }

        await api.post('api/pacientes', payload)

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

function formatarCpf(event: Event) {
    const input = event.target as HTMLInputElement

    let v = input.value.replace(/\D/g, '')

    if (v.length > 11) v = v.slice(0, 11)

    v = v.replace(/(\d{3})(\d)/, '$1.$2')
    v = v.replace(/(\d{3})(\d)/, '$1.$2')
    v = v.replace(/(\d{3})(\d{1,2})$/, '$1-$2')

    input.value = v
    form.value.cpf = v
}

function formatarTel(event: Event) {
    const input = event.target as HTMLInputElement

    let v = input.value.replace(/\D/g, '')

    if (v.length > 11) v = v.slice(0, 11)

    v = v.replace(/^(\d{2})(\d)/, '($1) $2')
    v = v.replace(/(\d{5})(\d)/, '$1-$2')


    input.value = v
    form.value.telefone = v
}

const formInvalido = computed(() => {
    return loading.value || !passwordRules.value.minLength || !passwordRules.value.hasCapital || !passwordRules.value.hasSpecial || !passwordRules.value.hasNumber
})
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
                        <input @input="formatarCpf" maxlength="14" :value="form.cpf" type="text" required
                            class="input-padrao" placeholder="000.000.000-00" />
                    </div>
                    <div>
                        <label class="block font-medium text-gray-700">Telefone</label>
                        <input @input="formatarTel" maxlength="15" :value="form.telefone" type="text" required
                            class="input-padrao" placeholder="(00) 00000-0000" />
                    </div>
                </div>

                <div>
                    <label class="block font-medium text-gray-700">E-mail</label>
                    <input v-model="form.email" type="email" required class="input-padrao"
                        placeholder="seu@email.com" />
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700">Senha</label>

                    <input :value="form.senha" @input="validarSenha" type="password" required
                        class="mt-1 block w-full rounded-md border border-gray-300 p-2 focus:border-blue-500 focus:ring focus:ring-blue-200 outline-none"
                        placeholder="******" />

                    <div class="mt-2 text-xs space-y-1 transition-all duration-300">
                        <p :class="passwordRules.minLength ? 'text-green-600 font-bold' : 'text-red-400'">
                            <span v-if="passwordRules.minLength">✓</span> ○ Mínimo 6 caracteres
                        </p>
                        <p :class="passwordRules.hasNumber ? 'text-green-600 font-bold' : 'text-red-400'">
                            <span v-if="passwordRules.hasNumber">✓</span> ○ Pelo menos um número
                        </p>
                        <p :class="passwordRules.hasSpecial ? 'text-green-600 font-bold' : 'text-red-400'">
                            <span v-if="passwordRules.hasSpecial">✓</span> ○ Pelo menos um caractere especial (!@#$...)
                        </p>
                        <p :class="passwordRules.hasCapital ? 'text-green-600 font-bold' : 'text-red-400'">
                            <span v-if="passwordRules.hasCapital">✓</span> ○ Pelo menos uma letra maiúscula
                        </p>
                    </div>
                </div>

                <div v-if="errorMessage" class="text-red-500 text-sm text-center bg-red-50 p-2 rounded">
                    {{ errorMessage }}
                </div>

                <button type="submit" :disabled="formInvalido"
                    class="mt-1 bg-blue-500 text-white p-2 rounded-xl w-full hover:scale-105 transition-all disabled:bg-gray-300 disabled:cursor-not-allowed disabled:hover:scale-100 disabled:shadow-none">
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