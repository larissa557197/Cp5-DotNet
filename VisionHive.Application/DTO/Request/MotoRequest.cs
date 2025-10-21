using VisionHive.Application.Enums;
using VisionHive.Domain.Entities;

namespace VisionHive.Application.DTO.Request
{
    public class MotoRequest
    {
        public string? Placa { get; set; }
        public string? Chassi { get; set; }
        public string? NumeroMotor { get; set; }
        public Prioridade Prioridade { get; set; }
        public Guid PatioId { get; set; }

        public Moto ToDomain()
        {
            return new Moto(Placa, Chassi, NumeroMotor, Prioridade, PatioId);
        }
    }
}
