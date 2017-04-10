using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.NET.Sample.Web.Models
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<MobileContext>();

            if (!context.Phones.Any())
            {
                context.Phones.AddRange(
                    new Phone
                    {
                        Name = "iPhone 6S",
                        Company = "Apple",
                        Price = 600
                    },
                    new Phone
                    {
                        Name = "Samsung Galaxy Edge",
                        Company = "Samsung",
                        Price = 550
                    },
                    new Phone
                    {
                        Name = "Lumia 950",
                        Company = "Microsoft",
                        Price = 500
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
