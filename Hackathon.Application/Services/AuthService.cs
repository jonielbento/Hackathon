using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.DataTransfers.Responses;
using Hackathon.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public AuthService(IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository)
        {
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<LoginResponse> LoginMedicoAsync(MedicoLoginRequest request)
        {
            var medicos = await _medicoRepository.GetAllAsync();
            var medico = medicos.FirstOrDefault(m => 
                m.MedicoLogin != null && 
                m.Crm == request.Crm && 
                m.MedicoLogin.Senha == request.Senha);

            if (medico == null)
            {
                return new LoginResponse
                {
                    Sucesso = false,
                    Mensagem = "Credenciais inválidas"
                };
            }

            // Aqui você pode gerar um token JWT usando um serviço de token
            // Para simplificar, estamos apenas retornando um token fictício
            return new LoginResponse
            {
                Sucesso = true,
                IdUsuario = medico.IdMedico,
                TipoUsuario = "Medico",
                Token = $"token-medico-{medico.IdMedico}-{Guid.NewGuid()}",
                Mensagem = "Login realizado com sucesso"
            };
        }

        public async Task<LoginResponse> LoginPacienteAsync(PacienteLoginRequest request)
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            var paciente = pacientes.FirstOrDefault(p => 
                p.PacienteLogin != null && 
                (p.PacienteLogin.Email == request.Identificador || p.PacienteLogin.Cpf == request.Identificador) && 
                p.PacienteLogin.Senha == request.Senha);

            if (paciente == null)
            {
                return new LoginResponse
                {
                    Sucesso = false,
                    Mensagem = "Credenciais inválidas"
                };
            }

            // Aqui você pode gerar um token JWT usando um serviço de token
            // Para simplificar, estamos apenas retornando um token fictício
            return new LoginResponse
            {
                Sucesso = true,
                IdUsuario = paciente.IdPaciente,
                TipoUsuario = "Paciente",
                Token = $"token-paciente-{paciente.IdPaciente}-{Guid.NewGuid()}",
                Mensagem = "Login realizado com sucesso"
            };
        }
    }
}