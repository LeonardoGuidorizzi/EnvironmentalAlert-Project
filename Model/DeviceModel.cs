using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.EnvironmentalAlert.Model
{
   
    public class DeviceModel
    {

        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Location { get; set; }

        // Relacionamentos
        public ICollection<DeviceConsumptionModel> DeviceConsumptions { get; set; } = new List<DeviceConsumptionModel>();
        public ICollection<ConsumptionAlertModel> ConsumptionAlerts { get; set; } = new List<ConsumptionAlertModel>();
    }
}
