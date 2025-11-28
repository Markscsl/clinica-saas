<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { api } from '@/shared/services/config/api.config';

const router = useRouter();
const token = ref('');
const novaSenha = ref('');
const loading = ref(false);

async function handleRedefinir() {
    loading.value = true;
    try {
        await api.post('/api/auth/redefinir-senha', {
            token: token.value,
            novaSenha: novaSenha.value
        });

        alert('Senha alterada! Faça login.');
        router.push('/login');
    } catch (e: any) {
        alert('Erro: ' + (e.response?.data || 'Código inválido'));
    } finally {
        loading.value = false;
    }
}
</script>

<template>
    <div class="flex min-h-screen items-center justify-center bg-gray-100">
        <div class="w-full max-w-md bg-white p-8 rounded-lg shadow-md">
            <h2 class="text-2xl font-bold mb-4 text-center">Definir Nova Senha</h2>
            <p class="text-gray-500 mb-6 text-center text-sm">Verifique seu e-mail (Mailtrap) e digite o código.</p>

            <form @submit.prevent="handleRedefinir" class="space-y-4">
                <div>
                    <label class="block text-sm font-medium">Código de Verificação</label>
                    <input v-model="token" type="text" placeholder="Ex: A1B2C3" required
                        class="w-full p-2 border rounded uppercase" />
                </div>

                <div>
                    <label class="block text-sm font-medium">Nova Senha</label>
                    <input v-model="novaSenha" type="password" placeholder="******" required
                        class="w-full p-2 border rounded" />
                </div>

                <button type="submit" :disabled="loading"
                    class="w-full bg-green-600 text-white p-2 rounded hover:bg-green-700 disabled:opacity-50">
                    {{ loading ? 'Salvando...' : 'Alterar Senha' }}
                </button>
            </form>
        </div>
    </div>
</template>