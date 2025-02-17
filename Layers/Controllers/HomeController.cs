using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WanderRiderRentACar.Models;

namespace WanderRiderRentACar.Controllers;

public class HomeController : Controller
{
    private readonly ICarService _carService;
    private readonly ISelectorService _selectorService;
    private readonly IMapper _mapper;

    public HomeController(ICarService carService, ISelectorService selectorService, IMapper mapper)
    {
        _carService = carService;
        _selectorService = selectorService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Filomuz()
    {
        var model = await _carService.GetAllCars();
        return View(model);
    }

    public async Task<IActionResult> Index()
    {
        var rentStores = _mapper.Map<List<RentStoreVM>>(await _selectorService.GetRentStores());
        var model = new IndexPageMainModel
        {
            RentStores = rentStores
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(IndexPageMainModel model)
    {
        if (model.SelectedOfficeId == 0)
        {
            var rentStores = _mapper.Map<List<RentStoreVM>>(await _selectorService.GetRentStores());
            model.RentStores = rentStores;
            TempData["Message"] = "Lutfen Bir Ofis Seciniz";
            return View(model);
        }

        HttpContext.Session.SetString("PickUpDate", model.PickupDate.ToString());
        HttpContext.Session.SetString("ReturnDate", model.ReturnDate.ToString());
        // kullanicin sectigi tarih bilgisini her yerden erisebilmek icin sessiona attiniz. session controller dan geçmek zorunda. static olarak yazamam. sayfada bir şeyler değişiyorsa controller a girmek zorundasın. yada react js
        //var availableCars = await _carService.GetAvailableCars(model.SelectedOfficeId, model.PickupDate, model.ReturnDate);

        // model.AvailableCars = availableCars;

        // if (!availableCars.Any())
        // {
        //     TempData["Message"] = "Seçtiğiniz tarihlerde uygun araç bulunamadı.";
        // }

        return RedirectToAction("Index", "Cars", model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult AboutUs()
    {

        return View();
    }

}
