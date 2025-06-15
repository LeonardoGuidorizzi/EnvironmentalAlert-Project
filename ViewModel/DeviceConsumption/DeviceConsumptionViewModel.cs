namespace Fiap.Api.EnvironmentalAlert.ViewModel.DeviceConsumption
{
    public class DeviceConsumptionViewModel
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public decimal ConsumptionKwh { get; set; }

        public decimal ExpectedLimitKwh { get; set; }

        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    }
}
