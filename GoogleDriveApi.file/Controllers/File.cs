using Microsoft.AspNetCore.Mvc;

namespace GoogleDriveApi.file.Controllers
{
    public class File : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
