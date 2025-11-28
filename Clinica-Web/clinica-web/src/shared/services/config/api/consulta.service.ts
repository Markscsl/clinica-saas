import { api } from "../api.config";
import type { Consulta, AgendarConsultaDto } from "@/shared/types/Domain";

export const ConsultaService = {
    getMyConsultas: async() : Promise<Consulta[]> => {
        const { data } = await api.get('/api/consultas/minhas');
        return data;
    },

    agendar: async(payload: AgendarConsultaDto): Promise<string> => {
        const { data } = await api.post('/api/consultas/agendar', payload);
        return data;
    }
}