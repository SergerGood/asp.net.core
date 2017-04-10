using System;

namespace ASP.NET.Sample.Web.Models
{
    public class Order
    {
        public string Address { get; set; } // адрес покупателя
        public string ContactPhone { get; set; } // контактный телефон покупателя
        public int OrderId { get; set; }
        public Phone Phone { get; set; }

        public int PhoneId { get; set; } // ссылка на связанную модель Phone
        public string User { get; set; } // имя фамилия покупателя
    }
}
