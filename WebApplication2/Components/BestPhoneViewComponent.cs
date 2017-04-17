using ASP.NET.Sample.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NET.Sample.Web.Components
{
    public class BestPhone : ViewComponent
    {
        List<Phone> phones;

        public BestPhone()
        {
            phones = new List<Phone>
            {
                new Phone {Name="iPhone 7", Price=56000 },
                new Phone {Name="Idol S4", Price=26000 },
                new Phone {Name="Elite x3", Price=55000 },
                new Phone {Name="Honor 8", Price=23000 }
            };
        }

        public string Invoke()
        {
            var item = phones.OrderByDescending(x => x.Price).Take(1).FirstOrDefault();

            return $"Самый дорогой телефон: {item.Name} Цена: {item.Price}";
        }
    }
}
