using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.EnvironmentalAlert.Model
{

    public class ConsumptionAlertModel
    {

        public int Id { get; set; }

        public int DeviceId { get; set; }

        public decimal? RecordedConsumption { get; set; }

        public decimal? ExpectedLimit { get; set; }

        public string? Message { get; set; }

        public DateTime AlertAt { get; set; } = DateTime.Now;

        // Relacionamento

        public DeviceModel Device { get; set; } = null!;
    }
}
