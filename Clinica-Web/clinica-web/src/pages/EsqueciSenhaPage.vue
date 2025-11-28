<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { api } from '@/shared/services/config/api.config';

const router = useRouter();
const email = ref('');
const loading = ref(false);

async function handleSolicitar() {
    loading.value = true;
    try {
        await api.post('/api/auth/esqueci-senha', { email: email.value });
        // Salva o email para facilitar e vai para a próxima tela
        localStorage.setItem('reset_email', email.value);
        router.push('/redefinir-senha');
    } catch (e) {
        alert('Erro ao solicitar.');
    } finally {
        loading.value = false;
    }
}
</script>

<template>
    <div class="flex min-h-screen items-center justify-center bg-gray-100">
        <div class="w-full max-w-md bg-white p-8 rounded-lg shadow-md text-center">
            <h2 class="text-2xl font-bold mb-4">Recuperar Senha</h2>
            <p class="text-gray-500 mb-6">Digite seu e-mail para receber o código.</p>

            <form @submit.prevent="handleSolicitar" class="space-y-4">
                <input v-model="email" type="email" placeholder="seu@email.com" required
                    class="w-full p-2 border rounded" />

                <button type="submit" :disabled="loading"
                    class="w-full bg-blue-600 text-white p-2 rounded hover:bg-blue-700 disabled:opacity-50">
                    {{ loading ? 'Enviando...' : 'Enviar Código' }}
                </button>
            </form>

            <RouterLink to="/login" class="block mt-4 text-sm text-blue-600">Voltar para Login</RouterLink>
        </div>
    </div>
</template>