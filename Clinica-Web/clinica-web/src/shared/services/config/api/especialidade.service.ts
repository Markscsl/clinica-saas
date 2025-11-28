import { api } from "../api.config";
import type { Especialidade } from "@/shared/types/Domain";

export const EspecialidadeService = {
    getAll: async(): Promise<Especialidade[]> => {
        const { data } = await api.get('/api/especialidades');
        return data;
    },

    create: async (nome: string): Promise<string> => {
        const { data } = await api.post('/api/especialidades', { nome });
        return data;
    }
}