using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace mvc.Controllers
{
    public class TableController : Controller
    {
        private readonly ILogger<TableController> _logger;

        public TableController(ILogger<TableController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
