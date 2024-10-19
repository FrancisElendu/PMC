using AutoMapper;
using PMC.Application.Command.CreateUser;
using PMC.Application.Dtos;
using PMC.Domain.Entities;

namespace PMC.Application.Mapper
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // User to UserDto mapping
                config.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
                    .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
                    .ForMember(dest => dest.Pharmacist, opt => opt.MapFrom(src => src.Pharmacist))
                    .ForMember(dest => dest.Notifications, opt => opt.MapFrom(src => src.Notifications));

                // UserDto to User mapping
                config.CreateMap<UserDto, User>().ReverseMap();

                // User to CreateUserCommand mapping
                config.CreateMap<User, CreateUserCommand>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

                config.CreateMap<CreateUserCommand, User>().ReverseMap();

                // CreateUserCommand to UserDto mapping
                config.CreateMap<CreateUserCommand, UserDto>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

                // Patient to PatientDto mapping
                config.CreateMap<Patient, PatientDto>()
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                    .ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions));

                // PatientDto to Patient mapping
                config.CreateMap<PatientDto, Patient>().ReverseMap();

                // Doctor to DoctorDto mapping
                config.CreateMap<Doctor, DoctorDto>()
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                    .ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions));

                // DoctorDto to Doctor mapping
                config.CreateMap<DoctorDto, Doctor>().ReverseMap();

                // Pharmacist to PharmacistDto mapping
                config.CreateMap<Pharmacist, PharmacistDto>()
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

                // PharmacistDto to Pharmacist mapping
                config.CreateMap<PharmacistDto, Pharmacist>().ReverseMap();

                // Prescription to PrescriptionDto mapping
                config.CreateMap<Prescription, PrescriptionDto>()
                    .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
                    .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
                    .ForMember(dest => dest.PrescriptionItems, opt => opt.MapFrom(src => src.PrescriptionItems));

                // PrescriptionDto to Prescription mapping
                config.CreateMap<PrescriptionDto, Prescription>().ReverseMap();

                // PrescriptionItem to PrescriptionItemDto mapping
                config.CreateMap<PrescriptionItem, PrescriptionItemDto>()
                    .ForMember(dest => dest.Prescription, opt => opt.MapFrom(src => src.Prescription))
                    .ForMember(dest => dest.Drug, opt => opt.MapFrom(src => src.Drug));

                // PrescriptionItemDto to PrescriptionItem mapping
                config.CreateMap<PrescriptionItemDto, PrescriptionItem>().ReverseMap();

                // Drug to DrugDto mapping
                config.CreateMap<Drug, DrugDto>()
                    .ForMember(dest => dest.PrescriptionItems, opt => opt.MapFrom(src => src.PrescriptionItems));

                // DrugDto to Drug mapping
                config.CreateMap<DrugDto, Drug>().ReverseMap();

                // Notification to NotificationDto mapping
                config.CreateMap<Notification, NotificationDto>()
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

                // NotificationDto to Notification mapping
                config.CreateMap<NotificationDto, Notification>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
