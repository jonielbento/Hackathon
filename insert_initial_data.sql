-- Script para inserir dados iniciais no banco de dados

USE HackathonDB;
GO

-- Inserir especialidades médicas comuns
IF NOT EXISTS (SELECT TOP 1 * FROM Especialidades)
BEGIN
    INSERT INTO Especialidades (Nome) VALUES 
    ('Cardiologia'),
    ('Dermatologia'),
    ('Ginecologia'),
    ('Ortopedia'),
    ('Pediatria'),
    ('Psiquiatria'),
    ('Oftalmologia'),
    ('Neurologia'),
    ('Endocrinologia'),
    ('Urologia');
    
    PRINT 'Especialidades inseridas com sucesso!';
END
ELSE
BEGIN
    PRINT 'Especialidades já existem no banco de dados.';
END
GO

-- Inserir médicos de teste
IF NOT EXISTS (SELECT TOP 1 * FROM Medicos)
BEGIN
    -- Inserir médicos
    INSERT INTO Medicos (Nome, Crm) VALUES
    ('Dr. Carlos Silva', '123456SP'),
    ('Dra. Ana Oliveira', '234567SP'),
    ('Dr. Roberto Santos', '345678SP'),
    ('Dra. Juliana Lima', '456789SP'),
    ('Dr. Marcelo Costa', '567890SP');
    
    -- Obter IDs dos médicos inseridos
    DECLARE @IdMedico1 BIGINT = (SELECT IdMedico FROM Medicos WHERE Crm = '123456SP');
    DECLARE @IdMedico2 BIGINT = (SELECT IdMedico FROM Medicos WHERE Crm = '234567SP');
    DECLARE @IdMedico3 BIGINT = (SELECT IdMedico FROM Medicos WHERE Crm = '345678SP');
    DECLARE @IdMedico4 BIGINT = (SELECT IdMedico FROM Medicos WHERE Crm = '456789SP');
    DECLARE @IdMedico5 BIGINT = (SELECT IdMedico FROM Medicos WHERE Crm = '567890SP');
    
    -- Inserir logins dos médicos (senha: 'senha123' para todos)
    INSERT INTO MedicosLogin (IdMedico, Email, Senha) VALUES
    (@IdMedico1, 'carlos.silva@medconsulta.com', 'senha123'),
    (@IdMedico2, 'ana.oliveira@medconsulta.com', 'senha123'),
    (@IdMedico3, 'roberto.santos@medconsulta.com', 'senha123'),
    (@IdMedico4, 'juliana.lima@medconsulta.com', 'senha123'),
    (@IdMedico5, 'marcelo.costa@medconsulta.com', 'senha123');
    
    -- Obter IDs das especialidades
    DECLARE @IdCardio BIGINT = (SELECT IdEspecialidade FROM Especialidades WHERE Nome = 'Cardiologia');
    DECLARE @IdDermato BIGINT = (SELECT IdEspecialidade FROM Especialidades WHERE Nome = 'Dermatologia');
    DECLARE @IdGineco BIGINT = (SELECT IdEspecialidade FROM Especialidades WHERE Nome = 'Ginecologia');
    DECLARE @IdOrto BIGINT = (SELECT IdEspecialidade FROM Especialidades WHERE Nome = 'Ortopedia');
    DECLARE @IdPediatria BIGINT = (SELECT IdEspecialidade FROM Especialidades WHERE Nome = 'Pediatria');
    DECLARE @IdPsiquiatria BIGINT = (SELECT IdEspecialidade FROM Especialidades WHERE Nome = 'Psiquiatria');
    DECLARE @IdNeuro BIGINT = (SELECT IdEspecialidade FROM Especialidades WHERE Nome = 'Neurologia');
    
    -- Associar médicos às especialidades
    INSERT INTO MedicosEspecialidades (IdMedico, IdEspecialidade) VALUES
    (@IdMedico1, @IdCardio),
    (@IdMedico2, @IdDermato),
    (@IdMedico2, @IdPediatria),
    (@IdMedico3, @IdOrto),
    (@IdMedico4, @IdGineco),
    (@IdMedico5, @IdPsiquiatria),
    (@IdMedico5, @IdNeuro);
    
    -- Inserir horários de trabalho para os médicos
    -- Dr. Carlos Silva - Cardiologista
    INSERT INTO MedicosHorariosTrabalho (IdMedico, DiaTrabalho, HoraInicio, HoraFim) VALUES
    (@IdMedico1, '2023-11-20', '08:00', '12:00'),
    (@IdMedico1, '2023-11-21', '13:00', '17:00'),
    (@IdMedico1, '2023-11-22', '08:00', '12:00');
    
    -- Dra. Ana Oliveira - Dermatologista e Pediatra
    INSERT INTO MedicosHorariosTrabalho (IdMedico, DiaTrabalho, HoraInicio, HoraFim) VALUES
    (@IdMedico2, '2023-11-20', '09:00', '15:00'),
    (@IdMedico2, '2023-11-22', '09:00', '15:00'),
    (@IdMedico2, '2023-11-24', '09:00', '15:00');
    
    -- Dr. Roberto Santos - Ortopedista
    INSERT INTO MedicosHorariosTrabalho (IdMedico, DiaTrabalho, HoraInicio, HoraFim) VALUES
    (@IdMedico3, '2023-11-20', '07:00', '13:00'),
    (@IdMedico3, '2023-11-21', '07:00', '13:00'),
    (@IdMedico3, '2023-11-23', '13:00', '19:00');
    
    -- Dra. Juliana Lima - Ginecologista
    INSERT INTO MedicosHorariosTrabalho (IdMedico, DiaTrabalho, HoraInicio, HoraFim) VALUES
    (@IdMedico4, '2023-11-21', '08:00', '16:00'),
    (@IdMedico4, '2023-11-23', '08:00', '16:00'),
    (@IdMedico4, '2023-11-25', '08:00', '12:00');
    
    -- Dr. Marcelo Costa - Psiquiatra
    INSERT INTO MedicosHorariosTrabalho (IdMedico, DiaTrabalho, HoraInicio, HoraFim) VALUES
    (@IdMedico5, '2023-11-20', '14:00', '20:00'),
    (@IdMedico5, '2023-11-22', '14:00', '20:00'),
    (@IdMedico5, '2023-11-24', '14:00', '20:00');
    
    PRINT 'Médicos, logins, especialidades e horários inseridos com sucesso!';
