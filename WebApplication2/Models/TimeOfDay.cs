using System.ComponentModel.DataAnnotations;

namespace ASP.NET.Sample.Web.Models
{
    public enum TimeOfDay
    {
        [Display(Name = "Утро")]
        Morning,

        [Display(Name = "День")]
        Afternoon,

        [Display(Name = "Вечер")]
        Evening,

        [Display(Name = "Ночь")]
        Night
    }
}
