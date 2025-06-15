namespace Fiap.Api.EnvironmentalAlert.ViewModel.DeviceConsumption
{
    public class CreateDeviceConsumptionViewModel
    {
        public int DeviceId { get; set; }

        public decimal ConsumptionKwh { get; set; }

        public decimal ExpectedLimitKwh { get; set; }
    }
}
