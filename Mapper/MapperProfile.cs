using AutoMapper;
using Fiap.Api.EnvironmentalAlert.Enums;
using Fiap.Api.EnvironmentalAlert.Model;
using Fiap.Api.EnvironmentalAlert.ViewModel.Auth;
using Fiap.Api.EnvironmentalAlert.ViewModel.ConsumptionAlert;
using Fiap.Api.EnvironmentalAlert.ViewModel.Device;
using Fiap.Api.EnvironmentalAlert.ViewModel.DeviceConsumption;


namespace Fiap.Api.EnvironmentalAlert.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ---------------- DEVICE ----------------

            // DeviceModel <-> DeviceViewModel (usado para leitura completa)
            CreateMap<DeviceModel, DeviceViewModel>().ReverseMap();

            // CreateDeviceViewModel -> DeviceModel (usado na criação)
            CreateMap<CreateDeviceViewModel, DeviceModel>();

            // UpdateDeviceViewModel -> DeviceModel (usado na atualização)
            CreateMap<UpdateDeviceViewModel, DeviceModel>();


            // ---------------- DEVICE CONSUMPTION ----------------

            // DeviceConsumptionModel <-> DeviceConsumptionViewModel (leitura completa)
            CreateMap<DeviceConsumptionModel, DeviceConsumptionViewModel>().ReverseMap();

            // CreateDeviceConsumptionViewModel -> DeviceConsumptionModel (usado na criação)
            CreateMap<CreateDeviceConsumptionViewModel, DeviceConsumptionModel>();

            // UpdateDeviceConsumptionViewModel -> DeviceConsumptionModel (usado na atualização)
            CreateMap<UpdateDeviceConsumptionViewModel, DeviceConsumptionModel>();

            // ---------------- CONSUMPTION ALERT ----------------

            // ConsumptionAlertModel <-> ConsumptionAlertViewModel (leitura completa)
            CreateMap<ConsumptionAlertModel, ConsumptionAlertViewModel>().ReverseMap();
            
            CreateMap<CreateConsumptionAlertViewModel, ConsumptionAlertModel>();

            CreateMap<UpdateConsumptionAlertViewModel, ConsumptionAlertModel>();

            // ---------------- AUTH ----------------

            CreateMap<RegisterViewModel, UserModel>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // será gerado depois com hashing
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role)); // padrão: User
            CreateMap<UserModel, AuthResponseViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Token, opt => opt.Ignore());

            CreateMap<RegisterViewModel, UserModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.Role)));

            // ---------------- USER ----------------
            CreateMap<UserModel, UserViewModel>().ReverseMap();

        }
    }
}
