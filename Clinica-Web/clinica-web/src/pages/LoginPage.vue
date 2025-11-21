<script setup lang="ts">
import { ref } from 'vue';
import { RouterLink, useRouter } from 'vue-router';
import { useAuthStore } from '@/shared/stores/auth.store';

const router = useRouter();
const auth = useAuthStore();

const email = ref('');
const password = ref('');
const errorMessage = ref('');
const loading = ref(false);

async function handleLogin() {
    loading.value = true;
    errorMessage.value = '';

    try {
        await auth.login(email.value, password.value);
        router.push('/'); 
    } catch (error) {
        errorMessage.value = 'Email ou senha incorretos.';
    } finally {
        loading.value = false;
    }
}
</script>

<template>
    <div class="flex justify-center items-center min-h-screen bg-blue-200">
        <div class="bg-white shadow-xl flex rounded-2xl space-y-8 flex-col p-10 px-16 w-full max-w-lg">
            <div class="flex flex-col justify-center items-center">
                <div class="mb-4">
                    <img src="../assets/img/hospital.png">
                </div>
                <h2 class="font-bold text-3xl text-gray-800">MediClinic</h2>
                <p class="font-normal text-gray-500">Acesse sua conta</p>
            </div>

            <form @submit.prevent="handleLogin" class="space-y-4">
                <div>
                    <label class="block font-medium text-gray-700">E-mail</label>
                    <input class="border mt-1 border-gray-200 p-3 outline-none rounded-xl w-full focus:border-blue-500 focus:ring focus:ring-blue-200" type="email" 
                    placeholder="seu@email.com"
                    v-model="email"
                    required>
                </div>

                <div>
                    <label class="block font-medium text-gray-700">Senha</label>
                    <input type="password"
                    v-model="password"
                    required
                    placeholder="* * * * * * *"
                    class="border mt-1 border-gray-200 p-3 outline-none rounded-xl w-full focus:border-blue-500 focus:ring focus:ring-blue-200">
                </div>

                <div v-if="errorMessage" class="text-red-500 text-sm text-center bg-red-50 p-2 rounded">{{ errorMessage }}</div>

                <button type="submit" :disabled="loading" class="mt-1 bg-blue-500 text-white p-2 rounded-xl w-full hover:scale-105 transition-all">{{ loading ? 'Entrando..' : 'Entrar' }}</button>
            </form>

            <p class="font-medium">Não possui conta? <RouterLink to="/register" class="hover:[text-shadow:_0_0_1px_currentColor] text-blue-600 cursor-pointer">Cadastre-se Aqui.</RouterLink></p>
        </div>
    </div>
</template>