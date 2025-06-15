namespace Fiap.Api.EnvironmentalAlert.ViewModel.DeviceConsumption
{
    public class UpdateDeviceConsumptionViewModel
    {
        public int DeviceId { get; set; }

        public decimal ConsumptionKwh { get; set; }

        public decimal ExpectedLimitKwh { get; set; }
    }
}
