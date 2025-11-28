import { api } from "../api.config";
import type { Paciente, CriarPacienteDto } from "@/shared/types/Domain";

export const PacienteService = {
    getAll: async (): Promise<Paciente[]> => {
        const { data } = await api.get('/api/pacientes');
        return data;
    },

    create: async (payload: CriarPacienteDto): Promise<string> => {
        const { data } = await api.post('/api/pacientes', payload);
        return data;
    }
}
