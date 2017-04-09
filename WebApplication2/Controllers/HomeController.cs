using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        MobileContext db;
        public HomeController(MobileContext context)
        {
            db = context;
        }

        [ActionName("Index")]
        public IActionResult Index()
        {
            return View(db.Phones.ToList());
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

        public string Square(Geometry geometry)
        {
            return $"Площадь треугольника с основанием {geometry.Altitude} и высотой {geometry.Height} равна {geometry.GetSquare()}";
        }

        [HttpPost]
        public string Square(int altitude, int height)
        {
            double square = altitude * height / 2;
            return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
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
