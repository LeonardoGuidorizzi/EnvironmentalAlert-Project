namespace Fiap.Api.EnvironmentalAlert.ViewModel.ConsumptionAlert
{
    public class ConsumptionAlertViewModel
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }

        public decimal? RecordedConsumption { get; set; }

        public decimal? ExpectedLimit { get; set; }

        public string? Message { get; set; }

        public DateTime AlertAt { get; set; } = DateTime.Now;
    }
}
