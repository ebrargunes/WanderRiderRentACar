using WanderRiderRentACar.DMO;

public interface ICarService
{
  Task<List<CarVM>> GetAllCars();
  // Task<int> RemoveCar(Car carToRemove);
  Task<List<CarVM>> GetCarsByOfficeId(int rentStoreId);
  //Task<List<CarVM>> GetAvailableCars(int rentStoreId, DateOnly pickupDate, DateOnly returnDate);
  List<CarVM> GetCarsByFilters(int fuelType = 0, int segment = 0, int transmission = 0);
  Task<CarDTO> AddCar(CarDTO car);
  Task<int> UpdateCar(CarDTO car);
  Task<CarVM> GetSingleCarById(int carId);
  Task<int> RemoveCar(Car carToRemove);
  Task<List<CarDTO>> GetCarsByDate(DateOnly pickupDate, DateOnly returnDate, int rentStoreId);
}