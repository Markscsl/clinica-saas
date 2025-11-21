import { Ref } from "vue";
import { defineStore } from 'pinia'
import api from '@/shared/services/config/api.config'
import { jwtDecode } from "jwt-decode";

interface UserToken{
    unique_name: string
    email: string
    role: string
}

export const useAuthStore = defineStore('auth', () => {
    const token = ref(localStorage.getItem('clinica_token'))
    const user = ref<UserToken | null>(null)

    if(token.value){

        try{
            user.value = jwtDecode(token.value)
        }

        catch{
            token.value = null
        }
    }

    const isAuthenticated = computed(() => !!token.value)
    const isAdmin = computed(() => user.value?.role == 'Admin')


    async function login(email: string, password: string) {
        
        try{
            
            const response = await api.post('/api/auth/login', { email, password })

            const newToken = response.data.token

            token.value = newToken
            localStorage.setItem('clinica_token', newToken)

            user.value = jwtDecode(newToken)

        }

        catch (error) {

            console.error('Erro login:', error)
            throw error

        }
    }

    function logout(){

        token.value = null
        user.value = null
        localStorage.removeItem('clinica_token')

    }

    return { token, user, isAuthenticated, isAdmin, login, logout }
})