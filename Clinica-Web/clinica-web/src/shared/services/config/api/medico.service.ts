import { api } from "../api.config";
import type { Medico } from "@/shared/types/Domain";

export const MedicoService = {
    getAll: async (especialidadeId?: string): Promise<Medico[]> => {
        const url = especialidadeId ? `/api/medicos?especialidadeId=${especialidadeId}` : '/api/medicos'

        const { data } = await api.get(url)
        return data;
    },

    create: async (payload: any): Promise<string> => {
        const { data } = await api.post('/api/medicos', payload);
        return data;
    }
}