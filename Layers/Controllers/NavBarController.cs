using Microsoft.AspNetCore.Mvc;

public class NavBarController:Controller
{
    public IActionResult KiralamaKosullari()
    {
        return View();
    }

    public IActionResult AydinlatmaMetni()
    {
        return View();
    }

    public IActionResult IptalveIade()
    {
        return View();
    }
}