using ASP.NET.Sample.Web.Models;
using ASP.NET.Sample.Web.Util;
using ControllersApp.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;

namespace ASP.NET.Sample.Web.Controllers
{
    public class HomeController : HelloBaseController
    {
        private readonly MobileContext db;
        private readonly IHostingEnvironment appEnvironment;

        private readonly Restrictions restrictions;

        public HomeController(MobileContext context, 
            IHostingEnvironment appEnvironment,
            IOptions<Restrictions> restrictions)
        {
            db = context;
            this.appEnvironment = appEnvironment;
            this.restrictions = restrictions.Value;
        }

        [HttpGet]
        public IActionResult Buy(int id)
        {
            ViewBag.PhoneId = id;
            return View();
        }

        [HttpPost]
        public string Buy(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();

            return $"Спасибо {order.User} за покуаку!";
        }

        [NonAction]
        public string Hello()
        {
            return "Hello ASP.NET";
        }

        public IActionResult Hello2([FromServices] IHostingEnvironment env)
        {
            return Content(env.ContentRootPath);
        }

        public IActionResult Hello3()
        {
            string ageInfo = $"Age:{restrictions.Age}";
            return Content(ageInfo);
        }

        [ActionName("Index")]
        public IActionResult Index()
        {
            return View(db.Phones.ToList());
        }

        public IActionResult Index2()
        {
            return Redirect("~/Home/About");
        }

        public IActionResult Index3()
        {
            return RedirectToAction("Square", "Home", new { altitude = 10, height = 3 });
        }

        public IActionResult Index4()
        {
            return RedirectToRoute("default", new { controller = "Home", action = "Square", height = 2, altitude = 20 });
        }

        public IActionResult Index5(int age)
        {
            if (age < 18)
                return Unauthorized();

            return View();
        }

        public string Square(Geometry geometry)
        {
            return $"Площадь треугольника с основанием {geometry.Altitude} и высотой {geometry.Height} равна {geometry.GetSquare()}";
        }

        [HttpPost]
        public IActionResult Square(int altitude, int height)
        {
            double square = altitude * height / 2;
            return Content($"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}");
        }

        public HtmlResult GetHtml()
        {
            return new HtmlResult("<h2>Привет ASP.NET Core</h2>");
        }

        public JsonResult GetName()
        {
            string name = "Tom";
            return Json(name);
        }

        public IActionResult GetFile()
        {
            string file_path = Path.Combine(appEnvironment.ContentRootPath, "Files/hello.txt");
            string file_type = "text/plain";

            return PhysicalFile(file_path, file_type);
        }

        public FileResult GetStream()
        {
            string file_path = Path.Combine(appEnvironment.ContentRootPath, "Files/hello.txt");
            string file_type = "text/plain";

            FileStream fs = new FileStream(file_path, FileMode.Open);

            return File(fs, file_type);
        }

        //public string Square()
        //{
        //    string altitudeString = Request.Query.FirstOrDefault(p => p.Key == "altitude").Value;
        //    int altitude = Int32.Parse(altitudeString);

        //    string heightString = Request.Query.FirstOrDefault(p => p.Key == "height").Value;
        //    int height = Int32.Parse(heightString);

        //    double square = altitude * height / 2;
        //    return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
        //}

        //[HttpPost]
        //public string Square()
        //{
        //    string altitudeString = Request.Form.FirstOrDefault(p => p.Key == "altitude").Value;
        //    int altitude = Int32.Parse(altitudeString);

        //    string heightString = Request.Form.FirstOrDefault(p => p.Key == "height").Value;
        //    int height = Int32.Parse(heightString);

        //    double square = altitude * height / 2;
        //    return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
        //}

        public string Sum(Geometry[] geoms)
        {
            return $"Сумма площадей равна {geoms.Sum(g => g.GetSquare())}";
        }
    }

    public class Geometry
    {
        public int Altitude { get; set; } // основание
        public int Height { get; set; } // высота

        public double GetSquare() // вычисление площади треугольника
        {
            return Altitude * Height / 2;
        }
    }
}
