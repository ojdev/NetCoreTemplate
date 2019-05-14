using Microsoft.AspNetCore.Mvc;

namespace Core.Template.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// GET: /<controller/>
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
