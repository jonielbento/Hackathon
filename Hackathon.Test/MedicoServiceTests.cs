using Hackathon.Application.DataTransfers.Requests;
using Hackathon.Application.DataTransfers.Responses;
using Hackathon.Application.Interfaces;
using Hackathon.Application.Services;
using Hackathon.Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Test;

[TestFixture]
public class MedicoServiceTests
{
    private Mock<IMedicoRepository> _mockRepository;
    private MedicoService _medicoService;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IMedicoRepository>();
        _medicoService = new MedicoService(_mockRepository.Object);
    }

    [Test]
    public async Task GetAllMedicosAsync_ReturnsAllMedicos()
    {
        // Arrange
        var medicosList = new List<Medico>
        {
            new Medico { IdMedico = 1, Nome = "Dr. João Silva", Crm = "12345-SP" },
            new Medico { IdMedico = 2, Nome = "Dra. Maria Santos", Crm = "67890-SP" }
        };

        _mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(medicosList);

        // Act
        var result = await _medicoService.GetAllMedicosAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("Dr. João Silva", result.First().Nome);
        Assert.AreEqual("Dra. Maria Santos", result.Last().Nome);
    }

    [Test]
    public async Task GetMedicoByIdAsync_WithValidId_ReturnsMedico()
    {
        // Arrange
        var medico = new Medico 
        { 
            IdMedico = 1, 
            Nome = "Dr. João Silva", 
            Crm = "12345-SP",
            MedicoEspecialidades = new List<MedicoEspecialidade>(),
            HorariosTrabalho = new List<MedicoHorarioTrabalho>()
        };

        _mockRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(medico);

        // Act
        var result = await _medicoService.GetMedicoByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.IdMedico);
        Assert.AreEqual("Dr. João Silva", result.Nome);
        Assert.AreEqual("12345-SP", result.Crm);
    }

    [Test]
    public async Task GetMedicoByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(999))
            .ReturnsAsync((Medico)null);

        // Act
        var result = await _medicoService.GetMedicoByIdAsync(999);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task CreateMedicoAsync_WithValidData_ReturnsMedicoResponse()
    {
        // Arrange
        var medicoRequest = new MedicoRequest
        {
            Nome = "Dr. Carlos Oliveira",
            Crm = "54321-SP",
            EspecialidadesIds = new List<long> { 1, 2 }
        };
    
        // Modificando para capturar o médico que está sendo passado para o repositório
        Medico capturedMedico = null;
        
        _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Medico>()))
            .Callback<Medico>(m => 
            {
                capturedMedico = m;
                // Configurar as propriedades que seriam definidas pelo banco de dados
                m.IdMedico = 3;
                // Configurar as especialidades com seus objetos completos
                foreach (var me in m.MedicoEspecialidades)
                {
                    me.IdMedico = 3; // Garantir que o IdMedico está definido
                    if (me.IdEspecialidade == 1)
                        me.Especialidade = new Especialidade { IdEspecialidade = 1, Nome = "Cardiologia" };
                    else if (me.IdEspecialidade == 2)
                        me.Especialidade = new Especialidade { IdEspecialidade = 2, Nome = "Neurologia" };
                }
            })
            .ReturnsAsync((Medico m) => m);
    
        // Act
        var result = await _medicoService.CreateMedicoAsync(medicoRequest);
    
        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.IdMedico);
        Assert.AreEqual("Dr. Carlos Oliveira", result.Nome);
        Assert.AreEqual("54321-SP", result.Crm);
        Assert.AreEqual(2, result.Especialidades.Count);
        
        // Verificar se as especialidades foram adicionadas corretamente
        var especialidades = result.Especialidades.OrderBy(e => e.IdEspecialidade).ToList();
        Assert.AreEqual(1, especialidades[0].IdEspecialidade);
        Assert.AreEqual("Cardiologia", especialidades[0].Nome);
        Assert.AreEqual(2, especialidades[1].IdEspecialidade);
        Assert.AreEqual("Neurologia", especialidades[1].Nome);
    }

    [Test]
    public async Task UpdateMedicoAsync_WithValidData_UpdatesMedico()
    {
        // Arrange
        var medicoRequest = new MedicoRequest
        {
            Nome = "Dr. João Silva Atualizado",
            Crm = "12345-SP"
        };

        var existingMedico = new Medico
        {
            IdMedico = 1,
            Nome = "Dr. João Silva",
            Crm = "12345-SP",
            MedicoEspecialidades = new List<MedicoEspecialidade>(),
            HorariosTrabalho = new List<MedicoHorarioTrabalho>()
        };

        _mockRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(existingMedico);

        // Act & Assert
        Assert.DoesNotThrowAsync(async () => await _medicoService.UpdateMedicoAsync(1, medicoRequest));
        _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Medico>()), Times.Once);
    }

    [Test]
    public void UpdateMedicoAsync_WithInvalidId_ThrowsException()
    {
        // Arrange
        var medicoRequest = new MedicoRequest
        {
            Nome = "Dr. Inexistente",
            Crm = "00000-SP"
        };

        _mockRepository.Setup(repo => repo.GetByIdAsync(999))
            .ReturnsAsync((Medico)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<Exception>(async () => 
            await _medicoService.UpdateMedicoAsync(999, medicoRequest));
        
        Assert.That(ex.Message, Is.EqualTo("Médico não encontrado"));
    }

    [Test]
    public async Task DeleteMedicoAsync_WithValidId_ReturnsTrue()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.DeleteAsync(1))
            .ReturnsAsync(true);

        // Act
        var result = await _medicoService.DeleteMedicoAsync(1);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task DeleteMedicoAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.DeleteAsync(999))
            .ReturnsAsync(false);

        // Act
        var result = await _medicoService.DeleteMedicoAsync(999);

        // Assert
        Assert.IsFalse(result);
    }
}