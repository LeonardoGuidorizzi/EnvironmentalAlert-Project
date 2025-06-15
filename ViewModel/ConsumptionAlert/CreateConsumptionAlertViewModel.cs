namespace Fiap.Api.EnvironmentalAlert.ViewModel.ConsumptionAlert
{
    public class CreateConsumptionAlertViewModel
    {
        public int DeviceId { get; set; }

        public decimal? RecordedConsumption { get; set; }

        public decimal? ExpectedLimit { get; set; }

        public string? Message { get; set; }


    }
}
