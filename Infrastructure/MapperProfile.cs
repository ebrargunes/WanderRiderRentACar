using AutoMapper;
using WanderRiderRentACar.DMO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transmission, TransmissionDTO>().ReverseMap();
        CreateMap<TransmissionDTO, TransmissionVM>().ReverseMap();
        CreateMap<FuelType, FuelTypeDTO>().ReverseMap();
        CreateMap<FuelTypeDTO, FuelTypeVM>().ReverseMap();
        CreateMap<Segment, SegmentDTO>().ReverseMap();
        CreateMap<SegmentDTO, SegmentVM>().ReverseMap();
        CreateMap<RentStore, RentStoreDTO>().ReverseMap();
        CreateMap<RentStoreDTO, RentStoreVM>().ReverseMap();
        CreateMap<Car, CarDTO>().ReverseMap();
        CreateMap<CarDTO, CarVM>().ReverseMap();

        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<UserDTO, UserVM>().ReverseMap();
        CreateMap<CarVM, WanderRiderRentACar.DMO.Car>();
        CreateMap<WanderRiderRentACar.DMO.Car, CarVM>();

    }
}