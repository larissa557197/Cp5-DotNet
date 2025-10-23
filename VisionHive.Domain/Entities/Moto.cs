﻿using VisionHive.Application.Enums;
namespace VisionHive.Domain.Entities
{
    public class Moto
    {
        
        public Guid Id { get; set; }
        public string? Placa { get; set; }

        public string? Chassi { get; set; }

        public string? NumeroMotor { get; set; }

        public Prioridade Prioridade { get; set; }

        
        public Moto(){}
             
        public Guid PatioId { get; set; }
        public Patio Patio { get; private set; }


        public Moto(string? placa, string? chassi, string? numeroMotor, Prioridade prioridade, Guid patioId)
        {
            Validar(placa, chassi, numeroMotor);

            Id = Guid.NewGuid();
            Placa = placa;
            Chassi = chassi;
            NumeroMotor = numeroMotor;
            Prioridade = prioridade;
            PatioId = patioId;

        }

        public void AtualizarDados(string? placa, string? chassi, string? numeroMotor, Prioridade prioridade, Guid patioId)
        {
            Validar(placa, chassi, numeroMotor);

            Placa = placa;
            Chassi = chassi;
            NumeroMotor = numeroMotor;
            Prioridade = prioridade;
            PatioId = patioId;

        }

        private void Validar(string? placa, string? chassi, string? numeroMotor)
        {
            if(string.IsNullOrWhiteSpace(placa) && string.IsNullOrWhiteSpace(chassi) && string.IsNullOrWhiteSpace(numeroMotor))
            {
                throw new Exception("É necessário informar pelo menos a placa, o chassi ou o número do motor.");
            }
        }

    }
}
