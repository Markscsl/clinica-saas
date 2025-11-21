<script setup lang="ts">
import { Ref } from 'vue/dist/vue.mjs';
import { useAuthStore } from '@/shared/stores/auth';
import { useRouter } from 'vue-router';
import { ref } from 'process';

const authStore = useAuthStore()
const router = useRouter()

const email = ref('')
const password = ref('')
const errorMsg = ref('')

async function handleLogin() {
    
    try {

        await authStore.login(email.value, password.value)

        router.push('/')

    } catch (e) {

        errorMsg.value = 'Login falhou. Verifique os dados.'

    }

}
</script>

<template>
    <div>
        <div>
            <h2></h2>
        </div>
    </div>
</template>