using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.DataTransfers.Responses;
using Hackathon.Application.Interfaces;
using Hackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Application.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMedicoRepository _medicoRepository;

        public ConsultaService(IConsultaRepository consultaRepository, IMedicoRepository medicoRepository)
        {
            _consultaRepository = consultaRepository;
            _medicoRepository = medicoRepository;
        }

        public async Task<IEnumerable<ConsultaResponse>> GetConsultasByMedicoIdAsync(long medicoId)
        {
            var consultas = await _consultaRepository.GetByMedicoIdAsync(medicoId);
            return consultas.Select(MapToConsultaResponse);
        }

        public async Task<IEnumerable<ConsultaResponse>> GetConsultasByPacienteIdAsync(long pacienteId)
        {
            var consultas = await _consultaRepository.GetByPacienteIdAsync(pacienteId);
            return consultas.Select(MapToConsultaResponse);
        }

        public async Task<ConsultaResponse> GetConsultaByIdAsync(long consultaId)
        {
            var consulta = await _consultaRepository.GetByIdAsync(consultaId);
            if (consulta == null)
                return null;

            return MapToConsultaResponse(consulta);
        }

        public async Task<ConsultaResponse> AgendarConsultaAsync(AgendarConsultaRequest request)
        {
            // Verificar se o médico existe
            var medico = await _medicoRepository.GetByIdAsync(request.IdMedico);
            if (medico == null)
                throw new Exception("Médico não encontrado");

            // Verificar disponibilidade do médico
            var horarioDisponivel = medico.HorariosTrabalho.Any(h => 
                h.DiaTrabalho.Date == request.DataConsultaInicio.Date &&
                h.HoraInicio <= request.DataConsultaInicio.TimeOfDay &&
                h.HoraFim >= request.DataConsultaFim.TimeOfDay);

            if (!horarioDisponivel)
                throw new Exception("O médico não está disponível no horário solicitado");

            // Verificar se já existe consulta agendada no mesmo horário
            var consultasExistentes = await _consultaRepository.GetByMedicoIdAsync(request.IdMedico);
            var conflito = consultasExistentes.Any(c =>
                (c.Status == StatusConsulta.Pendente || c.Status == StatusConsulta.Confirmada) &&
                ((request.DataConsultaInicio >= c.DataConsultaInicio && request.DataConsultaInicio < c.DataConsultaFim) ||
                (request.DataConsultaFim > c.DataConsultaInicio && request.DataConsultaFim <= c.DataConsultaFim) ||
                (request.DataConsultaInicio <= c.DataConsultaInicio && request.DataConsultaFim >= c.DataConsultaFim)));

            if (conflito)
                throw new Exception("Já existe uma consulta agendada neste horário");

            // Criar a consulta
            var consulta = new Consulta
            {
                IdMedico = request.IdMedico,
                IdPaciente = request.IdPaciente,
                DataConsultaInicio = request.DataConsultaInicio,
                DataConsultaFim = request.DataConsultaFim,
                ValorConsulta = request.ValorConsulta,
                Status = StatusConsulta.Pendente
            };

            await _consultaRepository.CreateAsync(consulta);
            return MapToConsultaResponse(consulta);
        }

        public async Task<ConsultaResponse> AtualizarStatusConsultaAsync(long consultaId, AtualizarStatusConsultaRequest request)
        {
            var consulta = await _consultaRepository.GetByIdAsync(consultaId);
            if (consulta == null)
                throw new Exception("Consulta não encontrada");

            // Verificar se a atualização de status é válida
            if (consulta.Status == StatusConsulta.Cancelada || consulta.Status == StatusConsulta.Recusada)
                throw new Exception("Não é possível alterar o status de uma consulta já cancelada ou recusada");

            consulta.Status = request.Status;
            consulta.Justificativa = request.Justificativa;

            await _consultaRepository.UpdateAsync(consulta);
            return MapToConsultaResponse(consulta);
        }

        private ConsultaResponse MapToConsultaResponse(Consulta consulta)
        {
            return new ConsultaResponse
            {
                IdConsulta = consulta.IdConsulta,
                IdMedico = consulta.IdMedico,
                NomeMedico = consulta.Medico?.Nome ?? string.Empty,
                CrmMedico = consulta.Medico?.Crm ?? string.Empty,
                IdPaciente = consulta.IdPaciente,
                NomePaciente = consulta.Paciente?.Nome ?? string.Empty,
                DataConsultaInicio = consulta.DataConsultaInicio,
                DataConsultaFim = consulta.DataConsultaFim,
                ValorConsulta = consulta.ValorConsulta,
                Status = consulta.Status,
                Justificativa = consulta.Justificativa
            };
        }
    }
}