END
ELSE
BEGIN
    PRINT 'Médicos já existem no banco de dados.';
END
GO

-- Inserir pacientes de teste
IF NOT EXISTS (SELECT TOP 1 * FROM Pacientes)
BEGIN
    -- Inserir pacientes
    INSERT INTO Pacientes (Nome, Cpf, Email, Telefone) VALUES
    ('João Pereira', '123.456.789-00', 'joao.pereira@email.com', '(11) 98765-4321'),
    ('Maria Souza', '987.654.321-00', 'maria.souza@email.com', '(11) 91234-5678'),
    ('Pedro Alves', '456.789.123-00', 'pedro.alves@email.com', '(11) 92345-6789'),
    ('Lucia Ferreira', '789.123.456-00', 'lucia.ferreira@email.com', '(11) 93456-7890');
    
    -- Obter IDs dos pacientes inseridos
    DECLARE @IdPaciente1 BIGINT = (SELECT IdPaciente FROM Pacientes WHERE Cpf = '123.456.789-00');
    DECLARE @IdPaciente2 BIGINT = (SELECT IdPaciente FROM Pacientes WHERE Cpf = '987.654.321-00');
    DECLARE @IdPaciente3 BIGINT = (SELECT IdPaciente FROM Pacientes WHERE Cpf = '456.789.123-00');
    DECLARE @IdPaciente4 BIGINT = (SELECT IdPaciente FROM Pacientes WHERE Cpf = '789.123.456-00');
    
    -- Inserir logins dos pacientes (senha: 'senha123' para todos)
    INSERT INTO PacientesLogin (IdPaciente, Email, Senha, Cpf) VALUES
    (@IdPaciente1, 'joao.pereira@email.com', 'senha123', '12345678900'),
    (@IdPaciente2, 'maria.souza@email.com', 'senha123', '98765432100'),
    (@IdPaciente3, 'pedro.alves@email.com', 'senha123', '45678912300'),
    (@IdPaciente4, 'lucia.ferreira@email.com', 'senha123', '78912345600');
    
    PRINT 'Pacientes e logins inseridos com sucesso!';
END
ELSE
BEGIN
    PRINT 'Pacientes já existem no banco de dados.';
END
GO

-- Inserir algumas consultas de exemplo
IF NOT EXISTS (SELECT TOP 1 * FROM Consultas)
BEGIN
    -- Obter IDs dos médicos
    DECLARE @IdMedico1 BIGINT = (SELECT IdMedico FROM Medicos WHERE Crm = '123456SP');
    DECLARE @IdMedico2 BIGINT = (SELECT IdMedico FROM Medicos WHERE Crm = '234567SP');
    
    -- Obter IDs dos pacientes
    DECLARE @IdPaciente1 BIGINT = (SELECT IdPaciente FROM Pacientes WHERE Cpf = '123.456.789-00');
    DECLARE @IdPaciente2 BIGINT = (SELECT IdPaciente FROM Pacientes WHERE Cpf = '987.654.321-00');
    
    -- Inserir consultas (algumas pendentes, outras confirmadas)
    INSERT INTO Consultas (IdMedico, IdPaciente, DataConsultaInicio, DataConsultaFim, ValorConsulta, Status) VALUES
    (@IdMedico1, @IdPaciente1, '2023-11-20 09:00:00', '2023-11-20 09:30:00', 150.00, 1), -- Confirmada
    (@IdMedico1, @IdPaciente2, '2023-11-20 10:00:00', '2023-11-20 10:30:00', 150.00, 0), -- Pendente
    (@IdMedico2, @IdPaciente1, '2023-11-22 14:00:00', '2023-11-22 14:30:00', 200.00, 1), -- Confirmada
    (@IdMedico2, @IdPaciente2, '2023-11-24 11:00:00', '2023-11-24 11:30:00', 200.00, 0); -- Pendente
    
    PRINT 'Consultas de exemplo inseridas com sucesso!';
END
ELSE
BEGIN
    PRINT 'Consultas já existem no banco de dados.';
END
GO

PRINT 'Todos os dados iniciais foram inseridos com sucesso!';
GO