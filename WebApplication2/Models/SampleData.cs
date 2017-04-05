using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication2.Models
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
