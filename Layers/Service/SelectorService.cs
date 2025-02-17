using AutoMapper;
using WanderRiderRentACar.DMO;

public interface ISelectorService
{
    Task<List<TransmissionDTO>> GetTransmissions();
    Task<List<FuelTypeDTO>> GetFuelTypes();
    Task<List<SegmentDTO>> GetSegments();
    Task<List<RentStoreDTO>> GetRentStores();
}

/// <summary>
/// Generic repo kullanarak dropdown listelerini olusturmak icin
/// </summary>
public class SelectorService : ISelectorService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Transmission> _transmissionRepository;
    private readonly IGenericRepository<FuelType> _fuelTypeRepository;
    private readonly IGenericRepository<Segment> _segmentRepository;
    private readonly IGenericRepository<RentStore> _rentStoreRepository;


    public SelectorService
    (
        IMapper mapper,
        IGenericRepository<Transmission> transmissionRepository,
        IGenericRepository<FuelType> fuelTypeRepository,
        IGenericRepository<Segment> segmentRepository,
        IGenericRepository<RentStore> rentStoreRepository
    )
    {
        _transmissionRepository = transmissionRepository;
        _fuelTypeRepository = fuelTypeRepository;
        _segmentRepository = segmentRepository;
        _rentStoreRepository = rentStoreRepository;
        _mapper = mapper;
    }

    public async Task<List<TransmissionDTO>> GetTransmissions()
    {
        return _mapper.Map<List<TransmissionDTO>>(await _transmissionRepository.GetAllAsync());
    }

    public async Task<List<FuelTypeDTO>> GetFuelTypes()
    {
        var dmo = await _fuelTypeRepository.GetAllAsync();
        return _mapper.Map<List<FuelTypeDTO>>(dmo);
    }
    public async Task<List<SegmentDTO>> GetSegments()
    {
        var dmo = await _segmentRepository.GetAllAsync();
        return _mapper.Map<List<SegmentDTO>>(dmo);
    }
    public async Task<List<RentStoreDTO>> GetRentStores()
    {
        var dmo = await _rentStoreRepository.GetAllAsync();
        return _mapper.Map<List<RentStoreDTO>>(dmo);
    }

}
