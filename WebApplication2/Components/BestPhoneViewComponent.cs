using ASP.NET.Sample.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.Sample.Web.Components
{
    public class BestPhone : ViewComponent
    {
        MobileContext context;

        public BestPhone(MobileContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPrice)
        {
            var items = await context.Phones
                .Where(x => x.Price <= maxPrice)
                .OrderByDescending(x => x.Price)
                .ToListAsync();

            return View(items);
        }
    }
}
