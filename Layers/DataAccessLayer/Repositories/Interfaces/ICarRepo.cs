using WanderRiderRentACar.DMO;

public interface ICarRepo
{
    Task<List<CarDTO>> GetAllCars();
    Task<List<CarDTO>> GetCarsByOfficeId(int rentStoreId);
    List<CarDTO> GetCarsByFilters(int fuelTypeId, int segmentId, int transmissionId);
    Task<CarDTO> GetSingleCarById(int id);
    Task<List<CarDTO>> GetOnlyAvailabeCarsAsync(DateOnly pickupDate, DateOnly returnDate, int rentStoreId);

}