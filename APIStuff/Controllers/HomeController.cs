using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using APIStuff.Models;
using Newtonsoft.Json;

namespace APIStuff.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpclient;

    public IActionResult ErrorPage()
    {
        return View();
    }


    public HomeController(ILogger<HomeController> logger, HttpClient httpclient)
    {
        _logger = logger;
        _httpclient = httpclient;
    }

    public async Task<IActionResult> Index()
    {
        var url = "https://opentdb.com/api.php?amount=10&type=multiple"; //Calling API
        var response = await _httpclient.GetAsync(url); // Waits for a response from the API
        
        if (!response.IsSuccessStatusCode) // If the response is not successful, it will redirect to error page
        {
            return RedirectToAction("ErrorPage");
        }

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<QuizQuestion>(content);
        
        return View(data);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
