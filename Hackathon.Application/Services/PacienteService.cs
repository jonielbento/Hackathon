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
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IEnumerable<PacienteResponse>> GetAllPacientesAsync()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return pacientes.Select(MapToPacienteResponse);
        }

        public async Task<PacienteResponse> GetPacienteByIdAsync(long id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            if (paciente == null)
                return null;

            return MapToPacienteResponse(paciente);
        }

        public async Task<PacienteResponse> CreatePacienteAsync(PacienteRequest pacienteRequest)
        {
            // Verificar se a senha foi fornecida para criação
            if (string.IsNullOrEmpty(pacienteRequest.Senha))
                throw new ArgumentException("A senha é obrigatória para criar um paciente");

            var paciente = new Paciente
            {
                Nome = pacienteRequest.Nome,
                Cpf = pacienteRequest.Cpf,
                Email = pacienteRequest.Email,
                Telefone = pacienteRequest.Telefone,
                PacienteLogin = new PacienteLogin
                {
                    Email = pacienteRequest.Email,
                    Cpf = pacienteRequest.Cpf,
                    Senha = pacienteRequest.Senha // Idealmente, esta senha deveria ser hasheada
                }
            };

            await _pacienteRepository.CreateAsync(paciente);
            return MapToPacienteResponse(paciente);
        }

        public async Task UpdatePacienteAsync(long id, PacienteRequest pacienteRequest)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            if (paciente == null)
                throw new Exception("Paciente não encontrado");

            paciente.Nome = pacienteRequest.Nome;
            paciente.Cpf = pacienteRequest.Cpf;
            paciente.Email = pacienteRequest.Email;
            paciente.Telefone = pacienteRequest.Telefone;

            // Atualizar login se existir
            if (paciente.PacienteLogin != null)
            {
                paciente.PacienteLogin.Email = pacienteRequest.Email;
                paciente.PacienteLogin.Cpf = pacienteRequest.Cpf;
                
                // Atualizar senha apenas se fornecida
                if (!string.IsNullOrEmpty(pacienteRequest.Senha))
                {
                    paciente.PacienteLogin.Senha = pacienteRequest.Senha; // Idealmente, esta senha deveria ser hasheada
                }
            }
            // Criar login se não existir e senha fornecida
            else if (!string.IsNullOrEmpty(pacienteRequest.Senha))
            {
                paciente.PacienteLogin = new PacienteLogin
                {
                    IdPaciente = paciente.IdPaciente,
                    Email = pacienteRequest.Email,
                    Cpf = pacienteRequest.Cpf,
                    Senha = pacienteRequest.Senha // Idealmente, esta senha deveria ser hasheada
                };
            }

            await _pacienteRepository.UpdateAsync(paciente);
        }

        public async Task<bool> DeletePacienteAsync(long id)
        {
            return await _pacienteRepository.DeleteAsync(id);
        }

        private PacienteResponse MapToPacienteResponse(Paciente paciente)
        {
            return new PacienteResponse
            {
                IdPaciente = paciente.IdPaciente,
                Nome = paciente.Nome,
                Cpf = paciente.Cpf,
                Email = paciente.Email,
                Telefone = paciente.Telefone,
                ConsultasIds = paciente.Consultas?.Select(c => c.IdConsulta)
            };
        }
    }
}