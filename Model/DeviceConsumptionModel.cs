using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.EnvironmentalAlert.Model
{

    public class DeviceConsumptionModel
    {

        public int Id { get; set; }

        public int DeviceId { get; set; }

        public decimal ConsumptionKwh { get; set; }

        public decimal ExpectedLimitKwh { get; set; }

        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        // Relacionamento N:1 com Device (correto)

        public DeviceModel Device { get; set; } = null!;
    }
}
