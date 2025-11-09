using System.Diagnostics;
using ASPCoreWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        //private static List<TodoItem> _items = new List<TodoItem>();
        //private static int _nextId = 1;

        //[HttpGet]
        //public IActionResult TodoIndex() => View(_items);

        //[HttpPost]
        //public IActionResult Add(string title)
        //{
        //    if (!string.IsNullOrWhiteSpace(title))
        //    {
        //        _items.Add(new TodoItem { Id = ++_nextId, Title = title, isDone = false });
        //    }
        //    return RedirectToAction(nameof(TodoIndex));
        //}

        //[HttpPost]
        //public IActionResult Toggle(int id)
        //{
        //    var item = _items.FirstOrDefault(x => x.Id == id);
        //    if (item != null) item.isDone = !item.isDone;
        //    return RedirectToAction(nameof(TodoIndex));
        //}

        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    var item = _items.FirstOrDefault(x => x.Id == id);
        //    if (item != null) _items.Remove(item);
        //    return RedirectToAction(nameof(TodoIndex));
        //}

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {

            var adminName = _configuration.GetValue<string>("Admin:Name");
            ViewData["AdminName"] = adminName;
            return View();
        }

        public IActionResult Privacy() => View(); // возвращаем через лямбды

        public IActionResult TestModel() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
