import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { jwtDecode } from 'jwt-decode'

import { api, saveToken, getToken, removeToken } from '../services/config/api.config'
import type { IUserToken } from '../types/auth.type'
import { remove } from 'lodash'


export const useAuthStore = defineStore('auth', () => {
    const token = ref<string | null>(getToken())
    const user = ref<IUserToken | null>(null)

    if(token.value){
        try{
            user.value = jwtDecode<IUserToken>(token.value)
        }

        catch (error){
            console.error("Token inválido ou expirado.")
            removeToken()
            token.value = null
        }
    }

    const isAuthenticated = computed(() => !!token.value)
    const isAdmin = computed(() => user.value?.role === 'Admin')
    const isSecretaria = computed(() => user.value?.role === 'Secretaria')

    async function login(email: string, password: string) {
        try{
            const { data } = await api.post('/api/auth/login', { email, senha: password })

            const newToken = data.token

            saveToken(newToken)

            token.value = newToken
            user.value = jwtDecode<IUserToken>(newToken)

            return true
        }

        catch (error) {
            console.error("Erro ao logar: ", error)
            throw error
        }
    }

    function logout() {
        removeToken()
        token.value = null
        user.value = null
    }

    return { token, user, isAuthenticated, isAdmin, isSecretaria, login, logout }
})