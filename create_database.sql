-- Script para criar todas as tabelas necessárias para o sistema de agendamento médico

-- Verificar se o banco de dados existe, se não, criar
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HackathonDB')
BEGIN
    CREATE DATABASE HackathonDB;
END
GO

USE HackathonDB;
GO

-- Tabela de Especialidades
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Especialidades')
BEGIN
    CREATE TABLE Especialidades
    (
        IdEspecialidade BIGINT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(255) NOT NULL
    );
END
GO

-- Tabela de Médicos
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Medicos')
BEGIN
    CREATE TABLE Medicos
    (
        IdMedico BIGINT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(255) NOT NULL,
        Crm NVARCHAR(9) NOT NULL
    );
END
GO

-- Tabela de Login dos Médicos
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MedicosLogin')
BEGIN
    CREATE TABLE MedicosLogin
    (
        IdMedico BIGINT PRIMARY KEY,
        Email NVARCHAR(255) NOT NULL,
        Senha NVARCHAR(255) NOT NULL,
        CONSTRAINT FK_MedicosLogin_Medicos FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico) ON DELETE CASCADE
    );
END
GO

-- Tabela de Pacientes
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Pacientes')
BEGIN
    CREATE TABLE Pacientes
    (
        IdPaciente BIGINT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(255) NOT NULL,
        Cpf NVARCHAR(14) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        Telefone NVARCHAR(20) NOT NULL
    );
END
GO

-- Tabela de Login dos Pacientes
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PacientesLogin')
BEGIN
    CREATE TABLE PacientesLogin
    (
        IdPaciente BIGINT PRIMARY KEY,
        Email NVARCHAR(255) NOT NULL,
        Senha NVARCHAR(255) NOT NULL,
        Cpf NVARCHAR(11) NOT NULL,
        CONSTRAINT FK_PacientesLogin_Pacientes FOREIGN KEY (IdPaciente) REFERENCES Pacientes(IdPaciente) ON DELETE CASCADE
    );
END
GO

-- Tabela de Relação entre Médicos e Especialidades
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MedicosEspecialidades')
BEGIN
    CREATE TABLE MedicosEspecialidades
    (
        IdMedico BIGINT NOT NULL,
        IdEspecialidade BIGINT NOT NULL,
        PRIMARY KEY (IdMedico, IdEspecialidade),
        CONSTRAINT FK_MedicosEspecialidades_Medicos FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico) ON DELETE CASCADE,
        CONSTRAINT FK_MedicosEspecialidades_Especialidades FOREIGN KEY (IdEspecialidade) REFERENCES Especialidades(IdEspecialidade) ON DELETE CASCADE
    );
END
GO

-- Tabela de Horários de Trabalho dos Médicos
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MedicosHorariosTrabalho')
BEGIN
    CREATE TABLE MedicosHorariosTrabalho
    (
        IdMedicoHorarioTrabalho BIGINT IDENTITY(1,1) PRIMARY KEY,
        IdMedico BIGINT NOT NULL,
        DiaTrabalho DATE NOT NULL,
        HoraInicio TIME NOT NULL,
        HoraFim TIME NOT NULL,
        CONSTRAINT FK_MedicosHorariosTrabalho_Medicos FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico) ON DELETE CASCADE
    );
END
GO

-- Tabela de Consultas
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Consultas')
BEGIN
    CREATE TABLE Consultas
    (
        IdConsulta BIGINT IDENTITY(1,1) PRIMARY KEY,
        IdMedico BIGINT NOT NULL,
        IdPaciente BIGINT NOT NULL,
        DataConsultaInicio DATETIME NOT NULL,
        DataConsultaFim DATETIME NOT NULL,
        ValorConsulta DECIMAL(18, 2) NOT NULL,
        Status INT NOT NULL DEFAULT(0), -- 0: Pendente, 1: Confirmada, 2: Recusada, 3: Cancelada
        Justificativa NVARCHAR(500) NULL,
        CONSTRAINT FK_Consultas_Medicos FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico),
        CONSTRAINT FK_Consultas_Pacientes FOREIGN KEY (IdPaciente) REFERENCES Pacientes(IdPaciente)
    );
END
GO

-- Criar índices para melhorar a performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Medicos_Crm')
BEGIN
    CREATE INDEX IX_Medicos_Crm ON Medicos(Crm);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pacientes_Cpf')
BEGIN
    CREATE INDEX IX_Pacientes_Cpf ON Pacientes(Cpf);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pacientes_Email')
BEGIN
    CREATE INDEX IX_Pacientes_Email ON Pacientes(Email);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MedicosHorariosTrabalho_IdMedico_DiaTrabalho')
BEGIN
    CREATE INDEX IX_MedicosHorariosTrabalho_IdMedico_DiaTrabalho ON MedicosHorariosTrabalho(IdMedico, DiaTrabalho);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Consultas_IdMedico_DataConsultaInicio')
BEGIN
    CREATE INDEX IX_Consultas_IdMedico_DataConsultaInicio ON Consultas(IdMedico, DataConsultaInicio);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Consultas_IdPaciente_DataConsultaInicio')
BEGIN
    CREATE INDEX IX_Consultas_IdPaciente_DataConsultaInicio ON Consultas(IdPaciente, DataConsultaInicio);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Consultas_Status')
BEGIN
    CREATE INDEX IX_Consultas_Status ON Consultas(Status);
END
GO

PRINT 'Todas as tabelas foram criadas com sucesso!';
GO