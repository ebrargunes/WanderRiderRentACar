
public interface IReservationService
{
    Task<int> AddUserUpdateCar(UserDTO user, CarDTO car, DateOnly pickUpDate, DateOnly returnDate);
}