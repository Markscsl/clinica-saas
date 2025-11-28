export interface Especialidade {
    id: string,
    nome: string
}

export interface Medico {
    id: string,
    nome: string,
    crm: string,
    nomeEspecialidade: string,
    especialidadeId: string
}

export interface Paciente {
    id: string,
    nome: string,
    cpf: string,
    email?: string
}

export interface Consulta {
    id: string,
    data: string,
    status: string,
    nomeMedico: string,
    nomePaciente: string,
    nomeEspecialidade: string
}

export interface CriarPacienteDto {
    nome: string,
    cpf: string,
    telefone: string,
    email: string,
    senha?: string
}

export interface AgendarConsultaDto {
    pacienteId: string;
    medicoId: string;
    dataHoraInicio: string;
}