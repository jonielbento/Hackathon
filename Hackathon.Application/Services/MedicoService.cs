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
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<IEnumerable<MedicoResponse>> GetAllMedicosAsync()
        {
            var medicos = await _medicoRepository.GetAllAsync();
            return medicos.Select(MapToMedicoResponse);
        }

        public async Task<MedicoResponse> GetMedicoByIdAsync(long id)
        {
            var medico = await _medicoRepository.GetByIdAsync(id);
            if (medico == null)
                return null;

            return MapToMedicoResponse(medico);
        }

        public async Task<MedicoResponse> CreateMedicoAsync(MedicoRequest medicoRequest)
        {
            var medico = new Medico
            {
                Nome = medicoRequest.Nome,
                Crm = medicoRequest.Crm
            };

            // Adicionar especialidades se fornecidas
            if (medicoRequest.EspecialidadesIds != null && medicoRequest.EspecialidadesIds.Any())
            {
                foreach (var especialidadeId in medicoRequest.EspecialidadesIds)
                {
                    medico.MedicoEspecialidades.Add(new MedicoEspecialidade
                    {
                        IdEspecialidade = especialidadeId
                    });
                }
            }

            await _medicoRepository.CreateAsync(medico);
            return MapToMedicoResponse(medico);
        }

        public async Task UpdateMedicoAsync(long id, MedicoRequest medicoRequest)
        {
            var medico = await _medicoRepository.GetByIdAsync(id);
            if (medico == null)
                throw new Exception("Médico não encontrado");

            medico.Nome = medicoRequest.Nome;
            medico.Crm = medicoRequest.Crm;

            await _medicoRepository.UpdateAsync(medico);
        }

        public async Task<bool> DeleteMedicoAsync(long id)
        {
            return await _medicoRepository.DeleteAsync(id);
        }

        private MedicoResponse MapToMedicoResponse(Medico medico)
        {
            return new MedicoResponse
            {
                IdMedico = medico.IdMedico,
                Nome = medico.Nome,
                Crm = medico.Crm,
                Especialidades = medico.MedicoEspecialidades
                    .Select(me => new EspecialidadeResponse
                    {
                        IdEspecialidade = me.IdEspecialidade,
                        Nome = me.Especialidade?.Nome ?? string.Empty
                    }).ToList(),
                HorariosTrabalho = medico.HorariosTrabalho
                    .Select(ht => new HorarioTrabalhoResponse
                    {
                        IdMedicoHorarioTrabalho = ht.IdMedicoHorarioTrabalho,
                        DiaTrabalho = ht.DiaTrabalho,
                        HoraInicio = ht.HoraInicio,
                        HoraFim = ht.HoraFim
                    }).ToList()
            };
        }
    }
}